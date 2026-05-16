using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticentroBiometrico.Application.Services;
using OpticentroBiometrico.Common;
using OpticentroBiometrico.Intrastructure.Devices;
using OpticentroBiometrico.Intrastructure.Repositories;

namespace OpticentroBiometrico
{
    public partial class FrmBiometricEnrollment : Form
    {
        private readonly ZK4500DeviceService _deviceService = new ZK4500DeviceService();
        private readonly EmployeeService _employeeService = new EmployeeService();
        private readonly FingerprintRepository _fingerprintRepository = new FingerprintRepository();
        private string _selectedEmployeeId;

        public FrmBiometricEnrollment()
        {
            InitializeComponent();
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

        private void btnLoadEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                var employees = _employeeService.GetEmployees();
                dgvEmployees.DataSource = employees;
                dgvEmployees.Columns["Id"].Visible = false;
                dgvEmployees.Columns["IsActive"].Visible = false;
                dgvEmployees.Columns["Nombre"].Visible = false;
                dgvEmployees.Columns["ApPaterno"].Visible = false;
                dgvEmployees.Columns["ApMaterno"].Visible = false;
                dgvEmployees.Columns["FullName"].HeaderText = "Nombre Completo";
                dgvEmployees.Columns["FullName"].Width = 300;
            }
            catch (Exception ex)
            {
                Logger.Log("Error al cargar empleados: " + ex.Message);
                MessageBox.Show("Error al cargar empleados: " + ex.Message);
            }
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvEmployees.Rows[e.RowIndex];
                var fullName = selectedRow.Cells["FullName"].Value?.ToString();
                _selectedEmployeeId = selectedRow.Cells["Id"].Value?.ToString();

                lblSelectedEmployee.Text = "Empleado seleccionado: " + fullName;
                btnStartEnrollment.Enabled = true;
            }
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
