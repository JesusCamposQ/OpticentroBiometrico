using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using OpticentroBiometrico.Application.Services;
using OpticentroBiometrico.Common;
using OpticentroBiometrico.Intrastructure.Devices;
using OpticentroBiometrico.Intrastructure.Repositories;

namespace OpticentroBiometrico
{
    public partial class FrmBiometricEnrollment : MaterialForm
    {
        private readonly ZK4500DeviceService _deviceService = new ZK4500DeviceService();
        private readonly EmployeeService _employeeService = new EmployeeService();
        private readonly FingerprintRepository _fingerprintRepository = new FingerprintRepository();
        private string _selectedEmployeeId;

        private System.Windows.Forms.Timer _skeletonTimer;
        private int _skeletonPhase;
        private const int SkeletonRowCount = 8;
        private readonly List<ListViewItem> _allEmployeeItems = new List<ListViewItem>();

        public FrmBiometricEnrollment()
        {
            InitializeComponent();

            var skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(
                Primary.Blue600,
                Primary.Blue700,
                Primary.Blue200,
                Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            try
            {
                bool connected = _deviceService.ConnectDevice();

                if (connected)
                {
                    lblStatus.Text = "Dispositivo conectado correctamente.";
                    MessageBox.Show("Dispositivo conectado correctamente");
                }
                else
                {
                    lblStatus.Text = "No se pudo conectar el dispositivo.";
                    MessageBox.Show("No se pudo conectar el dispositivo");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error al conectar el dispositivo: " + ex.Message);
                MessageBox.Show("Error al conectar el dispositivo: " + ex.Message);
            }
        }

        private async void btnLoadEmployees_Click(object sender, EventArgs e)
        {
            // Garantizar View=Details + columnas antes de mostrar skeleton.
            dgvEmployees.View = View.Details;
            if (dgvEmployees.Columns.Count == 0)
            {
                dgvEmployees.Columns.Add("CI", "CI", 120);
                dgvEmployees.Columns.Add("FullName", "Nombre Completo", 390);
                dgvEmployees.Columns.Add("Username", "Usuario", 160);
            }

            // Resetear selección previa y caché de búsqueda.
            _selectedEmployeeId = null;
            lblSelectedEmployee.Text = string.Empty;
            btnStartEnrollment.Enabled = false;
            btnLoadEmployees.Enabled = false;
            txtFilterEmployee.Enabled = false;
            txtFilterEmployee.Text = string.Empty;
            _allEmployeeItems.Clear();

            // Mostrar skeleton y arrancar animación de pulso.
            ShowSkeletonRows();
            StartSkeletonAnimation();
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            lblStatus.Text = "Cargando empleados...";

            try
            {
                var employees = await Task.Run(() => _employeeService.GetEmployees());

                StopSkeletonAnimation();

                if (employees == null)
                {
                    dgvEmployees.Items.Clear();
                    lblStatus.Text = "No se obtuvieron empleados del servicio.";
                    return;
                }

                dgvEmployees.Items.Clear();
                dgvEmployees.BeginUpdate();

                foreach (var emp in employees)
                {
                    if (emp == null) continue;

                    string id = emp.Id?.ToString();
                    if (string.IsNullOrEmpty(id)) continue;

                    var item = new ListViewItem(emp.Ci.ToString());
                    item.SubItems.Add(emp.FullName ?? string.Empty);
                    item.SubItems.Add(emp.Username ?? string.Empty);
                    item.Tag = id;
                    _allEmployeeItems.Add(item);
                    dgvEmployees.Items.Add(item);
                }

                dgvEmployees.EndUpdate();
                lblStatus.Text = $"Se cargaron {dgvEmployees.Items.Count} empleados.";
            }
            catch (Exception ex)
            {
                Logger.Log("Error al cargar empleados: " + ex.Message);
                dgvEmployees.Items.Clear();
                lblStatus.Text = "Error al cargar empleados.";
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
            finally
            {
                StopSkeletonAnimation();
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 0;
                btnLoadEmployees.Enabled = true;
                txtFilterEmployee.Enabled = _allEmployeeItems.Count > 0;
            }
        }

        private void txtFilterEmployee_TextChanged(object sender, EventArgs e)
        {
            if (_allEmployeeItems.Count == 0) return;

            string filter = txtFilterEmployee.Text?.Trim() ?? string.Empty;

            // Resetear selección — el empleado visible cambia con cada keystroke.
            _selectedEmployeeId = null;
            lblSelectedEmployee.Text = string.Empty;
            btnStartEnrollment.Enabled = false;

            dgvEmployees.BeginUpdate();
            dgvEmployees.Items.Clear();

            if (string.IsNullOrEmpty(filter))
            {
                dgvEmployees.Items.AddRange(_allEmployeeItems.ToArray());
            }
            else
            {
                foreach (ListViewItem item in _allEmployeeItems)
                {
                    string ci       = item.SubItems.Count > 0 ? item.SubItems[0].Text : string.Empty;
                    string fullName = item.SubItems.Count > 1 ? item.SubItems[1].Text : string.Empty;
                    string username = item.SubItems.Count > 2 ? item.SubItems[2].Text : string.Empty;

                    if (ci.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        fullName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        username.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        dgvEmployees.Items.Add(item);
                    }
                }
            }

            dgvEmployees.EndUpdate();
        }

        private void ShowSkeletonRows()
        {
            dgvEmployees.Items.Clear();
            dgvEmployees.BeginUpdate();
            for (int i = 0; i < SkeletonRowCount; i++)
            {
                var item = new ListViewItem("─────────");
                item.SubItems.Add("──────────────────────────");
                item.SubItems.Add("──────────────");
                item.ForeColor = System.Drawing.Color.FromArgb(210, 210, 210);
                item.Tag = null; // Tag nulo identifica filas skeleton.
                dgvEmployees.Items.Add(item);
            }
            dgvEmployees.EndUpdate();
        }

        private void StartSkeletonAnimation()
        {
            _skeletonPhase = 0;
            _skeletonTimer = new System.Windows.Forms.Timer { Interval = 600 };
            _skeletonTimer.Tick += SkeletonTimer_Tick;
            _skeletonTimer.Start();
        }

        private void SkeletonTimer_Tick(object sender, EventArgs e)
        {
            _skeletonPhase ^= 1;
            var color = _skeletonPhase == 0
                ? System.Drawing.Color.FromArgb(210, 210, 210)
                : System.Drawing.Color.FromArgb(235, 235, 235);

            foreach (ListViewItem item in dgvEmployees.Items)
            {
                if (item.Tag == null)
                    item.ForeColor = color;
            }
        }

        private void StopSkeletonAnimation()
        {
            if (_skeletonTimer == null) return;
            _skeletonTimer.Stop();
            _skeletonTimer.Tick -= SkeletonTimer_Tick;
            _skeletonTimer.Dispose();
            _skeletonTimer = null;
        }


        private void dgvEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedItems.Count == 0)
            {
                _selectedEmployeeId = null;
                lblSelectedEmployee.Text = string.Empty;
                btnStartEnrollment.Enabled = false;
                return;
            }

            ListViewItem item = dgvEmployees.SelectedItems[0];
            string id = item.Tag as string;

            if (string.IsNullOrEmpty(id))
            {
                _selectedEmployeeId = null;
                lblSelectedEmployee.Text = string.Empty;
                btnStartEnrollment.Enabled = false;
                return;
            }

            string fullName = item.SubItems.Count > 1 ? item.SubItems[1].Text : string.Empty;

            _selectedEmployeeId = id;
            lblSelectedEmployee.Text = "Empleado seleccionado: " + fullName;
            btnStartEnrollment.Enabled = true;
        }

        private async void btnStartEnrollment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedEmployeeId))
            {
                MessageBox.Show("Por favor, seleccione un empleado antes de iniciar la captura de huella.");
                return;
            }

