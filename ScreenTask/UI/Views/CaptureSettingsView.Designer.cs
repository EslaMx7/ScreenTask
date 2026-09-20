namespace ScreenTask.UI.Views
{
    partial class CaptureSettingsView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _thumbnailTimer.Stop();
                _thumbnailTimer.Dispose();

                foreach (var card in _displayCards.Concat(_windowCards))
                {
                    card.UpdateThumbnail(null);
                    card.Dispose();
                }
                _displayCards.Clear();
                _windowCards.Clear();

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.cardVideoSource = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTabDisplays = new System.Windows.Forms.Button();
            this.btnTabWindows = new System.Windows.Forms.Button();
            this.btnRefreshScreens = new System.Windows.Forms.Button();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.flowSourceCards = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNoSources = new System.Windows.Forms.Label();
            this.lblSelectedSource = new System.Windows.Forms.Label();
            this.comboScreens = new System.Windows.Forms.ComboBox();
            this.cardAudio = new System.Windows.Forms.Panel();
            this.lblAudioPill = new System.Windows.Forms.Label();
            this.cbAudio = new System.Windows.Forms.CheckBox();
            this.comboAudioDevices = new System.Windows.Forms.ComboBox();
            this.btnRefreshAudio = new System.Windows.Forms.Button();
            this.btnTestAudio = new System.Windows.Forms.Button();
            this.btnAudioDiag = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.comboFps = new System.Windows.Forms.ComboBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.cardVideoSource.SuspendLayout();
            this.flowSourceCards.SuspendLayout();
            this.cardAudio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // cardVideoSource
            // 
            this.cardVideoSource.BackColor = System.Drawing.SystemColors.Control;
            this.cardVideoSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardVideoSource.Controls.Add(this.label7);
            this.cardVideoSource.Controls.Add(this.btnTabDisplays);
            this.cardVideoSource.Controls.Add(this.btnTabWindows);
            this.cardVideoSource.Controls.Add(this.btnRefreshScreens);
            this.cardVideoSource.Controls.Add(this.cbCaptureMouse);
            this.cardVideoSource.Controls.Add(this.flowSourceCards);
            this.cardVideoSource.Controls.Add(this.lblSelectedSource);
            this.cardVideoSource.Controls.Add(this.comboScreens);
            this.cardVideoSource.Location = new System.Drawing.Point(0, 0);
            this.cardVideoSource.Name = "cardVideoSource";
            this.cardVideoSource.Size = new System.Drawing.Size(652, 340);
            this.cardVideoSource.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(12, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Share Source:";
            // 
            // btnTabDisplays
            // 
            this.btnTabDisplays.Location = new System.Drawing.Point(0, 0);
            this.btnTabDisplays.Name = "btnTabDisplays";
            this.btnTabDisplays.Size = new System.Drawing.Size(0, 0);
            this.btnTabDisplays.TabIndex = 1;
            this.btnTabDisplays.Visible = false;
            // 
            // btnTabWindows
            // 
            this.btnTabWindows.Location = new System.Drawing.Point(0, 0);
            this.btnTabWindows.Name = "btnTabWindows";
            this.btnTabWindows.Size = new System.Drawing.Size(0, 0);
            this.btnTabWindows.TabIndex = 2;
            this.btnTabWindows.Visible = false;
            // 
            // btnRefreshScreens
            // 
            this.btnRefreshScreens.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshScreens.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshScreens.Location = new System.Drawing.Point(105, 6);
            this.btnRefreshScreens.Name = "btnRefreshScreens";
            this.btnRefreshScreens.Size = new System.Drawing.Size(95, 27);
            this.btnRefreshScreens.TabIndex = 3;
            this.btnRefreshScreens.Text = "🔄 Refresh";
            this.btnRefreshScreens.UseVisualStyleBackColor = true;
            // 
            // cbCaptureMouse
            // 
            this.cbCaptureMouse.Checked = true;
            this.cbCaptureMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaptureMouse.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cbCaptureMouse.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbCaptureMouse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbCaptureMouse.Location = new System.Drawing.Point(215, 10);
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.Size = new System.Drawing.Size(150, 20);
            this.cbCaptureMouse.TabIndex = 4;
            this.cbCaptureMouse.Text = "Show mouse cursor";
            this.cbCaptureMouse.UseVisualStyleBackColor = true;
            // 
            // flowSourceCards
            // 
            this.flowSourceCards.AutoScroll = true;
            this.flowSourceCards.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flowSourceCards.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowSourceCards.Controls.Add(this.lblNoSources);
            this.flowSourceCards.Location = new System.Drawing.Point(10, 38);
            this.flowSourceCards.Name = "flowSourceCards";
            this.flowSourceCards.Padding = new System.Windows.Forms.Padding(6);
            this.flowSourceCards.Size = new System.Drawing.Size(630, 266);
            this.flowSourceCards.TabIndex = 5;
            // 
            // lblNoSources
            // 
            this.lblNoSources.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoSources.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNoSources.Location = new System.Drawing.Point(9, 6);
            this.lblNoSources.Name = "lblNoSources";
            this.lblNoSources.Size = new System.Drawing.Size(595, 40);
            this.lblNoSources.TabIndex = 0;
            this.lblNoSources.Text = "No capture sources found in this category.";
            this.lblNoSources.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNoSources.Visible = false;
            // 
            // lblSelectedSource
            // 
            this.lblSelectedSource.AutoEllipsis = true;
            this.lblSelectedSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSelectedSource.Location = new System.Drawing.Point(12, 310);
            this.lblSelectedSource.Name = "lblSelectedSource";
            this.lblSelectedSource.Size = new System.Drawing.Size(626, 22);
            this.lblSelectedSource.TabIndex = 7;
            this.lblSelectedSource.Text = "Selected: None";
            this.lblSelectedSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboScreens
            // 
            this.comboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboScreens.Location = new System.Drawing.Point(10, 312);
            this.comboScreens.Name = "comboScreens";
            this.comboScreens.Size = new System.Drawing.Size(10, 10);
            this.comboScreens.TabIndex = 6;
            this.comboScreens.Visible = false;
            // 
            // cardAudio
            // 
            this.cardAudio.BackColor = System.Drawing.SystemColors.Control;
            this.cardAudio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAudio.Controls.Add(this.lblAudioPill);
            this.cardAudio.Controls.Add(this.cbAudio);
            this.cardAudio.Controls.Add(this.comboAudioDevices);
            this.cardAudio.Controls.Add(this.btnRefreshAudio);
            this.cardAudio.Controls.Add(this.btnTestAudio);
            this.cardAudio.Controls.Add(this.btnAudioDiag);
            this.cardAudio.Location = new System.Drawing.Point(0, 350);
            this.cardAudio.Name = "cardAudio";
            this.cardAudio.Size = new System.Drawing.Size(652, 160);
            this.cardAudio.TabIndex = 1;
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
            this.cbAudio.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
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
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboFps);
            this.groupBox1.Controls.Add(this.lblQuality);
            this.groupBox1.Controls.Add(this.qualitySlider);
            this.groupBox1.Location = new System.Drawing.Point(0, 520);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 190);
            this.groupBox1.TabIndex = 2;
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
            this.AutoScroll = true;
            this.Controls.Add(this.cardVideoSource);
            this.Controls.Add(this.cardAudio);
            this.Controls.Add(this.groupBox1);
            this.Name = "CaptureSettingsView";
            this.Size = new System.Drawing.Size(652, 544);
            this.cardVideoSource.ResumeLayout(false);
            this.cardVideoSource.PerformLayout();
            this.flowSourceCards.ResumeLayout(false);
            this.cardAudio.ResumeLayout(false);
            this.cardAudio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        internal System.Windows.Forms.Panel cardVideoSource;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Button btnTabDisplays;
        internal System.Windows.Forms.Button btnTabWindows;
        internal System.Windows.Forms.Button btnRefreshScreens;
        internal System.Windows.Forms.CheckBox cbCaptureMouse;
        internal System.Windows.Forms.FlowLayoutPanel flowSourceCards;
        internal System.Windows.Forms.Label lblNoSources;
        internal System.Windows.Forms.Label lblSelectedSource;
        internal System.Windows.Forms.ComboBox comboScreens;

        internal System.Windows.Forms.Panel cardAudio;
        internal System.Windows.Forms.Label lblAudioPill;
        internal System.Windows.Forms.CheckBox cbAudio;
        internal System.Windows.Forms.ComboBox comboAudioDevices;
        internal System.Windows.Forms.Button btnRefreshAudio;
        internal System.Windows.Forms.Button btnTestAudio;
        internal System.Windows.Forms.Button btnAudioDiag;

        internal System.Windows.Forms.Panel groupBox1;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ComboBox comboFps;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.TrackBar qualitySlider;
    }
}
