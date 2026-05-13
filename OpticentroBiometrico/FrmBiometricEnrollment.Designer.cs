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
            this.btnConnectDevice = new System.Windows.Forms.Button();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.btnLoadEmployees = new System.Windows.Forms.Button();
            this.lblSelectedEmployee = new System.Windows.Forms.Label();
            this.btnStartEnrollment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnectDevice
            // 
            this.btnConnectDevice.Location = new System.Drawing.Point(37, 22);
            this.btnConnectDevice.Name = "btnConnectDevice";
            this.btnConnectDevice.Size = new System.Drawing.Size(194, 66);
            this.btnConnectDevice.TabIndex = 0;
            this.btnConnectDevice.Text = "Conectar dispositivo";
            this.btnConnectDevice.UseVisualStyleBackColor = true;
            this.btnConnectDevice.Click += new System.EventHandler(this.btnConnectDevice_Click);
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Location = new System.Drawing.Point(37, 97);
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.RowHeadersWidth = 51;
            this.dgvEmployees.RowTemplate.Height = 24;
            this.dgvEmployees.Size = new System.Drawing.Size(1081, 363);
            this.dgvEmployees.TabIndex = 1;
            this.dgvEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployees_CellClick);
            // 
            // btnLoadEmployees
            // 
            this.btnLoadEmployees.Location = new System.Drawing.Point(237, 22);
            this.btnLoadEmployees.Name = "btnLoadEmployees";
            this.btnLoadEmployees.Size = new System.Drawing.Size(205, 66);
            this.btnLoadEmployees.TabIndex = 2;
            this.btnLoadEmployees.Text = "Cargar empleados";
            this.btnLoadEmployees.UseVisualStyleBackColor = true;
            this.btnLoadEmployees.Click += new System.EventHandler(this.btnLoadEmployees_Click);
            // 
            // lblSelectedEmployee
            // 
            this.lblSelectedEmployee.AutoSize = true;
            this.lblSelectedEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedEmployee.Location = new System.Drawing.Point(32, 490);
            this.lblSelectedEmployee.Name = "lblSelectedEmployee";
            this.lblSelectedEmployee.Size = new System.Drawing.Size(0, 25);
            this.lblSelectedEmployee.TabIndex = 3;
            // 
            // btnStartEnrollment
            // 
            this.btnStartEnrollment.Enabled = false;
            this.btnStartEnrollment.Location = new System.Drawing.Point(448, 22);
            this.btnStartEnrollment.Name = "btnStartEnrollment";
            this.btnStartEnrollment.Size = new System.Drawing.Size(205, 66);
            this.btnStartEnrollment.TabIndex = 4;
            this.btnStartEnrollment.Text = "Iniciar enrolamiento";
            this.btnStartEnrollment.UseVisualStyleBackColor = true;
            // 
            // FrmBiometricEnrollment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 681);
            this.Controls.Add(this.btnStartEnrollment);
            this.Controls.Add(this.lblSelectedEmployee);
            this.Controls.Add(this.btnLoadEmployees);
            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.btnConnectDevice);
            this.Name = "FrmBiometricEnrollment";
            this.Text = "Opticentro Biométrico";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectDevice;
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Button btnLoadEmployees;
        private System.Windows.Forms.Label lblSelectedEmployee;
        private System.Windows.Forms.Button btnStartEnrollment;
    }
}

