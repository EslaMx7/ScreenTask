namespace ScreenTask.UI.Views
{
    partial class PreferencesView
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
            this.groupBox2 = new System.Windows.Forms.Panel();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.cbMiniStart = new System.Windows.Forms.CheckBox();
            this.cbCloseToTray = new System.Windows.Forms.CheckBox();
            this.cardLogs = new System.Windows.Forms.Panel();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnCopyLog = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.cardLogs.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2 (Startup & Window Preferences Card)
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBox2.Controls.Add(this.cbAutoStart);
            this.groupBox2.Controls.Add(this.cbMiniStart);
            this.groupBox2.Controls.Add(this.cbCloseToTray);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(652, 140);
            this.groupBox2.TabIndex = 0;
            // 
            // cbAutoStart
            // 
            this.cbAutoStart.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbAutoStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAutoStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbAutoStart.Location = new System.Drawing.Point(16, 35);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(500, 24);
            this.cbAutoStart.TabIndex = 0;
            this.cbAutoStart.Text = "Start broadcast automatically when ScreenTask opens";
            this.cbAutoStart.UseVisualStyleBackColor = true;
            // 
            // cbMiniStart
            // 
            this.cbMiniStart.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbMiniStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbMiniStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbMiniStart.Location = new System.Drawing.Point(16, 68);
            this.cbMiniStart.Name = "cbMiniStart";
            this.cbMiniStart.Size = new System.Drawing.Size(500, 24);
            this.cbMiniStart.TabIndex = 1;
            this.cbMiniStart.Text = "Start minimized to system tray";
            this.cbMiniStart.UseVisualStyleBackColor = true;
            // 
            // cbCloseToTray
            // 
            this.cbCloseToTray.Checked = true;
            this.cbCloseToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCloseToTray.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbCloseToTray.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbCloseToTray.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbCloseToTray.Location = new System.Drawing.Point(16, 101);
            this.cbCloseToTray.Name = "cbCloseToTray";
            this.cbCloseToTray.Size = new System.Drawing.Size(500, 24);
            this.cbCloseToTray.TabIndex = 2;
            this.cbCloseToTray.Text = "Minimize to system tray when closing window";
            this.cbCloseToTray.UseVisualStyleBackColor = true;
            // 
            // cardLogs (Diagnostic Logs Card)
            // 
            this.cardLogs.BackColor = System.Drawing.SystemColors.Control;
            this.cardLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardLogs.Controls.Add(this.btnClearLog);
            this.cardLogs.Controls.Add(this.btnCopyLog);
            this.cardLogs.Controls.Add(this.txtLog);
            this.cardLogs.Location = new System.Drawing.Point(0, 150);
            this.cardLogs.Name = "cardLogs";
            this.cardLogs.Size = new System.Drawing.Size(652, 340);
            this.cardLogs.TabIndex = 1;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearLog.Location = new System.Drawing.Point(460, 8);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(80, 24);
            this.btnClearLog.TabIndex = 0;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            // 
            // btnCopyLog
            // 
            this.btnCopyLog.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCopyLog.Location = new System.Drawing.Point(550, 8);
            this.btnCopyLog.Name = "btnCopyLog";
            this.btnCopyLog.Size = new System.Drawing.Size(90, 24);
            this.btnCopyLog.TabIndex = 1;
            this.btnCopyLog.Text = "📋 Copy Log";
            this.btnCopyLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLog.Location = new System.Drawing.Point(12, 38);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(628, 288);
            this.txtLog.TabIndex = 2;
            // 
            // PreferencesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cardLogs);
            this.Name = "PreferencesView";
            this.Size = new System.Drawing.Size(652, 504);
            this.groupBox2.ResumeLayout(false);
            this.cardLogs.ResumeLayout(false);
            this.cardLogs.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        internal System.Windows.Forms.Panel groupBox2;
        internal System.Windows.Forms.CheckBox cbAutoStart;
        internal System.Windows.Forms.CheckBox cbMiniStart;
        internal System.Windows.Forms.CheckBox cbCloseToTray;

        internal System.Windows.Forms.Panel cardLogs;
        internal System.Windows.Forms.Button btnClearLog;
        internal System.Windows.Forms.Button btnCopyLog;
        internal System.Windows.Forms.TextBox txtLog;
    }
}
