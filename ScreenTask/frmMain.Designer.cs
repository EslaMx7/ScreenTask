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

            // Menu Strip
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileToggleBroadcast = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileCopyLink = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileShowQr = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpenBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileMiniBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewBroadcast = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewViewers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewPrefs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCaptureRefreshSources = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCaptureSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCaptureShowMouse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCaptureEnableAudio = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCaptureSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCaptureAudioDiag = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNetworkRefreshAdapters = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNetworkSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuNetworkPinProtection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNetworkGenPin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNetworkCopyPin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpGitHub = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpBuyMeCoffee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();

            // Top Header Elements
            this.picAppIcon = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.btnMiniMode = new System.Windows.Forms.Button();

            // Legacy side navigation (off-screen container preserving controls for engine/tests)
            this.panelSideNav = new System.Windows.Forms.Panel();
            this.btnNavBroadcast = new System.Windows.Forms.Button();
            this.btnNavClients = new System.Windows.Forms.Button();
            this.btnNavCapture = new System.Windows.Forms.Button();
            this.btnNavNetwork = new System.Windows.Forms.Button();
            this.btnNavPrefs = new System.Windows.Forms.Button();

            // Core Engine / Settings Controls
            this.comboScreens = new System.Windows.Forms.ComboBox();
            this.btnRefreshScreens = new System.Windows.Forms.Button();
            this.comboIPs = new System.Windows.Forms.ComboBox();
            this.btnRefreshIPs = new System.Windows.Forms.Button();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.lblPortStatus = new System.Windows.Forms.Label();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnGenPin = new System.Windows.Forms.Button();
            this.btnCopyPin = new System.Windows.Forms.Button();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.cbAudio = new System.Windows.Forms.CheckBox();
            this.comboAudioDevices = new System.Windows.Forms.ComboBox();
            this.btnRefreshAudio = new System.Windows.Forms.Button();
            this.btnTestAudio = new System.Windows.Forms.Button();
            this.btnAudioDiag = new System.Windows.Forms.Button();
            this.comboFps = new System.Windows.Forms.ComboBox();
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.cbMiniStart = new System.Windows.Forms.CheckBox();
            this.cbCloseToTray = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnCopyLog = new System.Windows.Forms.Button();

            // Central Content Container hosting the Broadcast Console
            this.panelContentContainer = new System.Windows.Forms.Panel();

            // 1. Preview Card
            this.cardPreview = new System.Windows.Forms.Panel();
            this.monitorPreview = new ScreenTask.UI.MonitorPreview();
            this.lblCursorStatus = new System.Windows.Forms.Label();
            this.btnSwitchSource = new System.Windows.Forms.Button();

            // 2. Broadcast Control Card
            this.groupBox3 = new System.Windows.Forms.Panel();
            this.lblBroadcastDesc = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnQuickMute = new System.Windows.Forms.Button();
            this.btnAudioSetup = new System.Windows.Forms.Button();
            this.lblAudioMeterText = new System.Windows.Forms.Label();
            this.pbAudioMeter = new System.Windows.Forms.ProgressBar();
            this.lblLiveStats = new System.Windows.Forms.Label();

            // 3. Stream URL Card
            this.cardUrl = new System.Windows.Forms.Panel();
            this.lblUrlTitle = new System.Windows.Forms.Label();
            this.lblWifiBadge = new System.Windows.Forms.Label();
            this.lblPinDisplay = new System.Windows.Forms.Label();
            this.btnNetworkSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnCopyUrl = new System.Windows.Forms.Button();
            this.btnQr = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();

            // 4..7. Telemetry Chips
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

            // Footer & System Tray
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusLabelServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelViewers = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelFps = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelData = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelUptime = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelAudio = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelCopyright = new System.Windows.Forms.ToolStripStatusLabel();
            this.appNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);

            this.menuStripMain.SuspendLayout();
            this.panelSideNav.SuspendLayout();
            this.panelContentContainer.SuspendLayout();
            this.cardPreview.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cardUrl.SuspendLayout();
            this.cardChipViewers.SuspendLayout();
            this.cardChipFps.SuspendLayout();
            this.cardChipData.SuspendLayout();
            this.cardChipUptime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();

            // ── MenuStrip ──────────────────────────────────────────────────
            this.menuStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.menuStripMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuFile,
                this.menuView,
                this.menuCapture,
                this.menuNetwork,
                this.menuHelp
            });
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1000, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";

            // File Menu
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuFileToggleBroadcast,
                this.menuFileSep1,
                this.menuFileCopyLink,
                this.menuFileShowQr,
                this.menuFileOpenBrowser,
                this.menuFileSep2,
                this.menuFileMiniBar,
                this.menuFileSep3,
                this.menuFileExit
            });
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";

            this.menuFileToggleBroadcast.Name = "menuFileToggleBroadcast";
            this.menuFileToggleBroadcast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuFileToggleBroadcast.Size = new System.Drawing.Size(209, 22);
            this.menuFileToggleBroadcast.Text = "▶ &Start Broadcast";

            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(206, 6);

            this.menuFileCopyLink.Name = "menuFileCopyLink";
            this.menuFileCopyLink.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuFileCopyLink.Size = new System.Drawing.Size(209, 22);
            this.menuFileCopyLink.Text = "📋 Copy Stream &Link";

            this.menuFileShowQr.Name = "menuFileShowQr";
            this.menuFileShowQr.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuFileShowQr.Size = new System.Drawing.Size(209, 22);
            this.menuFileShowQr.Text = "📱 Show &QR Code...";

            this.menuFileOpenBrowser.Name = "menuFileOpenBrowser";
            this.menuFileOpenBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpenBrowser.Size = new System.Drawing.Size(209, 22);
            this.menuFileOpenBrowser.Text = "🌐 Open in &Browser";

            this.menuFileSep2.Name = "menuFileSep2";
            this.menuFileSep2.Size = new System.Drawing.Size(206, 6);

            this.menuFileMiniBar.Name = "menuFileMiniBar";
            this.menuFileMiniBar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menuFileMiniBar.Size = new System.Drawing.Size(209, 22);
            this.menuFileMiniBar.Text = "🗗 Switch to &Mini Bar";

            this.menuFileSep3.Name = "menuFileSep3";
            this.menuFileSep3.Size = new System.Drawing.Size(206, 6);

            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuFileExit.Size = new System.Drawing.Size(209, 22);
            this.menuFileExit.Text = "E&xit";

            // View Menu
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuViewBroadcast,
                this.menuViewViewers,
                this.menuViewCapture,
                this.menuViewNetwork,
                this.menuViewPrefs
            });
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(44, 20);
            this.menuView.Text = "&View";

            this.menuViewBroadcast.Name = "menuViewBroadcast";
            this.menuViewBroadcast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.menuViewBroadcast.Size = new System.Drawing.Size(244, 22);
            this.menuViewBroadcast.Text = "🔴 &Broadcast";

            this.menuViewViewers.Name = "menuViewViewers";
            this.menuViewViewers.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.menuViewViewers.Size = new System.Drawing.Size(244, 22);
            this.menuViewViewers.Text = "👥 Connected &Viewers & QR...";

            this.menuViewCapture.Name = "menuViewCapture";
            this.menuViewCapture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.menuViewCapture.Size = new System.Drawing.Size(244, 22);
            this.menuViewCapture.Text = "🖥️ &Screen & Audio Source...";

            this.menuViewNetwork.Name = "menuViewNetwork";
            this.menuViewNetwork.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.menuViewNetwork.Size = new System.Drawing.Size(244, 22);
            this.menuViewNetwork.Text = "🛡️ &Network & PIN...";

            this.menuViewPrefs.Name = "menuViewPrefs";
            this.menuViewPrefs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.menuViewPrefs.Size = new System.Drawing.Size(244, 22);
            this.menuViewPrefs.Text = "⚙️ &Settings & Logs...";

            // Capture Menu
            this.menuCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuCaptureRefreshSources,
                this.menuCaptureSep1,
                this.menuCaptureShowMouse,
                this.menuCaptureEnableAudio,
                this.menuCaptureSep2,
                this.menuCaptureAudioDiag
            });
            this.menuCapture.Name = "menuCapture";
            this.menuCapture.Size = new System.Drawing.Size(61, 20);
            this.menuCapture.Text = "&Capture";

            this.menuCaptureRefreshSources.Name = "menuCaptureRefreshSources";
            this.menuCaptureRefreshSources.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuCaptureRefreshSources.Size = new System.Drawing.Size(209, 22);
            this.menuCaptureRefreshSources.Text = "🔄 &Refresh Sources";

            this.menuCaptureSep1.Name = "menuCaptureSep1";
            this.menuCaptureSep1.Size = new System.Drawing.Size(206, 6);

            this.menuCaptureShowMouse.CheckOnClick = true;
            this.menuCaptureShowMouse.Name = "menuCaptureShowMouse";
            this.menuCaptureShowMouse.Size = new System.Drawing.Size(209, 22);
            this.menuCaptureShowMouse.Text = "🖱️ Show &Mouse Cursor";

            this.menuCaptureEnableAudio.CheckOnClick = true;
            this.menuCaptureEnableAudio.Name = "menuCaptureEnableAudio";
            this.menuCaptureEnableAudio.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.menuCaptureEnableAudio.Size = new System.Drawing.Size(209, 22);
            this.menuCaptureEnableAudio.Text = "🔊 Enable &Audio";

            this.menuCaptureSep2.Name = "menuCaptureSep2";
            this.menuCaptureSep2.Size = new System.Drawing.Size(206, 6);

            this.menuCaptureAudioDiag.Name = "menuCaptureAudioDiag";
            this.menuCaptureAudioDiag.Size = new System.Drawing.Size(209, 22);
            this.menuCaptureAudioDiag.Text = "🎙️ Audio &Diagnostics...";

            // Network Menu
            this.menuNetwork.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuNetworkRefreshAdapters,
                this.menuNetworkSep1,
                this.menuNetworkPinProtection,
                this.menuNetworkGenPin,
                this.menuNetworkCopyPin
            });
            this.menuNetwork.Name = "menuNetwork";
            this.menuNetwork.Size = new System.Drawing.Size(64, 20);
            this.menuNetwork.Text = "&Network";

            this.menuNetworkRefreshAdapters.Name = "menuNetworkRefreshAdapters";
            this.menuNetworkRefreshAdapters.Size = new System.Drawing.Size(203, 22);
            this.menuNetworkRefreshAdapters.Text = "🔄 Refresh &Adapters";

            this.menuNetworkSep1.Name = "menuNetworkSep1";
            this.menuNetworkSep1.Size = new System.Drawing.Size(200, 6);

            this.menuNetworkPinProtection.CheckOnClick = true;
            this.menuNetworkPinProtection.Name = "menuNetworkPinProtection";
            this.menuNetworkPinProtection.Size = new System.Drawing.Size(203, 22);
            this.menuNetworkPinProtection.Text = "🔒 &PIN Protection";

            this.menuNetworkGenPin.Name = "menuNetworkGenPin";
            this.menuNetworkGenPin.Size = new System.Drawing.Size(203, 22);
            this.menuNetworkGenPin.Text = "🎲 Generate &New PIN";

            this.menuNetworkCopyPin.Name = "menuNetworkCopyPin";
            this.menuNetworkCopyPin.Size = new System.Drawing.Size(203, 22);
            this.menuNetworkCopyPin.Text = "📋 &Copy PIN";

            // Help Menu
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuHelpWebsite,
                this.menuHelpGitHub,
                this.menuHelpBuyMeCoffee,
                this.menuHelpSep1,
                this.menuHelpAbout
            });
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";

            this.menuHelpWebsite.Name = "menuHelpWebsite";
            this.menuHelpWebsite.Size = new System.Drawing.Size(211, 22);
            this.menuHelpWebsite.Text = "🌐 ScreenTask &Website";

            this.menuHelpGitHub.Name = "menuHelpGitHub";
            this.menuHelpGitHub.Size = new System.Drawing.Size(211, 22);
            this.menuHelpGitHub.Text = "🐙 &GitHub Repository";

            this.menuHelpBuyMeCoffee.Name = "menuHelpBuyMeCoffee";
            this.menuHelpBuyMeCoffee.Size = new System.Drawing.Size(211, 22);
            this.menuHelpBuyMeCoffee.Text = "☕ &Buy Me a Coffee";

            this.menuHelpSep1.Name = "menuHelpSep1";
            this.menuHelpSep1.Size = new System.Drawing.Size(208, 6);

            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuHelpAbout.Size = new System.Drawing.Size(211, 22);
            this.menuHelpAbout.Text = "&About ScreenTask 2.0...";

            // ── Top Header Elements ─────────────────────────────────────────
            this.picAppIcon.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.picAppIcon.Location = new System.Drawing.Point(12, 32);
            this.picAppIcon.Name = "picAppIcon";
            this.picAppIcon.Size = new System.Drawing.Size(32, 30);
            this.picAppIcon.TabIndex = 0;
            this.picAppIcon.Text = "📺";

            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAppTitle.Location = new System.Drawing.Point(46, 28);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(107, 20);
            this.lblAppTitle.TabIndex = 1;
            this.lblAppTitle.Text = "ScreenTask 2.0";

            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAppSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAppSubtitle.Location = new System.Drawing.Point(47, 48);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(175, 12);
            this.lblAppSubtitle.TabIndex = 2;
            this.lblAppSubtitle.Text = "Offline & LAN Screen Sharing Broadcast";

            this.lblStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusBadge.AutoSize = true;
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblStatusBadge.Location = new System.Drawing.Point(796, 38);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(53, 13);
            this.lblStatusBadge.TabIndex = 5;
            this.lblStatusBadge.Text = "● READY";

            this.btnMiniMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMiniMode.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnMiniMode.Location = new System.Drawing.Point(864, 30);
            this.btnMiniMode.Name = "btnMiniMode";
            this.btnMiniMode.Size = new System.Drawing.Size(124, 28);
            this.btnMiniMode.TabIndex = 6;
            this.btnMiniMode.Text = "🗗  Mini Bar";
            this.btnMiniMode.UseVisualStyleBackColor = true;

            // ── Off-screen Legacy Container for Non-Visual Controls ─────────
            this.panelSideNav.Location = new System.Drawing.Point(-500, -500);
            this.panelSideNav.Name = "panelSideNav";
            this.panelSideNav.Size = new System.Drawing.Size(1, 1);
            this.panelSideNav.TabIndex = 0;
            this.panelSideNav.Visible = true;

            this.btnNavBroadcast.Name = "btnNavBroadcast";
            this.btnNavClients.Name = "btnNavClients";
            this.btnNavCapture.Name = "btnNavCapture";
            this.btnNavNetwork.Name = "btnNavNetwork";
            this.btnNavPrefs.Name = "btnNavPrefs";

            this.comboScreens.Name = "comboScreens";
            this.btnRefreshScreens.Name = "btnRefreshScreens";
            this.btnRefreshScreens.Text = "🔄";
            this.comboIPs.Name = "comboIPs";
            this.btnRefreshIPs.Name = "btnRefreshIPs";
            this.btnRefreshIPs.Text = "🔄";
            this.numPort.Name = "numPort";
            this.numPort.Minimum = 1;
            this.numPort.Maximum = 65535;
            this.numPort.Value = 7070;
            this.lblPortStatus.Name = "lblPortStatus";
            this.cbPrivate.Name = "cbPrivate";
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Enabled = false;
            this.btnGenPin.Name = "btnGenPin";
            this.btnGenPin.Enabled = false;
            this.btnCopyPin.Name = "btnCopyPin";
            this.btnCopyPin.Enabled = false;
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.Checked = true;
            this.cbAudio.Name = "cbAudio";
            this.comboAudioDevices.Name = "comboAudioDevices";
            this.comboAudioDevices.Enabled = false;
            this.btnRefreshAudio.Name = "btnRefreshAudio";
            this.btnRefreshAudio.Text = "🔄";
            this.btnTestAudio.Name = "btnTestAudio";
            this.btnTestAudio.Enabled = false;
            this.btnAudioDiag.Name = "btnAudioDiag";
            this.btnAudioDiag.Enabled = false;
            this.comboFps.Name = "comboFps";
            this.comboFps.Items.AddRange(new object[] { "60 FPS (Ultra)", "30 FPS (Recommended)", "15 FPS (Balanced)", "5 FPS (Low)" });
            this.comboFps.SelectedIndex = 1;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Minimum = 20;
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Value = 80;
            this.lblQuality.Name = "lblQuality";
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbMiniStart.Name = "cbMiniStart";
            this.cbCloseToTray.Name = "cbCloseToTray";
            this.cbCloseToTray.Checked = true;
            this.txtLog.Name = "txtLog";
            this.btnClearLog.Name = "btnClearLog";
            this.btnCopyLog.Name = "btnCopyLog";

            this.panelSideNav.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnNavBroadcast,
                this.btnNavClients,
                this.btnNavCapture,
                this.btnNavNetwork,
                this.btnNavPrefs,
                this.comboScreens,
                this.btnRefreshScreens,
                this.comboIPs,
                this.btnRefreshIPs,
                this.numPort,
                this.lblPortStatus,
                this.cbPrivate,
                this.txtPassword,
                this.btnGenPin,
                this.btnCopyPin,
                this.cbCaptureMouse,
                this.cbAudio,
                this.comboAudioDevices,
                this.btnRefreshAudio,
                this.btnTestAudio,
                this.btnAudioDiag,
                this.comboFps,
                this.qualitySlider,
                this.lblQuality,
                this.cbAutoStart,
                this.cbMiniStart,
                this.cbCloseToTray,
                this.txtLog,
                this.btnClearLog,
                this.btnCopyLog
            });

            // ── Central Content Container (Broadcast Console) ───────────────
            this.panelContentContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContentContainer.Controls.Add(this.cardPreview);
            this.panelContentContainer.Controls.Add(this.groupBox3);
            this.panelContentContainer.Controls.Add(this.cardUrl);
            this.panelContentContainer.Controls.Add(this.cardChipViewers);
            this.panelContentContainer.Controls.Add(this.cardChipFps);
            this.panelContentContainer.Controls.Add(this.cardChipData);
            this.panelContentContainer.Controls.Add(this.cardChipUptime);
            this.panelContentContainer.Location = new System.Drawing.Point(12, 70);
            this.panelContentContainer.Name = "panelContentContainer";
            this.panelContentContainer.Size = new System.Drawing.Size(976, 600);
            this.panelContentContainer.TabIndex = 1;

            // 1. Preview Card
            this.cardPreview.BackColor = System.Drawing.SystemColors.Control;
            this.cardPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardPreview.Controls.Add(this.monitorPreview);
            this.cardPreview.Controls.Add(this.lblCursorStatus);
            this.cardPreview.Controls.Add(this.btnSwitchSource);
            this.cardPreview.Location = new System.Drawing.Point(0, 0);
            this.cardPreview.Name = "cardPreview";
            this.cardPreview.Size = new System.Drawing.Size(696, 320);
            this.cardPreview.TabIndex = 0;

            this.monitorPreview.AudioInfoText = "Audio: Active";
            this.monitorPreview.BackColor = System.Drawing.Color.Black;
            this.monitorPreview.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.monitorPreview.FpsText = "30.0 FPS";
            this.monitorPreview.IsLive = false;
            this.monitorPreview.Location = new System.Drawing.Point(12, 12);
            this.monitorPreview.Name = "monitorPreview";
            this.monitorPreview.ResolutionText = "1920 × 1080";
            this.monitorPreview.Size = new System.Drawing.Size(670, 260);
            this.monitorPreview.SourceTitle = "Display 1 (Primary)";
            this.monitorPreview.TabIndex = 0;

            this.lblCursorStatus.AutoSize = true;
            this.lblCursorStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblCursorStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCursorStatus.Location = new System.Drawing.Point(14, 280);
            this.lblCursorStatus.Name = "lblCursorStatus";
            this.lblCursorStatus.Size = new System.Drawing.Size(126, 12);
            this.lblCursorStatus.TabIndex = 1;
            this.lblCursorStatus.Text = "Mouse Pointer: Visible";

            this.btnSwitchSource.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnSwitchSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSwitchSource.Location = new System.Drawing.Point(538, 274);
            this.btnSwitchSource.Name = "btnSwitchSource";
            this.btnSwitchSource.Size = new System.Drawing.Size(145, 28);
            this.btnSwitchSource.TabIndex = 2;
            this.btnSwitchSource.Text = "🖥️ Change Source...";
            this.btnSwitchSource.UseVisualStyleBackColor = true;

            // 2. Broadcast Control Card
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.groupBox3.Controls.Add(this.lblBroadcastDesc);
            this.groupBox3.Controls.Add(this.btnStartServer);
            this.groupBox3.Controls.Add(this.btnQuickMute);
            this.groupBox3.Controls.Add(this.btnAudioSetup);
            this.groupBox3.Controls.Add(this.lblAudioMeterText);
            this.groupBox3.Controls.Add(this.pbAudioMeter);
            this.groupBox3.Controls.Add(this.lblLiveStats);
            this.groupBox3.Location = new System.Drawing.Point(706, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 320);
            this.groupBox3.TabIndex = 1;

            this.lblBroadcastDesc.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblBroadcastDesc.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblBroadcastDesc.Location = new System.Drawing.Point(12, 12);
            this.lblBroadcastDesc.Name = "lblBroadcastDesc";
            this.lblBroadcastDesc.Size = new System.Drawing.Size(246, 40);
            this.lblBroadcastDesc.TabIndex = 0;
            this.lblBroadcastDesc.Text = "Share your screen with any phone, tablet, or browser on your local network.";

            this.btnStartServer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStartServer.Location = new System.Drawing.Point(12, 54);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(246, 40);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "▶  START BROADCAST";
            this.btnStartServer.UseVisualStyleBackColor = true;

            this.btnQuickMute.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnQuickMute.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickMute.Location = new System.Drawing.Point(12, 98);
            this.btnQuickMute.Name = "btnQuickMute";
            this.btnQuickMute.Size = new System.Drawing.Size(118, 28);
            this.btnQuickMute.TabIndex = 2;
            this.btnQuickMute.Text = "🔊 Unmute Audio";
            this.btnQuickMute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnQuickMute.UseVisualStyleBackColor = true;

            this.btnAudioSetup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAudioSetup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAudioSetup.Location = new System.Drawing.Point(134, 98);
            this.btnAudioSetup.Name = "btnAudioSetup";
            this.btnAudioSetup.Size = new System.Drawing.Size(124, 28);
            this.btnAudioSetup.TabIndex = 6;
            this.btnAudioSetup.Text = "🎙️ Audio Setup...";
            this.btnAudioSetup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAudioSetup.UseVisualStyleBackColor = true;

            this.lblAudioMeterText.AutoSize = true;
            this.lblAudioMeterText.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioMeterText.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioMeterText.Location = new System.Drawing.Point(12, 134);
            this.lblAudioMeterText.Name = "lblAudioMeterText";
            this.lblAudioMeterText.Size = new System.Drawing.Size(95, 12);
            this.lblAudioMeterText.TabIndex = 3;
            this.lblAudioMeterText.Text = "Sound Volume Level:";

            this.pbAudioMeter.Enabled = false;
            this.pbAudioMeter.Location = new System.Drawing.Point(12, 150);
            this.pbAudioMeter.Name = "pbAudioMeter";
            this.pbAudioMeter.Size = new System.Drawing.Size(246, 10);
            this.pbAudioMeter.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAudioMeter.TabIndex = 4;
            this.pbAudioMeter.Value = 0;

            this.lblLiveStats.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblLiveStats.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLiveStats.Location = new System.Drawing.Point(12, 166);
            this.lblLiveStats.Name = "lblLiveStats";
            this.lblLiveStats.Size = new System.Drawing.Size(246, 48);
            this.lblLiveStats.TabIndex = 5;
            this.lblLiveStats.Text = "⚡ Stopped • Ready to broadcast";
            this.lblLiveStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 3. Stream URL Card
            this.cardUrl.BackColor = System.Drawing.SystemColors.Control;
            this.cardUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardUrl.Controls.Add(this.lblUrlTitle);
            this.cardUrl.Controls.Add(this.lblWifiBadge);
            this.cardUrl.Controls.Add(this.lblPinDisplay);
            this.cardUrl.Controls.Add(this.btnNetworkSettings);
            this.cardUrl.Controls.Add(this.label1);
            this.cardUrl.Controls.Add(this.txtURL);
            this.cardUrl.Controls.Add(this.btnCopyUrl);
            this.cardUrl.Controls.Add(this.btnQr);
            this.cardUrl.Controls.Add(this.btnLaunch);
            this.cardUrl.Location = new System.Drawing.Point(0, 330);
            this.cardUrl.Name = "cardUrl";
            this.cardUrl.Size = new System.Drawing.Size(976, 86);
            this.cardUrl.TabIndex = 2;

            this.lblUrlTitle.AutoSize = true;
            this.lblUrlTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUrlTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUrlTitle.Location = new System.Drawing.Point(12, 10);
            this.lblUrlTitle.Name = "lblUrlTitle";
            this.lblUrlTitle.Size = new System.Drawing.Size(120, 15);
            this.lblUrlTitle.TabIndex = 0;
            this.lblUrlTitle.Text = "Local Stream Address";

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

            this.lblPinDisplay.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPinDisplay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPinDisplay.Location = new System.Drawing.Point(620, 10);
            this.lblPinDisplay.Name = "lblPinDisplay";
            this.lblPinDisplay.Size = new System.Drawing.Size(190, 16);
            this.lblPinDisplay.TabIndex = 2;
            this.lblPinDisplay.Text = "PIN: Not Required";
            this.lblPinDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.btnNetworkSettings.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.btnNetworkSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNetworkSettings.Location = new System.Drawing.Point(824, 7);
            this.btnNetworkSettings.Name = "btnNetworkSettings";
            this.btnNetworkSettings.Size = new System.Drawing.Size(140, 24);
            this.btnNetworkSettings.TabIndex = 8;
            this.btnNetworkSettings.Text = "🛡️ Network & PIN...";
            this.btnNetworkSettings.UseVisualStyleBackColor = true;

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "URL";

            this.txtURL.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtURL.Location = new System.Drawing.Point(44, 40);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(630, 22);
            this.txtURL.TabIndex = 4;

            this.btnCopyUrl.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyUrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCopyUrl.Location = new System.Drawing.Point(684, 38);
            this.btnCopyUrl.Name = "btnCopyUrl";
            this.btnCopyUrl.Size = new System.Drawing.Size(90, 26);
            this.btnCopyUrl.TabIndex = 5;
            this.btnCopyUrl.Text = "📋 Copy Link";
            this.btnCopyUrl.UseVisualStyleBackColor = true;

            this.btnQr.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnQr.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQr.Location = new System.Drawing.Point(780, 38);
            this.btnQr.Name = "btnQr";
            this.btnQr.Size = new System.Drawing.Size(90, 26);
            this.btnQr.TabIndex = 6;
            this.btnQr.Text = "📱 Show QR";
            this.btnQr.UseVisualStyleBackColor = true;

            this.btnLaunch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLaunch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLaunch.Location = new System.Drawing.Point(876, 38);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(88, 26);
            this.btnLaunch.TabIndex = 7;
            this.btnLaunch.Text = "🌐 Open";
            this.btnLaunch.UseVisualStyleBackColor = true;

            // 4. Chip Viewers
            this.cardChipViewers.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipViewers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardChipViewers.Controls.Add(this.lblIconViewers);
            this.cardChipViewers.Controls.Add(this.lblTitleViewers);
            this.cardChipViewers.Controls.Add(this.lblChipViewers);
            this.cardChipViewers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cardChipViewers.Location = new System.Drawing.Point(0, 524);
            this.cardChipViewers.Name = "cardChipViewers";
            this.cardChipViewers.Size = new System.Drawing.Size(236, 72);
            this.cardChipViewers.TabIndex = 3;

            this.lblIconViewers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconViewers.Location = new System.Drawing.Point(8, 8);
            this.lblIconViewers.Name = "lblIconViewers";
            this.lblIconViewers.Size = new System.Drawing.Size(28, 28);
            this.lblIconViewers.TabIndex = 0;
            this.lblIconViewers.Text = "👥";

            this.lblTitleViewers.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleViewers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleViewers.Location = new System.Drawing.Point(40, 8);
            this.lblTitleViewers.Name = "lblTitleViewers";
            this.lblTitleViewers.Size = new System.Drawing.Size(180, 16);
            this.lblTitleViewers.TabIndex = 1;
            this.lblTitleViewers.Text = "Connected Viewers";

            this.lblChipViewers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipViewers.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipViewers.Location = new System.Drawing.Point(8, 38);
            this.lblChipViewers.Name = "lblChipViewers";
            this.lblChipViewers.Size = new System.Drawing.Size(220, 22);
            this.lblChipViewers.TabIndex = 2;
            this.lblChipViewers.Text = "0 Connected";

            // 5. Chip FPS
            this.cardChipFps.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipFps.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardChipFps.Controls.Add(this.lblIconFps);
            this.cardChipFps.Controls.Add(this.lblTitleFps);
            this.cardChipFps.Controls.Add(this.lblChipFps);
            this.cardChipFps.Location = new System.Drawing.Point(244, 524);
            this.cardChipFps.Name = "cardChipFps";
            this.cardChipFps.Size = new System.Drawing.Size(236, 72);
            this.cardChipFps.TabIndex = 4;

            this.lblIconFps.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconFps.Location = new System.Drawing.Point(8, 8);
            this.lblIconFps.Name = "lblIconFps";
            this.lblIconFps.Size = new System.Drawing.Size(28, 28);
            this.lblIconFps.TabIndex = 0;
            this.lblIconFps.Text = "⚡";

            this.lblTitleFps.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleFps.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleFps.Location = new System.Drawing.Point(40, 8);
            this.lblTitleFps.Name = "lblTitleFps";
            this.lblTitleFps.Size = new System.Drawing.Size(180, 16);
            this.lblTitleFps.TabIndex = 1;
            this.lblTitleFps.Text = "Smoothness (FPS)";

            this.lblChipFps.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipFps.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipFps.Location = new System.Drawing.Point(8, 38);
            this.lblChipFps.Name = "lblChipFps";
            this.lblChipFps.Size = new System.Drawing.Size(220, 22);
            this.lblChipFps.TabIndex = 2;
            this.lblChipFps.Text = "0.0 FPS";

            // 6. Chip Data
            this.cardChipData.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardChipData.Controls.Add(this.lblIconData);
            this.cardChipData.Controls.Add(this.lblTitleData);
            this.cardChipData.Controls.Add(this.lblChipData);
            this.cardChipData.Location = new System.Drawing.Point(488, 524);
            this.cardChipData.Name = "cardChipData";
            this.cardChipData.Size = new System.Drawing.Size(236, 72);
            this.cardChipData.TabIndex = 5;

            this.lblIconData.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconData.Location = new System.Drawing.Point(8, 8);
            this.lblIconData.Name = "lblIconData";
            this.lblIconData.Size = new System.Drawing.Size(28, 28);
            this.lblIconData.TabIndex = 0;
            this.lblIconData.Text = "📊";

            this.lblTitleData.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleData.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleData.Location = new System.Drawing.Point(40, 8);
            this.lblTitleData.Name = "lblTitleData";
            this.lblTitleData.Size = new System.Drawing.Size(180, 16);
            this.lblTitleData.TabIndex = 1;
            this.lblTitleData.Text = "Data Transferred";

            this.lblChipData.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipData.Location = new System.Drawing.Point(8, 38);
            this.lblChipData.Name = "lblChipData";
            this.lblChipData.Size = new System.Drawing.Size(220, 22);
            this.lblChipData.TabIndex = 2;
            this.lblChipData.Text = "0.0 MB";

            // 7. Chip Uptime
            this.cardChipUptime.BackColor = System.Drawing.SystemColors.Control;
            this.cardChipUptime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cardChipUptime.Controls.Add(this.lblIconUptime);
            this.cardChipUptime.Controls.Add(this.lblTitleUptime);
            this.cardChipUptime.Controls.Add(this.lblChipUptime);
            this.cardChipUptime.Location = new System.Drawing.Point(732, 524);
            this.cardChipUptime.Name = "cardChipUptime";
            this.cardChipUptime.Size = new System.Drawing.Size(244, 72);
            this.cardChipUptime.TabIndex = 6;

            this.lblIconUptime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconUptime.Location = new System.Drawing.Point(8, 8);
            this.lblIconUptime.Name = "lblIconUptime";
            this.lblIconUptime.Size = new System.Drawing.Size(28, 28);
            this.lblIconUptime.TabIndex = 0;
            this.lblIconUptime.Text = "⏱";

            this.lblTitleUptime.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleUptime.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleUptime.Location = new System.Drawing.Point(40, 8);
            this.lblTitleUptime.Name = "lblTitleUptime";
            this.lblTitleUptime.Size = new System.Drawing.Size(180, 16);
            this.lblTitleUptime.TabIndex = 1;
            this.lblTitleUptime.Text = "Stream Time";

            this.lblChipUptime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChipUptime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblChipUptime.Location = new System.Drawing.Point(8, 38);
            this.lblChipUptime.Name = "lblChipUptime";
            this.lblChipUptime.Size = new System.Drawing.Size(220, 22);
            this.lblChipUptime.TabIndex = 2;
            this.lblChipUptime.Text = "00:00:00";

            // ── Footer StatusStrip ──────────────────────────────────────────
            this.statusStripMain.BackColor = System.Drawing.SystemColors.Control;
            this.statusStripMain.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.statusLabelServer,
                this.statusLabelViewers,
                this.statusLabelFps,
                this.statusLabelData,
                this.statusLabelUptime,
                this.statusLabelAudio,
                this.statusLabelCopyright
            });
            this.statusStripMain.Location = new System.Drawing.Point(0, 676);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.ShowItemToolTips = true;
            this.statusStripMain.Size = new System.Drawing.Size(1000, 24);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 2;

            this.statusLabelServer.Name = "statusLabelServer";
            this.statusLabelServer.Size = new System.Drawing.Size(340, 19);
            this.statusLabelServer.Spring = true;
            this.statusLabelServer.Text = "● Ready to stream";
            this.statusLabelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.statusLabelViewers.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelViewers.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelViewers.Name = "statusLabelViewers";
            this.statusLabelViewers.Size = new System.Drawing.Size(75, 19);
            this.statusLabelViewers.Text = "👥 0 viewers";

            this.statusLabelFps.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelFps.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelFps.Name = "statusLabelFps";
            this.statusLabelFps.Size = new System.Drawing.Size(55, 19);
            this.statusLabelFps.Text = "🎞️ 0 FPS";

            this.statusLabelData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelData.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelData.Name = "statusLabelData";
            this.statusLabelData.Size = new System.Drawing.Size(65, 19);
            this.statusLabelData.Text = "📊 0.0 MB";

            this.statusLabelUptime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelUptime.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelUptime.Name = "statusLabelUptime";
            this.statusLabelUptime.Size = new System.Drawing.Size(70, 19);
            this.statusLabelUptime.Text = "⏱️ 00:00:00";

            this.statusLabelAudio.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelAudio.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelAudio.Name = "statusLabelAudio";
            this.statusLabelAudio.Size = new System.Drawing.Size(85, 19);
            this.statusLabelAudio.Text = "🔇 Audio: Off";

            this.statusLabelCopyright.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelCopyright.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelCopyright.IsLink = true;
            this.statusLabelCopyright.Name = "statusLabelCopyright";
            this.statusLabelCopyright.Size = new System.Drawing.Size(155, 19);
            this.statusLabelCopyright.Text = "© 2014-2026 Eslam Hamouda";
            this.statusLabelCopyright.ToolTipText = "screentask.eslamx.com";

            // System Tray NotifyIcon
            this.appNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.appNotify.ContextMenuStrip = this.trayContextMenu;
            this.appNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("appNotify.Icon")));
            this.appNotify.Text = "ScreenTask 2.0";
            this.appNotify.Visible = true;

            // ── Form Main Definition ────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(880, 640);
            this.Controls.Add(this.panelContentContainer);
            this.Controls.Add(this.picAppIcon);
            this.Controls.Add(this.lblAppTitle);
            this.Controls.Add(this.lblAppSubtitle);
            this.Controls.Add(this.btnMiniMode);
            this.Controls.Add(this.lblStatusBadge);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.panelSideNav);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenTask 2.0 – Offline & LAN Screen Sharing";

            this.toolTipMain.SetToolTip(this.btnMiniMode, "Switch to compact mini bar");

            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelSideNav.ResumeLayout(false);
            this.panelSideNav.PerformLayout();
            this.panelContentContainer.ResumeLayout(false);
            this.cardPreview.ResumeLayout(false);
            this.cardPreview.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.cardUrl.ResumeLayout(false);
            this.cardUrl.PerformLayout();
            this.cardChipViewers.ResumeLayout(false);
            this.cardChipFps.ResumeLayout(false);
            this.cardChipData.ResumeLayout(false);
            this.cardChipUptime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Header controls
        private System.Windows.Forms.Label picAppIcon;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Button btnMiniMode;

        // Legacy / Non-visual Container Controls
        private System.Windows.Forms.Panel panelSideNav;
        internal System.Windows.Forms.Button btnNavBroadcast;
        internal System.Windows.Forms.Button btnNavClients;
        internal System.Windows.Forms.Button btnNavCapture;
        internal System.Windows.Forms.Button btnNavNetwork;
        internal System.Windows.Forms.Button btnNavPrefs;

        internal System.Windows.Forms.ComboBox comboScreens;
        internal System.Windows.Forms.Button btnRefreshScreens;
        internal System.Windows.Forms.ComboBox comboIPs;
        internal System.Windows.Forms.Button btnRefreshIPs;
        internal System.Windows.Forms.NumericUpDown numPort;
        internal System.Windows.Forms.Label lblPortStatus;
        internal System.Windows.Forms.CheckBox cbPrivate;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Button btnGenPin;
        internal System.Windows.Forms.Button btnCopyPin;
        internal System.Windows.Forms.CheckBox cbCaptureMouse;
        internal System.Windows.Forms.CheckBox cbAudio;
        internal System.Windows.Forms.ComboBox comboAudioDevices;
        internal System.Windows.Forms.Button btnRefreshAudio;
        internal System.Windows.Forms.Button btnTestAudio;
        internal System.Windows.Forms.Button btnAudioDiag;
        internal System.Windows.Forms.ComboBox comboFps;
        internal System.Windows.Forms.TrackBar qualitySlider;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.CheckBox cbAutoStart;
        internal System.Windows.Forms.CheckBox cbMiniStart;
        internal System.Windows.Forms.CheckBox cbCloseToTray;
        internal System.Windows.Forms.TextBox txtLog;
        internal System.Windows.Forms.Button btnClearLog;
        internal System.Windows.Forms.Button btnCopyLog;

        // Broadcast Console Direct Controls
        private System.Windows.Forms.Panel panelContentContainer;
        internal System.Windows.Forms.Panel cardPreview;
        internal ScreenTask.UI.MonitorPreview monitorPreview;
        internal System.Windows.Forms.Label lblCursorStatus;
        internal System.Windows.Forms.Button btnSwitchSource;

        internal System.Windows.Forms.Panel groupBox3;
        internal System.Windows.Forms.Label lblBroadcastDesc;
        internal System.Windows.Forms.Button btnStartServer;
        internal System.Windows.Forms.Button btnQuickMute;
        internal System.Windows.Forms.Button btnAudioSetup;
        internal System.Windows.Forms.Label lblAudioMeterText;
        internal System.Windows.Forms.ProgressBar pbAudioMeter;
        internal System.Windows.Forms.Label lblLiveStats;

        internal System.Windows.Forms.Panel cardUrl;
        internal System.Windows.Forms.Label lblUrlTitle;
        internal System.Windows.Forms.Label lblWifiBadge;
        internal System.Windows.Forms.Label lblPinDisplay;
        internal System.Windows.Forms.Button btnNetworkSettings;
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

        // Footer & System Tray
        internal System.Windows.Forms.StatusStrip statusStripMain;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelServer;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelViewers;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelFps;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelData;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelUptime;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelAudio;
        internal System.Windows.Forms.ToolStripStatusLabel statusLabelCopyright;
        private System.Windows.Forms.NotifyIcon appNotify;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;

        // Menu Strip
        internal System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        internal System.Windows.Forms.ToolStripMenuItem menuFileToggleBroadcast;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        internal System.Windows.Forms.ToolStripMenuItem menuFileCopyLink;
        internal System.Windows.Forms.ToolStripMenuItem menuFileShowQr;
        internal System.Windows.Forms.ToolStripMenuItem menuFileOpenBrowser;
        private System.Windows.Forms.ToolStripSeparator menuFileSep2;
        private System.Windows.Forms.ToolStripMenuItem menuFileMiniBar;
        private System.Windows.Forms.ToolStripSeparator menuFileSep3;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuViewBroadcast;
        private System.Windows.Forms.ToolStripMenuItem menuViewViewers;
        private System.Windows.Forms.ToolStripMenuItem menuViewCapture;
        private System.Windows.Forms.ToolStripMenuItem menuViewNetwork;
        private System.Windows.Forms.ToolStripMenuItem menuViewPrefs;
        private System.Windows.Forms.ToolStripMenuItem menuCapture;
        private System.Windows.Forms.ToolStripMenuItem menuCaptureRefreshSources;
        private System.Windows.Forms.ToolStripSeparator menuCaptureSep1;
        internal System.Windows.Forms.ToolStripMenuItem menuCaptureShowMouse;
        internal System.Windows.Forms.ToolStripMenuItem menuCaptureEnableAudio;
        private System.Windows.Forms.ToolStripSeparator menuCaptureSep2;
        private System.Windows.Forms.ToolStripMenuItem menuCaptureAudioDiag;
        private System.Windows.Forms.ToolStripMenuItem menuNetwork;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkRefreshAdapters;
        private System.Windows.Forms.ToolStripSeparator menuNetworkSep1;
        internal System.Windows.Forms.ToolStripMenuItem menuNetworkPinProtection;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkGenPin;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkCopyPin;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpWebsite;
        private System.Windows.Forms.ToolStripMenuItem menuHelpGitHub;
        private System.Windows.Forms.ToolStripMenuItem menuHelpBuyMeCoffee;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep1;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
    }
}
