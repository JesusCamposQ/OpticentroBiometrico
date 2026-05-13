using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpticentroBiometrico.Intrastructure.Devices;
using OpticentroBiometrico.Application.Services;
using OpticentroBiometrico.Common;

namespace OpticentroBiometrico
{
    public partial class FrmBiometricEnrollment : Form
    {
        private readonly ZK4500DeviceService _deviceService = new ZK4500DeviceService();
        private readonly EmployeeService _employeeService = new EmployeeService();
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
                    MessageBox.Show("Dispositivo conectado correctamente");
                }
                else
                {
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

                lblSelectedEmployee.Text = "Empleado seleccionado: " + fullName;
                btnStartEnrollment.Enabled = true;
            }
        }
    }
}
