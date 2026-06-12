namespace OpticentroBiometrico
{
    partial class FrmBiometricEnrollment
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnectDevice = new MaterialSkin.Controls.MaterialButton();
            this.btnLoadEmployees = new MaterialSkin.Controls.MaterialButton();
            this.btnStartEnrollment = new MaterialSkin.Controls.MaterialButton();
            this.lblSelectedEmployee = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.materialCard1 = new MaterialSkin.Controls.MaterialCard();
            this.txtFilterEmployee = new MaterialSkin.Controls.MaterialTextBox();
            this.dgvEmployees = new MaterialSkin.Controls.MaterialListView();
            this.colCI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFullName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialCard2 = new MaterialSkin.Controls.MaterialCard();
            this.materialCard1.SuspendLayout();
            this.materialCard2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectDevice
            // 
            this.btnConnectDevice.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConnectDevice.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnConnectDevice.Depth = 0;
            this.btnConnectDevice.HighEmphasis = true;
            this.btnConnectDevice.Icon = null;
            this.btnConnectDevice.Location = new System.Drawing.Point(18, 24);
            this.btnConnectDevice.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnectDevice.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnConnectDevice.Name = "btnConnectDevice";
            this.btnConnectDevice.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnConnectDevice.Size = new System.Drawing.Size(189, 36);
            this.btnConnectDevice.TabIndex = 0;
            this.btnConnectDevice.Text = "Conectar Dispositivo";
            this.btnConnectDevice.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnConnectDevice.UseAccentColor = false;
            this.btnConnectDevice.UseVisualStyleBackColor = true;
            this.btnConnectDevice.Click += new System.EventHandler(this.btnConnectDevice_Click);
            // 
            // btnLoadEmployees
            // 
            this.btnLoadEmployees.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadEmployees.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLoadEmployees.Depth = 0;
            this.btnLoadEmployees.HighEmphasis = false;
            this.btnLoadEmployees.Icon = null;
            this.btnLoadEmployees.Location = new System.Drawing.Point(18, 76);
            this.btnLoadEmployees.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLoadEmployees.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLoadEmployees.Name = "btnLoadEmployees";
            this.btnLoadEmployees.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLoadEmployees.Size = new System.Drawing.Size(168, 36);
            this.btnLoadEmployees.TabIndex = 2;
            this.btnLoadEmployees.Text = "Cargar Empleados";
            this.btnLoadEmployees.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnLoadEmployees.UseAccentColor = false;
            this.btnLoadEmployees.UseVisualStyleBackColor = true;
            this.btnLoadEmployees.Click += new System.EventHandler(this.btnLoadEmployees_Click);
            // 
            // btnStartEnrollment
            // 
            this.btnStartEnrollment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStartEnrollment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStartEnrollment.Depth = 0;
            this.btnStartEnrollment.Enabled = false;
            this.btnStartEnrollment.HighEmphasis = true;
            this.btnStartEnrollment.Icon = null;
            this.btnStartEnrollment.Location = new System.Drawing.Point(18, 128);
            this.btnStartEnrollment.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStartEnrollment.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStartEnrollment.Name = "btnStartEnrollment";
            this.btnStartEnrollment.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStartEnrollment.Size = new System.Drawing.Size(188, 36);
            this.btnStartEnrollment.TabIndex = 4;
            this.btnStartEnrollment.Text = "Iniciar Enrolamiento";
            this.btnStartEnrollment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStartEnrollment.UseAccentColor = false;
            this.btnStartEnrollment.UseVisualStyleBackColor = true;
            this.btnStartEnrollment.Click += new System.EventHandler(this.btnStartEnrollment_Click);
            // 
            // lblSelectedEmployee
            // 
            this.lblSelectedEmployee.AutoSize = true;
            this.lblSelectedEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedEmployee.Location = new System.Drawing.Point(11, 533);
            this.lblSelectedEmployee.Name = "lblSelectedEmployee";
            this.lblSelectedEmployee.Size = new System.Drawing.Size(0, 25);
            this.lblSelectedEmployee.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(11, 573);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 24);
            this.lblStatus.TabIndex = 5;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(11, 606);
            this.progressBar.Maximum = 3;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 23);
            this.progressBar.TabIndex = 6;
            // 
            // materialCard1
            // 
            this.materialCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard1.Controls.Add(this.txtFilterEmployee);
            this.materialCard1.Controls.Add(this.dgvEmployees);
            this.materialCard1.Controls.Add(this.progressBar);
            this.materialCard1.Controls.Add(this.lblStatus);
            this.materialCard1.Controls.Add(this.lblSelectedEmployee);
            this.materialCard1.Depth = 0;
            this.materialCard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard1.Location = new System.Drawing.Point(17, 97);
            this.materialCard1.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard1.Name = "materialCard1";
            this.materialCard1.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard1.Size = new System.Drawing.Size(757, 659);
            this.materialCard1.TabIndex = 7;
            // 
            // txtFilterEmployee
            // 
            this.txtFilterEmployee.AnimateReadOnly = false;
            this.txtFilterEmployee.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilterEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFilterEmployee.Depth = 0;
            this.txtFilterEmployee.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFilterEmployee.Hint = "Buscar Cliente por nombre o CI";
            this.txtFilterEmployee.LeadingIcon = null;
            this.txtFilterEmployee.Location = new System.Drawing.Point(30, 10);
            this.txtFilterEmployee.MaxLength = 50;
            this.txtFilterEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.txtFilterEmployee.Multiline = false;
            this.txtFilterEmployee.Name = "txtFilterEmployee";
            this.txtFilterEmployee.Size = new System.Drawing.Size(551, 50);
            this.txtFilterEmployee.TabIndex = 8;
            this.txtFilterEmployee.Text = "";
            this.txtFilterEmployee.TrailingIcon = null;
            this.txtFilterEmployee.TextChanged += new System.EventHandler(this.txtFilterEmployee_TextChanged);
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AutoSizeTable = false;
            this.dgvEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCI,
            this.colFullName,
            this.colUsername});
            this.dgvEmployees.Depth = 0;
            this.dgvEmployees.FullRowSelect = true;
            this.dgvEmployees.HideSelection = false;
            this.dgvEmployees.Location = new System.Drawing.Point(30, 82);
            this.dgvEmployees.MinimumSize = new System.Drawing.Size(200, 100);
            this.dgvEmployees.MouseLocation = new System.Drawing.Point(-1, -1);
            this.dgvEmployees.MouseState = MaterialSkin.MouseState.OUT;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.OwnerDraw = true;
            this.dgvEmployees.Size = new System.Drawing.Size(694, 444);
            this.dgvEmployees.TabIndex = 7;
            this.dgvEmployees.UseCompatibleStateImageBehavior = false;
            this.dgvEmployees.View = System.Windows.Forms.View.Details;
            this.dgvEmployees.SelectedIndexChanged += new System.EventHandler(this.dgvEmployees_SelectedIndexChanged);
            // 
            // colCI
            // 
            this.colCI.Text = "CI";
            this.colCI.Width = 120;
            //
            // colFullName
            //
            this.colFullName.Text = "Nombre Completo";
            this.colFullName.Width = 390;
            //
            // colUsername
            //
            this.colUsername.Text = "Usuario";
            this.colUsername.Width = 160;
            // 
            // materialCard2
            // 
            this.materialCard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialCard2.Controls.Add(this.btnConnectDevice);
            this.materialCard2.Controls.Add(this.btnLoadEmployees);
            this.materialCard2.Controls.Add(this.btnStartEnrollment);
            this.materialCard2.Depth = 0;
            this.materialCard2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialCard2.Location = new System.Drawing.Point(789, 97);
            this.materialCard2.Margin = new System.Windows.Forms.Padding(14);
            this.materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCard2.Name = "materialCard2";
            this.materialCard2.Padding = new System.Windows.Forms.Padding(14);
            this.materialCard2.Size = new System.Drawing.Size(290, 658);
            this.materialCard2.TabIndex = 8;
            // 
            // FrmBiometricEnrollment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 773);
            this.Controls.Add(this.materialCard1);
            this.Controls.Add(this.materialCard2);
            this.Name = "FrmBiometricEnrollment";
            this.Text = "BIOMETRICO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBiometricEnrollment_FormClosing);
            this.materialCard1.ResumeLayout(false);
            this.materialCard1.PerformLayout();
            this.materialCard2.ResumeLayout(false);
            this.materialCard2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton   btnConnectDevice;
        private MaterialSkin.Controls.MaterialButton   btnLoadEmployees;
        private MaterialSkin.Controls.MaterialButton   btnStartEnrollment;
        private System.Windows.Forms.Label             lblSelectedEmployee;
        private System.Windows.Forms.Label             lblStatus;
        private System.Windows.Forms.ProgressBar       progressBar;
        private MaterialSkin.Controls.MaterialCard     materialCard1;
        private MaterialSkin.Controls.MaterialCard     materialCard2;
        private MaterialSkin.Controls.MaterialListView dgvEmployees;
        private System.Windows.Forms.ColumnHeader colCI;
        private System.Windows.Forms.ColumnHeader colFullName;
        private System.Windows.Forms.ColumnHeader colUsername;
        private MaterialSkin.Controls.MaterialTextBox txtFilterEmployee;
    }
}

