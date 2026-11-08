namespace ScreenTask
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isGracefulShutdownComplete)
                {
                    _isGracefulShutdownComplete = true;
                    try { _statsTimer.Stop(); _statsTimer.Dispose(); } catch { }
                    try { _deviceChangeTimer.Stop(); _deviceChangeTimer.Dispose(); } catch { }
                    try { _thumbnailTimer.Stop(); _thumbnailTimer.Dispose(); } catch { }
                    try { _captureEngine?.Dispose(); } catch { }
                    try { _broadcaster?.Dispose(); } catch { }
                    try { appNotify?.Dispose(); } catch { }
                }
                if (components != null)
                {
                    components.Dispose();
                }
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

            // Header Elements
            this.panelHeader = new System.Windows.Forms.Panel();
            this.picAppIcon = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();

            // Modern Navigation Bar
            this.panelSideNav = new System.Windows.Forms.Panel();
            this.btnNavBroadcast = new System.Windows.Forms.Button();
            this.btnNavClients = new System.Windows.Forms.Button();
            this.btnNavCapture = new System.Windows.Forms.Button();
            this.btnNavNetwork = new System.Windows.Forms.Button();
            this.btnNavPrefs = new System.Windows.Forms.Button();

            // Central Content Container
            this.panelContentContainer = new System.Windows.Forms.Panel();

            // ── VIEW 0: Broadcast Console ──
            this.panelViewBroadcast = new System.Windows.Forms.Panel();
            this.cardPreview = new System.Windows.Forms.Panel();
            this.monitorPreview = new ScreenTask.UI.MonitorPreview();
            this.comboScreens = new System.Windows.Forms.ComboBox();
            this.btnRefreshScreens = new System.Windows.Forms.Button();
            this.btnPopout = new System.Windows.Forms.Button();
            this.btnSwitchSource = new System.Windows.Forms.Button();

            this.groupBox3 = new System.Windows.Forms.Panel();
            this.lblBroadcastDesc = new System.Windows.Forms.Label();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnQuickMute = new System.Windows.Forms.Button();
            this.btnAudioSetup = new System.Windows.Forms.Button();
            this.lblAudioMeterText = new System.Windows.Forms.Label();
            this.pbAudioMeter = new System.Windows.Forms.ProgressBar();
            this.lblLiveStats = new System.Windows.Forms.Label();
            this.btnFreeze = new System.Windows.Forms.Button();

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

            // ── VIEW 1: Connected Viewers & Fleet ──
            this.panelViewClients = new System.Windows.Forms.Panel();
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

            // ── VIEW 2: Video Sources ──
            this.panelViewCapture = new System.Windows.Forms.Panel();
            this.panelCaptureHeader = new System.Windows.Forms.Panel();
            this.panelCaptureFooter = new System.Windows.Forms.Panel();
            this.lblCaptureTitle = new System.Windows.Forms.Label();
            this.lblCaptureSubtitle = new System.Windows.Forms.Label();
            this.btnRefreshSourcesTab = new System.Windows.Forms.Button();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.lblSelectedSource = new System.Windows.Forms.Label();
            this.btnSelectSource = new System.Windows.Forms.Button();
            this.tabControlSources = new System.Windows.Forms.TabControl();
            this.tabDisplays = new System.Windows.Forms.TabPage();
            this.flowDisplays = new System.Windows.Forms.FlowLayoutPanel();
            this.tabWindows = new System.Windows.Forms.TabPage();
            this.flowWindows = new System.Windows.Forms.FlowLayoutPanel();

            // ── VIEW 3: Network & Security ──
            this.panelViewNetwork = new System.Windows.Forms.Panel();
            this.cardNetworkInterface = new System.Windows.Forms.Panel();
            this.lblNetworkTitle = new System.Windows.Forms.Label();
            this.lblNetworkSubtitle = new System.Windows.Forms.Label();
            this.lblAdapterDesc = new System.Windows.Forms.Label();
            this.comboIPs = new System.Windows.Forms.ComboBox();
            this.btnRefreshIPs = new System.Windows.Forms.Button();
            this.lblPortDesc = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.lblPortStatus = new System.Windows.Forms.Label();
            this.lblAdapterHint = new System.Windows.Forms.Label();

            this.cardNetworkPin = new System.Windows.Forms.Panel();
            this.lblPinTitle = new System.Windows.Forms.Label();
            this.lblPinSubtitle = new System.Windows.Forms.Label();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.lblPinCodeDesc = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnGenPin = new System.Windows.Forms.Button();
            this.btnCopyPin = new System.Windows.Forms.Button();
            this.lblNetworkPreviewUrlTitle = new System.Windows.Forms.Label();
            this.lblNetworkPreviewUrl = new System.Windows.Forms.Label();

            // ── VIEW 4: Preferences, Audio & Logs ──
            this.panelViewPrefs = new System.Windows.Forms.Panel();
            this.cardGeneralPrefs = new System.Windows.Forms.Panel();
            this.lblPrefsTitle = new System.Windows.Forms.Label();
            this.lblPrefsSubtitle = new System.Windows.Forms.Label();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.cbMiniStart = new System.Windows.Forms.CheckBox();
            this.cbCloseToTray = new System.Windows.Forms.CheckBox();
            this.lblFpsDesc = new System.Windows.Forms.Label();
            this.comboFps = new System.Windows.Forms.ComboBox();
            this.lblQualityDesc = new System.Windows.Forms.Label();
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.lblQuality = new System.Windows.Forms.Label();

            this.cardAudioDiag = new System.Windows.Forms.Panel();
            this.lblAudioDiagTitle = new System.Windows.Forms.Label();
            this.lblAudioDiagSubtitle = new System.Windows.Forms.Label();
            this.cbAudio = new System.Windows.Forms.CheckBox();
            this.lblAudioDeviceDesc = new System.Windows.Forms.Label();
            this.comboAudioDevices = new System.Windows.Forms.ComboBox();
            this.btnRefreshAudio = new System.Windows.Forms.Button();
            this.lblAudioDiagMeterText = new System.Windows.Forms.Label();
            this.pbAudioDiagMeter = new System.Windows.Forms.ProgressBar();
            this.btnTestSpeaker = new System.Windows.Forms.Button();
            this.btnTestMic = new System.Windows.Forms.Button();
            this.btnTestAudio = new System.Windows.Forms.Button();
            this.btnAudioDiag = new System.Windows.Forms.Button();
            this.lblAudioDiagInfo = new System.Windows.Forms.Label();

            this.cardLogs = new System.Windows.Forms.Panel();
            this.lblLogsTitle = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnCopyLog = new System.Windows.Forms.Button();

            // Status Strip & Tray
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
            this.panelHeader.SuspendLayout();
            this.panelSideNav.SuspendLayout();
            this.panelContentContainer.SuspendLayout();
            this.panelViewBroadcast.SuspendLayout();
            this.cardPreview.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cardUrl.SuspendLayout();
            this.panelViewClients.SuspendLayout();
            this.cardMetricViewers.SuspendLayout();
            this.cardMetricLatency.SuspendLayout();
            this.cardMetricSync.SuspendLayout();
            this.cardFleetTerminal.SuspendLayout();
            this.panelSessionList.SuspendLayout();
            this.cardPairingPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPairingQr)).BeginInit();
            this.panelViewCapture.SuspendLayout();
            this.panelCaptureHeader.SuspendLayout();
            this.tabControlSources.SuspendLayout();
            this.tabDisplays.SuspendLayout();
            this.tabWindows.SuspendLayout();
            this.panelViewNetwork.SuspendLayout();
            this.cardNetworkInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.cardNetworkPin.SuspendLayout();
            this.panelViewPrefs.SuspendLayout();
            this.cardGeneralPrefs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.cardAudioDiag.SuspendLayout();
            this.cardLogs.SuspendLayout();
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
                this.menuFileExit
            });
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";

            this.menuFileToggleBroadcast.Name = "menuFileToggleBroadcast";
            this.menuFileToggleBroadcast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuFileToggleBroadcast.Size = new System.Drawing.Size(209, 22);
            this.menuFileToggleBroadcast.Text = "▶ Start Broadcast";

            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(206, 6);

            this.menuFileCopyLink.Name = "menuFileCopyLink";
            this.menuFileCopyLink.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuFileCopyLink.Size = new System.Drawing.Size(209, 22);
            this.menuFileCopyLink.Text = "📋 Copy Stream Link";

            this.menuFileShowQr.Name = "menuFileShowQr";
            this.menuFileShowQr.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.menuFileShowQr.Size = new System.Drawing.Size(209, 22);
            this.menuFileShowQr.Text = "📱 Show QR Code...";

            this.menuFileOpenBrowser.Name = "menuFileOpenBrowser";
            this.menuFileOpenBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpenBrowser.Size = new System.Drawing.Size(209, 22);
            this.menuFileOpenBrowser.Text = "🌐 Open in Browser";

            this.menuFileSep2.Name = "menuFileSep2";
            this.menuFileSep2.Size = new System.Drawing.Size(206, 6);

            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuFileExit.Size = new System.Drawing.Size(209, 22);
            this.menuFileExit.Text = "❌ Exit";

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
            this.menuView.Text = "View";

            this.menuViewBroadcast.Name = "menuViewBroadcast";
            this.menuViewBroadcast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.menuViewBroadcast.Size = new System.Drawing.Size(223, 22);
            this.menuViewBroadcast.Text = "📺 Broadcast Console";

            this.menuViewViewers.Name = "menuViewViewers";
            this.menuViewViewers.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.menuViewViewers.Size = new System.Drawing.Size(223, 22);
            this.menuViewViewers.Text = "👥 Connected Viewers";

            this.menuViewCapture.Name = "menuViewCapture";
            this.menuViewCapture.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.menuViewCapture.Size = new System.Drawing.Size(223, 22);
            this.menuViewCapture.Text = "🎥 Video Sources";

            this.menuViewNetwork.Name = "menuViewNetwork";
            this.menuViewNetwork.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.menuViewNetwork.Size = new System.Drawing.Size(223, 22);
            this.menuViewNetwork.Text = "🌐 Network & Security";

            this.menuViewPrefs.Name = "menuViewPrefs";
            this.menuViewPrefs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.menuViewPrefs.Size = new System.Drawing.Size(223, 22);
            this.menuViewPrefs.Text = "⚙️ Preferences & Audio";

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
            this.menuCapture.Text = "Capture";

            this.menuCaptureRefreshSources.Name = "menuCaptureRefreshSources";
            this.menuCaptureRefreshSources.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuCaptureRefreshSources.Size = new System.Drawing.Size(206, 22);
            this.menuCaptureRefreshSources.Text = "🔄 Refresh Sources";

            this.menuCaptureSep1.Name = "menuCaptureSep1";
            this.menuCaptureSep1.Size = new System.Drawing.Size(203, 6);

            this.menuCaptureShowMouse.CheckOnClick = true;
            this.menuCaptureShowMouse.Name = "menuCaptureShowMouse";
            this.menuCaptureShowMouse.Size = new System.Drawing.Size(206, 22);
            this.menuCaptureShowMouse.Text = "🖱️ Capture Mouse Cursor";

            this.menuCaptureEnableAudio.CheckOnClick = true;
            this.menuCaptureEnableAudio.Name = "menuCaptureEnableAudio";
            this.menuCaptureEnableAudio.Size = new System.Drawing.Size(206, 22);
            this.menuCaptureEnableAudio.Text = "🔊 Stream Audio";

            this.menuCaptureSep2.Name = "menuCaptureSep2";
            this.menuCaptureSep2.Size = new System.Drawing.Size(203, 6);

            this.menuCaptureAudioDiag.Name = "menuCaptureAudioDiag";
            this.menuCaptureAudioDiag.Size = new System.Drawing.Size(206, 22);
            this.menuCaptureAudioDiag.Text = "🎙️ Audio Diagnostics...";

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
            this.menuNetwork.Text = "Network";

            this.menuNetworkRefreshAdapters.Name = "menuNetworkRefreshAdapters";
            this.menuNetworkRefreshAdapters.Size = new System.Drawing.Size(204, 22);
            this.menuNetworkRefreshAdapters.Text = "🔄 Refresh Adapters";

            this.menuNetworkSep1.Name = "menuNetworkSep1";
            this.menuNetworkSep1.Size = new System.Drawing.Size(201, 6);

            this.menuNetworkPinProtection.CheckOnClick = true;
            this.menuNetworkPinProtection.Name = "menuNetworkPinProtection";
            this.menuNetworkPinProtection.Size = new System.Drawing.Size(204, 22);
            this.menuNetworkPinProtection.Text = "🔒 Require PIN Code";

            this.menuNetworkGenPin.Name = "menuNetworkGenPin";
            this.menuNetworkGenPin.Size = new System.Drawing.Size(204, 22);
            this.menuNetworkGenPin.Text = "🎲 Generate New PIN";

            this.menuNetworkCopyPin.Name = "menuNetworkCopyPin";
            this.menuNetworkCopyPin.Size = new System.Drawing.Size(204, 22);
            this.menuNetworkCopyPin.Text = "📋 Copy PIN";

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
            this.menuHelp.Text = "Help";

            this.menuHelpWebsite.Name = "menuHelpWebsite";
            this.menuHelpWebsite.Size = new System.Drawing.Size(211, 22);
            this.menuHelpWebsite.Text = "🌐 ScreenTask Website";

            this.menuHelpGitHub.Name = "menuHelpGitHub";
            this.menuHelpGitHub.Size = new System.Drawing.Size(211, 22);
            this.menuHelpGitHub.Text = "🐙 GitHub Repository";

            this.menuHelpBuyMeCoffee.Name = "menuHelpBuyMeCoffee";
            this.menuHelpBuyMeCoffee.Size = new System.Drawing.Size(211, 22);
            this.menuHelpBuyMeCoffee.Text = "☕ Buy Me a Coffee";

            this.menuHelpSep1.Name = "menuHelpSep1";
            this.menuHelpSep1.Size = new System.Drawing.Size(208, 6);

            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.menuHelpAbout.Size = new System.Drawing.Size(211, 22);
            this.menuHelpAbout.Text = "About ScreenTask 2.0...";

            // ── Top Header Area ─────────────────────────────────────────────
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 44;
            this.panelHeader.BackColor = System.Drawing.SystemColors.Control;
            this.panelHeader.Controls.Add(this.picAppIcon);
            this.panelHeader.Controls.Add(this.lblAppTitle);
            this.panelHeader.Controls.Add(this.lblAppSubtitle);
            this.panelHeader.Controls.Add(this.lblStatusBadge);

            this.picAppIcon.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.picAppIcon.Location = new System.Drawing.Point(12, 6);
            this.picAppIcon.Name = "picAppIcon";
            this.picAppIcon.Size = new System.Drawing.Size(32, 32);
            this.picAppIcon.TabIndex = 0;
            this.picAppIcon.Text = "📺";

            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAppTitle.Location = new System.Drawing.Point(46, 4);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(107, 20);
            this.lblAppTitle.TabIndex = 1;
            this.lblAppTitle.Text = "ScreenTask 2.0";

            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAppSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAppSubtitle.Location = new System.Drawing.Point(47, 24);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(175, 12);
            this.lblAppSubtitle.TabIndex = 2;
            this.lblAppSubtitle.Text = "Offline and LAN Screen Sharing Broadcast";

            this.lblStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusBadge.AutoSize = true;
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblStatusBadge.Location = new System.Drawing.Point(920, 14);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(53, 13);
            this.lblStatusBadge.TabIndex = 5;
            this.lblStatusBadge.Text = "● READY";

            // ── Modern Navigation Bar ───────────────────────────────────────
            this.panelSideNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSideNav.Height = 36;
            this.panelSideNav.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelSideNav.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.panelSideNav.Name = "panelSideNav";

            this.btnNavBroadcast.Text = "📺 Broadcast";
            this.btnNavBroadcast.Name = "btnNavBroadcast";
            this.btnNavBroadcast.Size = new System.Drawing.Size(150, 30);
            this.btnNavBroadcast.Location = new System.Drawing.Point(8, 3);
            this.btnNavBroadcast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavBroadcast.FlatAppearance.BorderSize = 0;
            this.btnNavBroadcast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavBroadcast.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.btnNavClients.Text = "👥 Viewers & Fleet";
            this.btnNavClients.Name = "btnNavClients";
            this.btnNavClients.Size = new System.Drawing.Size(150, 30);
            this.btnNavClients.Location = new System.Drawing.Point(162, 3);
            this.btnNavClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavClients.FlatAppearance.BorderSize = 0;
            this.btnNavClients.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavClients.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnNavCapture.Text = "🎥 Video Sources";
            this.btnNavCapture.Name = "btnNavCapture";
            this.btnNavCapture.Size = new System.Drawing.Size(150, 30);
            this.btnNavCapture.Location = new System.Drawing.Point(316, 3);
            this.btnNavCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavCapture.FlatAppearance.BorderSize = 0;
            this.btnNavCapture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavCapture.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnNavNetwork.Text = "🌐 Network & PIN";
            this.btnNavNetwork.Name = "btnNavNetwork";
            this.btnNavNetwork.Size = new System.Drawing.Size(150, 30);
            this.btnNavNetwork.Location = new System.Drawing.Point(470, 3);
            this.btnNavNetwork.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavNetwork.FlatAppearance.BorderSize = 0;
            this.btnNavNetwork.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavNetwork.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.btnNavPrefs.Text = "⚙️ Preferences & Audio";
            this.btnNavPrefs.Name = "btnNavPrefs";
            this.btnNavPrefs.Size = new System.Drawing.Size(165, 30);
            this.btnNavPrefs.Location = new System.Drawing.Point(624, 3);
            this.btnNavPrefs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavPrefs.FlatAppearance.BorderSize = 0;
            this.btnNavPrefs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavPrefs.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.panelSideNav.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnNavBroadcast,
                this.btnNavClients,
                this.btnNavCapture,
                this.btnNavNetwork,
                this.btnNavPrefs
            });

            // ── Central Content Container ───────────────────────────────────
            this.panelContentContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentContainer.Name = "panelContentContainer";
            this.panelContentContainer.Padding = new System.Windows.Forms.Padding(8);
            this.panelContentContainer.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.panelViewBroadcast,
                this.panelViewClients,
                this.panelViewCapture,
                this.panelViewNetwork,
                this.panelViewPrefs
            });

            // ═════════════════════════════════════════════════════════════════
            // VIEW 0: Broadcast Console
            // ═════════════════════════════════════════════════════════════════
            this.panelViewBroadcast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewBroadcast.Name = "panelViewBroadcast";
            this.panelViewBroadcast.Visible = true;

            // 1. Preview Card
            this.cardPreview.BackColor = System.Drawing.SystemColors.Window;
            this.cardPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPreview.Location = new System.Drawing.Point(0, 0);
            this.cardPreview.Name = "cardPreview";
            this.cardPreview.Size = new System.Drawing.Size(700, 360);
            this.cardPreview.TabIndex = 0;

            this.monitorPreview.Location = new System.Drawing.Point(12, 12);
            this.monitorPreview.Name = "monitorPreview";
            this.monitorPreview.Size = new System.Drawing.Size(670, 300);
            this.monitorPreview.TabIndex = 0;

            this.comboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboScreens.Location = new System.Drawing.Point(12, 322);
            this.comboScreens.Name = "comboScreens";
            this.comboScreens.Size = new System.Drawing.Size(220, 21);
            this.comboScreens.TabIndex = 1;

            this.btnRefreshScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshScreens.Location = new System.Drawing.Point(238, 321);
            this.btnRefreshScreens.Name = "btnRefreshScreens";
            this.btnRefreshScreens.Size = new System.Drawing.Size(28, 24);
            this.btnRefreshScreens.TabIndex = 2;
            this.btnRefreshScreens.Text = "🔄";
            this.btnRefreshScreens.UseVisualStyleBackColor = true;

            this.btnPopout.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnPopout.Location = new System.Drawing.Point(272, 321);
            this.btnPopout.Name = "btnPopout";
            this.btnPopout.Size = new System.Drawing.Size(125, 24);
            this.btnPopout.TabIndex = 3;
            this.btnPopout.Text = "⤢ Expand Preview";
            this.btnPopout.UseVisualStyleBackColor = true;

            this.btnSwitchSource.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSwitchSource.Location = new System.Drawing.Point(402, 321);
            this.btnSwitchSource.Name = "btnSwitchSource";
            this.btnSwitchSource.Size = new System.Drawing.Size(130, 24);
            this.btnSwitchSource.TabIndex = 4;
            this.btnSwitchSource.Text = "🎥 Switch Source...";
            this.btnSwitchSource.UseVisualStyleBackColor = true;

            this.cardPreview.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.monitorPreview,
                this.comboScreens,
                this.btnRefreshScreens,
                this.btnPopout,
                this.btnSwitchSource
            });

            // 2. Broadcast Control Card (groupBox3)
            this.groupBox3.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBox3.Location = new System.Drawing.Point(710, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 360);
            this.groupBox3.TabIndex = 1;

            this.lblBroadcastDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBroadcastDesc.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblBroadcastDesc.Location = new System.Drawing.Point(12, 12);
            this.lblBroadcastDesc.Name = "lblBroadcastDesc";
            this.lblBroadcastDesc.Size = new System.Drawing.Size(244, 32);
            this.lblBroadcastDesc.Text = "Share your screen over local Wi-Fi or LAN with zero setup.";

            this.btnStartServer.BackColor = System.Drawing.Color.FromArgb(16, 124, 65);
            this.btnStartServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartServer.FlatAppearance.BorderSize = 0;
            this.btnStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartServer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.Location = new System.Drawing.Point(12, 48);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(244, 40);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "▶ START BROADCAST";
            this.btnStartServer.UseVisualStyleBackColor = false;

            this.btnQuickMute.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnQuickMute.Location = new System.Drawing.Point(12, 94);
            this.btnQuickMute.Name = "btnQuickMute";
            this.btnQuickMute.Size = new System.Drawing.Size(118, 26);
            this.btnQuickMute.TabIndex = 1;
            this.btnQuickMute.Text = "🔊 Audio: On";
            this.btnQuickMute.UseVisualStyleBackColor = true;

            this.btnAudioSetup.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAudioSetup.Location = new System.Drawing.Point(136, 94);
            this.btnAudioSetup.Name = "btnAudioSetup";
            this.btnAudioSetup.Size = new System.Drawing.Size(120, 26);
            this.btnAudioSetup.TabIndex = 2;
            this.btnAudioSetup.Text = "🎙 Audio Setup...";
            this.btnAudioSetup.UseVisualStyleBackColor = true;

            this.lblAudioMeterText.AutoSize = true;
            this.lblAudioMeterText.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioMeterText.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioMeterText.Location = new System.Drawing.Point(12, 126);
            this.lblAudioMeterText.Name = "lblAudioMeterText";
            this.lblAudioMeterText.Size = new System.Drawing.Size(107, 12);
            this.lblAudioMeterText.Text = "Sound Volume: Silent";

            this.pbAudioMeter.Location = new System.Drawing.Point(12, 142);
            this.pbAudioMeter.Name = "pbAudioMeter";
            this.pbAudioMeter.Size = new System.Drawing.Size(244, 10);
            this.pbAudioMeter.TabIndex = 3;

            this.lblLiveStats.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLiveStats.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblLiveStats.Location = new System.Drawing.Point(12, 160);
            this.lblLiveStats.Name = "lblLiveStats";
            this.lblLiveStats.Size = new System.Drawing.Size(244, 140);
            this.lblLiveStats.Text = "Ready to stream.\nClick Start Broadcast to begin.";

            this.btnFreeze.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnFreeze.Location = new System.Drawing.Point(12, 316);
            this.btnFreeze.Name = "btnFreeze";
            this.btnFreeze.Size = new System.Drawing.Size(244, 28);
            this.btnFreeze.TabIndex = 4;
            this.btnFreeze.Text = "⏸ Freeze Screen";
            this.btnFreeze.Enabled = false;
            this.btnFreeze.UseVisualStyleBackColor = true;

            this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBroadcastDesc,
                this.btnStartServer,
                this.btnQuickMute,
                this.btnAudioSetup,
                this.lblAudioMeterText,
                this.pbAudioMeter,
                this.lblLiveStats,
                this.btnFreeze
            });

            // 3. Stream URL Card
            this.cardUrl.BackColor = System.Drawing.SystemColors.Window;
            this.cardUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardUrl.Location = new System.Drawing.Point(0, 370);
            this.cardUrl.Name = "cardUrl";
            this.cardUrl.Size = new System.Drawing.Size(980, 86);
            this.cardUrl.TabIndex = 2;

            this.lblUrlTitle.AutoSize = true;
            this.lblUrlTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUrlTitle.Location = new System.Drawing.Point(12, 10);
            this.lblUrlTitle.Name = "lblUrlTitle";
            this.lblUrlTitle.Size = new System.Drawing.Size(147, 15);
            this.lblUrlTitle.Text = "📡 Stream Connection URL:";

            this.lblWifiBadge.AutoSize = true;
            this.lblWifiBadge.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblWifiBadge.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblWifiBadge.Location = new System.Drawing.Point(170, 12);
            this.lblWifiBadge.Name = "lblWifiBadge";
            this.lblWifiBadge.Size = new System.Drawing.Size(160, 12);
            this.lblWifiBadge.Text = "Share this URL with viewers on LAN";

            this.lblPinDisplay.AutoSize = true;
            this.lblPinDisplay.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPinDisplay.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPinDisplay.Location = new System.Drawing.Point(540, 12);
            this.lblPinDisplay.Name = "lblPinDisplay";
            this.lblPinDisplay.Size = new System.Drawing.Size(112, 14);
            this.lblPinDisplay.Text = "PIN: Not Required";

            this.btnNetworkSettings.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.btnNetworkSettings.Location = new System.Drawing.Point(860, 8);
            this.btnNetworkSettings.Name = "btnNetworkSettings";
            this.btnNetworkSettings.Size = new System.Drawing.Size(108, 22);
            this.btnNetworkSettings.TabIndex = 0;
            this.btnNetworkSettings.Text = "⚙️ Network & PIN...";
            this.btnNetworkSettings.UseVisualStyleBackColor = true;

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.Text = "Link: 🔗";

            this.txtURL.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtURL.Location = new System.Drawing.Point(62, 38);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(560, 21);
            this.txtURL.TabIndex = 1;

            this.btnCopyUrl.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyUrl.Location = new System.Drawing.Point(630, 36);
            this.btnCopyUrl.Name = "btnCopyUrl";
            this.btnCopyUrl.Size = new System.Drawing.Size(105, 26);
            this.btnCopyUrl.TabIndex = 2;
            this.btnCopyUrl.Text = "📋 Copy Link";
            this.btnCopyUrl.UseVisualStyleBackColor = true;

            this.btnQr.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnQr.Location = new System.Drawing.Point(740, 36);
            this.btnQr.Name = "btnQr";
            this.btnQr.Size = new System.Drawing.Size(110, 26);
            this.btnQr.TabIndex = 3;
            this.btnQr.Text = "📱 QR Code...";
            this.btnQr.UseVisualStyleBackColor = true;

            this.btnLaunch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLaunch.Location = new System.Drawing.Point(855, 36);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(113, 26);
            this.btnLaunch.TabIndex = 4;
            this.btnLaunch.Text = "🌐 Open Browser";
            this.btnLaunch.UseVisualStyleBackColor = true;

            this.cardUrl.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblUrlTitle,
                this.lblWifiBadge,
                this.lblPinDisplay,
                this.btnNetworkSettings,
                this.label1,
                this.txtURL,
                this.btnCopyUrl,
                this.btnQr,
                this.btnLaunch
            });

            this.panelViewBroadcast.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cardPreview,
                this.groupBox3,
                this.cardUrl
            });

            // ═════════════════════════════════════════════════════════════════
            // VIEW 1: Connected Viewers & Fleet
            // ═════════════════════════════════════════════════════════════════
            this.panelViewClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewClients.Name = "panelViewClients";
            this.panelViewClients.Visible = false;

            // Metric Viewers
            this.cardMetricViewers.BackColor = System.Drawing.SystemColors.Window;
            this.cardMetricViewers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMetricViewers.Location = new System.Drawing.Point(0, 0);
            this.cardMetricViewers.Name = "cardMetricViewers";
            this.cardMetricViewers.Size = new System.Drawing.Size(315, 72);

            this.lblIconMetricViewers.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconMetricViewers.Location = new System.Drawing.Point(8, 8);
            this.lblIconMetricViewers.Name = "lblIconMetricViewers";
            this.lblIconMetricViewers.Size = new System.Drawing.Size(24, 22);
            this.lblIconMetricViewers.Text = "👥";

            this.lblTitleMetricViewers.AutoSize = true;
            this.lblTitleMetricViewers.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleMetricViewers.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleMetricViewers.Location = new System.Drawing.Point(34, 10);
            this.lblTitleMetricViewers.Name = "lblTitleMetricViewers";
            this.lblTitleMetricViewers.Size = new System.Drawing.Size(95, 12);
            this.lblTitleMetricViewers.Text = "LAN Active Viewers";

            this.lblMetricViewersVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMetricViewersVal.Location = new System.Drawing.Point(8, 30);
            this.lblMetricViewersVal.Name = "lblMetricViewersVal";
            this.lblMetricViewersVal.Size = new System.Drawing.Size(295, 20);
            this.lblMetricViewersVal.Text = "0 / 32 Active";

            this.pbViewerCapacity.Location = new System.Drawing.Point(8, 52);
            this.pbViewerCapacity.Maximum = 32;
            this.pbViewerCapacity.Name = "pbViewerCapacity";
            this.pbViewerCapacity.Size = new System.Drawing.Size(295, 8);

            this.cardMetricViewers.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconMetricViewers,
                this.lblTitleMetricViewers,
                this.lblMetricViewersVal,
                this.pbViewerCapacity
            });

            // Metric Latency
            this.cardMetricLatency.BackColor = System.Drawing.SystemColors.Window;
            this.cardMetricLatency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMetricLatency.Location = new System.Drawing.Point(325, 0);
            this.cardMetricLatency.Name = "cardMetricLatency";
            this.cardMetricLatency.Size = new System.Drawing.Size(315, 72);

            this.lblIconMetricLatency.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconMetricLatency.Location = new System.Drawing.Point(8, 8);
            this.lblIconMetricLatency.Name = "lblIconMetricLatency";
            this.lblIconMetricLatency.Size = new System.Drawing.Size(24, 22);
            this.lblIconMetricLatency.Text = "⚡";

            this.lblTitleMetricLatency.AutoSize = true;
            this.lblTitleMetricLatency.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleMetricLatency.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleMetricLatency.Location = new System.Drawing.Point(34, 10);
            this.lblTitleMetricLatency.Name = "lblTitleMetricLatency";
            this.lblTitleMetricLatency.Size = new System.Drawing.Size(107, 12);
            this.lblTitleMetricLatency.Text = "Fleet Average Latency";

            this.lblMetricLatencyVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMetricLatencyVal.Location = new System.Drawing.Point(8, 30);
            this.lblMetricLatencyVal.Name = "lblMetricLatencyVal";
            this.lblMetricLatencyVal.Size = new System.Drawing.Size(295, 20);
            this.lblMetricLatencyVal.Text = "<1.0 ms Avg";

            this.lblMetricLatencySub.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblMetricLatencySub.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMetricLatencySub.Location = new System.Drawing.Point(8, 52);
            this.lblMetricLatencySub.Name = "lblMetricLatencySub";
            this.lblMetricLatencySub.Size = new System.Drawing.Size(295, 16);
            this.lblMetricLatencySub.Text = "<50ms pipeline • 0.0% frame drop";

            this.cardMetricLatency.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconMetricLatency,
                this.lblTitleMetricLatency,
                this.lblMetricLatencyVal,
                this.lblMetricLatencySub
            });

            // Metric Sync
            this.cardMetricSync.BackColor = System.Drawing.SystemColors.Window;
            this.cardMetricSync.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardMetricSync.Location = new System.Drawing.Point(650, 0);
            this.cardMetricSync.Name = "cardMetricSync";
            this.cardMetricSync.Size = new System.Drawing.Size(330, 72);

            this.lblIconMetricSync.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIconMetricSync.Location = new System.Drawing.Point(8, 8);
            this.lblIconMetricSync.Name = "lblIconMetricSync";
            this.lblIconMetricSync.Size = new System.Drawing.Size(24, 22);
            this.lblIconMetricSync.Text = "🎵";

            this.lblTitleMetricSync.AutoSize = true;
            this.lblTitleMetricSync.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblTitleMetricSync.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTitleMetricSync.Location = new System.Drawing.Point(34, 10);
            this.lblTitleMetricSync.Name = "lblTitleMetricSync";
            this.lblTitleMetricSync.Size = new System.Drawing.Size(107, 12);
            this.lblTitleMetricSync.Text = "Audio/Video Lip Sync";

            this.lblMetricSyncVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMetricSyncVal.Location = new System.Drawing.Point(8, 30);
            this.lblMetricSyncVal.Name = "lblMetricSyncVal";
            this.lblMetricSyncVal.Size = new System.Drawing.Size(310, 20);
            this.lblMetricSyncVal.Text = "Standby";

            this.lblMetricSyncSub.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblMetricSyncSub.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblMetricSyncSub.Location = new System.Drawing.Point(8, 52);
            this.lblMetricSyncSub.Name = "lblMetricSyncSub";
            this.lblMetricSyncSub.Size = new System.Drawing.Size(310, 16);
            this.lblMetricSyncSub.Text = "Audio/Video pipeline idle";

            this.cardMetricSync.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblIconMetricSync,
                this.lblTitleMetricSync,
                this.lblMetricSyncVal,
                this.lblMetricSyncSub
            });

            // Fleet Terminal Left Card
            this.cardFleetTerminal.BackColor = System.Drawing.SystemColors.Window;
            this.cardFleetTerminal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardFleetTerminal.Location = new System.Drawing.Point(0, 80);
            this.cardFleetTerminal.Name = "cardFleetTerminal";
            this.cardFleetTerminal.Size = new System.Drawing.Size(650, 460);

            this.lblFleetHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFleetHeader.Location = new System.Drawing.Point(10, 8);
            this.lblFleetHeader.Name = "lblFleetHeader";
            this.lblFleetHeader.Size = new System.Drawing.Size(400, 20);
            this.lblFleetHeader.Text = "Live Client Sessions (0 Connected):";

            this.panelSessionList.AutoScroll = true;
            this.panelSessionList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelSessionList.Location = new System.Drawing.Point(10, 32);
            this.panelSessionList.Name = "panelSessionList";
            this.panelSessionList.Size = new System.Drawing.Size(628, 340);

            this.lblNoClientsPlaceholder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblNoClientsPlaceholder.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNoClientsPlaceholder.Location = new System.Drawing.Point(10, 130);
            this.lblNoClientsPlaceholder.Name = "lblNoClientsPlaceholder";
            this.lblNoClientsPlaceholder.Size = new System.Drawing.Size(608, 40);
            this.lblNoClientsPlaceholder.Text = "No viewers currently connected. Share the link or scan the QR pass on the right to start watching.";
            this.lblNoClientsPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.panelSessionList.Controls.Add(this.lblNoClientsPlaceholder);

            this.lblAnnouncement.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAnnouncement.Location = new System.Drawing.Point(10, 380);
            this.lblAnnouncement.Name = "lblAnnouncement";
            this.lblAnnouncement.Size = new System.Drawing.Size(250, 16);
            this.lblAnnouncement.Text = "📢 Dispatch Broadcast Alert / Toast:";

            this.txtAnnouncement.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtAnnouncement.Location = new System.Drawing.Point(10, 398);
            this.txtAnnouncement.Name = "txtAnnouncement";
            this.txtAnnouncement.PlaceholderText = "Type announcement for all connected viewers...";
            this.txtAnnouncement.Size = new System.Drawing.Size(515, 23);

            this.btnSendAnnouncement.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSendAnnouncement.Location = new System.Drawing.Point(532, 397);
            this.btnSendAnnouncement.Name = "btnSendAnnouncement";
            this.btnSendAnnouncement.Size = new System.Drawing.Size(106, 25);
            this.btnSendAnnouncement.Text = "Send Toast";
            this.btnSendAnnouncement.UseVisualStyleBackColor = true;

            this.lblAnnouncementStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAnnouncementStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAnnouncementStatus.Location = new System.Drawing.Point(10, 426);
            this.lblAnnouncementStatus.Name = "lblAnnouncementStatus";
            this.lblAnnouncementStatus.Size = new System.Drawing.Size(628, 16);
            this.lblAnnouncementStatus.Text = "Alerts appear as live animated banners on all client screens.";

            this.cardFleetTerminal.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFleetHeader,
                this.panelSessionList,
                this.lblAnnouncement,
                this.txtAnnouncement,
                this.btnSendAnnouncement,
                this.lblAnnouncementStatus
            });

            // Pairing Pass Right Card
            this.cardPairingPass.BackColor = System.Drawing.SystemColors.Window;
            this.cardPairingPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPairingPass.Location = new System.Drawing.Point(660, 80);
            this.cardPairingPass.Name = "cardPairingPass";
            this.cardPairingPass.Size = new System.Drawing.Size(320, 460);

            this.picPairingQr.BackColor = System.Drawing.Color.White;
            this.picPairingQr.Location = new System.Drawing.Point(80, 16);
            this.picPairingQr.Name = "picPairingQr";
            this.picPairingQr.Size = new System.Drawing.Size(160, 160);
            this.picPairingQr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.lblPairingUrlTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblPairingUrlTitle.Location = new System.Drawing.Point(12, 190);
            this.lblPairingUrlTitle.Name = "lblPairingUrlTitle";
            this.lblPairingUrlTitle.Size = new System.Drawing.Size(296, 16);
            this.lblPairingUrlTitle.Text = "Direct Web Viewer URL:";

            this.txtPairingUrl.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.txtPairingUrl.Location = new System.Drawing.Point(12, 210);
            this.txtPairingUrl.Name = "txtPairingUrl";
            this.txtPairingUrl.ReadOnly = true;
            this.txtPairingUrl.Size = new System.Drawing.Size(215, 21);

            this.btnPairingCopy.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.btnPairingCopy.Location = new System.Drawing.Point(235, 209);
            this.btnPairingCopy.Name = "btnPairingCopy";
            this.btnPairingCopy.Size = new System.Drawing.Size(72, 23);
            this.btnPairingCopy.Text = "📋 Copy";
            this.btnPairingCopy.UseVisualStyleBackColor = true;

            this.lblPairingPin.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.lblPairingPin.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblPairingPin.Location = new System.Drawing.Point(12, 244);
            this.lblPairingPin.Name = "lblPairingPin";
            this.lblPairingPin.Size = new System.Drawing.Size(215, 20);
            this.lblPairingPin.Text = "PIN: Not Required";

            this.btnPairingRegen.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.btnPairingRegen.Location = new System.Drawing.Point(235, 242);
            this.btnPairingRegen.Name = "btnPairingRegen";
            this.btnPairingRegen.Size = new System.Drawing.Size(72, 23);
            this.btnPairingRegen.Text = "🎲 New";
            this.btnPairingRegen.UseVisualStyleBackColor = true;

            this.lblZeroConf.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblZeroConf.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblZeroConf.Location = new System.Drawing.Point(12, 276);
            this.lblZeroConf.Name = "lblZeroConf";
            this.lblZeroConf.Size = new System.Drawing.Size(296, 40);
            this.lblZeroConf.Text = "📡 ZeroConf / mDNS: screentask.local\nAdapter: Wi-Fi";

            this.cardPairingPass.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.picPairingQr,
                this.lblPairingUrlTitle,
                this.txtPairingUrl,
                this.btnPairingCopy,
                this.lblPairingPin,
                this.btnPairingRegen,
                this.lblZeroConf
            });

            this.panelViewClients.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cardMetricViewers,
                this.cardMetricLatency,
                this.cardMetricSync,
                this.cardFleetTerminal,
                this.cardPairingPass
            });

            // ═════════════════════════════════════════════════════════════════
            // VIEW 2: Video Sources
            // ═════════════════════════════════════════════════════════════════
            this.panelViewCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewCapture.Name = "panelViewCapture";
            this.panelViewCapture.Visible = false;

            this.panelCaptureHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCaptureHeader.Height = 54;
            this.panelCaptureHeader.Name = "panelCaptureHeader";

            this.lblCaptureTitle.AutoSize = true;
            this.lblCaptureTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblCaptureTitle.Location = new System.Drawing.Point(6, 6);
            this.lblCaptureTitle.Name = "lblCaptureTitle";
            this.lblCaptureTitle.Size = new System.Drawing.Size(262, 21);
            this.lblCaptureTitle.Text = "🖥️ Select Video Source to Share";

            this.lblCaptureSubtitle.AutoSize = true;
            this.lblCaptureSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCaptureSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCaptureSubtitle.Location = new System.Drawing.Point(8, 30);
            this.lblCaptureSubtitle.Name = "lblCaptureSubtitle";
            this.lblCaptureSubtitle.Size = new System.Drawing.Size(360, 13);
            this.lblCaptureSubtitle.Text = "Choose a physical display monitor or an individual application window.";

            this.cbCaptureMouse.AutoSize = true;
            this.cbCaptureMouse.Checked = true;
            this.cbCaptureMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaptureMouse.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbCaptureMouse.Location = new System.Drawing.Point(400, 18);
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.Size = new System.Drawing.Size(155, 19);
            this.cbCaptureMouse.Text = "🖱️ Capture Mouse Cursor";

            this.lblSelectedSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedSource.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSelectedSource.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblSelectedSource.Location = new System.Drawing.Point(570, 18);
            this.lblSelectedSource.Name = "lblSelectedSource";
            this.lblSelectedSource.Size = new System.Drawing.Size(280, 20);
            this.lblSelectedSource.Text = "Selected: Display 1 (Primary)";
            this.lblSelectedSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.btnRefreshSourcesTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshSourcesTab.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshSourcesTab.Location = new System.Drawing.Point(860, 12);
            this.btnRefreshSourcesTab.Name = "btnRefreshSourcesTab";
            this.btnRefreshSourcesTab.Size = new System.Drawing.Size(115, 30);
            this.btnRefreshSourcesTab.Text = "🔄 Refresh Sources";

            this.panelCaptureHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblCaptureTitle,
                this.lblCaptureSubtitle,
                this.cbCaptureMouse,
                this.lblSelectedSource,
                this.btnRefreshSourcesTab
            });

            this.tabControlSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSources.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControlSources.Location = new System.Drawing.Point(0, 54);
            this.tabControlSources.Name = "tabControlSources";
            this.tabControlSources.SelectedIndex = 0;
            this.tabControlSources.Size = new System.Drawing.Size(980, 440);

            this.tabDisplays.Location = new System.Drawing.Point(4, 24);
            this.tabDisplays.Name = "tabDisplays";
            this.tabDisplays.Padding = new System.Windows.Forms.Padding(8);
            this.tabDisplays.Size = new System.Drawing.Size(972, 412);
            this.tabDisplays.Text = "🖥️ Displays";
            this.tabDisplays.UseVisualStyleBackColor = true;

            this.flowDisplays.AutoScroll = true;
            this.flowDisplays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDisplays.Location = new System.Drawing.Point(8, 8);
            this.flowDisplays.Name = "flowDisplays";
            this.flowDisplays.Size = new System.Drawing.Size(956, 396);

            this.tabDisplays.Controls.Add(this.flowDisplays);

            this.tabWindows.Location = new System.Drawing.Point(4, 24);
            this.tabWindows.Name = "tabWindows";
            this.tabWindows.Padding = new System.Windows.Forms.Padding(8);
            this.tabWindows.Size = new System.Drawing.Size(972, 412);
            this.tabWindows.Text = "🪟 Windows";
            this.tabWindows.UseVisualStyleBackColor = true;

            this.flowWindows.AutoScroll = true;
            this.flowWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowWindows.Location = new System.Drawing.Point(8, 8);
            this.flowWindows.Name = "flowWindows";
            this.flowWindows.Size = new System.Drawing.Size(956, 396);

            this.tabWindows.Controls.Add(this.flowWindows);

            this.tabControlSources.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.tabDisplays,
                this.tabWindows
            });

            this.panelCaptureFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCaptureFooter.Height = 46;
            this.panelCaptureFooter.Name = "panelCaptureFooter";
            this.panelCaptureFooter.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);

            this.btnSelectSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectSource.BackColor = System.Drawing.Color.FromArgb(16, 124, 65);
            this.btnSelectSource.FlatAppearance.BorderSize = 0;
            this.btnSelectSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelectSource.ForeColor = System.Drawing.Color.White;
            this.btnSelectSource.Location = new System.Drawing.Point(810, 6);
            this.btnSelectSource.Name = "btnSelectSource";
            this.btnSelectSource.Size = new System.Drawing.Size(160, 32);
            this.btnSelectSource.Text = "✓ Switch to Source";
            this.btnSelectSource.UseVisualStyleBackColor = false;

            this.panelCaptureFooter.Controls.Add(this.btnSelectSource);

            this.panelViewCapture.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.tabControlSources,
                this.panelCaptureFooter,
                this.panelCaptureHeader
            });

            // ═════════════════════════════════════════════════════════════════
            // VIEW 3: Network & Security
            // ═════════════════════════════════════════════════════════════════
            this.panelViewNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewNetwork.Name = "panelViewNetwork";
            this.panelViewNetwork.Visible = false;

            this.cardNetworkInterface.BackColor = System.Drawing.SystemColors.Window;
            this.cardNetworkInterface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardNetworkInterface.Location = new System.Drawing.Point(0, 0);
            this.cardNetworkInterface.Name = "cardNetworkInterface";
            this.cardNetworkInterface.Size = new System.Drawing.Size(480, 530);

            this.lblNetworkTitle.AutoSize = true;
            this.lblNetworkTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblNetworkTitle.Location = new System.Drawing.Point(14, 12);
            this.lblNetworkTitle.Name = "lblNetworkTitle";
            this.lblNetworkTitle.Size = new System.Drawing.Size(262, 21);
            this.lblNetworkTitle.Text = "📡 Network Interface & Adapter";

            this.lblNetworkSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNetworkSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblNetworkSubtitle.Location = new System.Drawing.Point(16, 36);
            this.lblNetworkSubtitle.Name = "lblNetworkSubtitle";
            this.lblNetworkSubtitle.Size = new System.Drawing.Size(445, 28);
            this.lblNetworkSubtitle.Text = "Select which local network adapter and port to broadcast on. Clients must be on the same network.";

            this.lblAdapterDesc.AutoSize = true;
            this.lblAdapterDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblAdapterDesc.Location = new System.Drawing.Point(16, 80);
            this.lblAdapterDesc.Name = "lblAdapterDesc";
            this.lblAdapterDesc.Size = new System.Drawing.Size(164, 15);
            this.lblAdapterDesc.Text = "Network Adapter (IP Address):";

            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboIPs.Location = new System.Drawing.Point(16, 102);
            this.comboIPs.Name = "comboIPs";
            this.comboIPs.Size = new System.Drawing.Size(370, 23);

            this.btnRefreshIPs.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshIPs.Location = new System.Drawing.Point(395, 100);
            this.btnRefreshIPs.Name = "btnRefreshIPs";
            this.btnRefreshIPs.Size = new System.Drawing.Size(65, 27);
            this.btnRefreshIPs.Text = "🔄";
            this.btnRefreshIPs.UseVisualStyleBackColor = true;

            this.lblPortDesc.AutoSize = true;
            this.lblPortDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPortDesc.Location = new System.Drawing.Point(16, 150);
            this.lblPortDesc.Name = "lblPortDesc";
            this.lblPortDesc.Size = new System.Drawing.Size(130, 15);
            this.lblPortDesc.Text = "Server Port (1 - 65535):";

            this.numPort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numPort.Location = new System.Drawing.Point(16, 172);
            this.numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(140, 23);
            this.numPort.Value = new decimal(new int[] { 7070, 0, 0, 0 });

            this.lblPortStatus.AutoSize = true;
            this.lblPortStatus.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblPortStatus.ForeColor = System.Drawing.Color.Green;
            this.lblPortStatus.Location = new System.Drawing.Point(165, 177);
            this.lblPortStatus.Name = "lblPortStatus";
            this.lblPortStatus.Size = new System.Drawing.Size(126, 12);
            this.lblPortStatus.Text = "✓ Default ScreenTask Port";

            this.lblAdapterHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAdapterHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAdapterHint.Location = new System.Drawing.Point(16, 220);
            this.lblAdapterHint.Name = "lblAdapterHint";
            this.lblAdapterHint.Size = new System.Drawing.Size(445, 120);
            this.lblAdapterHint.Text = "💡 Helpful Tips:\n• Wi-Fi or Ethernet: Best for sharing with phones, tablets, or computers on your local router.\n• Mobile Hotspot: Turn on Windows Mobile Hotspot and select the Hotspot adapter to share anywhere without a router.\n• Loopback (127.0.0.1): Use for local testing on this machine.";

            this.cardNetworkInterface.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNetworkTitle,
                this.lblNetworkSubtitle,
                this.lblAdapterDesc,
                this.comboIPs,
                this.btnRefreshIPs,
                this.lblPortDesc,
                this.numPort,
                this.lblPortStatus,
                this.lblAdapterHint
            });

            this.cardNetworkPin.BackColor = System.Drawing.SystemColors.Window;
            this.cardNetworkPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardNetworkPin.Location = new System.Drawing.Point(495, 0);
            this.cardNetworkPin.Name = "cardNetworkPin";
            this.cardNetworkPin.Size = new System.Drawing.Size(485, 530);

            this.lblPinTitle.AutoSize = true;
            this.lblPinTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblPinTitle.Location = new System.Drawing.Point(14, 12);
            this.lblPinTitle.Name = "lblPinTitle";
            this.lblPinTitle.Size = new System.Drawing.Size(287, 21);
            this.lblPinTitle.Text = "🛡️ Viewer Access & PIN Security";

            this.lblPinSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPinSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPinSubtitle.Location = new System.Drawing.Point(16, 36);
            this.lblPinSubtitle.Name = "lblPinSubtitle";
            this.lblPinSubtitle.Size = new System.Drawing.Size(450, 28);
            this.lblPinSubtitle.Text = "Optionally restrict stream access so viewers must enter a 4-digit PIN code to connect.";

            this.cbPrivate.AutoSize = true;
            this.cbPrivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cbPrivate.Location = new System.Drawing.Point(16, 80);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(193, 19);
            this.cbPrivate.Text = "🔒 Require PIN Code to Connect";

            this.lblPinCodeDesc.AutoSize = true;
            this.lblPinCodeDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPinCodeDesc.Location = new System.Drawing.Point(16, 120);
            this.lblPinCodeDesc.Name = "lblPinCodeDesc";
            this.lblPinCodeDesc.Size = new System.Drawing.Size(60, 15);
            this.lblPinCodeDesc.Text = "PIN Code:";

            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.txtPassword.Location = new System.Drawing.Point(16, 142);
            this.txtPassword.MaxLength = 10;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(180, 26);

            this.btnGenPin.Enabled = false;
            this.btnGenPin.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnGenPin.Location = new System.Drawing.Point(210, 141);
            this.btnGenPin.Name = "btnGenPin";
            this.btnGenPin.Size = new System.Drawing.Size(130, 28);
            this.btnGenPin.Text = "🎲 Generate PIN";
            this.btnGenPin.UseVisualStyleBackColor = true;

            this.btnCopyPin.Enabled = false;
            this.btnCopyPin.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnCopyPin.Location = new System.Drawing.Point(350, 141);
            this.btnCopyPin.Name = "btnCopyPin";
            this.btnCopyPin.Size = new System.Drawing.Size(100, 28);
            this.btnCopyPin.Text = "📋 Copy";
            this.btnCopyPin.UseVisualStyleBackColor = true;

            this.lblNetworkPreviewUrlTitle.AutoSize = true;
            this.lblNetworkPreviewUrlTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNetworkPreviewUrlTitle.Location = new System.Drawing.Point(16, 220);
            this.lblNetworkPreviewUrlTitle.Name = "lblNetworkPreviewUrlTitle";
            this.lblNetworkPreviewUrlTitle.Size = new System.Drawing.Size(133, 15);
            this.lblNetworkPreviewUrlTitle.Text = "Active Stream Address:";

            this.lblNetworkPreviewUrl.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.lblNetworkPreviewUrl.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblNetworkPreviewUrl.Location = new System.Drawing.Point(16, 244);
            this.lblNetworkPreviewUrl.Name = "lblNetworkPreviewUrl";
            this.lblNetworkPreviewUrl.Size = new System.Drawing.Size(450, 24);
            this.lblNetworkPreviewUrl.Text = "http://127.0.0.1:7070";

            this.cardNetworkPin.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblPinTitle,
                this.lblPinSubtitle,
                this.cbPrivate,
                this.lblPinCodeDesc,
                this.txtPassword,
                this.btnGenPin,
                this.btnCopyPin,
                this.lblNetworkPreviewUrlTitle,
                this.lblNetworkPreviewUrl
            });

            this.panelViewNetwork.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cardNetworkInterface,
                this.cardNetworkPin
            });

            // ═════════════════════════════════════════════════════════════════
            // VIEW 4: Preferences, Audio & Logs
            // ═════════════════════════════════════════════════════════════════
            this.panelViewPrefs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViewPrefs.Name = "panelViewPrefs";
            this.panelViewPrefs.Visible = false;

            // Left Card: General Preferences
            this.cardGeneralPrefs.BackColor = System.Drawing.SystemColors.Window;
            this.cardGeneralPrefs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardGeneralPrefs.Location = new System.Drawing.Point(0, 0);
            this.cardGeneralPrefs.Name = "cardGeneralPrefs";
            this.cardGeneralPrefs.Size = new System.Drawing.Size(380, 530);

            this.lblPrefsTitle.AutoSize = true;
            this.lblPrefsTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblPrefsTitle.Location = new System.Drawing.Point(14, 12);
            this.lblPrefsTitle.Name = "lblPrefsTitle";
            this.lblPrefsTitle.Size = new System.Drawing.Size(206, 21);
            this.lblPrefsTitle.Text = "⚙️ Preferences & Quality";

            this.lblPrefsSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPrefsSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPrefsSubtitle.Location = new System.Drawing.Point(16, 36);
            this.lblPrefsSubtitle.Name = "lblPrefsSubtitle";
            this.lblPrefsSubtitle.Size = new System.Drawing.Size(345, 26);
            this.lblPrefsSubtitle.Text = "Application startup and streaming performance settings.";

            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAutoStart.Location = new System.Drawing.Point(16, 75);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(224, 19);
            this.cbAutoStart.Text = "Start broadcasting automatically on open";

            this.cbMiniStart.AutoSize = true;
            this.cbMiniStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbMiniStart.Location = new System.Drawing.Point(16, 105);
            this.cbMiniStart.Name = "cbMiniStart";
            this.cbMiniStart.Size = new System.Drawing.Size(107, 19);
            this.cbMiniStart.Text = "Start minimized";

            this.cbCloseToTray.AutoSize = true;
            this.cbCloseToTray.Checked = true;
            this.cbCloseToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCloseToTray.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbCloseToTray.Location = new System.Drawing.Point(16, 135);
            this.cbCloseToTray.Name = "cbCloseToTray";
            this.cbCloseToTray.Size = new System.Drawing.Size(161, 19);
            this.cbCloseToTray.Text = "Minimize to tray on close";

            this.lblFpsDesc.AutoSize = true;
            this.lblFpsDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblFpsDesc.Location = new System.Drawing.Point(16, 185);
            this.lblFpsDesc.Name = "lblFpsDesc";
            this.lblFpsDesc.Size = new System.Drawing.Size(155, 15);
            this.lblFpsDesc.Text = "Target Streaming Framerate:";

            this.comboFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFps.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboFps.Items.AddRange(new object[] { "15 FPS", "30 FPS", "60 FPS" });
            this.comboFps.Location = new System.Drawing.Point(16, 207);
            this.comboFps.Name = "comboFps";
            this.comboFps.Size = new System.Drawing.Size(200, 23);

            this.lblQualityDesc.AutoSize = true;
            this.lblQualityDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblQualityDesc.Location = new System.Drawing.Point(16, 255);
            this.lblQualityDesc.Name = "lblQualityDesc";
            this.lblQualityDesc.Size = new System.Drawing.Size(155, 15);
            this.lblQualityDesc.Text = "JPEG Compression Quality:";

            this.qualitySlider.LargeChange = 10;
            this.qualitySlider.Location = new System.Drawing.Point(16, 277);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 20;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(280, 45);
            this.qualitySlider.TickFrequency = 10;
            this.qualitySlider.Value = 80;

            this.lblQuality.AutoSize = true;
            this.lblQuality.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuality.Location = new System.Drawing.Point(305, 285);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(30, 15);
            this.lblQuality.Text = "80%";

            this.cardGeneralPrefs.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblPrefsTitle,
                this.lblPrefsSubtitle,
                this.cbAutoStart,
                this.cbMiniStart,
                this.cbCloseToTray,
                this.lblFpsDesc,
                this.comboFps,
                this.lblQualityDesc,
                this.qualitySlider,
                this.lblQuality
            });

            // Right-Top Card: Audio Diagnostics & Setup
            this.cardAudioDiag.BackColor = System.Drawing.SystemColors.Window;
            this.cardAudioDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardAudioDiag.Location = new System.Drawing.Point(395, 0);
            this.cardAudioDiag.Name = "cardAudioDiag";
            this.cardAudioDiag.Size = new System.Drawing.Size(585, 230);

            this.lblAudioDiagTitle.AutoSize = true;
            this.lblAudioDiagTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblAudioDiagTitle.Location = new System.Drawing.Point(14, 12);
            this.lblAudioDiagTitle.Name = "lblAudioDiagTitle";
            this.lblAudioDiagTitle.Size = new System.Drawing.Size(232, 21);
            this.lblAudioDiagTitle.Text = "🎙️ Audio Setup & Diagnostics";

            this.lblAudioDiagSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblAudioDiagSubtitle.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioDiagSubtitle.Location = new System.Drawing.Point(16, 36);
            this.lblAudioDiagSubtitle.Name = "lblAudioDiagSubtitle";
            this.lblAudioDiagSubtitle.Size = new System.Drawing.Size(550, 16);
            this.lblAudioDiagSubtitle.Text = "Select and diagnose WASAPI loopback or microphone devices.";

            this.cbAudio.AutoSize = true;
            this.cbAudio.Checked = true;
            this.cbAudio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.cbAudio.Location = new System.Drawing.Point(16, 60);
            this.cbAudio.Name = "cbAudio";
            this.cbAudio.Size = new System.Drawing.Size(195, 19);
            this.cbAudio.Text = "🔊 Enable System Audio Stream";

            this.lblAudioDeviceDesc.AutoSize = true;
            this.lblAudioDeviceDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAudioDeviceDesc.Location = new System.Drawing.Point(16, 88);
            this.lblAudioDeviceDesc.Name = "lblAudioDeviceDesc";
            this.lblAudioDeviceDesc.Size = new System.Drawing.Size(124, 15);
            this.lblAudioDeviceDesc.Text = "Capture Audio Device:";

            this.comboAudioDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioDevices.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboAudioDevices.Location = new System.Drawing.Point(150, 85);
            this.comboAudioDevices.Name = "comboAudioDevices";
            this.comboAudioDevices.Size = new System.Drawing.Size(350, 21);

            this.btnRefreshAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshAudio.Location = new System.Drawing.Point(510, 83);
            this.btnRefreshAudio.Name = "btnRefreshAudio";
            this.btnRefreshAudio.Size = new System.Drawing.Size(60, 25);
            this.btnRefreshAudio.Text = "🔄";
            this.btnRefreshAudio.UseVisualStyleBackColor = true;

            this.lblAudioDiagMeterText.AutoSize = true;
            this.lblAudioDiagMeterText.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioDiagMeterText.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioDiagMeterText.Location = new System.Drawing.Point(16, 120);
            this.lblAudioDiagMeterText.Name = "lblAudioDiagMeterText";
            this.lblAudioDiagMeterText.Size = new System.Drawing.Size(107, 12);
            this.lblAudioDiagMeterText.Text = "Input Volume: Silent";

            this.pbAudioDiagMeter.Location = new System.Drawing.Point(16, 136);
            this.pbAudioDiagMeter.Name = "pbAudioDiagMeter";
            this.pbAudioDiagMeter.Size = new System.Drawing.Size(554, 8);

            this.btnTestSpeaker.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnTestSpeaker.Location = new System.Drawing.Point(16, 156);
            this.btnTestSpeaker.Name = "btnTestSpeaker";
            this.btnTestSpeaker.Size = new System.Drawing.Size(120, 26);
            this.btnTestSpeaker.Text = "🔊 Test Chime";
            this.btnTestSpeaker.UseVisualStyleBackColor = true;

            this.btnTestMic.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnTestMic.Location = new System.Drawing.Point(145, 156);
            this.btnTestMic.Name = "btnTestMic";
            this.btnTestMic.Size = new System.Drawing.Size(130, 26);
            this.btnTestMic.Text = "🎤 Test Mic (2s)";
            this.btnTestMic.UseVisualStyleBackColor = true;

            this.btnTestAudio.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnTestAudio.Location = new System.Drawing.Point(285, 156);
            this.btnTestAudio.Name = "btnTestAudio";
            this.btnTestAudio.Size = new System.Drawing.Size(120, 26);
            this.btnTestAudio.Text = "▶ Test Device";
            this.btnTestAudio.UseVisualStyleBackColor = true;

            this.btnAudioDiag.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnAudioDiag.Location = new System.Drawing.Point(415, 156);
            this.btnAudioDiag.Name = "btnAudioDiag";
            this.btnAudioDiag.Size = new System.Drawing.Size(155, 26);
            this.btnAudioDiag.Text = "🎙️ Diagnostics Details";
            this.btnAudioDiag.UseVisualStyleBackColor = true;

            this.lblAudioDiagInfo.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioDiagInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAudioDiagInfo.Location = new System.Drawing.Point(16, 192);
            this.lblAudioDiagInfo.Name = "lblAudioDiagInfo";
            this.lblAudioDiagInfo.Size = new System.Drawing.Size(554, 28);
            this.lblAudioDiagInfo.Text = "WASAPI Loopback • 48.0 kHz • 2 Channels • 32-bit Float PCM • Latency: <10ms";

            this.cardAudioDiag.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblAudioDiagTitle,
                this.lblAudioDiagSubtitle,
                this.cbAudio,
                this.lblAudioDeviceDesc,
                this.comboAudioDevices,
                this.btnRefreshAudio,
                this.lblAudioDiagMeterText,
                this.pbAudioDiagMeter,
                this.btnTestSpeaker,
                this.btnTestMic,
                this.btnTestAudio,
                this.btnAudioDiag,
                this.lblAudioDiagInfo
            });

            // Right-Bottom Card: Activity Logs
            this.cardLogs.BackColor = System.Drawing.SystemColors.Window;
            this.cardLogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardLogs.Location = new System.Drawing.Point(395, 240);
            this.cardLogs.Name = "cardLogs";
            this.cardLogs.Size = new System.Drawing.Size(585, 290);

            this.lblLogsTitle.AutoSize = true;
            this.lblLogsTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblLogsTitle.Location = new System.Drawing.Point(14, 10);
            this.lblLogsTitle.Name = "lblLogsTitle";
            this.lblLogsTitle.Size = new System.Drawing.Size(193, 21);
            this.lblLogsTitle.Text = "📋 Activity & Stream Logs";

            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnClearLog.Location = new System.Drawing.Point(415, 8);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 24);
            this.btnClearLog.Text = "🗑️ Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;

            this.btnCopyLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyLog.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyLog.Location = new System.Drawing.Point(495, 8);
            this.btnCopyLog.Name = "btnCopyLog";
            this.btnCopyLog.Size = new System.Drawing.Size(75, 24);
            this.btnCopyLog.Text = "📋 Copy";
            this.btnCopyLog.UseVisualStyleBackColor = true;

            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.txtLog.Location = new System.Drawing.Point(14, 38);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(556, 238);
            this.txtLog.WordWrap = false;

            this.cardLogs.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblLogsTitle,
                this.btnClearLog,
                this.btnCopyLog,
                this.txtLog
            });

            this.panelViewPrefs.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.cardGeneralPrefs,
                this.cardAudioDiag,
                this.cardLogs
            });

            // ── Footer & System Tray ────────────────────────────────────────
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
            this.statusStripMain.Size = new System.Drawing.Size(1000, 24);
            this.statusStripMain.TabIndex = 6;

            this.statusLabelServer.Name = "statusLabelServer";
            this.statusLabelServer.Size = new System.Drawing.Size(102, 19);
            this.statusLabelServer.Spring = true;
            this.statusLabelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabelServer.Text = "● Ready to stream";

            this.statusLabelViewers.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelViewers.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelViewers.Name = "statusLabelViewers";
            this.statusLabelViewers.Size = new System.Drawing.Size(71, 19);
            this.statusLabelViewers.Text = "👥 0 viewers";

            this.statusLabelFps.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelFps.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelFps.Name = "statusLabelFps";
            this.statusLabelFps.Size = new System.Drawing.Size(54, 19);
            this.statusLabelFps.Text = "🎞️ 0 FPS";

            this.statusLabelData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelData.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelData.Name = "statusLabelData";
            this.statusLabelData.Size = new System.Drawing.Size(65, 19);
            this.statusLabelData.Text = "📊 0.0 MB";

            this.statusLabelUptime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelUptime.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelUptime.Name = "statusLabelUptime";
            this.statusLabelUptime.Size = new System.Drawing.Size(73, 19);
            this.statusLabelUptime.Text = "⏱️ 00:00:00";

            this.statusLabelAudio.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelAudio.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelAudio.Name = "statusLabelAudio";
            this.statusLabelAudio.Size = new System.Drawing.Size(81, 19);
            this.statusLabelAudio.Text = "🔊 Audio: On";

            this.statusLabelCopyright.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelCopyright.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelCopyright.IsLink = true;
            this.statusLabelCopyright.Name = "statusLabelCopyright";
            this.statusLabelCopyright.Size = new System.Drawing.Size(155, 19);
            this.statusLabelCopyright.Text = "© 2014-2026 Eslam Hamouda";
            this.statusLabelCopyright.ToolTipText = "ScreenTask 2.0 • Free & Open Source under GPLv3 • screentask.eslamx.com";

            this.appNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.appNotify.Text = "ScreenTask 2.0";
            this.appNotify.Visible = true;
            this.appNotify.ContextMenuStrip = this.trayContextMenu;

            // ── Form Main Configuration ─────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(880, 640);
            this.Controls.Add(this.panelContentContainer);
            this.Controls.Add(this.panelSideNav);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenTask 2.0";

            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSideNav.ResumeLayout(false);
            this.panelContentContainer.ResumeLayout(false);
            this.panelViewBroadcast.ResumeLayout(false);
            this.cardPreview.ResumeLayout(false);
            this.cardPreview.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cardUrl.ResumeLayout(false);
            this.cardUrl.PerformLayout();
            this.panelViewClients.ResumeLayout(false);
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
            this.panelViewCapture.ResumeLayout(false);
            this.panelCaptureHeader.ResumeLayout(false);
            this.panelCaptureHeader.PerformLayout();
            this.tabControlSources.ResumeLayout(false);
            this.tabDisplays.ResumeLayout(false);
            this.tabWindows.ResumeLayout(false);
            this.panelViewNetwork.ResumeLayout(false);
            this.cardNetworkInterface.ResumeLayout(false);
            this.cardNetworkInterface.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.cardNetworkPin.ResumeLayout(false);
            this.cardNetworkPin.PerformLayout();
            this.panelViewPrefs.ResumeLayout(false);
            this.cardGeneralPrefs.ResumeLayout(false);
            this.cardGeneralPrefs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.cardAudioDiag.ResumeLayout(false);
            this.cardAudioDiag.PerformLayout();
            this.cardLogs.ResumeLayout(false);
            this.cardLogs.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Header Elements
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label picAppIcon;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;
        internal System.Windows.Forms.Label lblStatusBadge;

        // Navigation Bar
        internal System.Windows.Forms.Panel panelSideNav;
        internal System.Windows.Forms.Button btnNavBroadcast;
        internal System.Windows.Forms.Button btnNavClients;
        internal System.Windows.Forms.Button btnNavCapture;
        internal System.Windows.Forms.Button btnNavNetwork;
        internal System.Windows.Forms.Button btnNavPrefs;

        // Content Container & View Panels
        internal System.Windows.Forms.Panel panelContentContainer;
        internal System.Windows.Forms.Panel panelViewBroadcast;
        internal System.Windows.Forms.Panel panelViewClients;
        internal System.Windows.Forms.Panel panelViewCapture;
        internal System.Windows.Forms.Panel panelViewNetwork;
        internal System.Windows.Forms.Panel panelViewPrefs;

        // VIEW 0 Controls (Broadcast)
        internal System.Windows.Forms.Panel cardPreview;
        internal ScreenTask.UI.MonitorPreview monitorPreview;
        internal System.Windows.Forms.ComboBox comboScreens;
        internal System.Windows.Forms.Button btnRefreshScreens;
        internal System.Windows.Forms.Button btnPopout;
        internal System.Windows.Forms.Button btnSwitchSource;

        internal System.Windows.Forms.Panel groupBox3;
        internal System.Windows.Forms.Label lblBroadcastDesc;
        internal System.Windows.Forms.Button btnStartServer;
        internal System.Windows.Forms.Button btnQuickMute;
        internal System.Windows.Forms.Button btnAudioSetup;
        internal System.Windows.Forms.Label lblAudioMeterText;
        internal System.Windows.Forms.ProgressBar pbAudioMeter;
        internal System.Windows.Forms.Label lblLiveStats;
        internal System.Windows.Forms.Button btnFreeze;

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

        // VIEW 1 Controls (Connected Viewers & Fleet)
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

        // VIEW 2 Controls (Video Sources)
        internal System.Windows.Forms.Panel panelCaptureHeader;
        internal System.Windows.Forms.Panel panelCaptureFooter;
        internal System.Windows.Forms.Label lblCaptureTitle;
        internal System.Windows.Forms.Label lblCaptureSubtitle;
        internal System.Windows.Forms.Button btnRefreshSourcesTab;
        internal System.Windows.Forms.CheckBox cbCaptureMouse;
        internal System.Windows.Forms.Label lblSelectedSource;
        internal System.Windows.Forms.Button btnSelectSource;
        internal System.Windows.Forms.TabControl tabControlSources;
        internal System.Windows.Forms.TabPage tabDisplays;
        internal System.Windows.Forms.FlowLayoutPanel flowDisplays;
        internal System.Windows.Forms.TabPage tabWindows;
        internal System.Windows.Forms.FlowLayoutPanel flowWindows;

        // VIEW 3 Controls (Network & Security)
        internal System.Windows.Forms.Panel cardNetworkInterface;
        internal System.Windows.Forms.Label lblNetworkTitle;
        internal System.Windows.Forms.Label lblNetworkSubtitle;
        internal System.Windows.Forms.Label lblAdapterDesc;
        internal System.Windows.Forms.ComboBox comboIPs;
        internal System.Windows.Forms.Button btnRefreshIPs;
        internal System.Windows.Forms.Label lblPortDesc;
        internal System.Windows.Forms.NumericUpDown numPort;
        internal System.Windows.Forms.Label lblPortStatus;
        internal System.Windows.Forms.Label lblAdapterHint;

        internal System.Windows.Forms.Panel cardNetworkPin;
        internal System.Windows.Forms.Label lblPinTitle;
        internal System.Windows.Forms.Label lblPinSubtitle;
        internal System.Windows.Forms.CheckBox cbPrivate;
        internal System.Windows.Forms.Label lblPinCodeDesc;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Button btnGenPin;
        internal System.Windows.Forms.Button btnCopyPin;
        internal System.Windows.Forms.Label lblNetworkPreviewUrlTitle;
        internal System.Windows.Forms.Label lblNetworkPreviewUrl;

        // VIEW 4 Controls (Preferences, Audio & Logs)
        internal System.Windows.Forms.Panel cardGeneralPrefs;
        internal System.Windows.Forms.Label lblPrefsTitle;
        internal System.Windows.Forms.Label lblPrefsSubtitle;
        internal System.Windows.Forms.CheckBox cbAutoStart;
        internal System.Windows.Forms.CheckBox cbMiniStart;
        internal System.Windows.Forms.CheckBox cbCloseToTray;
        internal System.Windows.Forms.Label lblFpsDesc;
        internal System.Windows.Forms.ComboBox comboFps;
        internal System.Windows.Forms.Label lblQualityDesc;
        internal System.Windows.Forms.TrackBar qualitySlider;
        internal System.Windows.Forms.Label lblQuality;

        internal System.Windows.Forms.Panel cardAudioDiag;
        internal System.Windows.Forms.Label lblAudioDiagTitle;
        internal System.Windows.Forms.Label lblAudioDiagSubtitle;
        internal System.Windows.Forms.CheckBox cbAudio;
        internal System.Windows.Forms.Label lblAudioDeviceDesc;
        internal System.Windows.Forms.ComboBox comboAudioDevices;
        internal System.Windows.Forms.Button btnRefreshAudio;
        internal System.Windows.Forms.Label lblAudioDiagMeterText;
        internal System.Windows.Forms.ProgressBar pbAudioDiagMeter;
        internal System.Windows.Forms.Button btnTestSpeaker;
        internal System.Windows.Forms.Button btnTestMic;
        internal System.Windows.Forms.Button btnTestAudio;
        internal System.Windows.Forms.Button btnAudioDiag;
        internal System.Windows.Forms.Label lblAudioDiagInfo;

        internal System.Windows.Forms.Panel cardLogs;
        internal System.Windows.Forms.Label lblLogsTitle;
        internal System.Windows.Forms.TextBox txtLog;
        internal System.Windows.Forms.Button btnClearLog;
        internal System.Windows.Forms.Button btnCopyLog;

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
