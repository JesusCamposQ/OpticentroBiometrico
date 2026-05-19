namespace OpticentroBiometrico
{
    partial class FrmBiometricAttendance
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.pnlSensor = new System.Windows.Forms.Panel();
            this.lblClock = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblMainMessage = new System.Windows.Forms.Label();
            this.lblSubMessage = new System.Windows.Forms.Label();
            this.lblTemplatesInfo = new System.Windows.Forms.Label();
            this.lblEscHint = new System.Windows.Forms.Label();
            this.pnlBackground.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlBackground
            //
            this.pnlBackground.BackColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.pnlBackground.Controls.Add(this.pnlSensor);
            this.pnlBackground.Controls.Add(this.lblClock);
            this.pnlBackground.Controls.Add(this.lblDate);
            this.pnlBackground.Controls.Add(this.lblMainMessage);
            this.pnlBackground.Controls.Add(this.lblSubMessage);
            this.pnlBackground.Controls.Add(this.lblTemplatesInfo);
            this.pnlBackground.Controls.Add(this.lblEscHint);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.TabIndex = 0;
            //
            // pnlSensor
            //
            this.pnlSensor.BackColor = System.Drawing.Color.Transparent;
            this.pnlSensor.Name = "pnlSensor";
            this.pnlSensor.Size = new System.Drawing.Size(180, 180);
            this.pnlSensor.TabIndex = 4;
            this.pnlSensor.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSensor_Paint);
            //
            // lblClock
            //
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblClock.ForeColor = System.Drawing.Color.White;
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(260, 56);
            this.lblClock.TabIndex = 0;
            this.lblClock.Text = "00:00:00";
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblDate
            //
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(210, 255, 255, 255);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(260, 26);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblMainMessage
            //
            this.lblMainMessage.Font = new System.Drawing.Font("Segoe UI", 42F, System.Drawing.FontStyle.Bold);
            this.lblMainMessage.ForeColor = System.Drawing.Color.White;
            this.lblMainMessage.Name = "lblMainMessage";
            this.lblMainMessage.Size = new System.Drawing.Size(1024, 110);
            this.lblMainMessage.TabIndex = 1;
            this.lblMainMessage.Text = "Iniciando...";
            this.lblMainMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSubMessage
            //
            this.lblSubMessage.Font = new System.Drawing.Font("Segoe UI", 28F);
            this.lblSubMessage.ForeColor = System.Drawing.Color.FromArgb(230, 255, 255, 255);
            this.lblSubMessage.Name = "lblSubMessage";
            this.lblSubMessage.Size = new System.Drawing.Size(1024, 80);
            this.lblSubMessage.TabIndex = 2;
            this.lblSubMessage.Text = "";
            this.lblSubMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTemplatesInfo
            //
            this.lblTemplatesInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTemplatesInfo.ForeColor = System.Drawing.Color.FromArgb(150, 255, 255, 255);
            this.lblTemplatesInfo.Name = "lblTemplatesInfo";
            this.lblTemplatesInfo.Size = new System.Drawing.Size(300, 24);
            this.lblTemplatesInfo.TabIndex = 3;
            this.lblTemplatesInfo.Text = "";
            //
            // lblEscHint
            //
            this.lblEscHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEscHint.ForeColor = System.Drawing.Color.FromArgb(110, 255, 255, 255);
            this.lblEscHint.Name = "lblEscHint";
            this.lblEscHint.Size = new System.Drawing.Size(120, 24);
            this.lblEscHint.TabIndex = 6;
            this.lblEscHint.Text = "ESC: Salir";
            this.lblEscHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // FrmBiometricAttendance
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmBiometricAttendance";
            this.Text = "Marcacion Biometrica - Opticentro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBiometricAttendance_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBiometricAttendance_KeyDown);
            this.Load += new System.EventHandler(this.FrmBiometricAttendance_Load);
            this.pnlBackground.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel pnlSensor;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblMainMessage;
        private System.Windows.Forms.Label lblSubMessage;
        private System.Windows.Forms.Label lblTemplatesInfo;
        private System.Windows.Forms.Label lblEscHint;
    }
}
