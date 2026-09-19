namespace ScreenTask.UI.Views
{
    partial class ConnectedViewersView
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
            this.cardMetricViewers = new System.Windows.Forms.Panel();
            this.lblIconMetricViewers = new System.Windows.Forms.Label();
            this.lblTitleMetricViewers = new System.Windows.Forms.Label();
            this.lblMetricViewersVal = new System.Windows.Forms.Label();
            this.pbViewerCapacity = new System.Windows.Forms.ProgressBar();
            this.cardMetricLatency = new System.Windows.Forms.Panel();
            this.lblIconMetricLatency = new System.Windows.Forms.Label();
            this.lblTitleMetricLatency = new System.Windows.Forms.Label();
            this.lblMetricLatencyVal = new System.Windows.Forms.Label();
            this.lblMetricLatencySub = new System.Windows.Forms.Label();
            this.cardMetricSync = new System.Windows.Forms.Panel();
            this.lblIconMetricSync = new System.Windows.Forms.Label();
            this.lblTitleMetricSync = new System.Windows.Forms.Label();
            this.lblMetricSyncVal = new System.Windows.Forms.Label();
            this.lblMetricSyncSub = new System.Windows.Forms.Label();
            this.cardFleetTerminal = new System.Windows.Forms.Panel();
            this.lblFleetHeader = new System.Windows.Forms.Label();
            this.panelSessionList = new System.Windows.Forms.Panel();
            this.lblNoClientsPlaceholder = new System.Windows.Forms.Label();
            this.lblAnnouncement = new System.Windows.Forms.Label();
            this.txtAnnouncement = new System.Windows.Forms.TextBox();
            this.btnSendAnnouncement = new System.Windows.Forms.Button();
            this.lblAnnouncementStatus = new System.Windows.Forms.Label();
            this.cardPairingPass = new System.Windows.Forms.Panel();
            this.picPairingQr = new System.Windows.Forms.PictureBox();
            this.lblPairingUrlTitle = new System.Windows.Forms.Label();
            this.txtPairingUrl = new System.Windows.Forms.TextBox();
            this.btnPairingCopy = new System.Windows.Forms.Button();
            this.lblPairingPin = new System.Windows.Forms.Label();
            this.btnPairingRegen = new System.Windows.Forms.Button();
            this.lblZeroConf = new System.Windows.Forms.Label();
            this.panelSpotlight = new System.Windows.Forms.Panel();
            this.lblSpotlightTitle = new System.Windows.Forms.Label();
            this.lblSpotlight1 = new System.Windows.Forms.Label();
            this.lblSpotlight2 = new System.Windows.Forms.Label();
            this.lblSpotlight3 = new System.Windows.Forms.Label();
            this.lblSpotlight4 = new System.Windows.Forms.Label();
            this.lblSpotlight5 = new System.Windows.Forms.Label();
            this.cardMetricViewers.SuspendLayout();
            this.cardMetricLatency.SuspendLayout();
            this.cardMetricSync.SuspendLayout();
            this.cardFleetTerminal.SuspendLayout();
            this.panelSessionList.SuspendLayout();
            this.cardPairingPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPairingQr)).BeginInit();
            this.panelSpotlight.SuspendLayout();
            this.SuspendLayout();
            // 
            // cardMetricViewers
            // 
            this.cardMetricViewers.BackColor = System.Drawing.SystemColors.Control;
            this.cardMetricViewers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMetricViewers.Controls.Add(this.lblIconMetricViewers);
            this.cardMetricViewers.Controls.Add(this.lblTitleMetricViewers);
            this.cardMetricViewers.Controls.Add(this.lblMetricViewersVal);
            this.cardMetricViewers.Controls.Add(this.pbViewerCapacity);
            this.cardMetricViewers.Location = new System.Drawing.Point(0, 0);
            this.cardMetricViewers.Name = "cardMetricViewers";
            this.cardMetricViewers.Size = new System.Drawing.Size(212, 72);
            this.cardMetricViewers.TabIndex = 0;
            // 
            // lblIconMetricViewers
            // 
            this.lblIconMetricViewers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconMetricViewers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconMetricViewers.Location = new System.Drawing.Point(8, 8);
            this.lblIconMetricViewers.Name = "lblIconMetricViewers";
            this.lblIconMetricViewers.Size = new System.Drawing.Size(24, 22);
            this.lblIconMetricViewers.TabIndex = 0;
            this.lblIconMetricViewers.Text = "👥";
            // 
            // lblTitleMetricViewers
            // 
            this.lblTitleMetricViewers.AutoSize = true;
            this.lblTitleMetricViewers.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleMetricViewers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleMetricViewers.Location = new System.Drawing.Point(34, 10);
            this.lblTitleMetricViewers.Name = "lblTitleMetricViewers";
            this.lblTitleMetricViewers.Size = new System.Drawing.Size(89, 12);
            this.lblTitleMetricViewers.TabIndex = 1;
            this.lblTitleMetricViewers.Text = "LAN Active Viewers";
            // 
            // lblMetricViewersVal
            // 
            this.lblMetricViewersVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMetricViewersVal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMetricViewersVal.Location = new System.Drawing.Point(8, 30);
            this.lblMetricViewersVal.Name = "lblMetricViewersVal";
            this.lblMetricViewersVal.Size = new System.Drawing.Size(196, 20);
            this.lblMetricViewersVal.TabIndex = 2;
            this.lblMetricViewersVal.Text = "0 / 32 Active";
            // 
            // pbViewerCapacity
            // 
            this.pbViewerCapacity.Location = new System.Drawing.Point(8, 54);
            this.pbViewerCapacity.Maximum = 32;
            this.pbViewerCapacity.Name = "pbViewerCapacity";
            this.pbViewerCapacity.Size = new System.Drawing.Size(196, 8);
            this.pbViewerCapacity.TabIndex = 3;
            this.pbViewerCapacity.Value = 0;
            // 
            // cardMetricLatency
            // 
            this.cardMetricLatency.BackColor = System.Drawing.SystemColors.Control;
            this.cardMetricLatency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMetricLatency.Controls.Add(this.lblIconMetricLatency);
            this.cardMetricLatency.Controls.Add(this.lblTitleMetricLatency);
            this.cardMetricLatency.Controls.Add(this.lblMetricLatencyVal);
            this.cardMetricLatency.Controls.Add(this.lblMetricLatencySub);
            this.cardMetricLatency.Location = new System.Drawing.Point(220, 0);
            this.cardMetricLatency.Name = "cardMetricLatency";
            this.cardMetricLatency.Size = new System.Drawing.Size(212, 72);
            this.cardMetricLatency.TabIndex = 1;
            // 
            // lblIconMetricLatency
            // 
            this.lblIconMetricLatency.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconMetricLatency.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconMetricLatency.Location = new System.Drawing.Point(8, 8);
            this.lblIconMetricLatency.Name = "lblIconMetricLatency";
            this.lblIconMetricLatency.Size = new System.Drawing.Size(24, 22);
            this.lblIconMetricLatency.TabIndex = 0;
            this.lblIconMetricLatency.Text = "⚡";
            // 
            // lblTitleMetricLatency
            // 
            this.lblTitleMetricLatency.AutoSize = true;
            this.lblTitleMetricLatency.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleMetricLatency.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleMetricLatency.Location = new System.Drawing.Point(34, 10);
            this.lblTitleMetricLatency.Name = "lblTitleMetricLatency";
            this.lblTitleMetricLatency.Size = new System.Drawing.Size(90, 12);
            this.lblTitleMetricLatency.TabIndex = 1;
            this.lblTitleMetricLatency.Text = "Fleet Avg RTT Ping";
            // 
            // lblMetricLatencyVal
            // 
            this.lblMetricLatencyVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMetricLatencyVal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMetricLatencyVal.Location = new System.Drawing.Point(8, 30);
            this.lblMetricLatencyVal.Name = "lblMetricLatencyVal";
            this.lblMetricLatencyVal.Size = new System.Drawing.Size(196, 20);
            this.lblMetricLatencyVal.TabIndex = 2;
            this.lblMetricLatencyVal.Text = "<1.0 ms Avg";
            // 
            // lblMetricLatencySub
            // 
            this.lblMetricLatencySub.AutoSize = true;
            this.lblMetricLatencySub.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblMetricLatencySub.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMetricLatencySub.Location = new System.Drawing.Point(8, 52);
            this.lblMetricLatencySub.Name = "lblMetricLatencySub";
            this.lblMetricLatencySub.Size = new System.Drawing.Size(161, 12);
            this.lblMetricLatencySub.TabIndex = 3;
            this.lblMetricLatencySub.Text = "<50ms pipeline • 0.0% frame drop";
            // 
            // cardMetricSync
            // 
            this.cardMetricSync.BackColor = System.Drawing.SystemColors.Control;
            this.cardMetricSync.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMetricSync.Controls.Add(this.lblIconMetricSync);
            this.cardMetricSync.Controls.Add(this.lblTitleMetricSync);
            this.cardMetricSync.Controls.Add(this.lblMetricSyncVal);
            this.cardMetricSync.Controls.Add(this.lblMetricSyncSub);
            this.cardMetricSync.Location = new System.Drawing.Point(440, 0);
            this.cardMetricSync.Name = "cardMetricSync";
            this.cardMetricSync.Size = new System.Drawing.Size(212, 72);
            this.cardMetricSync.TabIndex = 2;
            // 
            // lblIconMetricSync
            // 
            this.lblIconMetricSync.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconMetricSync.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconMetricSync.Location = new System.Drawing.Point(8, 8);
            this.lblIconMetricSync.Name = "lblIconMetricSync";
            this.lblIconMetricSync.Size = new System.Drawing.Size(24, 22);
            this.lblIconMetricSync.TabIndex = 0;
            this.lblIconMetricSync.Text = "🔒";
            // 
            // lblTitleMetricSync
            // 
            this.lblTitleMetricSync.AutoSize = true;
            this.lblTitleMetricSync.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleMetricSync.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleMetricSync.Location = new System.Drawing.Point(34, 10);
            this.lblTitleMetricSync.Name = "lblTitleMetricSync";
            this.lblTitleMetricSync.Size = new System.Drawing.Size(89, 12);
            this.lblTitleMetricSync.TabIndex = 1;
            this.lblTitleMetricSync.Text = "A/V Lip-Sync Lock";
            // 
            // lblMetricSyncVal
            // 
            this.lblMetricSyncVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMetricSyncVal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMetricSyncVal.Location = new System.Drawing.Point(8, 30);
            this.lblMetricSyncVal.Name = "lblMetricSyncVal";
            this.lblMetricSyncVal.Size = new System.Drawing.Size(196, 20);
            this.lblMetricSyncVal.TabIndex = 2;
            this.lblMetricSyncVal.Text = "100% Locked";
            // 
            // lblMetricSyncSub
            // 
            this.lblMetricSyncSub.AutoSize = true;
            this.lblMetricSyncSub.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblMetricSyncSub.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMetricSyncSub.Location = new System.Drawing.Point(8, 52);
            this.lblMetricSyncSub.Name = "lblMetricSyncSub";
            this.lblMetricSyncSub.Size = new System.Drawing.Size(176, 12);
            this.lblMetricSyncSub.TabIndex = 3;
            this.lblMetricSyncSub.Text = "WASAPI + Display (0 drop, ±0.0ms drift)";
            // 
            // cardFleetTerminal
            // 
            this.cardFleetTerminal.BackColor = System.Drawing.SystemColors.Control;
            this.cardFleetTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardFleetTerminal.Controls.Add(this.lblFleetHeader);
            this.cardFleetTerminal.Controls.Add(this.panelSessionList);
            this.cardFleetTerminal.Controls.Add(this.lblAnnouncement);
            this.cardFleetTerminal.Controls.Add(this.txtAnnouncement);
            this.cardFleetTerminal.Controls.Add(this.btnSendAnnouncement);
            this.cardFleetTerminal.Controls.Add(this.lblAnnouncementStatus);
            this.cardFleetTerminal.Location = new System.Drawing.Point(0, 80);
            this.cardFleetTerminal.Name = "cardFleetTerminal";
            this.cardFleetTerminal.Size = new System.Drawing.Size(395, 458);
            this.cardFleetTerminal.TabIndex = 3;
            // 
            // lblFleetHeader
            // 
            this.lblFleetHeader.AutoSize = true;
            this.lblFleetHeader.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFleetHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFleetHeader.Location = new System.Drawing.Point(10, 24);
            this.lblFleetHeader.Name = "lblFleetHeader";
            this.lblFleetHeader.Size = new System.Drawing.Size(142, 13);
            this.lblFleetHeader.TabIndex = 0;
            this.lblFleetHeader.Text = "Live Client Sessions (0):";
            // 
            // panelSessionList
            // 
            this.panelSessionList.AutoScroll = true;
            this.panelSessionList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelSessionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSessionList.Controls.Add(this.lblNoClientsPlaceholder);
            this.panelSessionList.Location = new System.Drawing.Point(10, 42);
            this.panelSessionList.Name = "panelSessionList";
            this.panelSessionList.Size = new System.Drawing.Size(374, 308);
            this.panelSessionList.TabIndex = 1;
            // 
            // lblNoClientsPlaceholder
            // 
            this.lblNoClientsPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNoClientsPlaceholder.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNoClientsPlaceholder.Location = new System.Drawing.Point(10, 80);
            this.lblNoClientsPlaceholder.Name = "lblNoClientsPlaceholder";
            this.lblNoClientsPlaceholder.Size = new System.Drawing.Size(350, 120);
            this.lblNoClientsPlaceholder.TabIndex = 0;
            this.lblNoClientsPlaceholder.Text = "No viewers currently connected.\n\nScan the QR code or open the Stream URL on any phone, tablet, or PC on your Wi-Fi network to start streaming!";
            this.lblNoClientsPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAnnouncement
            // 
            this.lblAnnouncement.AutoSize = true;
            this.lblAnnouncement.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblAnnouncement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAnnouncement.Location = new System.Drawing.Point(10, 368);
            this.lblAnnouncement.Name = "lblAnnouncement";
            this.lblAnnouncement.Size = new System.Drawing.Size(161, 12);
            this.lblAnnouncement.TabIndex = 2;
            this.lblAnnouncement.Text = "Broadcast Announcement Dispatcher:";
            // 
            // txtAnnouncement
            // 
            this.txtAnnouncement.BackColor = System.Drawing.SystemColors.Window;
            this.txtAnnouncement.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtAnnouncement.Location = new System.Drawing.Point(10, 388);
            this.txtAnnouncement.Name = "txtAnnouncement";
            this.txtAnnouncement.PlaceholderText = "Send on-screen broadcast message...";
            this.txtAnnouncement.Size = new System.Drawing.Size(280, 23);
            this.txtAnnouncement.TabIndex = 3;
            // 
            // btnSendAnnouncement
            // 
            this.btnSendAnnouncement.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSendAnnouncement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSendAnnouncement.Location = new System.Drawing.Point(296, 386);
            this.btnSendAnnouncement.Name = "btnSendAnnouncement";
            this.btnSendAnnouncement.Size = new System.Drawing.Size(88, 27);
            this.btnSendAnnouncement.TabIndex = 4;
            this.btnSendAnnouncement.Text = "📢 Send";
            this.btnSendAnnouncement.UseVisualStyleBackColor = true;
            // 
            // lblAnnouncementStatus
            // 
            this.lblAnnouncementStatus.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblAnnouncementStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAnnouncementStatus.Location = new System.Drawing.Point(10, 420);
            this.lblAnnouncementStatus.Name = "lblAnnouncementStatus";
            this.lblAnnouncementStatus.Size = new System.Drawing.Size(374, 28);
            this.lblAnnouncementStatus.TabIndex = 5;
            this.lblAnnouncementStatus.Text = "Announcement displays as an animated toast on all connected viewer screens.";
            // 
            // cardPairingPass
            // 
            this.cardPairingPass.BackColor = System.Drawing.SystemColors.Control;
            this.cardPairingPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPairingPass.Controls.Add(this.picPairingQr);
            this.cardPairingPass.Controls.Add(this.lblPairingUrlTitle);
            this.cardPairingPass.Controls.Add(this.txtPairingUrl);
            this.cardPairingPass.Controls.Add(this.btnPairingCopy);
            this.cardPairingPass.Controls.Add(this.lblPairingPin);
            this.cardPairingPass.Controls.Add(this.btnPairingRegen);
            this.cardPairingPass.Controls.Add(this.lblZeroConf);
            this.cardPairingPass.Controls.Add(this.panelSpotlight);
            this.cardPairingPass.Location = new System.Drawing.Point(403, 80);
            this.cardPairingPass.Name = "cardPairingPass";
            this.cardPairingPass.Size = new System.Drawing.Size(249, 458);
            this.cardPairingPass.TabIndex = 4;
            // 
            // picPairingQr
            // 
            this.picPairingQr.BackColor = System.Drawing.Color.White;
            this.picPairingQr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPairingQr.Location = new System.Drawing.Point(44, 24);
            this.picPairingQr.Name = "picPairingQr";
            this.picPairingQr.Size = new System.Drawing.Size(160, 160);
            this.picPairingQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPairingQr.TabIndex = 0;
            this.picPairingQr.TabStop = false;
            // 
            // lblPairingUrlTitle
            // 
            this.lblPairingUrlTitle.AutoSize = true;
            this.lblPairingUrlTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblPairingUrlTitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPairingUrlTitle.Location = new System.Drawing.Point(10, 188);
            this.lblPairingUrlTitle.Name = "lblPairingUrlTitle";
            this.lblPairingUrlTitle.Size = new System.Drawing.Size(81, 12);
            this.lblPairingUrlTitle.TabIndex = 1;
            this.lblPairingUrlTitle.Text = "Direct Stream URL:";
            // 
            // txtPairingUrl
            // 
            this.txtPairingUrl.BackColor = System.Drawing.SystemColors.Window;
            this.txtPairingUrl.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtPairingUrl.Location = new System.Drawing.Point(10, 204);
            this.txtPairingUrl.Name = "txtPairingUrl";
            this.txtPairingUrl.ReadOnly = true;
            this.txtPairingUrl.Size = new System.Drawing.Size(158, 20);
            this.txtPairingUrl.TabIndex = 2;
            this.txtPairingUrl.Text = "http://127.0.0.1:7070";
            // 
            // btnPairingCopy
            // 
            this.btnPairingCopy.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.btnPairingCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPairingCopy.Location = new System.Drawing.Point(172, 202);
            this.btnPairingCopy.Name = "btnPairingCopy";
            this.btnPairingCopy.Size = new System.Drawing.Size(66, 25);
            this.btnPairingCopy.TabIndex = 3;
            this.btnPairingCopy.Text = "📋 Copy";
            this.btnPairingCopy.UseVisualStyleBackColor = true;
            // 
            // lblPairingPin
            // 
            this.lblPairingPin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPairingPin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPairingPin.Location = new System.Drawing.Point(10, 230);
            this.lblPairingPin.Name = "lblPairingPin";
            this.lblPairingPin.Size = new System.Drawing.Size(140, 24);
            this.lblPairingPin.TabIndex = 4;
            this.lblPairingPin.Text = "PIN Code: None";
            // 
            // btnPairingRegen
            // 
            this.btnPairingRegen.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.btnPairingRegen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPairingRegen.Location = new System.Drawing.Point(152, 229);
            this.btnPairingRegen.Name = "btnPairingRegen";
            this.btnPairingRegen.Size = new System.Drawing.Size(86, 25);
            this.btnPairingRegen.TabIndex = 5;
            this.btnPairingRegen.Text = "🔄 New PIN";
            this.btnPairingRegen.UseVisualStyleBackColor = true;
            // 
            // lblZeroConf
            // 
            this.lblZeroConf.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblZeroConf.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblZeroConf.Location = new System.Drawing.Point(10, 256);
            this.lblZeroConf.Name = "lblZeroConf";
            this.lblZeroConf.Size = new System.Drawing.Size(228, 30);
            this.lblZeroConf.TabIndex = 6;
            this.lblZeroConf.Text = "📡 ZeroConf/mDNS: screentask.local\nAdapter: Loopback";
            // 
            // panelSpotlight
            // 
            this.panelSpotlight.BackColor = System.Drawing.SystemColors.Window;
            this.panelSpotlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSpotlight.Controls.Add(this.lblSpotlightTitle);
            this.panelSpotlight.Controls.Add(this.lblSpotlight1);
            this.panelSpotlight.Controls.Add(this.lblSpotlight2);
            this.panelSpotlight.Controls.Add(this.lblSpotlight3);
            this.panelSpotlight.Controls.Add(this.lblSpotlight4);
            this.panelSpotlight.Controls.Add(this.lblSpotlight5);
            this.panelSpotlight.Location = new System.Drawing.Point(10, 290);
            this.panelSpotlight.Name = "panelSpotlight";
            this.panelSpotlight.Size = new System.Drawing.Size(228, 154);
            this.panelSpotlight.TabIndex = 7;
            // 
            // lblSpotlightTitle
            // 
            this.lblSpotlightTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSpotlightTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSpotlightTitle.Location = new System.Drawing.Point(8, 6);
            this.lblSpotlightTitle.Name = "lblSpotlightTitle";
            this.lblSpotlightTitle.Size = new System.Drawing.Size(210, 16);
            this.lblSpotlightTitle.TabIndex = 0;
            this.lblSpotlightTitle.Text = "✨ Mobile Web Spotlight";
            // 
            // lblSpotlight1
            // 
            this.lblSpotlight1.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSpotlight1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSpotlight1.Location = new System.Drawing.Point(8, 26);
            this.lblSpotlight1.Name = "lblSpotlight1";
            this.lblSpotlight1.Size = new System.Drawing.Size(210, 16);
            this.lblSpotlight1.TabIndex = 1;
            this.lblSpotlight1.Text = "🔍 5x Vector Pinch Zoom & Pan";
            // 
            // lblSpotlight2
            // 
            this.lblSpotlight2.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSpotlight2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSpotlight2.Location = new System.Drawing.Point(8, 48);
            this.lblSpotlight2.Name = "lblSpotlight2";
            this.lblSpotlight2.Size = new System.Drawing.Size(210, 16);
            this.lblSpotlight2.TabIndex = 2;
            this.lblSpotlight2.Text = "🔄 Auto-Fullscreen on Rotation";
            // 
            // lblSpotlight3
            // 
            this.lblSpotlight3.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSpotlight3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSpotlight3.Location = new System.Drawing.Point(8, 70);
            this.lblSpotlight3.Name = "lblSpotlight3";
            this.lblSpotlight3.Size = new System.Drawing.Size(210, 16);
            this.lblSpotlight3.TabIndex = 3;
            this.lblSpotlight3.Text = "👆 Double-Tap Cursor Magnify";
            // 
            // lblSpotlight4
            // 
            this.lblSpotlight4.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSpotlight4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSpotlight4.Location = new System.Drawing.Point(8, 92);
            this.lblSpotlight4.Name = "lblSpotlight4";
            this.lblSpotlight4.Size = new System.Drawing.Size(210, 16);
            this.lblSpotlight4.TabIndex = 4;
            this.lblSpotlight4.Text = "⚡ <50ms Dual-WS Pipeline";
            // 
            // lblSpotlight5
            // 
            this.lblSpotlight5.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSpotlight5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSpotlight5.Location = new System.Drawing.Point(8, 114);
            this.lblSpotlight5.Name = "lblSpotlight5";
            this.lblSpotlight5.Size = new System.Drawing.Size(210, 16);
            this.lblSpotlight5.TabIndex = 5;
            this.lblSpotlight5.Text = "🔒 Zero-Install PWA Pairing";
            // 
            // ConnectedViewersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cardMetricViewers);
            this.Controls.Add(this.cardMetricLatency);
            this.Controls.Add(this.cardMetricSync);
            this.Controls.Add(this.cardFleetTerminal);
            this.Controls.Add(this.cardPairingPass);
            this.Name = "ConnectedViewersView";
            this.Size = new System.Drawing.Size(652, 540);
            this.cardMetricViewers.ResumeLayout(false);
            this.cardMetricViewers.PerformLayout();
            this.cardMetricLatency.ResumeLayout(false);
            this.cardMetricLatency.PerformLayout();
            this.cardMetricSync.ResumeLayout(false);
            this.cardMetricSync.PerformLayout();
            this.cardFleetTerminal.ResumeLayout(false);
            this.cardFleetTerminal.PerformLayout();
            this.panelSessionList.ResumeLayout(false);
            this.cardPairingPass.ResumeLayout(false);
            this.cardPairingPass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPairingQr)).EndInit();
            this.panelSpotlight.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        internal System.Windows.Forms.Panel cardMetricViewers;
        internal System.Windows.Forms.Label lblIconMetricViewers;
        internal System.Windows.Forms.Label lblTitleMetricViewers;
        internal System.Windows.Forms.Label lblMetricViewersVal;
        internal System.Windows.Forms.ProgressBar pbViewerCapacity;

        internal System.Windows.Forms.Panel cardMetricLatency;
        internal System.Windows.Forms.Label lblIconMetricLatency;
        internal System.Windows.Forms.Label lblTitleMetricLatency;
        internal System.Windows.Forms.Label lblMetricLatencyVal;
        internal System.Windows.Forms.Label lblMetricLatencySub;

        internal System.Windows.Forms.Panel cardMetricSync;
        internal System.Windows.Forms.Label lblIconMetricSync;
        internal System.Windows.Forms.Label lblTitleMetricSync;
        internal System.Windows.Forms.Label lblMetricSyncVal;
        internal System.Windows.Forms.Label lblMetricSyncSub;

        internal System.Windows.Forms.Panel cardFleetTerminal;
        internal System.Windows.Forms.Label lblFleetHeader;
        internal System.Windows.Forms.Panel panelSessionList;
        internal System.Windows.Forms.Label lblNoClientsPlaceholder;
        internal System.Windows.Forms.Label lblAnnouncement;
        internal System.Windows.Forms.TextBox txtAnnouncement;
        internal System.Windows.Forms.Button btnSendAnnouncement;
        internal System.Windows.Forms.Label lblAnnouncementStatus;

        internal System.Windows.Forms.Panel cardPairingPass;
        internal System.Windows.Forms.PictureBox picPairingQr;
        internal System.Windows.Forms.Label lblPairingUrlTitle;
        internal System.Windows.Forms.TextBox txtPairingUrl;
        internal System.Windows.Forms.Button btnPairingCopy;
        internal System.Windows.Forms.Label lblPairingPin;
        internal System.Windows.Forms.Button btnPairingRegen;
        internal System.Windows.Forms.Label lblZeroConf;

        internal System.Windows.Forms.Panel panelSpotlight;
        internal System.Windows.Forms.Label lblSpotlightTitle;
        internal System.Windows.Forms.Label lblSpotlight1;
        internal System.Windows.Forms.Label lblSpotlight2;
        internal System.Windows.Forms.Label lblSpotlight3;
        internal System.Windows.Forms.Label lblSpotlight4;
        internal System.Windows.Forms.Label lblSpotlight5;
    }
}
