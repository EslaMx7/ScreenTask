namespace ScreenTask.UI.Views
{
    partial class NetworkSettingsView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.cardNetwork = new ScreenTask.UI.Controls.FluentCard();
            this.lblNetworkPill = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboIPs = new ScreenTask.UI.Controls.FluentComboBox();
            this.btnRefreshIPs = new ScreenTask.UI.Controls.FluentButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.lblPortStatus = new System.Windows.Forms.Label();
            this.gbOptions = new ScreenTask.UI.Controls.FluentCard();
            this.cbPrivate = new ScreenTask.UI.Controls.FluentToggleSwitch();
            this.lblPinDesc = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnGenPin = new ScreenTask.UI.Controls.FluentButton();
            this.btnCopyPin = new ScreenTask.UI.Controls.FluentButton();
            this.cardNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardNetwork
            // 
            this.cardNetwork.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardNetwork.Controls.Add(this.lblNetworkPill);
            this.cardNetwork.Controls.Add(this.label5);
            this.cardNetwork.Controls.Add(this.comboIPs);
            this.cardNetwork.Controls.Add(this.btnRefreshIPs);
            this.cardNetwork.Controls.Add(this.label2);
            this.cardNetwork.Controls.Add(this.numPort);
            this.cardNetwork.Controls.Add(this.lblPortStatus);
            this.cardNetwork.Location = new System.Drawing.Point(0, 0);
            this.cardNetwork.Name = "cardNetwork";
            this.cardNetwork.Size = new System.Drawing.Size(652, 185);
            this.cardNetwork.TabIndex = 0;
            this.cardNetwork.Title = "Network Connection";
            // 
            // lblNetworkPill
            // 
            this.lblNetworkPill.AutoSize = true;
            this.lblNetworkPill.BackColor = System.Drawing.Color.Transparent;
            this.lblNetworkPill.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblNetworkPill.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNetworkPill.Location = new System.Drawing.Point(525, 10);
            this.lblNetworkPill.Name = "lblNetworkPill";
            this.lblNetworkPill.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblNetworkPill.Size = new System.Drawing.Size(107, 14);
            this.lblNetworkPill.TabIndex = 0;
            this.lblNetworkPill.Text = "Network Connected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(16, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Network Connection (IP Address):";
            // 
            // comboIPs
            // 
            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPs.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboIPs.Location = new System.Drawing.Point(16, 58);
            this.comboIPs.Name = "comboIPs";
            this.comboIPs.Size = new System.Drawing.Size(515, 24);
            this.comboIPs.TabIndex = 2;
            // 
            // btnRefreshIPs
            // 
            this.btnRefreshIPs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshIPs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshIPs.Location = new System.Drawing.Point(540, 57);
            this.btnRefreshIPs.Name = "btnRefreshIPs";
            this.btnRefreshIPs.Size = new System.Drawing.Size(95, 26);
            this.btnRefreshIPs.TabIndex = 3;
            this.btnRefreshIPs.Text = "🔄 Refresh";
            this.btnRefreshIPs.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(16, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port Number:";
            // 
            // numPort
            // 
            this.numPort.BackColor = System.Drawing.SystemColors.Window;
            this.numPort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.numPort.Font = new System.Drawing.Font("Consolas", 9F);
            this.numPort.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numPort.Location = new System.Drawing.Point(16, 128);
            this.numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(120, 22);
            this.numPort.TabIndex = 5;
            this.numPort.Value = new decimal(new int[] { 7070, 0, 0, 0 });
            // 
            // lblPortStatus
            // 
            this.lblPortStatus.AutoSize = true;
            this.lblPortStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblPortStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPortStatus.Location = new System.Drawing.Point(145, 132);
            this.lblPortStatus.Name = "lblPortStatus";
            this.lblPortStatus.Size = new System.Drawing.Size(165, 12);
            this.lblPortStatus.TabIndex = 6;
            this.lblPortStatus.Text = "Default: 7070 (Recommended)";
            // 
            // gbOptions
            // 
            this.gbOptions.CardBackColor = System.Drawing.SystemColors.Control;
            this.gbOptions.Controls.Add(this.cbPrivate);
            this.gbOptions.Controls.Add(this.lblPinDesc);
            this.gbOptions.Controls.Add(this.txtPassword);
            this.gbOptions.Controls.Add(this.btnGenPin);
            this.gbOptions.Controls.Add(this.btnCopyPin);
            this.gbOptions.Location = new System.Drawing.Point(0, 198);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(652, 185);
            this.gbOptions.TabIndex = 1;
            this.gbOptions.Title = "Access Security & PIN";
            // 
            // cbPrivate
            // 
            this.cbPrivate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbPrivate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbPrivate.Location = new System.Drawing.Point(16, 35);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(500, 24);
            this.cbPrivate.TabIndex = 0;
            this.cbPrivate.Text = "Protect stream with a PIN code";
            this.cbPrivate.UseVisualStyleBackColor = true;
            // 
            // lblPinDesc
            // 
            this.lblPinDesc.AutoSize = true;
            this.lblPinDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPinDesc.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPinDesc.Location = new System.Drawing.Point(16, 75);
            this.lblPinDesc.Name = "lblPinDesc";
            this.lblPinDesc.Size = new System.Drawing.Size(350, 13);
            this.lblPinDesc.TabIndex = 1;
            this.lblPinDesc.Text = "Viewers must enter this 4-digit PIN in their browser to watch:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPassword.Location = new System.Drawing.Point(16, 102);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(160, 26);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGenPin
            // 
            this.btnGenPin.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnGenPin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGenPin.Location = new System.Drawing.Point(190, 101);
            this.btnGenPin.Name = "btnGenPin";
            this.btnGenPin.Size = new System.Drawing.Size(130, 28);
            this.btnGenPin.TabIndex = 3;
            this.btnGenPin.Text = "🎲 New PIN";
            this.btnGenPin.UseVisualStyleBackColor = true;
            // 
            // btnCopyPin
            // 
            this.btnCopyPin.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyPin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCopyPin.Location = new System.Drawing.Point(330, 101);
            this.btnCopyPin.Name = "btnCopyPin";
            this.btnCopyPin.Size = new System.Drawing.Size(130, 28);
            this.btnCopyPin.TabIndex = 4;
            this.btnCopyPin.Text = "📋 Copy PIN";
            this.btnCopyPin.UseVisualStyleBackColor = true;
            // 
            // NetworkSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardNetwork);
            this.Controls.Add(this.gbOptions);
            this.Name = "NetworkSettingsView";
            this.Size = new System.Drawing.Size(652, 504);
            this.cardNetwork.ResumeLayout(false);
            this.cardNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        internal ScreenTask.UI.Controls.FluentCard cardNetwork;
        internal System.Windows.Forms.Label lblNetworkPill;
        internal System.Windows.Forms.Label label5;
        internal ScreenTask.UI.Controls.FluentComboBox comboIPs;
        internal ScreenTask.UI.Controls.FluentButton btnRefreshIPs;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown numPort;
        internal System.Windows.Forms.Label lblPortStatus;

        internal ScreenTask.UI.Controls.FluentCard gbOptions;
        internal ScreenTask.UI.Controls.FluentToggleSwitch cbPrivate;
        internal System.Windows.Forms.Label lblPinDesc;
        internal System.Windows.Forms.TextBox txtPassword;
        internal ScreenTask.UI.Controls.FluentButton btnGenPin;
        internal ScreenTask.UI.Controls.FluentButton btnCopyPin;
    }
}
