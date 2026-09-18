namespace ScreenTask
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));

            // Top Header Elements
            this.picAppIcon = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblFluentBadge = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.btnMiniMode = new ScreenTask.UI.Controls.FluentButton();

            // Side Navigation
            this.panelSideNav = new System.Windows.Forms.Panel();
            this.btnNavBroadcast = new ScreenTask.UI.Controls.FluentButton();
            this.btnNavCapture = new ScreenTask.UI.Controls.FluentButton();
            this.btnNavNetwork = new ScreenTask.UI.Controls.FluentButton();
            this.btnNavPreferences = new ScreenTask.UI.Controls.FluentButton();
            this.btnQuickMute = new ScreenTask.UI.Controls.FluentButton();

            // Tab Panels
            this.panelTabBroadcast = new System.Windows.Forms.Panel();
            this.panelTabCapture = new System.Windows.Forms.Panel();
            this.panelTabNetwork = new System.Windows.Forms.Panel();
            this.panelTabPreferences = new System.Windows.Forms.Panel();
            this.panelCompactBar = new System.Windows.Forms.Panel();

            // TAB 1: Broadcast Controls
            this.cardPreview = new ScreenTask.UI.Controls.FluentCard();
            this.monitorPreview = new ScreenTask.UI.Controls.FluentMonitorPreview();
            this.lblCursorStatus = new System.Windows.Forms.Label();
            this.btnSwitchSource = new System.Windows.Forms.Button();

            this.groupBox3 = new ScreenTask.UI.Controls.FluentCard();
            this.lblBroadcastDesc = new System.Windows.Forms.Label();
            this.btnStartServer = new ScreenTask.UI.Controls.FluentButton();
            this.lblAudioMeterText = new System.Windows.Forms.Label();
            this.pbAudioMeter = new ScreenTask.UI.Controls.FluentAudioMeter();
            this.lblLiveStats = new System.Windows.Forms.Label();

            // Row 2: Stream URL Card
            this.cardUrl = new ScreenTask.UI.Controls.FluentCard();
            this.lblUrlTitle = new System.Windows.Forms.Label();
            this.lblWifiBadge = new System.Windows.Forms.Label();
            this.lblPinDisplay = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnCopyUrl = new ScreenTask.UI.Controls.FluentButton();
            this.btnQr = new ScreenTask.UI.Controls.FluentButton();
            this.btnLaunch = new ScreenTask.UI.Controls.FluentButton();

            // Row 3: 4 Metric Cards
            this.cardChipViewers = new ScreenTask.UI.Controls.FluentCard();
            this.lblIconViewers = new System.Windows.Forms.Label();
            this.lblTitleViewers = new System.Windows.Forms.Label();
            this.lblChipViewers = new System.Windows.Forms.Label();

            this.cardChipFps = new ScreenTask.UI.Controls.FluentCard();
            this.lblIconFps = new System.Windows.Forms.Label();
            this.lblTitleFps = new System.Windows.Forms.Label();
            this.lblChipFps = new System.Windows.Forms.Label();

            this.cardChipData = new ScreenTask.UI.Controls.FluentCard();
            this.lblIconData = new System.Windows.Forms.Label();
            this.lblTitleData = new System.Windows.Forms.Label();
            this.lblChipData = new System.Windows.Forms.Label();

            this.cardChipUptime = new ScreenTask.UI.Controls.FluentCard();
            this.lblIconUptime = new System.Windows.Forms.Label();
            this.lblTitleUptime = new System.Windows.Forms.Label();
            this.lblChipUptime = new System.Windows.Forms.Label();

            // TAB 2: Capture & Audio Controls
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

            // TAB 3: Network & Security Controls
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

            // TAB 4: Preferences & Logs Controls
            this.groupBox2 = new ScreenTask.UI.Controls.FluentCard();
            this.cbAutoStart = new ScreenTask.UI.Controls.FluentToggleSwitch();
            this.cbMiniStart = new ScreenTask.UI.Controls.FluentToggleSwitch();
            this.cbCloseToTray = new ScreenTask.UI.Controls.FluentToggleSwitch();

            this.cardLogs = new ScreenTask.UI.Controls.FluentCard();
            this.btnClearLog = new ScreenTask.UI.Controls.FluentButton();
            this.btnCopyLog = new ScreenTask.UI.Controls.FluentButton();
            this.txtLog = new System.Windows.Forms.TextBox();

            // Compact Bar Controls
            this.lblCompactDot = new System.Windows.Forms.Label();
            this.lblCompactStatus = new System.Windows.Forms.Label();
            this.lblCompactUrl = new System.Windows.Forms.Label();
            this.btnCompactCopy = new ScreenTask.UI.Controls.FluentButton();
            this.btnCompactStop = new ScreenTask.UI.Controls.FluentButton();
            this.btnCompactExpand = new ScreenTask.UI.Controls.FluentButton();

            // Footer & Tray
            this.lblMe = new System.Windows.Forms.Label();
            this.appNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);

            this.panelSideNav.SuspendLayout();
            this.panelTabBroadcast.SuspendLayout();
            this.panelTabCapture.SuspendLayout();
            this.panelTabNetwork.SuspendLayout();
            this.panelTabPreferences.SuspendLayout();
            this.panelCompactBar.SuspendLayout();
            this.cardPreview.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cardUrl.SuspendLayout();
            this.cardChipViewers.SuspendLayout();
            this.cardChipFps.SuspendLayout();
            this.cardChipData.SuspendLayout();
            this.cardChipUptime.SuspendLayout();
            this.cardVideoSource.SuspendLayout();
            this.cardAudio.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.cardNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.gbOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cardLogs.SuspendLayout();
            this.SuspendLayout();

            // 
            // picAppIcon
            // 
            this.picAppIcon.BackColor = System.Drawing.Color.Transparent;
            this.picAppIcon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.picAppIcon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.picAppIcon.Location = new System.Drawing.Point(10, 10);
            this.picAppIcon.Name = "picAppIcon";
            this.picAppIcon.Size = new System.Drawing.Size(26, 26);
            this.picAppIcon.Text = "📡";
            this.picAppIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAppTitle.Location = new System.Drawing.Point(38, 12);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(120, 21);
            this.lblAppTitle.Text = "ScreenTask 2.0";

            // 
            // lblFluentBadge
            // 
            this.lblFluentBadge.AutoSize = true;
            this.lblFluentBadge.BackColor = System.Drawing.Color.Transparent;
            this.lblFluentBadge.Font = new System.Drawing.Font("Consolas", 7F, System.Drawing.FontStyle.Bold);
            this.lblFluentBadge.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblFluentBadge.Location = new System.Drawing.Point(38, 34);
            this.lblFluentBadge.Name = "lblFluentBadge";
            this.lblFluentBadge.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblFluentBadge.Size = new System.Drawing.Size(53, 14);
            this.lblFluentBadge.Text = "2.0";
            this.lblFluentBadge.Visible = false;

            // 
            // lblAppSubtitle
            // 
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAppSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAppSubtitle.Location = new System.Drawing.Point(10, 38);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(142, 12);
            this.lblAppSubtitle.Text = "Instant Local Screen Sharing";

            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.BackColor = System.Drawing.SystemColors.Control;
            this.lblStatusBadge.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatusBadge.Location = new System.Drawing.Point(604, 12);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(145, 25);
            this.lblStatusBadge.Text = "● Ready to Stream";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnMiniMode
            // 
            this.btnMiniMode.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnMiniMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMiniMode.Location = new System.Drawing.Point(758, 12);
            this.btnMiniMode.Name = "btnMiniMode";
            this.btnMiniMode.Size = new System.Drawing.Size(90, 25);
            this.btnMiniMode.Text = "🗖 Mini Bar";
            this.btnMiniMode.UseVisualStyleBackColor = true;

            // 
            // panelSideNav
            // 
            this.panelSideNav.BackColor = System.Drawing.SystemColors.Control;
            this.panelSideNav.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSideNav.Controls.Add(this.picAppIcon);
            this.panelSideNav.Controls.Add(this.lblAppTitle);
            this.panelSideNav.Controls.Add(this.lblFluentBadge);
            this.panelSideNav.Controls.Add(this.lblAppSubtitle);
            this.panelSideNav.Controls.Add(this.btnNavBroadcast);
            this.panelSideNav.Controls.Add(this.btnNavCapture);
            this.panelSideNav.Controls.Add(this.btnNavNetwork);
            this.panelSideNav.Controls.Add(this.btnNavPreferences);
            this.panelSideNav.Controls.Add(this.btnQuickMute);
            this.panelSideNav.Location = new System.Drawing.Point(12, 12);
            this.panelSideNav.Name = "panelSideNav";
            this.panelSideNav.Size = new System.Drawing.Size(174, 574);

            // 
            // btnNavBroadcast
            // 
            this.btnNavBroadcast.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNavBroadcast.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavBroadcast.Location = new System.Drawing.Point(8, 66);
            this.btnNavBroadcast.Name = "btnNavBroadcast";
            this.btnNavBroadcast.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnNavBroadcast.Size = new System.Drawing.Size(156, 38);
            this.btnNavBroadcast.Text = "📡  Broadcast";
            this.btnNavBroadcast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBroadcast.UseVisualStyleBackColor = true;

            // 
            // btnNavCapture
            // 
            this.btnNavCapture.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavCapture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavCapture.Location = new System.Drawing.Point(8, 110);
            this.btnNavCapture.Name = "btnNavCapture";
            this.btnNavCapture.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnNavCapture.Size = new System.Drawing.Size(156, 38);
            this.btnNavCapture.Text = "🎥  Screen & Audio";
            this.btnNavCapture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCapture.UseVisualStyleBackColor = true;

            // 
            // btnNavNetwork
            // 
            this.btnNavNetwork.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavNetwork.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavNetwork.Location = new System.Drawing.Point(8, 154);
            this.btnNavNetwork.Name = "btnNavNetwork";
            this.btnNavNetwork.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnNavNetwork.Size = new System.Drawing.Size(156, 38);
            this.btnNavNetwork.Text = "🛡️  Network & PIN";
            this.btnNavNetwork.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNetwork.UseVisualStyleBackColor = true;

            // 
            // btnNavPreferences
            // 
            this.btnNavPreferences.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNavPreferences.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavPreferences.Location = new System.Drawing.Point(8, 198);
            this.btnNavPreferences.Name = "btnNavPreferences";
            this.btnNavPreferences.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnNavPreferences.Size = new System.Drawing.Size(156, 38);
            this.btnNavPreferences.Text = "⚙️  Settings & Logs";
            this.btnNavPreferences.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPreferences.UseVisualStyleBackColor = true;

            // 
            // btnQuickMute
            // 
            this.btnQuickMute.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnQuickMute.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickMute.Location = new System.Drawing.Point(8, 526);
            this.btnQuickMute.Name = "btnQuickMute";
            this.btnQuickMute.Size = new System.Drawing.Size(156, 36);
            this.btnQuickMute.Text = "🔇 Audio Off";
            this.btnQuickMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnQuickMute.UseVisualStyleBackColor = true;

            // 
            // panelTabBroadcast
            // 
            this.panelTabBroadcast.Controls.Add(this.cardPreview);
            this.panelTabBroadcast.Controls.Add(this.groupBox3);
            this.panelTabBroadcast.Controls.Add(this.cardUrl);
            this.panelTabBroadcast.Controls.Add(this.cardChipViewers);
            this.panelTabBroadcast.Controls.Add(this.cardChipFps);
            this.panelTabBroadcast.Controls.Add(this.cardChipData);
            this.panelTabBroadcast.Controls.Add(this.cardChipUptime);
            this.panelTabBroadcast.Location = new System.Drawing.Point(196, 44);
            this.panelTabBroadcast.Name = "panelTabBroadcast";
            this.panelTabBroadcast.Size = new System.Drawing.Size(652, 504);

            // 
            // cardPreview (Left Column: Live Monitor Preview)
            // 
            this.cardPreview.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardPreview.Controls.Add(this.monitorPreview);
            this.cardPreview.Controls.Add(this.lblCursorStatus);
            this.cardPreview.Controls.Add(this.btnSwitchSource);
            this.cardPreview.Location = new System.Drawing.Point(0, 0);
            this.cardPreview.Name = "cardPreview";
            this.cardPreview.Size = new System.Drawing.Size(384, 222);
            this.cardPreview.Title = "Live Screen Preview";

            // 
            // monitorPreview
            // 
            this.monitorPreview.AudioInfoText = "Audio: Active";
            this.monitorPreview.BackColor = System.Drawing.Color.Black;
            this.monitorPreview.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.monitorPreview.FpsText = "30.0 FPS";
            this.monitorPreview.IsLive = false;
            this.monitorPreview.Location = new System.Drawing.Point(12, 32);
            this.monitorPreview.Name = "monitorPreview";
            this.monitorPreview.ResolutionText = "1920 × 1080";
            this.monitorPreview.Size = new System.Drawing.Size(360, 155);
            this.monitorPreview.SourceTitle = "Display 1 (Primary)";

            // 
            // lblCursorStatus
            // 
            this.lblCursorStatus.AutoSize = true;
            this.lblCursorStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblCursorStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCursorStatus.Location = new System.Drawing.Point(14, 196);
            this.lblCursorStatus.Name = "lblCursorStatus";
            this.lblCursorStatus.Size = new System.Drawing.Size(126, 12);
            this.lblCursorStatus.Text = "Mouse Pointer: Visible";

            // 
            // btnSwitchSource
            // 
            this.btnSwitchSource.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnSwitchSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSwitchSource.Location = new System.Drawing.Point(240, 190);
            this.btnSwitchSource.Name = "btnSwitchSource";
            this.btnSwitchSource.Size = new System.Drawing.Size(132, 24);
            this.btnSwitchSource.Text = "Change Source →";
            this.btnSwitchSource.UseVisualStyleBackColor = true;

            // 
            // groupBox3 (Right Column: Broadcast Control Card)
            // 
            this.groupBox3.CardBackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.lblBroadcastDesc);
            this.groupBox3.Controls.Add(this.btnStartServer);
            this.groupBox3.Controls.Add(this.lblAudioMeterText);
            this.groupBox3.Controls.Add(this.pbAudioMeter);
            this.groupBox3.Controls.Add(this.lblLiveStats);
            this.groupBox3.Location = new System.Drawing.Point(394, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 222);
            this.groupBox3.Title = "Broadcast Controls";

            // 
            // lblBroadcastDesc
            // 
            this.lblBroadcastDesc.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblBroadcastDesc.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblBroadcastDesc.Location = new System.Drawing.Point(12, 32);
            this.lblBroadcastDesc.Name = "lblBroadcastDesc";
            this.lblBroadcastDesc.Size = new System.Drawing.Size(234, 30);
            this.lblBroadcastDesc.Text = "Share your screen with any phone, tablet, or browser on your local network.";

            // 
            // btnStartServer
            // 
            this.btnStartServer.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnStartServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartServer.Location = new System.Drawing.Point(12, 66);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(234, 46);
            this.btnStartServer.Text = "▶  START BROADCAST";
            this.btnStartServer.UseVisualStyleBackColor = true;

            // 
            // lblAudioMeterText
            // 
            this.lblAudioMeterText.AutoSize = true;
            this.lblAudioMeterText.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioMeterText.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioMeterText.Location = new System.Drawing.Point(12, 122);
            this.lblAudioMeterText.Name = "lblAudioMeterText";
            this.lblAudioMeterText.Size = new System.Drawing.Size(95, 12);
            this.lblAudioMeterText.Text = "Sound Volume Level:";

            // 
            // pbAudioMeter
            // 
            this.pbAudioMeter.Enabled = false;
            this.pbAudioMeter.Location = new System.Drawing.Point(12, 140);
            this.pbAudioMeter.Name = "pbAudioMeter";
            this.pbAudioMeter.Size = new System.Drawing.Size(234, 12);
            this.pbAudioMeter.Value = 0;

            // 
            // lblLiveStats
            // 
            this.lblLiveStats.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblLiveStats.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLiveStats.Location = new System.Drawing.Point(12, 162);
            this.lblLiveStats.Name = "lblLiveStats";
            this.lblLiveStats.Size = new System.Drawing.Size(234, 50);
            this.lblLiveStats.Text = "⚡ Stopped • Ready to broadcast";
            this.lblLiveStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // cardUrl (Middle Row: Stream URL Card)
            // 
            this.cardUrl.CardBackColor = System.Drawing.SystemColors.Control;
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
            this.cardUrl.Title = "Stream Web Address";

            // 
            // lblUrlTitle
            // 
            this.lblUrlTitle.AutoSize = true;
            this.lblUrlTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUrlTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUrlTitle.Location = new System.Drawing.Point(12, 10);
            this.lblUrlTitle.Name = "lblUrlTitle";
            this.lblUrlTitle.Size = new System.Drawing.Size(120, 15);
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
            this.lblWifiBadge.Text = "Local Network (Zero Config)";

            // 
            // lblPinDisplay
            // 
            this.lblPinDisplay.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPinDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPinDisplay.Location = new System.Drawing.Point(470, 10);
            this.lblPinDisplay.Name = "lblPinDisplay";
            this.lblPinDisplay.Size = new System.Drawing.Size(170, 16);
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
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.Text = "Link:";

            // 
            // txtURL
            // 
            this.txtURL.BackColor = System.Drawing.SystemColors.Window;
            this.txtURL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtURL.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtURL.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtURL.Location = new System.Drawing.Point(48, 40);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(315, 22);
            this.txtURL.Text = "http://127.0.0.1:7070";

            // 
            // btnCopyUrl
            // 
            this.btnCopyUrl.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyUrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCopyUrl.Location = new System.Drawing.Point(372, 38);
            this.btnCopyUrl.Name = "btnCopyUrl";
            this.btnCopyUrl.Size = new System.Drawing.Size(85, 26);
            this.btnCopyUrl.Text = "📋 Copy Link";
            this.btnCopyUrl.UseVisualStyleBackColor = true;

            // 
            // btnQr
            // 
            this.btnQr.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnQr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQr.Location = new System.Drawing.Point(463, 38);
            this.btnQr.Name = "btnQr";
            this.btnQr.Size = new System.Drawing.Size(80, 26);
            this.btnQr.Text = "📱 Show QR";
            this.btnQr.UseVisualStyleBackColor = true;

            // 
            // btnLaunch
            // 
            this.btnLaunch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLaunch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLaunch.Location = new System.Drawing.Point(549, 38);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(92, 26);
            this.btnLaunch.Text = "🌐 Open Browser";
            this.btnLaunch.UseVisualStyleBackColor = true;

            // 
            // cardChipViewers
            // 
            this.cardChipViewers.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardChipViewers.Controls.Add(this.lblIconViewers);
            this.cardChipViewers.Controls.Add(this.lblTitleViewers);
            this.cardChipViewers.Controls.Add(this.lblChipViewers);
            this.cardChipViewers.Location = new System.Drawing.Point(0, 330);
            this.cardChipViewers.Name = "cardChipViewers";
            this.cardChipViewers.Size = new System.Drawing.Size(155, 75);

            // 
            // lblIconViewers
            // 
            this.lblIconViewers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconViewers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconViewers.Location = new System.Drawing.Point(10, 10);
            this.lblIconViewers.Name = "lblIconViewers";
            this.lblIconViewers.Size = new System.Drawing.Size(26, 24);
            this.lblIconViewers.Text = "👥";

            // 
            // lblTitleViewers
            // 
            this.lblTitleViewers.AutoSize = true;
            this.lblTitleViewers.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleViewers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleViewers.Location = new System.Drawing.Point(40, 12);
            this.lblTitleViewers.Name = "lblTitleViewers";
            this.lblTitleViewers.Size = new System.Drawing.Size(95, 12);
            this.lblTitleViewers.Text = "Connected Viewers";

            // 
            // lblChipViewers
            // 
            this.lblChipViewers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChipViewers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipViewers.Location = new System.Drawing.Point(10, 40);
            this.lblChipViewers.Name = "lblChipViewers";
            this.lblChipViewers.Size = new System.Drawing.Size(135, 24);
            this.lblChipViewers.Text = "0 Connected";

            // 
            // cardChipFps
            // 
            this.cardChipFps.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardChipFps.Controls.Add(this.lblIconFps);
            this.cardChipFps.Controls.Add(this.lblTitleFps);
            this.cardChipFps.Controls.Add(this.lblChipFps);
            this.cardChipFps.Location = new System.Drawing.Point(165, 330);
            this.cardChipFps.Name = "cardChipFps";
            this.cardChipFps.Size = new System.Drawing.Size(155, 75);

            // 
            // lblIconFps
            // 
            this.lblIconFps.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconFps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconFps.Location = new System.Drawing.Point(10, 10);
            this.lblIconFps.Name = "lblIconFps";
            this.lblIconFps.Size = new System.Drawing.Size(26, 24);
            this.lblIconFps.Text = "⚡";

            // 
            // lblTitleFps
            // 
            this.lblTitleFps.AutoSize = true;
            this.lblTitleFps.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleFps.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleFps.Location = new System.Drawing.Point(40, 12);
            this.lblTitleFps.Name = "lblTitleFps";
            this.lblTitleFps.Size = new System.Drawing.Size(89, 12);
            this.lblTitleFps.Text = "Smoothness (FPS)";

            // 
            // lblChipFps
            // 
            this.lblChipFps.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChipFps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipFps.Location = new System.Drawing.Point(10, 40);
            this.lblChipFps.Name = "lblChipFps";
            this.lblChipFps.Size = new System.Drawing.Size(135, 24);
            this.lblChipFps.Text = "0.0 FPS";

            // 
            // cardChipData
            // 
            this.cardChipData.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardChipData.Controls.Add(this.lblIconData);
            this.cardChipData.Controls.Add(this.lblTitleData);
            this.cardChipData.Controls.Add(this.lblChipData);
            this.cardChipData.Location = new System.Drawing.Point(330, 330);
            this.cardChipData.Name = "cardChipData";
            this.cardChipData.Size = new System.Drawing.Size(155, 75);

            // 
            // lblIconData
            // 
            this.lblIconData.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconData.Location = new System.Drawing.Point(10, 10);
            this.lblIconData.Name = "lblIconData";
            this.lblIconData.Size = new System.Drawing.Size(26, 24);
            this.lblIconData.Text = "📶";

            // 
            // lblTitleData
            // 
            this.lblTitleData.AutoSize = true;
            this.lblTitleData.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleData.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleData.Location = new System.Drawing.Point(40, 12);
            this.lblTitleData.Name = "lblTitleData";
            this.lblTitleData.Size = new System.Drawing.Size(84, 12);
            this.lblTitleData.Text = "Data Transferred";

            // 
            // lblChipData
            // 
            this.lblChipData.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChipData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipData.Location = new System.Drawing.Point(10, 40);
            this.lblChipData.Name = "lblChipData";
            this.lblChipData.Size = new System.Drawing.Size(135, 24);
            this.lblChipData.Text = "0.0 MB";

            // 
            // cardChipUptime
            // 
            this.cardChipUptime.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardChipUptime.Controls.Add(this.lblIconUptime);
            this.cardChipUptime.Controls.Add(this.lblTitleUptime);
            this.cardChipUptime.Controls.Add(this.lblChipUptime);
            this.cardChipUptime.Location = new System.Drawing.Point(495, 330);
            this.cardChipUptime.Name = "cardChipUptime";
            this.cardChipUptime.Size = new System.Drawing.Size(157, 75);

            // 
            // lblIconUptime
            // 
            this.lblIconUptime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconUptime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblIconUptime.Location = new System.Drawing.Point(10, 10);
            this.lblIconUptime.Name = "lblIconUptime";
            this.lblIconUptime.Size = new System.Drawing.Size(26, 24);
            this.lblIconUptime.Text = "⏱";

            // 
            // lblTitleUptime
            // 
            this.lblTitleUptime.AutoSize = true;
            this.lblTitleUptime.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleUptime.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleUptime.Location = new System.Drawing.Point(40, 12);
            this.lblTitleUptime.Name = "lblTitleUptime";
            this.lblTitleUptime.Size = new System.Drawing.Size(64, 12);
            this.lblTitleUptime.Text = "Stream Time";

            // 
            // lblChipUptime
            // 
            this.lblChipUptime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblChipUptime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipUptime.Location = new System.Drawing.Point(10, 40);
            this.lblChipUptime.Name = "lblChipUptime";
            this.lblChipUptime.Size = new System.Drawing.Size(135, 24);
            this.lblChipUptime.Text = "00:00:00";

            // 
            // panelTabCapture
            // 
            this.panelTabCapture.Controls.Add(this.cardVideoSource);
            this.panelTabCapture.Controls.Add(this.cardAudio);
            this.panelTabCapture.Controls.Add(this.groupBox1);
            this.panelTabCapture.Location = new System.Drawing.Point(196, 44);
            this.panelTabCapture.Name = "panelTabCapture";
            this.panelTabCapture.Size = new System.Drawing.Size(652, 504);
            this.panelTabCapture.Visible = false;

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
            this.label7.Text = "Share Display / Window:";

            // 
            // comboScreens
            // 
            this.comboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboScreens.Location = new System.Drawing.Point(165, 33);
            this.comboScreens.Name = "comboScreens";
            this.comboScreens.Size = new System.Drawing.Size(370, 24);

            // 
            // btnRefreshScreens
            // 
            this.btnRefreshScreens.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshScreens.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshScreens.Location = new System.Drawing.Point(545, 32);
            this.btnRefreshScreens.Name = "btnRefreshScreens";
            this.btnRefreshScreens.Size = new System.Drawing.Size(90, 26);
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
            this.lblAudioPill.Text = "High Quality Audio (48 kHz)";

            // 
            // cbAudio
            // 
            this.cbAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAudio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbAudio.Location = new System.Drawing.Point(16, 35);
            this.cbAudio.Name = "cbAudio";
            this.cbAudio.Size = new System.Drawing.Size(450, 24);
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

            // 
            // btnRefreshAudio
            // 
            this.btnRefreshAudio.Enabled = false;
            this.btnRefreshAudio.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshAudio.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshAudio.Location = new System.Drawing.Point(540, 69);
            this.btnRefreshAudio.Name = "btnRefreshAudio";
            this.btnRefreshAudio.Size = new System.Drawing.Size(95, 26);
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
            this.btnAudioDiag.Text = "⚙ Audio Device Setup & Test";
            this.btnAudioDiag.UseVisualStyleBackColor = true;

            // 
            // groupBox1 (Compression & Framerate Card)
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
            this.lblQuality.Text = "Image Quality: 80% (High Detail)";

            // 
            // qualitySlider
            // 
            this.qualitySlider.Location = new System.Drawing.Point(14, 128);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 20;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(620, 45);
            this.qualitySlider.TickFrequency = 10;
            this.qualitySlider.Value = 80;

            // 
            // panelTabNetwork
            // 
            this.panelTabNetwork.Controls.Add(this.cardNetwork);
            this.panelTabNetwork.Controls.Add(this.gbOptions);
            this.panelTabNetwork.Location = new System.Drawing.Point(196, 44);
            this.panelTabNetwork.Name = "panelTabNetwork";
            this.panelTabNetwork.Size = new System.Drawing.Size(652, 504);
            this.panelTabNetwork.Visible = false;

            // 
            // cardNetwork
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
            this.label5.Text = "Network Connection (IP Address):";

            // 
            // comboIPs
            // 
            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPs.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboIPs.Location = new System.Drawing.Point(16, 58);
            this.comboIPs.Name = "comboIPs";
            this.comboIPs.Size = new System.Drawing.Size(515, 24);

            // 
            // btnRefreshIPs
            // 
            this.btnRefreshIPs.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefreshIPs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefreshIPs.Location = new System.Drawing.Point(540, 57);
            this.btnRefreshIPs.Name = "btnRefreshIPs";
            this.btnRefreshIPs.Size = new System.Drawing.Size(95, 26);
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
            this.numPort.Value = new decimal(new int[] { 7070, 0, 0, 0 });

            // 
            // lblPortStatus
            // 
            this.lblPortStatus.AutoSize = true;
            this.lblPortStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPortStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPortStatus.Location = new System.Drawing.Point(155, 131);
            this.lblPortStatus.Name = "lblPortStatus";
            this.lblPortStatus.Size = new System.Drawing.Size(206, 15);
            this.lblPortStatus.Text = "✓ Port 7070 is available for streaming";

            // 
            // gbOptions (Security Card)
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
            this.gbOptions.Title = "Access Security & PIN";

            // 
            // cbPrivate
            // 
            this.cbPrivate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbPrivate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbPrivate.Location = new System.Drawing.Point(16, 35);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(500, 24);
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
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // btnGenPin
            // 
            this.btnGenPin.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnGenPin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnGenPin.Location = new System.Drawing.Point(190, 101);
            this.btnGenPin.Name = "btnGenPin";
            this.btnGenPin.Size = new System.Drawing.Size(130, 28);
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
            this.btnCopyPin.Text = "📋 Copy PIN";
            this.btnCopyPin.UseVisualStyleBackColor = true;

            // 
            // panelTabPreferences
            // 
            this.panelTabPreferences.Controls.Add(this.groupBox2);
            this.panelTabPreferences.Controls.Add(this.cardLogs);
            this.panelTabPreferences.Location = new System.Drawing.Point(196, 44);
            this.panelTabPreferences.Name = "panelTabPreferences";
            this.panelTabPreferences.Size = new System.Drawing.Size(652, 504);
            this.panelTabPreferences.Visible = false;

            // 
            // groupBox2 (Preferences Card)
            // 
            this.groupBox2.CardBackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.cbAutoStart);
            this.groupBox2.Controls.Add(this.cbMiniStart);
            this.groupBox2.Controls.Add(this.cbCloseToTray);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(652, 140);
            this.groupBox2.Title = "Startup & Window Preferences";

            // 
            // cbAutoStart
            // 
            this.cbAutoStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAutoStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbAutoStart.Location = new System.Drawing.Point(16, 35);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(500, 24);
            this.cbAutoStart.Text = "Start broadcast automatically when ScreenTask opens";
            this.cbAutoStart.UseVisualStyleBackColor = true;

            // 
            // cbMiniStart
            // 
            this.cbMiniStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbMiniStart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbMiniStart.Location = new System.Drawing.Point(16, 68);
            this.cbMiniStart.Name = "cbMiniStart";
            this.cbMiniStart.Size = new System.Drawing.Size(500, 24);
            this.cbMiniStart.Text = "Start minimized to system tray";
            this.cbMiniStart.UseVisualStyleBackColor = true;

            // 
            // cbCloseToTray
            // 
            this.cbCloseToTray.Checked = true;
            this.cbCloseToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCloseToTray.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbCloseToTray.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbCloseToTray.Location = new System.Drawing.Point(16, 101);
            this.cbCloseToTray.Name = "cbCloseToTray";
            this.cbCloseToTray.Size = new System.Drawing.Size(500, 24);
            this.cbCloseToTray.Text = "Minimize to system tray when closing window";
            this.cbCloseToTray.UseVisualStyleBackColor = true;

            // 
            // cardLogs (Diagnostic Logs Card)
            // 
            this.cardLogs.CardBackColor = System.Drawing.SystemColors.Control;
            this.cardLogs.Controls.Add(this.btnClearLog);
            this.cardLogs.Controls.Add(this.btnCopyLog);
            this.cardLogs.Controls.Add(this.txtLog);
            this.cardLogs.Location = new System.Drawing.Point(0, 150);
            this.cardLogs.Name = "cardLogs";
            this.cardLogs.Size = new System.Drawing.Size(652, 340);
            this.cardLogs.Title = "Activity & Connection Log";

            // 
            // btnClearLog
            // 
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnClearLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClearLog.Location = new System.Drawing.Point(460, 8);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(80, 24);
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

            // 
            // panelCompactBar
            // 
            this.panelCompactBar.BackColor = System.Drawing.SystemColors.Control;
            this.panelCompactBar.Controls.Add(this.lblCompactDot);
            this.panelCompactBar.Controls.Add(this.lblCompactStatus);
            this.panelCompactBar.Controls.Add(this.lblCompactUrl);
            this.panelCompactBar.Controls.Add(this.btnCompactCopy);
            this.panelCompactBar.Controls.Add(this.btnCompactStop);
            this.panelCompactBar.Controls.Add(this.btnCompactExpand);
            this.panelCompactBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCompactBar.Location = new System.Drawing.Point(0, 0);
            this.panelCompactBar.Name = "panelCompactBar";
            this.panelCompactBar.Size = new System.Drawing.Size(860, 68);
            this.panelCompactBar.Visible = false;

            // 
            // lblCompactDot
            // 
            this.lblCompactDot.AutoSize = true;
            this.lblCompactDot.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCompactDot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCompactDot.Location = new System.Drawing.Point(12, 18);
            this.lblCompactDot.Name = "lblCompactDot";
            this.lblCompactDot.Size = new System.Drawing.Size(26, 25);
            this.lblCompactDot.Text = "●";

            // 
            // lblCompactStatus
            // 
            this.lblCompactStatus.AutoSize = true;
            this.lblCompactStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompactStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCompactStatus.Location = new System.Drawing.Point(38, 14);
            this.lblCompactStatus.Name = "lblCompactStatus";
            this.lblCompactStatus.Size = new System.Drawing.Size(126, 15);
            this.lblCompactStatus.Text = "● Ready to Stream";

            // 
            // lblCompactUrl
            // 
            this.lblCompactUrl.AutoSize = true;
            this.lblCompactUrl.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblCompactUrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCompactUrl.Location = new System.Drawing.Point(38, 36);
            this.lblCompactUrl.Name = "lblCompactUrl";
            this.lblCompactUrl.Size = new System.Drawing.Size(145, 13);
            this.lblCompactUrl.Text = "http://127.0.0.1:7070";

            // 
            // btnCompactCopy
            // 
            this.btnCompactCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompactCopy.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCompactCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompactCopy.Location = new System.Drawing.Point(395, 18);
            this.btnCompactCopy.Name = "btnCompactCopy";
            this.btnCompactCopy.Size = new System.Drawing.Size(85, 32);
            this.btnCompactCopy.Text = "📋 Copy Link";
            this.btnCompactCopy.UseVisualStyleBackColor = true;

            // 
            // btnCompactStop
            // 
            this.btnCompactStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompactStop.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCompactStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompactStop.Location = new System.Drawing.Point(490, 18);
            this.btnCompactStop.Name = "btnCompactStop";
            this.btnCompactStop.Size = new System.Drawing.Size(80, 32);
            this.btnCompactStop.Text = "Start";
            this.btnCompactStop.UseVisualStyleBackColor = true;

            // 
            // btnCompactExpand
            // 
            this.btnCompactExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompactExpand.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCompactExpand.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompactExpand.Location = new System.Drawing.Point(580, 18);
            this.btnCompactExpand.Name = "btnCompactExpand";
            this.btnCompactExpand.Size = new System.Drawing.Size(88, 32);
            this.btnCompactExpand.Text = "🗖 Full View";
            this.btnCompactExpand.UseVisualStyleBackColor = true;

            // 
            // lblMe
            // 
            this.lblMe.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblMe.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMe.Location = new System.Drawing.Point(196, 594);
            this.lblMe.Name = "lblMe";
            this.lblMe.Size = new System.Drawing.Size(652, 18);
            this.lblMe.Text = "ScreenTask 2.0 • Free & Open Source under GPLv3 • © Eslam Hamouda 2014 - 2026 • screentask.eslamx.com";
            this.lblMe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // appNotify
            // 
            this.appNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.appNotify.ContextMenuStrip = this.trayContextMenu;
            this.appNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("appNotify.Icon")));
            this.appNotify.Text = "ScreenTask 2.0";
            this.appNotify.Visible = true;

            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(860, 620);
            this.Controls.Add(this.panelCompactBar);
            this.Controls.Add(this.lblMe);
            this.Controls.Add(this.panelTabPreferences);
            this.Controls.Add(this.panelTabNetwork);
            this.Controls.Add(this.panelTabCapture);
            this.Controls.Add(this.panelTabBroadcast);
            this.Controls.Add(this.panelSideNav);
            this.Controls.Add(this.btnMiniMode);
            this.Controls.Add(this.lblStatusBadge);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenTask 2.0 – Offline & LAN Screen Sharing";

            // Tooltips
            this.toolTipMain.SetToolTip(this.btnRefreshScreens, "Refresh screens and open application windows");
            this.toolTipMain.SetToolTip(this.btnRefreshIPs, "Refresh available network connections");
            this.toolTipMain.SetToolTip(this.btnRefreshAudio, "Refresh audio devices");
            this.toolTipMain.SetToolTip(this.btnTestAudio, "Play test sound through speakers");
            this.toolTipMain.SetToolTip(this.btnAudioDiag, "Open audio setup and microphone testing");
            this.toolTipMain.SetToolTip(this.btnCopyUrl, "Copy stream address to clipboard");
            this.toolTipMain.SetToolTip(this.btnLaunch, "Open stream in web browser");
            this.toolTipMain.SetToolTip(this.btnQr, "Show QR code for phone or tablet");
            this.toolTipMain.SetToolTip(this.btnMiniMode, "Switch to compact mini bar");
            this.toolTipMain.SetToolTip(this.btnQuickMute, "Mute or unmute audio");

            this.panelSideNav.ResumeLayout(false);
            this.panelSideNav.PerformLayout();
            this.panelTabBroadcast.ResumeLayout(false);
            this.panelTabCapture.ResumeLayout(false);
            this.panelTabNetwork.ResumeLayout(false);
            this.panelTabPreferences.ResumeLayout(false);
            this.panelCompactBar.ResumeLayout(false);
            this.panelCompactBar.PerformLayout();
            this.cardPreview.ResumeLayout(false);
            this.cardPreview.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cardUrl.ResumeLayout(false);
            this.cardUrl.PerformLayout();
            this.cardChipViewers.ResumeLayout(false);
            this.cardChipViewers.PerformLayout();
            this.cardChipFps.ResumeLayout(false);
            this.cardChipFps.PerformLayout();
            this.cardChipData.ResumeLayout(false);
            this.cardChipData.PerformLayout();
            this.cardChipUptime.ResumeLayout(false);
            this.cardChipUptime.PerformLayout();
            this.cardVideoSource.ResumeLayout(false);
            this.cardVideoSource.PerformLayout();
            this.cardAudio.ResumeLayout(false);
            this.cardAudio.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.cardNetwork.ResumeLayout(false);
            this.cardNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.cardLogs.ResumeLayout(false);
            this.cardLogs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Header controls
        private System.Windows.Forms.Label picAppIcon;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblFluentBadge;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Label lblStatusBadge;
        private ScreenTask.UI.Controls.FluentButton btnMiniMode;

        // Side Navigation
        private System.Windows.Forms.Panel panelSideNav;
        private ScreenTask.UI.Controls.FluentButton btnNavBroadcast;
        private ScreenTask.UI.Controls.FluentButton btnNavCapture;
        private ScreenTask.UI.Controls.FluentButton btnNavNetwork;
        private ScreenTask.UI.Controls.FluentButton btnNavPreferences;
        private ScreenTask.UI.Controls.FluentButton btnQuickMute;

        // Tab Panels
        private System.Windows.Forms.Panel panelTabBroadcast;
        private System.Windows.Forms.Panel panelTabCapture;
        private System.Windows.Forms.Panel panelTabNetwork;
        private System.Windows.Forms.Panel panelTabPreferences;
        private System.Windows.Forms.Panel panelCompactBar;

        // Tab 1: Broadcast Controls
        private ScreenTask.UI.Controls.FluentCard cardPreview;
        private ScreenTask.UI.Controls.FluentMonitorPreview monitorPreview;
        private System.Windows.Forms.Label lblCursorStatus;
        private System.Windows.Forms.Button btnSwitchSource;

        private ScreenTask.UI.Controls.FluentCard groupBox3;
        private System.Windows.Forms.Label lblBroadcastDesc;
        private ScreenTask.UI.Controls.FluentButton btnStartServer;
        private System.Windows.Forms.Label lblAudioMeterText;
        private ScreenTask.UI.Controls.FluentAudioMeter pbAudioMeter;
        private System.Windows.Forms.Label lblLiveStats;

        private ScreenTask.UI.Controls.FluentCard cardUrl;
        private System.Windows.Forms.Label lblUrlTitle;
        private System.Windows.Forms.Label lblWifiBadge;
        private System.Windows.Forms.Label lblPinDisplay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURL;
        private ScreenTask.UI.Controls.FluentButton btnCopyUrl;
        private ScreenTask.UI.Controls.FluentButton btnQr;
        private ScreenTask.UI.Controls.FluentButton btnLaunch;

        private ScreenTask.UI.Controls.FluentCard cardChipViewers;
        private System.Windows.Forms.Label lblIconViewers;
        private System.Windows.Forms.Label lblTitleViewers;
        private System.Windows.Forms.Label lblChipViewers;

        private ScreenTask.UI.Controls.FluentCard cardChipFps;
        private System.Windows.Forms.Label lblIconFps;
        private System.Windows.Forms.Label lblTitleFps;
        private System.Windows.Forms.Label lblChipFps;

        private ScreenTask.UI.Controls.FluentCard cardChipData;
        private System.Windows.Forms.Label lblIconData;
        private System.Windows.Forms.Label lblTitleData;
        private System.Windows.Forms.Label lblChipData;

        private ScreenTask.UI.Controls.FluentCard cardChipUptime;
        private System.Windows.Forms.Label lblIconUptime;
        private System.Windows.Forms.Label lblTitleUptime;
        private System.Windows.Forms.Label lblChipUptime;

        // Tab 2: Capture & Audio Controls
        private ScreenTask.UI.Controls.FluentCard cardVideoSource;
        private System.Windows.Forms.Label label7;
        private ScreenTask.UI.Controls.FluentComboBox comboScreens;
        private ScreenTask.UI.Controls.FluentButton btnRefreshScreens;
        private ScreenTask.UI.Controls.FluentToggleSwitch cbCaptureMouse;

        private ScreenTask.UI.Controls.FluentCard cardAudio;
        private System.Windows.Forms.Label lblAudioPill;
        private ScreenTask.UI.Controls.FluentToggleSwitch cbAudio;
        private ScreenTask.UI.Controls.FluentComboBox comboAudioDevices;
        private ScreenTask.UI.Controls.FluentButton btnRefreshAudio;
        private ScreenTask.UI.Controls.FluentButton btnTestAudio;
        private ScreenTask.UI.Controls.FluentButton btnAudioDiag;

        private ScreenTask.UI.Controls.FluentCard groupBox1;
        private System.Windows.Forms.Label label8;
        private ScreenTask.UI.Controls.FluentSegmentedControl fpsSegmentedControl;
        private ScreenTask.UI.Controls.FluentComboBox comboFps;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.TrackBar qualitySlider;

        // Tab 3: Network & Security Controls
        private ScreenTask.UI.Controls.FluentCard cardNetwork;
        private System.Windows.Forms.Label lblNetworkPill;
        private System.Windows.Forms.Label label5;
        private ScreenTask.UI.Controls.FluentComboBox comboIPs;
        private ScreenTask.UI.Controls.FluentButton btnRefreshIPs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label lblPortStatus;

        private ScreenTask.UI.Controls.FluentCard gbOptions;
        private ScreenTask.UI.Controls.FluentToggleSwitch cbPrivate;
        private System.Windows.Forms.Label lblPinDesc;
        private System.Windows.Forms.TextBox txtPassword;
        private ScreenTask.UI.Controls.FluentButton btnGenPin;
        private ScreenTask.UI.Controls.FluentButton btnCopyPin;

        // Tab 4: Preferences & Logs Controls
        private ScreenTask.UI.Controls.FluentCard groupBox2;
        private ScreenTask.UI.Controls.FluentToggleSwitch cbAutoStart;
        private ScreenTask.UI.Controls.FluentToggleSwitch cbMiniStart;
        private ScreenTask.UI.Controls.FluentToggleSwitch cbCloseToTray;

        private ScreenTask.UI.Controls.FluentCard cardLogs;
        private ScreenTask.UI.Controls.FluentButton btnClearLog;
        private ScreenTask.UI.Controls.FluentButton btnCopyLog;
        private System.Windows.Forms.TextBox txtLog;

        // Compact Mode Controls
        private System.Windows.Forms.Label lblCompactDot;
        private System.Windows.Forms.Label lblCompactStatus;
        private System.Windows.Forms.Label lblCompactUrl;
        private ScreenTask.UI.Controls.FluentButton btnCompactCopy;
        private ScreenTask.UI.Controls.FluentButton btnCompactStop;
        private ScreenTask.UI.Controls.FluentButton btnCompactExpand;

        // Footer & System Tray
        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.NotifyIcon appNotify;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
    }
}
