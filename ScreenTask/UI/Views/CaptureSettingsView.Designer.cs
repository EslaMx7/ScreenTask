namespace ScreenTask.UI.Views
{
    partial class CaptureSettingsView
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
            this.cardVideoSource = new ScreenTask.UI.Controls.FluentCard();
            this.label7 = new System.Windows.Forms.Label();
            this.comboScreens = new ScreenTask.UI.Controls.FluentComboBox();
            this.btnRefreshScreens = new ScreenTask.UI.Controls.FluentButton();
            this.cbCaptureMouse = new ScreenTask.UI.Controls.FluentToggleSwitch();
            this.cardAudio = new ScreenTask.UI.Controls.FluentCard();
            this.lblAudioPill = new System.Windows.Forms.Label();
            this.cbAudio = new ScreenTask.UI.Controls.FluentToggleSwitch();
            this.comboAudioDevices = new ScreenTask.UI.Controls.FluentComboBox();
            this.btnRefreshAudio = new ScreenTask.UI.Controls.FluentButton();
            this.btnTestAudio = new ScreenTask.UI.Controls.FluentButton();
            this.btnAudioDiag = new ScreenTask.UI.Controls.FluentButton();
            this.groupBox1 = new ScreenTask.UI.Controls.FluentCard();
            this.label8 = new System.Windows.Forms.Label();
            this.fpsSegmentedControl = new ScreenTask.UI.Controls.FluentSegmentedControl();
            this.comboFps = new ScreenTask.UI.Controls.FluentComboBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.cardVideoSource.SuspendLayout();
            this.cardAudio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // cardVideoSource
            // 
            this.cardVideoSource.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardVideoSource.Controls.Add(this.label7);
            this.cardVideoSource.Controls.Add(this.comboScreens);
            this.cardVideoSource.Controls.Add(this.btnRefreshScreens);
            this.cardVideoSource.Controls.Add(this.cbCaptureMouse);
            this.cardVideoSource.Location = new System.Drawing.Point(0, 0);
            this.cardVideoSource.Name = "cardVideoSource";
            this.cardVideoSource.Size = new System.Drawing.Size(652, 115);
            this.cardVideoSource.TabIndex = 0;
            this.cardVideoSource.Title = "Screen & Window Selection";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(16, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Share Display / Window:";
            // 
            // comboScreens
            // 
            this.comboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboScreens.Location = new System.Drawing.Point(165, 33);
            this.comboScreens.Name = "comboScreens";
            this.comboScreens.Size = new System.Drawing.Size(370, 24);
            this.comboScreens.TabIndex = 1;
            // 
            // btnRefreshScreens
            // 
            this.btnRefreshScreens.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshScreens.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshScreens.Location = new System.Drawing.Point(545, 32);
            this.btnRefreshScreens.Name = "btnRefreshScreens";
            this.btnRefreshScreens.Size = new System.Drawing.Size(90, 26);
            this.btnRefreshScreens.TabIndex = 2;
            this.btnRefreshScreens.Text = "🔄 Refresh";
            this.btnRefreshScreens.UseVisualStyleBackColor = true;
            // 
            // cbCaptureMouse
            // 
            this.cbCaptureMouse.Checked = true;
            this.cbCaptureMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaptureMouse.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbCaptureMouse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbCaptureMouse.Location = new System.Drawing.Point(16, 72);
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.Size = new System.Drawing.Size(550, 24);
            this.cbCaptureMouse.TabIndex = 3;
            this.cbCaptureMouse.Text = "Show mouse cursor on stream";
            this.cbCaptureMouse.UseVisualStyleBackColor = true;
            // 
            // cardAudio
            // 
            this.cardAudio.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardAudio.Controls.Add(this.lblAudioPill);
            this.cardAudio.Controls.Add(this.cbAudio);
            this.cardAudio.Controls.Add(this.comboAudioDevices);
            this.cardAudio.Controls.Add(this.btnRefreshAudio);
            this.cardAudio.Controls.Add(this.btnTestAudio);
            this.cardAudio.Controls.Add(this.btnAudioDiag);
            this.cardAudio.Location = new System.Drawing.Point(0, 125);
            this.cardAudio.Name = "cardAudio";
            this.cardAudio.Size = new System.Drawing.Size(652, 160);
            this.cardAudio.TabIndex = 1;
            this.cardAudio.Title = "Audio & Microphone";
            // 
            // lblAudioPill
            // 
            this.lblAudioPill.AutoSize = true;
            this.lblAudioPill.BackColor = System.Drawing.Color.Transparent;
            this.lblAudioPill.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblAudioPill.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAudioPill.Location = new System.Drawing.Point(485, 10);
            this.lblAudioPill.Name = "lblAudioPill";
            this.lblAudioPill.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblAudioPill.Size = new System.Drawing.Size(147, 14);
            this.lblAudioPill.TabIndex = 0;
            this.lblAudioPill.Text = "High Quality Audio (48 kHz)";
            // 
            // cbAudio
            // 
            this.cbAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAudio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbAudio.Location = new System.Drawing.Point(16, 35);
            this.cbAudio.Name = "cbAudio";
            this.cbAudio.Size = new System.Drawing.Size(450, 24);
            this.cbAudio.TabIndex = 1;
            this.cbAudio.Text = "Include sound in stream (Speakers or Microphone)";
            this.cbAudio.UseVisualStyleBackColor = true;
            // 
            // comboAudioDevices
            // 
            this.comboAudioDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioDevices.Enabled = false;
            this.comboAudioDevices.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboAudioDevices.Location = new System.Drawing.Point(16, 70);
            this.comboAudioDevices.Name = "comboAudioDevices";
            this.comboAudioDevices.Size = new System.Drawing.Size(515, 24);
            this.comboAudioDevices.TabIndex = 2;
            // 
            // btnRefreshAudio
            // 
            this.btnRefreshAudio.Enabled = false;
            this.btnRefreshAudio.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshAudio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshAudio.Location = new System.Drawing.Point(540, 69);
            this.btnRefreshAudio.Name = "btnRefreshAudio";
            this.btnRefreshAudio.Size = new System.Drawing.Size(95, 26);
            this.btnRefreshAudio.TabIndex = 3;
            this.btnRefreshAudio.Text = "🔄 Refresh";
            this.btnRefreshAudio.UseVisualStyleBackColor = true;
            // 
            // btnTestAudio
            // 
            this.btnTestAudio.Enabled = false;
            this.btnTestAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnTestAudio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTestAudio.Location = new System.Drawing.Point(16, 110);
            this.btnTestAudio.Name = "btnTestAudio";
            this.btnTestAudio.Size = new System.Drawing.Size(180, 32);
            this.btnTestAudio.TabIndex = 4;
            this.btnTestAudio.Text = "🔔 Test Speakers";
            this.btnTestAudio.UseVisualStyleBackColor = true;
            // 
            // btnAudioDiag
            // 
            this.btnAudioDiag.Enabled = false;
            this.btnAudioDiag.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnAudioDiag.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAudioDiag.Location = new System.Drawing.Point(210, 110);
            this.btnAudioDiag.Name = "btnAudioDiag";
            this.btnAudioDiag.Size = new System.Drawing.Size(210, 32);
            this.btnAudioDiag.TabIndex = 5;
            this.btnAudioDiag.Text = "⚙ Audio Device Setup & Test";
            this.btnAudioDiag.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.CardBackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.fpsSegmentedControl);
            this.groupBox1.Controls.Add(this.comboFps);
            this.groupBox1.Controls.Add(this.lblQuality);
            this.groupBox1.Controls.Add(this.qualitySlider);
            this.groupBox1.Location = new System.Drawing.Point(0, 295);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 190);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.Title = "Video Smoothness & Quality";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(16, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Framerate (Smoothness):";
            // 
            // fpsSegmentedControl
            // 
            this.fpsSegmentedControl.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.fpsSegmentedControl.Items.AddRange(new string[] {
                "60 Ultra",
                "30 Smooth",
                "15 Bal",
                "5 Saver"
            });
            this.fpsSegmentedControl.Location = new System.Drawing.Point(16, 56);
            this.fpsSegmentedControl.Name = "fpsSegmentedControl";
            this.fpsSegmentedControl.SelectedIndex = 1;
            this.fpsSegmentedControl.Size = new System.Drawing.Size(380, 32);
            this.fpsSegmentedControl.TabIndex = 1;
            this.fpsSegmentedControl.Visible = false;
            // 
            // comboFps
            // 
            this.comboFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFps.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboFps.Items.AddRange(new object[] {
                "60 FPS (Ultra)",
                "30 FPS (Smooth)",
                "15 FPS (Balanced)",
                "5 FPS (Saver)"
            });
            this.comboFps.Location = new System.Drawing.Point(16, 56);
            this.comboFps.Name = "comboFps";
            this.comboFps.Size = new System.Drawing.Size(240, 24);
            this.comboFps.TabIndex = 2;
            this.comboFps.Visible = true;
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblQuality.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblQuality.Location = new System.Drawing.Point(16, 105);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(202, 15);
            this.lblQuality.TabIndex = 3;
            this.lblQuality.Text = "Image Quality: 80% (High Detail)";
            // 
            // qualitySlider
            // 
            this.qualitySlider.Location = new System.Drawing.Point(14, 128);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 20;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(620, 45);
            this.qualitySlider.TabIndex = 4;
            this.qualitySlider.TickFrequency = 10;
            this.qualitySlider.Value = 80;
            // 
            // CaptureSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardVideoSource);
            this.Controls.Add(this.cardAudio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CaptureSettingsView";
            this.Size = new System.Drawing.Size(652, 504);
            this.cardVideoSource.ResumeLayout(false);
            this.cardVideoSource.PerformLayout();
            this.cardAudio.ResumeLayout(false);
            this.cardAudio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        internal ScreenTask.UI.Controls.FluentCard cardVideoSource;
        internal System.Windows.Forms.Label label7;
        internal ScreenTask.UI.Controls.FluentComboBox comboScreens;
        internal ScreenTask.UI.Controls.FluentButton btnRefreshScreens;
        internal ScreenTask.UI.Controls.FluentToggleSwitch cbCaptureMouse;

        internal ScreenTask.UI.Controls.FluentCard cardAudio;
        internal System.Windows.Forms.Label lblAudioPill;
        internal ScreenTask.UI.Controls.FluentToggleSwitch cbAudio;
        internal ScreenTask.UI.Controls.FluentComboBox comboAudioDevices;
        internal ScreenTask.UI.Controls.FluentButton btnRefreshAudio;
        internal ScreenTask.UI.Controls.FluentButton btnTestAudio;
        internal ScreenTask.UI.Controls.FluentButton btnAudioDiag;

        internal ScreenTask.UI.Controls.FluentCard groupBox1;
        internal System.Windows.Forms.Label label8;
        internal ScreenTask.UI.Controls.FluentSegmentedControl fpsSegmentedControl;
        internal ScreenTask.UI.Controls.FluentComboBox comboFps;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.TrackBar qualitySlider;
    }
}
