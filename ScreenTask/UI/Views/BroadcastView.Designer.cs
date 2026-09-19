namespace ScreenTask.UI.Views
{
    partial class BroadcastView
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
            this.cardPreview = new System.Windows.Forms.Panel();
            this.monitorPreview = new ScreenTask.UI.Controls.MonitorPreviewControl();
            this.lblCursorStatus = new System.Windows.Forms.Label();
            this.btnSwitchSource = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.Panel();
            this.lblBroadcastDesc = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnQuickMute = new System.Windows.Forms.Button();
            this.lblAudioMeterText = new System.Windows.Forms.Label();
            this.pbAudioMeter = new System.Windows.Forms.ProgressBar();
            this.lblLiveStats = new System.Windows.Forms.Label();
            this.cardUrl = new System.Windows.Forms.Panel();
            this.lblUrlTitle = new System.Windows.Forms.Label();
            this.lblWifiBadge = new System.Windows.Forms.Label();
            this.lblPinDisplay = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnCopyUrl = new System.Windows.Forms.Button();
            this.btnQr = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.cardChipViewers = new System.Windows.Forms.Panel();
            this.lblIconViewers = new System.Windows.Forms.Label();
            this.lblTitleViewers = new System.Windows.Forms.Label();
            this.lblChipViewers = new System.Windows.Forms.Label();
            this.cardChipFps = new System.Windows.Forms.Panel();
            this.lblIconFps = new System.Windows.Forms.Label();
            this.lblTitleFps = new System.Windows.Forms.Label();
            this.lblChipFps = new System.Windows.Forms.Label();
            this.cardChipData = new System.Windows.Forms.Panel();
            this.lblIconData = new System.Windows.Forms.Label();
            this.lblTitleData = new System.Windows.Forms.Label();
            this.lblChipData = new System.Windows.Forms.Label();
            this.cardChipUptime = new System.Windows.Forms.Panel();
            this.lblIconUptime = new System.Windows.Forms.Label();
            this.lblTitleUptime = new System.Windows.Forms.Label();
            this.lblChipUptime = new System.Windows.Forms.Label();
            this.cardPreview.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cardUrl.SuspendLayout();
            this.cardChipViewers.SuspendLayout();
            this.cardChipFps.SuspendLayout();
            this.cardChipData.SuspendLayout();
            this.cardChipUptime.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardPreview (Live Monitor Preview)
            // 
            this.cardPreview.BackColor = System.Drawing.SystemColors.Control;
            this.cardPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPreview.Controls.Add(this.monitorPreview);
            this.cardPreview.Controls.Add(this.lblCursorStatus);
            this.cardPreview.Controls.Add(this.btnSwitchSource);
            this.cardPreview.Location = new System.Drawing.Point(0, 0);
            this.cardPreview.Name = "cardPreview";
            this.cardPreview.Size = new System.Drawing.Size(384, 222);
            this.cardPreview.TabIndex = 0;
            // 
            // monitorPreview
            // 
            this.monitorPreview.AudioInfoText = "Audio: Active";
            this.monitorPreview.BackColor = System.Drawing.Color.Black;
            this.monitorPreview.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.monitorPreview.FpsText = "30.0 FPS";
            this.monitorPreview.IsLive = false;
            this.monitorPreview.Location = new System.Drawing.Point(12, 12);
            this.monitorPreview.Name = "monitorPreview";
            this.monitorPreview.ResolutionText = "1920 × 1080";
            this.monitorPreview.Size = new System.Drawing.Size(360, 175);
            this.monitorPreview.SourceTitle = "Display 1 (Primary)";
            this.monitorPreview.TabIndex = 0;
            // 
            // lblCursorStatus
            // 
            this.lblCursorStatus.AutoSize = true;
            this.lblCursorStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblCursorStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCursorStatus.Location = new System.Drawing.Point(14, 196);
            this.lblCursorStatus.Name = "lblCursorStatus";
            this.lblCursorStatus.Size = new System.Drawing.Size(126, 12);
            this.lblCursorStatus.TabIndex = 1;
            this.lblCursorStatus.Text = "Mouse Pointer: Visible";
            // 
            // btnSwitchSource
            // 
            this.btnSwitchSource.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSwitchSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSwitchSource.Location = new System.Drawing.Point(240, 190);
            this.btnSwitchSource.Name = "btnSwitchSource";
            this.btnSwitchSource.Size = new System.Drawing.Size(132, 24);
            this.btnSwitchSource.TabIndex = 2;
            this.btnSwitchSource.Text = "Change Source →";
            this.btnSwitchSource.UseVisualStyleBackColor = true;
            // 
            // groupBox3 (Broadcast Control Card)
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBox3.Controls.Add(this.lblBroadcastDesc);
            this.groupBox3.Controls.Add(this.btnStartServer);
            this.groupBox3.Controls.Add(this.btnQuickMute);
            this.groupBox3.Controls.Add(this.lblAudioMeterText);
            this.groupBox3.Controls.Add(this.pbAudioMeter);
            this.groupBox3.Controls.Add(this.lblLiveStats);
            this.groupBox3.Location = new System.Drawing.Point(394, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 222);
            this.groupBox3.TabIndex = 1;
            // 
            // lblBroadcastDesc
            // 
            this.lblBroadcastDesc.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblBroadcastDesc.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblBroadcastDesc.Location = new System.Drawing.Point(12, 12);
            this.lblBroadcastDesc.Name = "lblBroadcastDesc";
            this.lblBroadcastDesc.Size = new System.Drawing.Size(234, 40);
            this.lblBroadcastDesc.TabIndex = 0;
            this.lblBroadcastDesc.Text = "Share your screen with any phone, tablet, or browser on your local network.";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartServer.Location = new System.Drawing.Point(12, 54);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(234, 40);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "▶  START BROADCAST";
            this.btnStartServer.UseVisualStyleBackColor = true;
            // 
            // btnQuickMute
            // 
            this.btnQuickMute.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnQuickMute.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickMute.Location = new System.Drawing.Point(12, 98);
            this.btnQuickMute.Name = "btnQuickMute";
            this.btnQuickMute.Size = new System.Drawing.Size(234, 30);
            this.btnQuickMute.TabIndex = 2;
            this.btnQuickMute.Text = "🔊 Unmute Audio";
            this.btnQuickMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnQuickMute.UseVisualStyleBackColor = true;
            // 
            // lblAudioMeterText
            // 
            this.lblAudioMeterText.AutoSize = true;
            this.lblAudioMeterText.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioMeterText.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioMeterText.Location = new System.Drawing.Point(12, 134);
            this.lblAudioMeterText.Name = "lblAudioMeterText";
            this.lblAudioMeterText.Size = new System.Drawing.Size(95, 12);
            this.lblAudioMeterText.TabIndex = 3;
            this.lblAudioMeterText.Text = "Sound Volume Level:";
            // 
            // pbAudioMeter
            // 
            this.pbAudioMeter.Enabled = false;
            this.pbAudioMeter.Location = new System.Drawing.Point(12, 150);
            this.pbAudioMeter.Name = "pbAudioMeter";
            this.pbAudioMeter.Size = new System.Drawing.Size(234, 10);
            this.pbAudioMeter.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAudioMeter.TabIndex = 4;
            this.pbAudioMeter.Value = 0;
            // 
            // lblLiveStats
            // 
            this.lblLiveStats.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblLiveStats.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLiveStats.Location = new System.Drawing.Point(12, 166);
            this.lblLiveStats.Name = "lblLiveStats";
            this.lblLiveStats.Size = new System.Drawing.Size(234, 48);
            this.lblLiveStats.TabIndex = 5;
            this.lblLiveStats.Text = "⚡ Stopped • Ready to broadcast";
            this.lblLiveStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardUrl (Stream URL Card)
            // 
            this.cardUrl.BackColor = System.Drawing.SystemColors.Control;
            this.cardUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardUrl.Controls.Add(this.lblUrlTitle);
            this.cardUrl.Controls.Add(this.lblWifiBadge);
            this.cardUrl.Controls.Add(this.lblPinDisplay);
            this.cardUrl.Controls.Add(this.label1);
            this.cardUrl.Controls.Add(this.txtURL);
            this.cardUrl.Controls.Add(this.btnCopyUrl);
            this.cardUrl.Controls.Add(this.btnQr);
            this.cardUrl.Controls.Add(this.btnLaunch);
            this.cardUrl.Location = new System.Drawing.Point(0, 230);
            this.cardUrl.Name = "cardUrl";
            this.cardUrl.Size = new System.Drawing.Size(652, 90);
            this.cardUrl.TabIndex = 2;
            // 
            // lblUrlTitle
            // 
            this.lblUrlTitle.AutoSize = true;
            this.lblUrlTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUrlTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUrlTitle.Location = new System.Drawing.Point(12, 10);
            this.lblUrlTitle.Name = "lblUrlTitle";
            this.lblUrlTitle.Size = new System.Drawing.Size(120, 15);
            this.lblUrlTitle.TabIndex = 0;
            this.lblUrlTitle.Text = "Local Stream Address";
            // 
            // lblWifiBadge
            // 
            this.lblWifiBadge.AutoSize = true;
            this.lblWifiBadge.BackColor = System.Drawing.Color.Transparent;
            this.lblWifiBadge.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblWifiBadge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblWifiBadge.Location = new System.Drawing.Point(140, 11);
            this.lblWifiBadge.Name = "lblWifiBadge";
            this.lblWifiBadge.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblWifiBadge.Size = new System.Drawing.Size(130, 14);
            this.lblWifiBadge.TabIndex = 1;
            this.lblWifiBadge.Text = "Local Network (Zero Config)";
            // 
            // lblPinDisplay
            // 
            this.lblPinDisplay.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPinDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPinDisplay.Location = new System.Drawing.Point(470, 10);
            this.lblPinDisplay.Name = "lblPinDisplay";
            this.lblPinDisplay.Size = new System.Drawing.Size(170, 16);
            this.lblPinDisplay.TabIndex = 2;
            this.lblPinDisplay.Text = "PIN: Not Required";
            this.lblPinDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "URL";
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtURL.Location = new System.Drawing.Point(44, 40);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(320, 22);
            this.txtURL.TabIndex = 4;
            // 
            // btnCopyUrl
            // 
            this.btnCopyUrl.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyUrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCopyUrl.Location = new System.Drawing.Point(372, 38);
            this.btnCopyUrl.Name = "btnCopyUrl";
            this.btnCopyUrl.Size = new System.Drawing.Size(86, 26);
            this.btnCopyUrl.TabIndex = 5;
            this.btnCopyUrl.Text = "📋 Copy Link";
            this.btnCopyUrl.UseVisualStyleBackColor = true;
            // 
            // btnQr
            // 
            this.btnQr.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnQr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQr.Location = new System.Drawing.Point(464, 38);
            this.btnQr.Name = "btnQr";
            this.btnQr.Size = new System.Drawing.Size(86, 26);
            this.btnQr.TabIndex = 6;
            this.btnQr.Text = "📱 Show QR";
            this.btnQr.UseVisualStyleBackColor = true;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLaunch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLaunch.Location = new System.Drawing.Point(556, 38);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(86, 26);
            this.btnLaunch.TabIndex = 7;
            this.btnLaunch.Text = "🌐 Open";
            this.btnLaunch.UseVisualStyleBackColor = true;
            // 
            // cardChipViewers
            // 
            this.cardChipViewers.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipViewers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardChipViewers.Controls.Add(this.lblIconViewers);
            this.cardChipViewers.Controls.Add(this.lblTitleViewers);
            this.cardChipViewers.Controls.Add(this.lblChipViewers);
            this.cardChipViewers.Location = new System.Drawing.Point(0, 328);
            this.cardChipViewers.Name = "cardChipViewers";
            this.cardChipViewers.Size = new System.Drawing.Size(157, 72);
            this.cardChipViewers.TabIndex = 3;
            // 
            // lblIconViewers
            // 
            this.lblIconViewers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconViewers.Location = new System.Drawing.Point(8, 8);
            this.lblIconViewers.Name = "lblIconViewers";
            this.lblIconViewers.Size = new System.Drawing.Size(28, 28);
            this.lblIconViewers.TabIndex = 0;
            this.lblIconViewers.Text = "👥";
            // 
            // lblTitleViewers
            // 
            this.lblTitleViewers.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleViewers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleViewers.Location = new System.Drawing.Point(40, 8);
            this.lblTitleViewers.Name = "lblTitleViewers";
            this.lblTitleViewers.Size = new System.Drawing.Size(110, 16);
            this.lblTitleViewers.TabIndex = 1;
            this.lblTitleViewers.Text = "Connected Viewers";
            // 
            // lblChipViewers
            // 
            this.lblChipViewers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipViewers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipViewers.Location = new System.Drawing.Point(8, 38);
            this.lblChipViewers.Name = "lblChipViewers";
            this.lblChipViewers.Size = new System.Drawing.Size(142, 22);
            this.lblChipViewers.TabIndex = 2;
            this.lblChipViewers.Text = "0 Connected";
            // 
            // cardChipFps
            // 
            this.cardChipFps.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipFps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardChipFps.Controls.Add(this.lblIconFps);
            this.cardChipFps.Controls.Add(this.lblTitleFps);
            this.cardChipFps.Controls.Add(this.lblChipFps);
            this.cardChipFps.Location = new System.Drawing.Point(165, 328);
            this.cardChipFps.Name = "cardChipFps";
            this.cardChipFps.Size = new System.Drawing.Size(157, 72);
            this.cardChipFps.TabIndex = 4;
            // 
            // lblIconFps
            // 
            this.lblIconFps.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconFps.Location = new System.Drawing.Point(8, 8);
            this.lblIconFps.Name = "lblIconFps";
            this.lblIconFps.Size = new System.Drawing.Size(28, 28);
            this.lblIconFps.TabIndex = 0;
            this.lblIconFps.Text = "⚡";
            // 
            // lblTitleFps
            // 
            this.lblTitleFps.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleFps.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleFps.Location = new System.Drawing.Point(40, 8);
            this.lblTitleFps.Name = "lblTitleFps";
            this.lblTitleFps.Size = new System.Drawing.Size(110, 16);
            this.lblTitleFps.TabIndex = 1;
            this.lblTitleFps.Text = "Smoothness (FPS)";
            // 
            // lblChipFps
            // 
            this.lblChipFps.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipFps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipFps.Location = new System.Drawing.Point(8, 38);
            this.lblChipFps.Name = "lblChipFps";
            this.lblChipFps.Size = new System.Drawing.Size(142, 22);
            this.lblChipFps.TabIndex = 2;
            this.lblChipFps.Text = "0.0 FPS";
            // 
            // cardChipData
            // 
            this.cardChipData.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardChipData.Controls.Add(this.lblIconData);
            this.cardChipData.Controls.Add(this.lblTitleData);
            this.cardChipData.Controls.Add(this.lblChipData);
            this.cardChipData.Location = new System.Drawing.Point(330, 328);
            this.cardChipData.Name = "cardChipData";
            this.cardChipData.Size = new System.Drawing.Size(157, 72);
            this.cardChipData.TabIndex = 5;
            // 
            // lblIconData
            // 
            this.lblIconData.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconData.Location = new System.Drawing.Point(8, 8);
            this.lblIconData.Name = "lblIconData";
            this.lblIconData.Size = new System.Drawing.Size(28, 28);
            this.lblIconData.TabIndex = 0;
            this.lblIconData.Text = "📊";
            // 
            // lblTitleData
            // 
            this.lblTitleData.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleData.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleData.Location = new System.Drawing.Point(40, 8);
            this.lblTitleData.Name = "lblTitleData";
            this.lblTitleData.Size = new System.Drawing.Size(110, 16);
            this.lblTitleData.TabIndex = 1;
            this.lblTitleData.Text = "Data Transferred";
            // 
            // lblChipData
            // 
            this.lblChipData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipData.Location = new System.Drawing.Point(8, 38);
            this.lblChipData.Name = "lblChipData";
            this.lblChipData.Size = new System.Drawing.Size(142, 22);
            this.lblChipData.TabIndex = 2;
            this.lblChipData.Text = "0.0 MB";
            // 
            // cardChipUptime
            // 
            this.cardChipUptime.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipUptime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardChipUptime.Controls.Add(this.lblIconUptime);
            this.cardChipUptime.Controls.Add(this.lblTitleUptime);
            this.cardChipUptime.Controls.Add(this.lblChipUptime);
            this.cardChipUptime.Location = new System.Drawing.Point(495, 328);
            this.cardChipUptime.Name = "cardChipUptime";
            this.cardChipUptime.Size = new System.Drawing.Size(157, 72);
            this.cardChipUptime.TabIndex = 6;
            // 
            // lblIconUptime
            // 
            this.lblIconUptime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconUptime.Location = new System.Drawing.Point(8, 8);
            this.lblIconUptime.Name = "lblIconUptime";
            this.lblIconUptime.Size = new System.Drawing.Size(28, 28);
            this.lblIconUptime.TabIndex = 0;
            this.lblIconUptime.Text = "⏱";
            // 
            // lblTitleUptime
            // 
            this.lblTitleUptime.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleUptime.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleUptime.Location = new System.Drawing.Point(40, 8);
            this.lblTitleUptime.Name = "lblTitleUptime";
            this.lblTitleUptime.Size = new System.Drawing.Size(110, 16);
            this.lblTitleUptime.TabIndex = 1;
            this.lblTitleUptime.Text = "Stream Time";
            // 
            // lblChipUptime
            // 
            this.lblChipUptime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipUptime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipUptime.Location = new System.Drawing.Point(8, 38);
            this.lblChipUptime.Name = "lblChipUptime";
            this.lblChipUptime.Size = new System.Drawing.Size(142, 22);
            this.lblChipUptime.TabIndex = 2;
            this.lblChipUptime.Text = "00:00:00";
            // 
            // BroadcastView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardPreview);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cardUrl);
            this.Controls.Add(this.cardChipViewers);
            this.Controls.Add(this.cardChipFps);
            this.Controls.Add(this.cardChipData);
            this.Controls.Add(this.cardChipUptime);
            this.Name = "BroadcastView";
            this.Size = new System.Drawing.Size(652, 504);
            this.cardPreview.ResumeLayout(false);
            this.cardPreview.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cardUrl.ResumeLayout(false);
            this.cardUrl.PerformLayout();
            this.cardChipViewers.ResumeLayout(false);
            this.cardChipFps.ResumeLayout(false);
            this.cardChipData.ResumeLayout(false);
            this.cardChipUptime.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        internal System.Windows.Forms.Panel cardPreview;
        internal ScreenTask.UI.Controls.MonitorPreviewControl monitorPreview;
        internal System.Windows.Forms.Label lblCursorStatus;
        internal System.Windows.Forms.Button btnSwitchSource;

        internal System.Windows.Forms.Panel groupBox3;
        internal System.Windows.Forms.Label lblBroadcastDesc;
        internal System.Windows.Forms.Button btnStartServer;
        internal System.Windows.Forms.Button btnQuickMute;
        internal System.Windows.Forms.Label lblAudioMeterText;
        internal System.Windows.Forms.ProgressBar pbAudioMeter;
        internal System.Windows.Forms.Label lblLiveStats;

        internal System.Windows.Forms.Panel cardUrl;
        internal System.Windows.Forms.Label lblUrlTitle;
        internal System.Windows.Forms.Label lblWifiBadge;
        internal System.Windows.Forms.Label lblPinDisplay;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtURL;
        internal System.Windows.Forms.Button btnCopyUrl;
        internal System.Windows.Forms.Button btnQr;
        internal System.Windows.Forms.Button btnLaunch;

        internal System.Windows.Forms.Panel cardChipViewers;
        internal System.Windows.Forms.Label lblIconViewers;
        internal System.Windows.Forms.Label lblTitleViewers;
        internal System.Windows.Forms.Label lblChipViewers;

        internal System.Windows.Forms.Panel cardChipFps;
        internal System.Windows.Forms.Label lblIconFps;
        internal System.Windows.Forms.Label lblTitleFps;
        internal System.Windows.Forms.Label lblChipFps;

        internal System.Windows.Forms.Panel cardChipData;
        internal System.Windows.Forms.Label lblIconData;
        internal System.Windows.Forms.Label lblTitleData;
        internal System.Windows.Forms.Label lblChipData;

        internal System.Windows.Forms.Panel cardChipUptime;
        internal System.Windows.Forms.Label lblIconUptime;
        internal System.Windows.Forms.Label lblTitleUptime;
        internal System.Windows.Forms.Label lblChipUptime;
    }
}
