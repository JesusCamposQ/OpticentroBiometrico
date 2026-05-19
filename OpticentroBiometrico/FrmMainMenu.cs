using System.Windows.Forms;

namespace OpticentroBiometrico
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void btnEnrollment_Click(object sender, System.EventArgs e)
        {
            Hide();
            var frm = new FrmBiometricEnrollment();
            frm.FormClosed += (s, ev) => Show();
            frm.Show();
        }

        private void btnAttendance_Click(object sender, System.EventArgs e)
        {
            Hide();
            var frm = new FrmBiometricAttendance();
            frm.FormClosed += (s, ev) => Show();
            frm.Show();
        }
    }
}
