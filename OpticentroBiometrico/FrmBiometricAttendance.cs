using libzkfpcsharp;
using OpticentroBiometrico.Application.Services;
using OpticentroBiometrico.Common;
using OpticentroBiometrico.Intrastructure.Devices;
using OpticentroBiometrico.Intrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticentroBiometrico
{
    public partial class FrmBiometricAttendance : Form
    {
        private readonly ZK4500DeviceService _deviceService = new ZK4500DeviceService();
        private readonly FingerprintVerificationService _verificationService = new FingerprintVerificationService();
        private readonly FingerprintRepository _fingerprintRepository = new FingerprintRepository();
        private readonly MarcacionRepository _marcacionRepository = new MarcacionRepository();
        private readonly EmployeeService _employeeService = new EmployeeService();

        private readonly Dictionary<string, string> _employeeNames = new Dictionary<string, string>();
        private CancellationTokenSource _cts;
        private Task _captureTask;
        private System.Windows.Forms.Timer _clockTimer;

        // Color fade animation
        private Color _targetBgColor = Color.FromArgb(100, 116, 139);
        private System.Windows.Forms.Timer _colorTimer;

        // Sensor pulse animation
        private int _pulseRadius;
        private bool _pulseGrowing = true;
        private System.Windows.Forms.Timer _pulseTimer;

        private const int ImgBufferSize = 200000;
        private const int TemplateBufferSize = 2048;

        private enum AttendanceState { Initializing, Waiting, Processing, Success, Error, Cooldown }

        public FrmBiometricAttendance()
        {
            InitializeComponent();
            DoubleBuffered = true;
            InitAnimationTimers();
        }

        // ── Initialization ───────────────────────────────────────────────────

        private void InitAnimationTimers()
        {
            _colorTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _colorTimer.Tick += ColorTimer_Tick;

            _pulseTimer = new System.Windows.Forms.Timer { Interval = 40 };
            _pulseTimer.Tick += PulseTimer_Tick;
        }

        private async void FrmBiometricAttendance_Load(object sender, EventArgs e)
        {
            PositionControls();
            StartClock();
            SetState(AttendanceState.Initializing);

            bool connected = await Task.Run(() => _deviceService.ConnectDevice());
            if (!connected)
            {
                MessageBox.Show(
                    "No se pudo conectar el lector de huellas.\nVerifique que el dispositivo este conectado.",
                    "Error de dispositivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            int loaded = await Task.Run(() =>
            {
                var templates = _fingerprintRepository.GetEmployeesWithTemplates();

                foreach (var emp in _employeeService.GetEmployees())
                    _employeeNames[emp.Id] = emp.FullName;

                return _verificationService.LoadTemplates(templates);
            });

            lblTemplatesInfo.Text = $"Huellas en memoria: {loaded}";
            Logger.Log($"FrmBiometricAttendance listo. Templates cargados: {loaded}");

            _cts = new CancellationTokenSource();
            _captureTask = Task.Factory.StartNew(
                () => CaptureLoop(_cts.Token),
                _cts.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            SetState(AttendanceState.Waiting);
        }

        // ── Capture loop ─────────────────────────────────────────────────────

        private void CaptureLoop(CancellationToken token)
        {
            var imgBuffer = new byte[ImgBufferSize];
            var tpBuffer = new byte[TemplateBufferSize];

            while (!token.IsCancellationRequested)
            {
                if (_deviceService.DeviceHandle == IntPtr.Zero)
                {
                    token.WaitHandle.WaitOne(200);
                    continue;
                }

                int tpSize = tpBuffer.Length;
                int ret = zkfp2.AcquireFingerprint(_deviceService.DeviceHandle, imgBuffer, tpBuffer, ref tpSize);

                if (ret == -8) // sin dedo en el sensor
                {
                    Thread.Sleep(50);
                    continue;
                }

                if (ret != zkfp.ZKFP_ERR_OK)
                {
                    Logger.Log($"AcquireFingerprint error codigo: {ret}");
                    Thread.Sleep(50);
                    continue;
                }

                SetState(AttendanceState.Processing);

                var capturedTemplate = new byte[tpSize];
                Array.Copy(tpBuffer, capturedTemplate, tpSize);

                string employeeId = _verificationService.IdentifyFingerprint(capturedTemplate);

                if (employeeId != null)
                {
                    string name = _employeeNames.TryGetValue(employeeId, out string n) ? n : "Empleado";
                    var lastMarcacion = _marcacionRepository.GetLastMarcacion(employeeId);

                    bool inCooldown = false;
                    int remaining = 0;

                    if (lastMarcacion != null)
                    {
                        DateTime lastFecha = lastMarcacion["fecha"].AsBsonDateTime.ToUniversalTime();
                        double elapsedMin = (DateTime.UtcNow - lastFecha).TotalMinutes;
                        if (elapsedMin < 10)
                        {
                            inCooldown = true;
                            remaining = (int)Math.Ceiling(10 - elapsedMin);
                        }
                    }

                    if (inCooldown)
                        SetState(AttendanceState.Cooldown, name, remaining);
                    else
                    {
                        _marcacionRepository.SaveMarcacion(employeeId);
                        SetState(AttendanceState.Success, name);
                    }
                }
                else
                {
                    SetState(AttendanceState.Error);
                }

                token.WaitHandle.WaitOne(2500);

                if (!token.IsCancellationRequested)
                    SetState(AttendanceState.Waiting);
            }
        }

        // ── State management ─────────────────────────────────────────────────

        private void SetState(AttendanceState state, string employeeName = null, int? remainingMinutes = null)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SetState(state, employeeName)));
                return;
            }

            switch (state)
            {
                case AttendanceState.Initializing:
                    pnlBackground.BackColor = Color.FromArgb(100, 116, 139);
                    _targetBgColor = pnlBackground.BackColor;
                    _pulseTimer.Stop();
                    lblMainMessage.Text = "Iniciando sistema...";
                    lblSubMessage.Text = "Cargando huellas desde la base de datos";
                    break;

                case AttendanceState.Waiting:
                    StartColorTransition(Color.FromArgb(245, 158, 11));
                    _pulseRadius = 0;
                    _pulseGrowing = true;
                    _pulseTimer.Start();
                    lblMainMessage.Text = "Coloque su dedo en el lector";
                    lblSubMessage.Text = "";
                    break;

                case AttendanceState.Processing:
                    StartColorTransition(Color.FromArgb(59, 130, 246));
                    _pulseTimer.Stop();
                    _pulseRadius = 0;
                    pnlSensor.Invalidate();
                    lblMainMessage.Text = "Validando huella...";
                    lblSubMessage.Text = "";
                    break;

                case AttendanceState.Success:
                    StartColorTransition(Color.FromArgb(16, 185, 129));
                    _pulseTimer.Stop();
                    _pulseRadius = 0;
                    pnlSensor.Invalidate();
                    lblMainMessage.Text = "Acceso Correcto!";
                    lblSubMessage.Text = employeeName ?? "";
                    break;

                case AttendanceState.Error:
                    StartColorTransition(Color.FromArgb(239, 68, 68));
                    _pulseTimer.Stop();
                    _pulseRadius = 0;
                    pnlSensor.Invalidate();
                    lblMainMessage.Text = "Huella no reconocida";
                    lblSubMessage.Text = "Intente nuevamente";
                    break;

                case AttendanceState.Cooldown:
                    StartColorTransition(Color.FromArgb(234, 88, 12));
                    _pulseTimer.Stop();
                    _pulseRadius = 0;
                    pnlSensor.Invalidate();
                    lblMainMessage.Text = $"Hola, {employeeName ?? "Empleado"}";
                    lblSubMessage.Text = $"Ya marcaste recientemente. Espera {remainingMinutes} min.";
                    break;
            }
        }

        // ── Color fade animation ──────────────────────────────────────────────

        private void StartColorTransition(Color target)
        {
            _targetBgColor = target;
            _colorTimer.Start();
        }

        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            var c = pnlBackground.BackColor;
            int r = LerpChannel(c.R, _targetBgColor.R);
            int g = LerpChannel(c.G, _targetBgColor.G);
            int b = LerpChannel(c.B, _targetBgColor.B);
            pnlBackground.BackColor = Color.FromArgb(r, g, b);
            pnlSensor.Invalidate();

            if (r == _targetBgColor.R && g == _targetBgColor.G && b == _targetBgColor.B)
                _colorTimer.Stop();
        }

        private static int LerpChannel(int from, int to)
        {
            const int step = 14;
            if (from < to) return Math.Min(from + step, to);
            if (from > to) return Math.Max(from - step, to);
            return to;
        }

        // ── Sensor pulse animation ────────────────────────────────────────────

        private void PulseTimer_Tick(object sender, EventArgs e)
        {
            _pulseRadius += _pulseGrowing ? 3 : -3;
            if (_pulseRadius >= 42) _pulseGrowing = false;
            if (_pulseRadius <= 0) { _pulseRadius = 0; _pulseGrowing = true; }
            pnlSensor.Invalidate();
        }

        private void pnlSensor_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int cx = pnlSensor.Width / 2;
            int cy = pnlSensor.Height / 2;
            const int baseR = 62;

            // Pulse ring (only during Waiting)
            if (_pulseRadius > 0)
            {
                int pr = baseR + _pulseRadius;
                int alpha = Math.Max(0, 90 - _pulseRadius * 2);
                using (var brush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)))
                    g.FillEllipse(brush, cx - pr, cy - pr, pr * 2, pr * 2);
            }

            // Main circle fill
            using (var brush = new SolidBrush(Color.FromArgb(50, 255, 255, 255)))
                g.FillEllipse(brush, cx - baseR, cy - baseR, baseR * 2, baseR * 2);

            // Circle border
            using (var pen = new Pen(Color.FromArgb(180, 255, 255, 255), 2.5F))
                g.DrawEllipse(pen, cx - baseR, cy - baseR, baseR * 2, baseR * 2);

            // Fingerprint arch ridges
            using (var pen = new Pen(Color.White, 2.5F))
            {
                // 5 concentric arches (arch shape: sweep from lower-left, through top, to lower-right)
                for (int i = 1; i <= 5; i++)
                {
                    int r = i * 9;
                    g.DrawArc(pen, cx - r, cy - r + 12, r * 2, r * 2, 200, 140);
                }
                // Center dot at apex of innermost arch
                g.FillEllipse(Brushes.White, cx - 3, cy + 12 - 3, 6, 6);
            }
        }

        // ── Layout ───────────────────────────────────────────────────────────

        private void PositionControls()
        {
            int w = pnlBackground.Width;
            int h = pnlBackground.Height;

            // Top-right: clock + date
            lblClock.Location = new Point(w - lblClock.Width - 24, 22);
            lblDate.Location = new Point(w - lblDate.Width - 24, lblClock.Bottom + 4);

            // Center: sensor indicator
            pnlSensor.Location = new Point((w - pnlSensor.Width) / 2, (int)(h * 0.22));

            // Messages
            lblMainMessage.Location = new Point(0, (int)(h * 0.50));
            lblMainMessage.Width = w;
            lblSubMessage.Location = new Point(0, (int)(h * 0.64));
            lblSubMessage.Width = w;

            // Bottom bar
            lblTemplatesInfo.Location = new Point(12, h - lblTemplatesInfo.Height - 12);
            lblEscHint.Location = new Point(w - lblEscHint.Width - 12, h - lblEscHint.Height - 12);
        }

        // ── Clock ─────────────────────────────────────────────────────────────

        private void StartClock()
        {
            _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _clockTimer.Tick += (s, ev) =>
            {
                var now = DateTime.Now;
                lblClock.Text = now.ToString("HH:mm:ss");
                lblDate.Text = now.ToString("dddd, d 'de' MMMM",
                    new System.Globalization.CultureInfo("es-PE"));
            };
            _clockTimer.Start();
            var now0 = DateTime.Now;
            lblClock.Text = now0.ToString("HH:mm:ss");
            lblDate.Text = now0.ToString("dddd, d 'de' MMMM",
                new System.Globalization.CultureInfo("es-PE"));
        }

        // ── Input ─────────────────────────────────────────────────────────────

        private void FrmBiometricAttendance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        // ── Cleanup ───────────────────────────────────────────────────────────

        private void FrmBiometricAttendance_FormClosing(object sender, FormClosingEventArgs e)
        {
            _clockTimer?.Stop();
            _clockTimer?.Dispose();

            _colorTimer?.Stop();
            _colorTimer?.Dispose();

            _pulseTimer?.Stop();
            _pulseTimer?.Dispose();

            _cts?.Cancel();
            Thread.Sleep(200);

            _verificationService?.Dispose();
            _deviceService.DisconnectDevice();
        }
    }
}
