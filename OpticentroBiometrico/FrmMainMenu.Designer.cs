namespace OpticentroBiometrico
{
    partial class FrmMainMenu
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnEnrollment = new System.Windows.Forms.Button();
            this.lblChoose = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(480, 110);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(0, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "OPTICENTRO BIOMÉTRICO";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSubtitle
            //
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(186, 213, 243);
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.Location = new System.Drawing.Point(0, 64);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(480, 30);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Sistema de Control de Acceso Biométrico";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlContent.Controls.Add(this.btnAttendance);
            this.pnlContent.Controls.Add(this.btnEnrollment);
            this.pnlContent.Controls.Add(this.lblChoose);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 110);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(480, 230);
            this.pnlContent.TabIndex = 1;
            //
            // lblChoose
            //
            this.lblChoose.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblChoose.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblChoose.Location = new System.Drawing.Point(0, 22);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(480, 28);
            this.lblChoose.TabIndex = 0;
            this.lblChoose.Text = "Seleccione una opción:";
            this.lblChoose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnEnrollment
            //
            this.btnEnrollment.BackColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.btnEnrollment.FlatAppearance.BorderSize = 0;
            this.btnEnrollment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollment.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnEnrollment.ForeColor = System.Drawing.Color.White;
            this.btnEnrollment.Location = new System.Drawing.Point(60, 62);
            this.btnEnrollment.Name = "btnEnrollment";
            this.btnEnrollment.Size = new System.Drawing.Size(360, 56);
            this.btnEnrollment.TabIndex = 1;
            this.btnEnrollment.Text = "Enrolar Empleado";
            this.btnEnrollment.UseVisualStyleBackColor = false;
            this.btnEnrollment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnrollment.Click += new System.EventHandler(this.btnEnrollment_Click);
            //
            // btnAttendance
            //
            this.btnAttendance.BackColor = System.Drawing.Color.FromArgb(16, 150, 100);
            this.btnAttendance.FlatAppearance.BorderSize = 0;
            this.btnAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendance.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnAttendance.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.Location = new System.Drawing.Point(60, 134);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(360, 56);
            this.btnAttendance.TabIndex = 2;
            this.btnAttendance.Text = "Iniciar Marcación";
            this.btnAttendance.UseVisualStyleBackColor = false;
            this.btnAttendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            //
            // FrmMainMenu
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 340);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opticentro Biométrico";
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblChoose;
        private System.Windows.Forms.Button btnEnrollment;
        private System.Windows.Forms.Button btnAttendance;
    }
}