            if (_deviceService.DeviceHandle == IntPtr.Zero)
            {
                MessageBox.Show("El dispositivo no está conectado. Presione 'Conectar dispositivo' primero.");
                return;
            }

            btnStartEnrollment.Enabled = false;
            btnConnectDevice.Enabled = false;
            lblStatus.Text = "Iniciando proceso de captura...";
            progressBar.Value = 0;

            var service = new FingerprintEnrollmentService(_deviceService.DeviceHandle);
            var progress = new Progress<string>(msg => lblStatus.Text = msg);
            var progressStep = new Progress<int>(step => progressBar.Value = step);

            string template = await Task.Factory.StartNew(
                () => service.CaptureTemplate(progress, progressStep),
                System.Threading.CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            if (!string.IsNullOrEmpty(template))
            {
                try
                {
                    _fingerprintRepository.SaveFingerprint(_selectedEmployeeId, template);
                    lblStatus.Text = "Huella capturada y guardada correctamente.";
                }
                catch (Exception ex)
                {
                    Logger.Log("Error al guardar la huella: " + ex.Message);
                    lblStatus.Text = "Error al guardar la huella en la base de datos.";
                    MessageBox.Show("Error al guardar la huella: " + ex.Message);
                }
            }
            else
            {
                lblStatus.Text = "No se pudo capturar la huella. Intente nuevamente.";
                MessageBox.Show("No se pudo capturar la huella. Revise el log para más detalles.");
            }

            btnStartEnrollment.Enabled = true;
            btnConnectDevice.Enabled = true;
        }

        private void FrmBiometricEnrollment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _deviceService.DisconnectDevice();
        }
    }
}
