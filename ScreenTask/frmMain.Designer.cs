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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            menuStripMain = new MenuStrip();
            menuFile = new ToolStripMenuItem();
            menuFileToggleBroadcast = new ToolStripMenuItem();
            menuFileSep1 = new ToolStripSeparator();
            menuFileCopyLink = new ToolStripMenuItem();
            menuFileOpenBrowser = new ToolStripMenuItem();
            menuFileSep2 = new ToolStripSeparator();
            menuFileExit = new ToolStripMenuItem();
            menuView = new ToolStripMenuItem();
            menuViewBroadcast = new ToolStripMenuItem();
            menuViewViewers = new ToolStripMenuItem();
            menuViewSettings = new ToolStripMenuItem();
            menuCapture = new ToolStripMenuItem();
            menuCapturePickSource = new ToolStripMenuItem();
            menuCaptureRefreshSources = new ToolStripMenuItem();
            menuCaptureSep1 = new ToolStripSeparator();
            menuCaptureShowMouse = new ToolStripMenuItem();
            menuCaptureEnableAudio = new ToolStripMenuItem();
            menuNetwork = new ToolStripMenuItem();
            menuNetworkRefreshAdapters = new ToolStripMenuItem();
            menuNetworkFirewallRule = new ToolStripMenuItem();
            menuNetworkSep1 = new ToolStripSeparator();
            menuNetworkPinProtection = new ToolStripMenuItem();
            menuNetworkGenPin = new ToolStripMenuItem();
            menuNetworkCopyPin = new ToolStripMenuItem();
            menuHelp = new ToolStripMenuItem();
            menuHelpReportBug = new ToolStripMenuItem();
            menuHelpRequestFeature = new ToolStripMenuItem();
            menuHelpSepFeedback = new ToolStripSeparator();
            menuHelpWebsite = new ToolStripMenuItem();
            menuHelpGitHub = new ToolStripMenuItem();
            menuHelpBuyMeCoffee = new ToolStripMenuItem();
            menuHelpSep1 = new ToolStripSeparator();
            menuHelpAbout = new ToolStripMenuItem();
            menuHelpCheckUpdates = new ToolStripMenuItem();
            menuFeedbackQuick = new ToolStripMenuItem();
            tabControlMain = new TabControl();
            tabBroadcast = new TabPage();
            tableBroadcast = new TableLayoutPanel();
            grpPreview = new GroupBox();
            tablePreviewInner = new TableLayoutPanel();
            monitorPreview = new ScreenTask.UI.MonitorPreview();
            panelSourceBar = new TableLayoutPanel();
            lblSource = new Label();
            comboScreens = new ComboBox();
            btnRefreshScreens = new Button();
            btnSwitchSource = new Button();
            panelCaptureFlags = new FlowLayoutPanel();
            cbCaptureMouse = new CheckBox();
            cbAudio = new CheckBox();
            btnPopout = new Button();
            tableBroadcastRight = new TableLayoutPanel();
            grpSessionControl = new GroupBox();
            tableSessionControl = new TableLayoutPanel();
            btnStartServer = new Button();
            panelSessionSubActions = new TableLayoutPanel();
            btnFreeze = new Button();
            btnQuickMute = new Button();
            lblAudioDevice = new Label();
            panelAudioSelector = new TableLayoutPanel();
            comboAudioDevices = new ComboBox();
            btnRefreshAudio = new Button();
            panelAudioMeterRow = new TableLayoutPanel();
            lblAudioMeterText = new Label();
            pbAudioMeter = new ProgressBar();
            grpConnection = new GroupBox();
            tableConnection = new TableLayoutPanel();
            lblAdapter = new Label();
            panelAdapterRow = new TableLayoutPanel();
            comboIPs = new ComboBox();
            numPort = new NumericUpDown();
            btnRefreshIPs = new Button();
            lblUrl = new Label();
            panelUrlRow = new TableLayoutPanel();
            txtURL = new TextBox();
            btnCopyUrl = new Button();
            btnLaunch = new Button();
            panelPinQrRow = new TableLayoutPanel();
            panelPinControls = new FlowLayoutPanel();
            cbPrivate = new CheckBox();
            panelPinInputRow = new FlowLayoutPanel();
            txtPassword = new TextBox();
            btnGenPin = new Button();
            btnCopyPin = new Button();
            lblQrHint = new Label();
            picPairingQr = new PictureBox();
            tabViewers = new TabPage();
            tableViewers = new TableLayoutPanel();
            grpViewerFleet = new GroupBox();
            tableViewerFleet = new TableLayoutPanel();
            panelViewerToolbar = new TableLayoutPanel();
            lblFleetSummary = new Label();
            btnMuteSelectedViewer = new Button();
            btnKickSelectedViewer = new Button();
            lvViewers = new ListView();
            colDevice = new ColumnHeader();
            colIp = new ColumnHeader();
            colBrowser = new ColumnHeader();
            colMode = new ColumnHeader();
            colRtt = new ColumnHeader();
            colUptime = new ColumnHeader();
            colAudio = new ColumnHeader();
            grpAnnouncement = new GroupBox();
            tableAnnouncement = new TableLayoutPanel();
            txtAnnouncement = new TextBox();
            btnSendAnnouncement = new Button();
            lblAnnouncementStatus = new Label();
            tabSettings = new TabPage();
            tableSettings = new TableLayoutPanel();
            tableSettingsTop = new TableLayoutPanel();
            grpQualityStartup = new GroupBox();
            tableQualityStartup = new TableLayoutPanel();
            lblFpsDesc = new Label();
            comboFps = new ComboBox();
            lblQualityDesc = new Label();
            panelQualityRow = new TableLayoutPanel();
            qualitySlider = new TrackBar();
            lblQuality = new Label();
            cbAutoStart = new CheckBox();
            cbMiniStart = new CheckBox();
            cbCloseToTray = new CheckBox();
            grpDiagnosticsFirewall = new GroupBox();
            tableDiagFirewall = new TableLayoutPanel();
            lblAudioDiagInfo = new Label();
            panelAudioTestButtons = new FlowLayoutPanel();
            btnTestSpeaker = new Button();
            btnTestMic = new Button();
            lblFirewallDesc = new Label();
            btnFirewallRule = new Button();
            grpLogs = new GroupBox();
            tableLogs = new TableLayoutPanel();
            txtLog = new TextBox();
            panelLogButtons = new FlowLayoutPanel();
            btnCopyLog = new Button();
            btnClearLog = new Button();
            btnReportIssue = new Button();
            statusStripMain = new StatusStrip();
            statusLabelServer = new ToolStripStatusLabel();
            statusLabelViewers = new ToolStripStatusLabel();
            statusLabelFps = new ToolStripStatusLabel();
            statusLabelData = new ToolStripStatusLabel();
            statusLabelUptime = new ToolStripStatusLabel();
            statusLabelAudio = new ToolStripStatusLabel();
            statusLabelCopyright = new ToolStripStatusLabel();
            appNotify = new NotifyIcon(components);
            trayContextMenu = new ContextMenuStrip(components);
            toolTipMain = new ToolTip(components);
            menuStripMain.SuspendLayout();
            tabControlMain.SuspendLayout();
            tabBroadcast.SuspendLayout();
            tableBroadcast.SuspendLayout();
            grpPreview.SuspendLayout();
            tablePreviewInner.SuspendLayout();
            panelSourceBar.SuspendLayout();
            panelCaptureFlags.SuspendLayout();
            tableBroadcastRight.SuspendLayout();
            grpSessionControl.SuspendLayout();
            tableSessionControl.SuspendLayout();
            panelSessionSubActions.SuspendLayout();
            panelAudioSelector.SuspendLayout();
            panelAudioMeterRow.SuspendLayout();
            grpConnection.SuspendLayout();
            tableConnection.SuspendLayout();
            panelAdapterRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            panelUrlRow.SuspendLayout();
            panelPinQrRow.SuspendLayout();
            panelPinControls.SuspendLayout();
            panelPinInputRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPairingQr).BeginInit();
            tabViewers.SuspendLayout();
            tableViewers.SuspendLayout();
            grpViewerFleet.SuspendLayout();
            tableViewerFleet.SuspendLayout();
            panelViewerToolbar.SuspendLayout();
            grpAnnouncement.SuspendLayout();
            tableAnnouncement.SuspendLayout();
            tabSettings.SuspendLayout();
            tableSettings.SuspendLayout();
            tableSettingsTop.SuspendLayout();
            grpQualityStartup.SuspendLayout();
            tableQualityStartup.SuspendLayout();
            panelQualityRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)qualitySlider).BeginInit();
            grpDiagnosticsFirewall.SuspendLayout();
            tableDiagFirewall.SuspendLayout();
            panelAudioTestButtons.SuspendLayout();
            grpLogs.SuspendLayout();
            tableLogs.SuspendLayout();
            panelLogButtons.SuspendLayout();
            statusStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Font = new Font("Segoe UI", 9F);
            menuStripMain.ImageScalingSize = new Size(24, 24);
            menuStripMain.Items.AddRange(new ToolStripItem[] { menuFile, menuView, menuCapture, menuNetwork, menuHelp, menuFeedbackQuick });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Padding = new Padding(9, 3, 0, 3);
            menuStripMain.Size = new Size(1114, 35);
            menuStripMain.TabIndex = 0;
            // 
            // menuFile
            // 
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuFileToggleBroadcast, menuFileSep1, menuFileCopyLink, menuFileOpenBrowser, menuFileSep2, menuFileExit });
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(54, 29);
            menuFile.Text = "&File";
            // 
            // menuFileToggleBroadcast
            // 
            menuFileToggleBroadcast.Name = "menuFileToggleBroadcast";
            menuFileToggleBroadcast.ShortcutKeys = Keys.Control | Keys.B;
            menuFileToggleBroadcast.Size = new Size(311, 34);
            menuFileToggleBroadcast.Text = "Start Broadcast";
            // 
            // menuFileSep1
            // 
            menuFileSep1.Name = "menuFileSep1";
            menuFileSep1.Size = new Size(308, 6);
            // 
            // menuFileCopyLink
            // 
            menuFileCopyLink.Name = "menuFileCopyLink";
            menuFileCopyLink.ShortcutKeys = Keys.Control | Keys.L;
            menuFileCopyLink.Size = new Size(311, 34);
            menuFileCopyLink.Text = "Copy Stream Link";
            // 
            // menuFileOpenBrowser
            // 
            menuFileOpenBrowser.Name = "menuFileOpenBrowser";
            menuFileOpenBrowser.ShortcutKeys = Keys.Control | Keys.O;
            menuFileOpenBrowser.Size = new Size(311, 34);
            menuFileOpenBrowser.Text = "Open in Browser";
            // 
            // menuFileSep2
            // 
            menuFileSep2.Name = "menuFileSep2";
            menuFileSep2.Size = new Size(308, 6);
            // 
            // menuFileExit
            // 
            menuFileExit.Name = "menuFileExit";
            menuFileExit.ShortcutKeys = Keys.Alt | Keys.F4;
            menuFileExit.Size = new Size(311, 34);
            menuFileExit.Text = "E&xit";
            // 
            // menuView
            // 
            menuView.DropDownItems.AddRange(new ToolStripItem[] { menuViewBroadcast, menuViewViewers, menuViewSettings });
            menuView.Name = "menuView";
            menuView.Size = new Size(65, 29);
            menuView.Text = "&View";
            // 
            // menuViewBroadcast
            // 
            menuViewBroadcast.Name = "menuViewBroadcast";
            menuViewBroadcast.ShortcutKeys = Keys.Control | Keys.D1;
            menuViewBroadcast.Size = new Size(325, 34);
            menuViewBroadcast.Text = "Broadcast Console";
            // 
            // menuViewViewers
            // 
            menuViewViewers.Name = "menuViewViewers";
            menuViewViewers.ShortcutKeys = Keys.Control | Keys.D2;
            menuViewViewers.Size = new Size(325, 34);
            menuViewViewers.Text = "Connected Viewers";
            // 
            // menuViewSettings
            // 
            menuViewSettings.Name = "menuViewSettings";
            menuViewSettings.ShortcutKeys = Keys.Control | Keys.D3;
            menuViewSettings.Size = new Size(325, 34);
            menuViewSettings.Text = "Settings && Logs";
            // 
            // menuCapture
            // 
            menuCapture.DropDownItems.AddRange(new ToolStripItem[] { menuCapturePickSource, menuCaptureRefreshSources, menuCaptureSep1, menuCaptureShowMouse, menuCaptureEnableAudio });
            menuCapture.Name = "menuCapture";
            menuCapture.Size = new Size(90, 29);
            menuCapture.Text = "&Capture";
            // 
            // menuCapturePickSource
            // 
            menuCapturePickSource.Name = "menuCapturePickSource";
            menuCapturePickSource.ShortcutKeys = Keys.Control | Keys.P;
            menuCapturePickSource.Size = new Size(341, 34);
            menuCapturePickSource.Text = "Pick Source Visually...";
            // 
            // menuCaptureRefreshSources
            // 
            menuCaptureRefreshSources.Name = "menuCaptureRefreshSources";
            menuCaptureRefreshSources.ShortcutKeys = Keys.F5;
            menuCaptureRefreshSources.Size = new Size(341, 34);
            menuCaptureRefreshSources.Text = "Refresh Sources";
            // 
            // menuCaptureSep1
            // 
            menuCaptureSep1.Name = "menuCaptureSep1";
            menuCaptureSep1.Size = new Size(338, 6);
            // 
            // menuCaptureShowMouse
            // 
            menuCaptureShowMouse.CheckOnClick = true;
            menuCaptureShowMouse.Name = "menuCaptureShowMouse";
            menuCaptureShowMouse.Size = new Size(341, 34);
            menuCaptureShowMouse.Text = "Capture Mouse Cursor";
            // 
            // menuCaptureEnableAudio
            // 
            menuCaptureEnableAudio.CheckOnClick = true;
            menuCaptureEnableAudio.Name = "menuCaptureEnableAudio";
            menuCaptureEnableAudio.Size = new Size(341, 34);
            menuCaptureEnableAudio.Text = "Stream Audio";
            // 
            // menuNetwork
            // 
            menuNetwork.DropDownItems.AddRange(new ToolStripItem[] { menuNetworkRefreshAdapters, menuNetworkFirewallRule, menuNetworkSep1, menuNetworkPinProtection, menuNetworkGenPin, menuNetworkCopyPin });
            menuNetwork.Name = "menuNetwork";
            menuNetwork.Size = new Size(95, 29);
            menuNetwork.Text = "&Network";
            // 
            // menuNetworkRefreshAdapters
            // 
            menuNetworkRefreshAdapters.Name = "menuNetworkRefreshAdapters";
            menuNetworkRefreshAdapters.Size = new Size(368, 34);
            menuNetworkRefreshAdapters.Text = "Refresh Network Adapters";
            // 
            // menuNetworkFirewallRule
            // 
            menuNetworkFirewallRule.Name = "menuNetworkFirewallRule";
            menuNetworkFirewallRule.Size = new Size(368, 34);
            menuNetworkFirewallRule.Text = "Allow Port in Windows Firewall...";
            // 
            // menuNetworkSep1
            // 
            menuNetworkSep1.Name = "menuNetworkSep1";
            menuNetworkSep1.Size = new Size(365, 6);
            // 
            // menuNetworkPinProtection
            // 
            menuNetworkPinProtection.CheckOnClick = true;
            menuNetworkPinProtection.Name = "menuNetworkPinProtection";
            menuNetworkPinProtection.Size = new Size(368, 34);
            menuNetworkPinProtection.Text = "Require PIN Code";
            // 
            // menuNetworkGenPin
            // 
            menuNetworkGenPin.Name = "menuNetworkGenPin";
            menuNetworkGenPin.Size = new Size(368, 34);
            menuNetworkGenPin.Text = "Generate New PIN";
            // 
            // menuNetworkCopyPin
            // 
            menuNetworkCopyPin.Name = "menuNetworkCopyPin";
            menuNetworkCopyPin.Size = new Size(368, 34);
            menuNetworkCopyPin.Text = "Copy PIN";
            // 
            // menuHelp
            // 
            menuHelp.DropDownItems.AddRange(new ToolStripItem[] { menuHelpReportBug, menuHelpRequestFeature, menuHelpSepFeedback, menuHelpCheckUpdates, menuHelpWebsite, menuHelpGitHub, menuHelpBuyMeCoffee, menuHelpSep1, menuHelpAbout });
            menuHelpCheckUpdates.Name = "menuHelpCheckUpdates";
            menuHelpCheckUpdates.Size = new Size(326, 34);
            menuHelpCheckUpdates.Text = "Check for Updates...";
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(65, 29);
            menuHelp.Text = "&Help";
            // 
            // menuHelpReportBug
            // 
            menuHelpReportBug.Name = "menuHelpReportBug";
            menuHelpReportBug.Size = new Size(326, 34);
            menuHelpReportBug.Text = "Report a Bug...";
            // 
            // menuHelpRequestFeature
            // 
            menuHelpRequestFeature.Name = "menuHelpRequestFeature";
            menuHelpRequestFeature.Size = new Size(326, 34);
            menuHelpRequestFeature.Text = "Request a Feature...";
            // 
            // menuHelpSepFeedback
            // 
            menuHelpSepFeedback.Name = "menuHelpSepFeedback";
            menuHelpSepFeedback.Size = new Size(323, 6);
            // 
            // menuHelpWebsite
            // 
            menuHelpWebsite.Name = "menuHelpWebsite";
            menuHelpWebsite.Size = new Size(326, 34);
            menuHelpWebsite.Text = "ScreenTask Website";
            // 
            // menuHelpGitHub
            // 
            menuHelpGitHub.Name = "menuHelpGitHub";
            menuHelpGitHub.Size = new Size(326, 34);
            menuHelpGitHub.Text = "GitHub Repository";
            // 
            // menuHelpBuyMeCoffee
            // 
            menuHelpBuyMeCoffee.Name = "menuHelpBuyMeCoffee";
            menuHelpBuyMeCoffee.Size = new Size(326, 34);
            menuHelpBuyMeCoffee.Text = "Buy Me a Coffee";
            // 
            // menuHelpSep1
            // 
            menuHelpSep1.Name = "menuHelpSep1";
            menuHelpSep1.Size = new Size(323, 6);
            // 
            // menuHelpAbout
            // 
            menuHelpAbout.Name = "menuHelpAbout";
            menuHelpAbout.ShortcutKeys = Keys.F1;
            menuHelpAbout.Size = new Size(326, 34);
            menuHelpAbout.Text = "About ScreenTask 2.0...";
            // 
            // menuFeedbackQuick
            // 
            menuFeedbackQuick.Alignment = ToolStripItemAlignment.Right;
            menuFeedbackQuick.ForeColor = Color.FromArgb(20, 90, 170);
            menuFeedbackQuick.Name = "menuFeedbackQuick";
            menuFeedbackQuick.Padding = new Padding(8, 0, 12, 0);
            menuFeedbackQuick.Size = new Size(268, 29);
            menuFeedbackQuick.Text = "Report Bug / Request Feature";
            menuFeedbackQuick.ToolTipText = "Open the Feedback & Bug Report dialog (GitHub Issues or support@eslamx.com)";
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabBroadcast);
            tabControlMain.Controls.Add(tabViewers);
            tabControlMain.Controls.Add(tabSettings);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Font = new Font("Segoe UI", 9F);
            tabControlMain.Location = new Point(0, 35);
            tabControlMain.Margin = new Padding(4, 5, 4, 5);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.Padding = new Point(12, 4);
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1114, 862);
            tabControlMain.TabIndex = 1;
            // 
            // tabBroadcast
            // 
            tabBroadcast.Controls.Add(tableBroadcast);
            tabBroadcast.Location = new Point(4, 36);
            tabBroadcast.Margin = new Padding(4, 5, 4, 5);
            tabBroadcast.Name = "tabBroadcast";
            tabBroadcast.Padding = new Padding(9, 10, 9, 10);
            tabBroadcast.Size = new Size(1106, 822);
            tabBroadcast.TabIndex = 0;
            tabBroadcast.Text = "Broadcast";
            tabBroadcast.UseVisualStyleBackColor = true;
            // 
            // tableBroadcast
            // 
            tableBroadcast.ColumnCount = 2;
            tableBroadcast.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64F));
            tableBroadcast.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tableBroadcast.Controls.Add(grpPreview, 0, 0);
            tableBroadcast.Controls.Add(tableBroadcastRight, 1, 0);
            tableBroadcast.Dock = DockStyle.Fill;
            tableBroadcast.Location = new Point(9, 10);
            tableBroadcast.Margin = new Padding(4, 5, 4, 5);
            tableBroadcast.Name = "tableBroadcast";
            tableBroadcast.RowCount = 1;
            tableBroadcast.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableBroadcast.Size = new Size(1088, 802);
            tableBroadcast.TabIndex = 0;
            // 
            // grpPreview
            // 
            grpPreview.Controls.Add(tablePreviewInner);
            grpPreview.Dock = DockStyle.Fill;
            grpPreview.Location = new Point(4, 5);
            grpPreview.Margin = new Padding(4, 5, 9, 5);
            grpPreview.Name = "grpPreview";
            grpPreview.Padding = new Padding(11, 13, 11, 13);
            grpPreview.Size = new Size(585, 792);
            grpPreview.TabIndex = 0;
            grpPreview.TabStop = false;
            grpPreview.Text = "Capture Source & Live Preview";
            // 
            // tablePreviewInner
            // 
            tablePreviewInner.ColumnCount = 1;
            tablePreviewInner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tablePreviewInner.Controls.Add(monitorPreview, 0, 0);
            tablePreviewInner.Controls.Add(panelSourceBar, 0, 1);
            tablePreviewInner.Controls.Add(panelCaptureFlags, 0, 2);
            tablePreviewInner.Dock = DockStyle.Fill;
            tablePreviewInner.Location = new Point(11, 37);
            tablePreviewInner.Margin = new Padding(4, 5, 4, 5);
            tablePreviewInner.Name = "tablePreviewInner";
            tablePreviewInner.RowCount = 3;
            tablePreviewInner.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tablePreviewInner.RowStyles.Add(new RowStyle(SizeType.Absolute, 57F));
            tablePreviewInner.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tablePreviewInner.Size = new Size(563, 742);
            tablePreviewInner.TabIndex = 0;
            // 
            // monitorPreview
            // 
            monitorPreview.AudioInfoText = "Audio: Muted";
            monitorPreview.BackColor = Color.Black;
            monitorPreview.Dock = DockStyle.Fill;
            monitorPreview.Font = new Font("Segoe UI", 8.5F);
            monitorPreview.FpsText = "⚡ 30.0 FPS";
            monitorPreview.IsLive = false;
            monitorPreview.Location = new Point(0, 3);
            monitorPreview.Margin = new Padding(0, 3, 0, 10);
            monitorPreview.Name = "monitorPreview";
            monitorPreview.ResolutionText = "1920 × 1080";
            monitorPreview.Size = new Size(563, 619);
            monitorPreview.SourceTitle = "Display 1 (Primary)";
            monitorPreview.TabIndex = 0;
            // 
            // panelSourceBar
            // 
            panelSourceBar.ColumnCount = 4;
            panelSourceBar.ColumnStyles.Add(new ColumnStyle());
            panelSourceBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelSourceBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49F));
            panelSourceBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 157F));
            panelSourceBar.Controls.Add(lblSource, 0, 0);
            panelSourceBar.Controls.Add(comboScreens, 1, 0);
            panelSourceBar.Controls.Add(btnRefreshScreens, 2, 0);
            panelSourceBar.Controls.Add(btnSwitchSource, 3, 0);
            panelSourceBar.Dock = DockStyle.Fill;
            panelSourceBar.Location = new Point(0, 632);
            panelSourceBar.Margin = new Padding(0);
            panelSourceBar.Name = "panelSourceBar";
            panelSourceBar.RowCount = 1;
            panelSourceBar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelSourceBar.Size = new Size(563, 57);
            panelSourceBar.TabIndex = 1;
            // 
            // lblSource
            // 
            lblSource.Anchor = AnchorStyles.Left;
            lblSource.AutoSize = true;
            lblSource.Location = new Point(0, 16);
            lblSource.Margin = new Padding(0, 0, 9, 0);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(60, 25);
            lblSource.TabIndex = 0;
            lblSource.Text = "Share:";
            // 
            // comboScreens
            // 
            comboScreens.Dock = DockStyle.Fill;
            comboScreens.DropDownStyle = ComboBoxStyle.DropDownList;
            comboScreens.Location = new Point(69, 7);
            comboScreens.Margin = new Padding(0, 7, 6, 7);
            comboScreens.Name = "comboScreens";
            comboScreens.Size = new Size(282, 33);
            comboScreens.TabIndex = 1;
            // 
            // btnRefreshScreens
            // 
            btnRefreshScreens.Dock = DockStyle.Fill;
            btnRefreshScreens.Location = new Point(357, 5);
            btnRefreshScreens.Margin = new Padding(0, 5, 6, 5);
            btnRefreshScreens.Name = "btnRefreshScreens";
            btnRefreshScreens.Size = new Size(43, 47);
            btnRefreshScreens.TabIndex = 2;
            btnRefreshScreens.Text = "↻";
            btnRefreshScreens.UseVisualStyleBackColor = true;
            // 
            // btnSwitchSource
            // 
            btnSwitchSource.Dock = DockStyle.Fill;
            btnSwitchSource.Location = new Point(406, 5);
            btnSwitchSource.Margin = new Padding(0, 5, 0, 5);
            btnSwitchSource.Name = "btnSwitchSource";
            btnSwitchSource.Size = new Size(157, 47);
            btnSwitchSource.TabIndex = 3;
            btnSwitchSource.Text = "Pick Visually...";
            btnSwitchSource.UseVisualStyleBackColor = true;
            // 
            // panelCaptureFlags
            // 
            panelCaptureFlags.Controls.Add(cbCaptureMouse);
            panelCaptureFlags.Controls.Add(cbAudio);
            panelCaptureFlags.Controls.Add(btnPopout);
            panelCaptureFlags.Dock = DockStyle.Fill;
            panelCaptureFlags.Location = new Point(0, 689);
            panelCaptureFlags.Margin = new Padding(0);
            panelCaptureFlags.Name = "panelCaptureFlags";
            panelCaptureFlags.Size = new Size(563, 53);
            panelCaptureFlags.TabIndex = 2;
            panelCaptureFlags.WrapContents = false;
            // 
            // cbCaptureMouse
            // 
            cbCaptureMouse.AutoSize = true;
            cbCaptureMouse.Checked = true;
            cbCaptureMouse.CheckState = CheckState.Checked;
            cbCaptureMouse.Location = new Point(3, 10);
            cbCaptureMouse.Margin = new Padding(3, 10, 20, 3);
            cbCaptureMouse.Name = "cbCaptureMouse";
            cbCaptureMouse.Size = new Size(198, 29);
            cbCaptureMouse.TabIndex = 0;
            cbCaptureMouse.Text = "Show Mouse Cursor";
            cbCaptureMouse.UseVisualStyleBackColor = true;
            // 
            // cbAudio
            // 
            cbAudio.AutoSize = true;
            cbAudio.Location = new Point(224, 10);
            cbAudio.Margin = new Padding(3, 10, 20, 3);
            cbAudio.Name = "cbAudio";
            cbAudio.Size = new Size(146, 29);
            cbAudio.TabIndex = 1;
            cbAudio.Text = "Stream Audio";
            cbAudio.UseVisualStyleBackColor = true;
            // 
            // btnPopout
            // 
            btnPopout.Location = new Point(393, 5);
            btnPopout.Margin = new Padding(3, 5, 0, 3);
            btnPopout.Name = "btnPopout";
            btnPopout.Size = new Size(160, 42);
            btnPopout.TabIndex = 2;
            btnPopout.Text = "Expand Preview";
            btnPopout.UseVisualStyleBackColor = true;
            // 
            // tableBroadcastRight
            // 
            tableBroadcastRight.ColumnCount = 1;
            tableBroadcastRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableBroadcastRight.Controls.Add(grpSessionControl, 0, 0);
            tableBroadcastRight.Controls.Add(grpConnection, 0, 1);
            tableBroadcastRight.Dock = DockStyle.Fill;
            tableBroadcastRight.Location = new Point(598, 0);
            tableBroadcastRight.Margin = new Padding(0);
            tableBroadcastRight.Name = "tableBroadcastRight";
            tableBroadcastRight.RowCount = 2;
            tableBroadcastRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 308F));
            tableBroadcastRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableBroadcastRight.Size = new Size(490, 802);
            tableBroadcastRight.TabIndex = 1;
            // 
            // grpSessionControl
            // 
            grpSessionControl.Controls.Add(tableSessionControl);
            grpSessionControl.Dock = DockStyle.Fill;
            grpSessionControl.Location = new Point(4, 5);
            grpSessionControl.Margin = new Padding(4, 5, 4, 10);
            grpSessionControl.Name = "grpSessionControl";
            grpSessionControl.Padding = new Padding(11, 10, 11, 10);
            grpSessionControl.Size = new Size(482, 293);
            grpSessionControl.TabIndex = 0;
            grpSessionControl.TabStop = false;
            grpSessionControl.Text = "Broadcast & Audio Control";
            // 
            // tableSessionControl
            // 
            tableSessionControl.ColumnCount = 1;
            tableSessionControl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableSessionControl.Controls.Add(btnStartServer, 0, 0);
            tableSessionControl.Controls.Add(panelSessionSubActions, 0, 1);
            tableSessionControl.Controls.Add(lblAudioDevice, 0, 2);
            tableSessionControl.Controls.Add(panelAudioSelector, 0, 3);
            tableSessionControl.Controls.Add(panelAudioMeterRow, 0, 4);
            tableSessionControl.Dock = DockStyle.Fill;
            tableSessionControl.Location = new Point(11, 34);
            tableSessionControl.Margin = new Padding(4, 5, 4, 5);
            tableSessionControl.Name = "tableSessionControl";
            tableSessionControl.RowCount = 5;
            tableSessionControl.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
            tableSessionControl.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableSessionControl.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableSessionControl.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableSessionControl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableSessionControl.Size = new Size(460, 249);
            tableSessionControl.TabIndex = 0;
            // 
            // btnStartServer
            // 
            btnStartServer.BackColor = Color.FromArgb(16, 124, 65);
            btnStartServer.Cursor = Cursors.Hand;
            btnStartServer.Dock = DockStyle.Fill;
            btnStartServer.FlatAppearance.BorderSize = 0;
            btnStartServer.FlatStyle = FlatStyle.Flat;
            btnStartServer.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnStartServer.ForeColor = Color.White;
            btnStartServer.Location = new Point(0, 3);
            btnStartServer.Margin = new Padding(0, 3, 0, 7);
            btnStartServer.Name = "btnStartServer";
            btnStartServer.Size = new Size(460, 57);
            btnStartServer.TabIndex = 0;
            btnStartServer.Text = "▶  START BROADCAST";
            btnStartServer.UseVisualStyleBackColor = false;
            // 
            // panelSessionSubActions
            // 
            panelSessionSubActions.ColumnCount = 2;
            panelSessionSubActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelSessionSubActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelSessionSubActions.Controls.Add(btnFreeze, 0, 0);
            panelSessionSubActions.Controls.Add(btnQuickMute, 1, 0);
            panelSessionSubActions.Dock = DockStyle.Fill;
            panelSessionSubActions.Location = new Point(0, 67);
            panelSessionSubActions.Margin = new Padding(0);
            panelSessionSubActions.Name = "panelSessionSubActions";
            panelSessionSubActions.RowCount = 1;
            panelSessionSubActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelSessionSubActions.Size = new Size(460, 53);
            panelSessionSubActions.TabIndex = 1;
            // 
            // btnFreeze
            // 
            btnFreeze.Dock = DockStyle.Fill;
            btnFreeze.Enabled = false;
            btnFreeze.Location = new Point(0, 3);
            btnFreeze.Margin = new Padding(0, 3, 4, 3);
            btnFreeze.Name = "btnFreeze";
            btnFreeze.Size = new Size(226, 47);
            btnFreeze.TabIndex = 0;
            btnFreeze.Text = "Freeze Screen";
            btnFreeze.UseVisualStyleBackColor = true;
            // 
            // btnQuickMute
            // 
            btnQuickMute.Dock = DockStyle.Fill;
            btnQuickMute.Location = new Point(234, 3);
            btnQuickMute.Margin = new Padding(4, 3, 0, 3);
            btnQuickMute.Name = "btnQuickMute";
            btnQuickMute.Size = new Size(226, 47);
            btnQuickMute.TabIndex = 1;
            btnQuickMute.Text = "Unmute Audio";
            btnQuickMute.UseVisualStyleBackColor = true;
            // 
            // lblAudioDevice
            // 
            lblAudioDevice.Anchor = AnchorStyles.Left;
            lblAudioDevice.AutoSize = true;
            lblAudioDevice.Font = new Font("Segoe UI", 8.25F);
            lblAudioDevice.Location = new Point(0, 125);
            lblAudioDevice.Margin = new Padding(0, 3, 0, 0);
            lblAudioDevice.Name = "lblAudioDevice";
            lblAudioDevice.Size = new Size(313, 23);
            lblAudioDevice.TabIndex = 2;
            lblAudioDevice.Text = "Audio Source (Loopback / Microphone):";
            // 
            // panelAudioSelector
            // 
            panelAudioSelector.ColumnCount = 2;
            panelAudioSelector.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelAudioSelector.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49F));
            panelAudioSelector.Controls.Add(comboAudioDevices, 0, 0);
            panelAudioSelector.Controls.Add(btnRefreshAudio, 1, 0);
            panelAudioSelector.Dock = DockStyle.Fill;
            panelAudioSelector.Location = new Point(0, 150);
            panelAudioSelector.Margin = new Padding(0);
            panelAudioSelector.Name = "panelAudioSelector";
            panelAudioSelector.RowCount = 1;
            panelAudioSelector.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelAudioSelector.Size = new Size(460, 50);
            panelAudioSelector.TabIndex = 3;
            // 
            // comboAudioDevices
            // 
            comboAudioDevices.Dock = DockStyle.Fill;
            comboAudioDevices.DropDownStyle = ComboBoxStyle.DropDownList;
            comboAudioDevices.Enabled = false;
            comboAudioDevices.Font = new Font("Segoe UI", 8.5F);
            comboAudioDevices.Location = new Point(0, 3);
            comboAudioDevices.Margin = new Padding(0, 3, 6, 3);
            comboAudioDevices.Name = "comboAudioDevices";
            comboAudioDevices.Size = new Size(405, 31);
            comboAudioDevices.TabIndex = 0;
            // 
            // btnRefreshAudio
            // 
            btnRefreshAudio.Dock = DockStyle.Fill;
            btnRefreshAudio.Enabled = false;
            btnRefreshAudio.Location = new Point(411, 2);
            btnRefreshAudio.Margin = new Padding(0, 2, 0, 2);
            btnRefreshAudio.Name = "btnRefreshAudio";
            btnRefreshAudio.Size = new Size(49, 46);
            btnRefreshAudio.TabIndex = 1;
            btnRefreshAudio.Text = "↻";
            btnRefreshAudio.UseVisualStyleBackColor = true;
            // 
            // panelAudioMeterRow
            // 
            panelAudioMeterRow.ColumnCount = 2;
            panelAudioMeterRow.ColumnStyles.Add(new ColumnStyle());
            panelAudioMeterRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelAudioMeterRow.Controls.Add(lblAudioMeterText, 0, 0);
            panelAudioMeterRow.Controls.Add(pbAudioMeter, 1, 0);
            panelAudioMeterRow.Dock = DockStyle.Fill;
            panelAudioMeterRow.Location = new Point(0, 200);
            panelAudioMeterRow.Margin = new Padding(0);
            panelAudioMeterRow.Name = "panelAudioMeterRow";
            panelAudioMeterRow.RowCount = 1;
            panelAudioMeterRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelAudioMeterRow.Size = new Size(460, 49);
            panelAudioMeterRow.TabIndex = 4;
            // 
            // lblAudioMeterText
            // 
            lblAudioMeterText.Anchor = AnchorStyles.Left;
            lblAudioMeterText.AutoSize = true;
            lblAudioMeterText.Font = new Font("Segoe UI", 8F);
            lblAudioMeterText.Location = new Point(0, 14);
            lblAudioMeterText.Margin = new Padding(0, 0, 9, 0);
            lblAudioMeterText.Name = "lblAudioMeterText";
            lblAudioMeterText.Size = new Size(115, 21);
            lblAudioMeterText.TabIndex = 0;
            lblAudioMeterText.Text = "Volume: Muted";
            // 
            // pbAudioMeter
            // 
            pbAudioMeter.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            pbAudioMeter.Enabled = false;
            pbAudioMeter.Location = new Point(124, 16);
            pbAudioMeter.Margin = new Padding(0, 7, 0, 7);
            pbAudioMeter.Name = "pbAudioMeter";
            pbAudioMeter.Size = new Size(336, 17);
            pbAudioMeter.TabIndex = 1;
            // 
            // grpConnection
            // 
            grpConnection.Controls.Add(tableConnection);
            grpConnection.Dock = DockStyle.Fill;
            grpConnection.Location = new Point(4, 308);
            grpConnection.Margin = new Padding(4, 0, 4, 5);
            grpConnection.Name = "grpConnection";
            grpConnection.Padding = new Padding(11, 10, 11, 10);
            grpConnection.Size = new Size(482, 489);
            grpConnection.TabIndex = 1;
            grpConnection.TabStop = false;
            grpConnection.Text = "Network Address, PIN & Phone Pairing";
            // 
            // tableConnection
            // 
            tableConnection.ColumnCount = 1;
            tableConnection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableConnection.Controls.Add(lblAdapter, 0, 0);
            tableConnection.Controls.Add(panelAdapterRow, 0, 1);
            tableConnection.Controls.Add(lblUrl, 0, 2);
            tableConnection.Controls.Add(panelUrlRow, 0, 3);
            tableConnection.Controls.Add(panelPinQrRow, 0, 4);
            tableConnection.Dock = DockStyle.Fill;
            tableConnection.Location = new Point(11, 34);
            tableConnection.Margin = new Padding(4, 5, 4, 5);
            tableConnection.Name = "tableConnection";
            tableConnection.RowCount = 5;
            tableConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableConnection.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableConnection.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableConnection.Size = new Size(460, 445);
            tableConnection.TabIndex = 0;
            // 
            // lblAdapter
            // 
            lblAdapter.Anchor = AnchorStyles.Left;
            lblAdapter.AutoSize = true;
            lblAdapter.Font = new Font("Segoe UI", 8.25F);
            lblAdapter.Location = new Point(0, 3);
            lblAdapter.Margin = new Padding(0);
            lblAdapter.Name = "lblAdapter";
            lblAdapter.Size = new Size(191, 23);
            lblAdapter.TabIndex = 0;
            lblAdapter.Text = "Network Interface & Port:";
            // 
            // panelAdapterRow
            // 
            panelAdapterRow.ColumnCount = 3;
            panelAdapterRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelAdapterRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 97F));
            panelAdapterRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 49F));
            panelAdapterRow.Controls.Add(comboIPs, 0, 0);
            panelAdapterRow.Controls.Add(numPort, 1, 0);
            panelAdapterRow.Controls.Add(btnRefreshIPs, 2, 0);
            panelAdapterRow.Dock = DockStyle.Fill;
            panelAdapterRow.Location = new Point(0, 30);
            panelAdapterRow.Margin = new Padding(0);
            panelAdapterRow.Name = "panelAdapterRow";
            panelAdapterRow.RowCount = 1;
            panelAdapterRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelAdapterRow.Size = new Size(460, 50);
            panelAdapterRow.TabIndex = 1;
            // 
            // comboIPs
            // 
            comboIPs.Dock = DockStyle.Fill;
            comboIPs.DropDownStyle = ComboBoxStyle.DropDownList;
            comboIPs.Font = new Font("Segoe UI", 8.5F);
            comboIPs.Location = new Point(0, 3);
            comboIPs.Margin = new Padding(0, 3, 6, 3);
            comboIPs.Name = "comboIPs";
            comboIPs.Size = new Size(308, 31);
            comboIPs.TabIndex = 0;
            // 
            // numPort
            // 
            numPort.Dock = DockStyle.Fill;
            numPort.Font = new Font("Segoe UI", 8.5F);
            numPort.Location = new Point(314, 3);
            numPort.Margin = new Padding(0, 3, 6, 3);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(91, 30);
            numPort.TabIndex = 1;
            numPort.Value = new decimal(new int[] { 7070, 0, 0, 0 });
            // 
            // btnRefreshIPs
            // 
            btnRefreshIPs.Dock = DockStyle.Fill;
            btnRefreshIPs.Location = new Point(411, 2);
            btnRefreshIPs.Margin = new Padding(0, 2, 0, 2);
            btnRefreshIPs.Name = "btnRefreshIPs";
            btnRefreshIPs.Size = new Size(49, 46);
            btnRefreshIPs.TabIndex = 2;
            btnRefreshIPs.Text = "↻";
            btnRefreshIPs.UseVisualStyleBackColor = true;
            // 
            // lblUrl
            // 
            lblUrl.Anchor = AnchorStyles.Left;
            lblUrl.AutoSize = true;
            lblUrl.Font = new Font("Segoe UI", 8.25F);
            lblUrl.Location = new Point(0, 83);
            lblUrl.Margin = new Padding(0);
            lblUrl.Name = "lblUrl";
            lblUrl.Size = new Size(403, 23);
            lblUrl.TabIndex = 2;
            lblUrl.Text = "Stream Web Address (Open on any device browser):";
            // 
            // panelUrlRow
            // 
            panelUrlRow.ColumnCount = 3;
            panelUrlRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelUrlRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 83F));
            panelUrlRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 83F));
            panelUrlRow.Controls.Add(txtURL, 0, 0);
            panelUrlRow.Controls.Add(btnCopyUrl, 1, 0);
            panelUrlRow.Controls.Add(btnLaunch, 2, 0);
            panelUrlRow.Dock = DockStyle.Fill;
            panelUrlRow.Location = new Point(0, 110);
            panelUrlRow.Margin = new Padding(0);
            panelUrlRow.Name = "panelUrlRow";
            panelUrlRow.RowCount = 1;
            panelUrlRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelUrlRow.Size = new Size(460, 50);
            panelUrlRow.TabIndex = 3;
            // 
            // txtURL
            // 
            txtURL.Dock = DockStyle.Fill;
            txtURL.Font = new Font("Consolas", 9F);
            txtURL.Location = new Point(0, 3);
            txtURL.Margin = new Padding(0, 3, 6, 3);
            txtURL.Name = "txtURL";
            txtURL.ReadOnly = true;
            txtURL.Size = new Size(288, 29);
            txtURL.TabIndex = 0;
            txtURL.Text = "http://127.0.0.1:7070";
            // 
            // btnCopyUrl
            // 
            btnCopyUrl.Dock = DockStyle.Fill;
            btnCopyUrl.Location = new Point(294, 2);
            btnCopyUrl.Margin = new Padding(0, 2, 6, 2);
            btnCopyUrl.Name = "btnCopyUrl";
            btnCopyUrl.Size = new Size(77, 46);
            btnCopyUrl.TabIndex = 1;
            btnCopyUrl.Text = "Copy";
            btnCopyUrl.UseVisualStyleBackColor = true;
            // 
            // btnLaunch
            // 
            btnLaunch.Dock = DockStyle.Fill;
            btnLaunch.Location = new Point(377, 2);
            btnLaunch.Margin = new Padding(0, 2, 0, 2);
            btnLaunch.Name = "btnLaunch";
            btnLaunch.Size = new Size(83, 46);
            btnLaunch.TabIndex = 2;
            btnLaunch.Text = "Open";
            btnLaunch.UseVisualStyleBackColor = true;
            // 
            // panelPinQrRow
            // 
            panelPinQrRow.ColumnCount = 2;
            panelPinQrRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelPinQrRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 169F));
            panelPinQrRow.Controls.Add(panelPinControls, 0, 0);
            panelPinQrRow.Controls.Add(picPairingQr, 1, 0);
            panelPinQrRow.Dock = DockStyle.Fill;
            panelPinQrRow.Location = new Point(0, 167);
            panelPinQrRow.Margin = new Padding(0, 7, 0, 0);
            panelPinQrRow.Name = "panelPinQrRow";
            panelPinQrRow.RowCount = 1;
            panelPinQrRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelPinQrRow.Size = new Size(460, 278);
            panelPinQrRow.TabIndex = 4;
            // 
            // panelPinControls
            // 
            panelPinControls.Controls.Add(cbPrivate);
            panelPinControls.Controls.Add(panelPinInputRow);
            panelPinControls.Controls.Add(lblQrHint);
            panelPinControls.Dock = DockStyle.Fill;
            panelPinControls.FlowDirection = FlowDirection.TopDown;
            panelPinControls.Location = new Point(0, 0);
            panelPinControls.Margin = new Padding(0);
            panelPinControls.Name = "panelPinControls";
            panelPinControls.Size = new Size(291, 278);
            panelPinControls.TabIndex = 0;
            panelPinControls.WrapContents = false;
            // 
            // cbPrivate
            // 
            cbPrivate.AutoSize = true;
            cbPrivate.Location = new Point(0, 7);
            cbPrivate.Margin = new Padding(0, 7, 0, 7);
            cbPrivate.Name = "cbPrivate";
            cbPrivate.Size = new Size(177, 29);
            cbPrivate.TabIndex = 0;
            cbPrivate.Text = "Require PIN Code";
            cbPrivate.UseVisualStyleBackColor = true;
            // 
            // panelPinInputRow
            // 
            panelPinInputRow.AutoSize = true;
            panelPinInputRow.Controls.Add(txtPassword);
            panelPinInputRow.Controls.Add(btnGenPin);
            panelPinInputRow.Controls.Add(btnCopyPin);
            panelPinInputRow.Location = new Point(0, 43);
            panelPinInputRow.Margin = new Padding(0, 0, 0, 7);
            panelPinInputRow.Name = "panelPinInputRow";
            panelPinInputRow.Size = new Size(255, 42);
            panelPinInputRow.TabIndex = 1;
            panelPinInputRow.WrapContents = false;
            // 
            // txtPassword
            // 
            txtPassword.Enabled = false;
            txtPassword.Font = new Font("Consolas", 9.5F, FontStyle.Bold);
            txtPassword.Location = new Point(0, 2);
            txtPassword.Margin = new Padding(0, 2, 6, 2);
            txtPassword.MaxLength = 10;
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(95, 30);
            txtPassword.TabIndex = 0;
            // 
            // btnGenPin
            // 
            btnGenPin.Enabled = false;
            btnGenPin.Location = new Point(101, 0);
            btnGenPin.Margin = new Padding(0, 0, 6, 0);
            btnGenPin.Name = "btnGenPin";
            btnGenPin.Size = new Size(74, 42);
            btnGenPin.TabIndex = 1;
            btnGenPin.Text = "New";
            btnGenPin.UseVisualStyleBackColor = true;
            // 
            // btnCopyPin
            // 
            btnCopyPin.Enabled = false;
            btnCopyPin.Location = new Point(181, 0);
            btnCopyPin.Margin = new Padding(0);
            btnCopyPin.Name = "btnCopyPin";
            btnCopyPin.Size = new Size(74, 42);
            btnCopyPin.TabIndex = 2;
            btnCopyPin.Text = "Copy";
            btnCopyPin.UseVisualStyleBackColor = true;
            // 
            // lblQrHint
            // 
            lblQrHint.AutoSize = true;
            lblQrHint.Font = new Font("Segoe UI", 8F);
            lblQrHint.ForeColor = SystemColors.GrayText;
            lblQrHint.Location = new Point(0, 95);
            lblQrHint.Margin = new Padding(0, 3, 0, 0);
            lblQrHint.Name = "lblQrHint";
            lblQrHint.Size = new Size(219, 42);
            lblQrHint.TabIndex = 2;
            lblQrHint.Text = "Scan QR with phone camera\n(Click QR to enlarge) →";
            // 
            // picPairingQr
            // 
            picPairingQr.BackColor = Color.White;
            picPairingQr.Cursor = Cursors.Hand;
            picPairingQr.BorderStyle = BorderStyle.FixedSingle;
            picPairingQr.Dock = DockStyle.Fill;
            picPairingQr.Location = new Point(297, 3);
            picPairingQr.Margin = new Padding(6, 3, 0, 3);
            picPairingQr.Name = "picPairingQr";
            picPairingQr.Size = new Size(163, 272);
            picPairingQr.SizeMode = PictureBoxSizeMode.Zoom;
            picPairingQr.TabIndex = 1;
            picPairingQr.TabStop = false;
            // 
            // tabViewers
            // 
            tabViewers.Controls.Add(tableViewers);
            tabViewers.Location = new Point(4, 36);
            tabViewers.Margin = new Padding(4, 5, 4, 5);
            tabViewers.Name = "tabViewers";
            tabViewers.Padding = new Padding(9, 10, 9, 10);
            tabViewers.Size = new Size(1106, 822);
            tabViewers.TabIndex = 1;
            tabViewers.Text = "Connected Viewers (0)";
            tabViewers.UseVisualStyleBackColor = true;
            // 
            // tableViewers
            // 
            tableViewers.ColumnCount = 1;
            tableViewers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableViewers.Controls.Add(grpViewerFleet, 0, 0);
            tableViewers.Controls.Add(grpAnnouncement, 0, 1);
            tableViewers.Dock = DockStyle.Fill;
            tableViewers.Location = new Point(9, 10);
            tableViewers.Margin = new Padding(4, 5, 4, 5);
            tableViewers.Name = "tableViewers";
            tableViewers.RowCount = 2;
            tableViewers.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableViewers.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            tableViewers.Size = new Size(1088, 802);
            tableViewers.TabIndex = 0;
            // 
            // grpViewerFleet
            // 
            grpViewerFleet.Controls.Add(tableViewerFleet);
            grpViewerFleet.Dock = DockStyle.Fill;
            grpViewerFleet.Location = new Point(4, 5);
            grpViewerFleet.Margin = new Padding(4, 5, 4, 10);
            grpViewerFleet.Name = "grpViewerFleet";
            grpViewerFleet.Padding = new Padding(11, 13, 11, 13);
            grpViewerFleet.Size = new Size(1080, 627);
            grpViewerFleet.TabIndex = 0;
            grpViewerFleet.TabStop = false;
            grpViewerFleet.Text = "Active Viewer Sessions";
            // 
            // tableViewerFleet
            // 
            tableViewerFleet.ColumnCount = 1;
            tableViewerFleet.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableViewerFleet.Controls.Add(panelViewerToolbar, 0, 0);
            tableViewerFleet.Controls.Add(lvViewers, 0, 1);
            tableViewerFleet.Dock = DockStyle.Fill;
            tableViewerFleet.Location = new Point(11, 37);
            tableViewerFleet.Margin = new Padding(4, 5, 4, 5);
            tableViewerFleet.Name = "tableViewerFleet";
            tableViewerFleet.RowCount = 2;
            tableViewerFleet.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableViewerFleet.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableViewerFleet.Size = new Size(1058, 577);
            tableViewerFleet.TabIndex = 0;
            // 
            // panelViewerToolbar
            // 
            panelViewerToolbar.ColumnCount = 3;
            panelViewerToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelViewerToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 179F));
            panelViewerToolbar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 179F));
            panelViewerToolbar.Controls.Add(lblFleetSummary, 0, 0);
            panelViewerToolbar.Controls.Add(btnMuteSelectedViewer, 1, 0);
            panelViewerToolbar.Controls.Add(btnKickSelectedViewer, 2, 0);
            panelViewerToolbar.Dock = DockStyle.Fill;
            panelViewerToolbar.Location = new Point(0, 0);
            panelViewerToolbar.Margin = new Padding(0);
            panelViewerToolbar.Name = "panelViewerToolbar";
            panelViewerToolbar.RowCount = 1;
            panelViewerToolbar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelViewerToolbar.Size = new Size(1058, 53);
            panelViewerToolbar.TabIndex = 0;
            // 
            // lblFleetSummary
            // 
            lblFleetSummary.Anchor = AnchorStyles.Left;
            lblFleetSummary.AutoSize = true;
            lblFleetSummary.Location = new Point(4, 14);
            lblFleetSummary.Margin = new Padding(4, 0, 4, 0);
            lblFleetSummary.Name = "lblFleetSummary";
            lblFleetSummary.Size = new Size(502, 25);
            lblFleetSummary.TabIndex = 0;
            lblFleetSummary.Text = "0 active viewers • Average latency: <1.0 ms • 0.0% frame drop";
            // 
            // btnMuteSelectedViewer
            // 
            btnMuteSelectedViewer.Dock = DockStyle.Fill;
            btnMuteSelectedViewer.Enabled = false;
            btnMuteSelectedViewer.Location = new Point(706, 3);
            btnMuteSelectedViewer.Margin = new Padding(6, 3, 6, 3);
            btnMuteSelectedViewer.Name = "btnMuteSelectedViewer";
            btnMuteSelectedViewer.Size = new Size(167, 47);
            btnMuteSelectedViewer.TabIndex = 1;
            btnMuteSelectedViewer.Text = "Mute / Unmute";
            btnMuteSelectedViewer.UseVisualStyleBackColor = true;
            // 
            // btnKickSelectedViewer
            // 
            btnKickSelectedViewer.Dock = DockStyle.Fill;
            btnKickSelectedViewer.Enabled = false;
            btnKickSelectedViewer.Location = new Point(879, 3);
            btnKickSelectedViewer.Margin = new Padding(0, 3, 0, 3);
            btnKickSelectedViewer.Name = "btnKickSelectedViewer";
            btnKickSelectedViewer.Size = new Size(179, 47);
            btnKickSelectedViewer.TabIndex = 2;
            btnKickSelectedViewer.Text = "Disconnect Viewer";
            btnKickSelectedViewer.UseVisualStyleBackColor = true;
            // 
            // lvViewers
            // 
            lvViewers.Columns.AddRange(new ColumnHeader[] { colDevice, colIp, colBrowser, colMode, colRtt, colUptime, colAudio });
            lvViewers.Dock = DockStyle.Fill;
            lvViewers.FullRowSelect = true;
            lvViewers.GridLines = true;
            lvViewers.Location = new Point(0, 60);
            lvViewers.Margin = new Padding(0, 7, 0, 0);
            lvViewers.MultiSelect = false;
            lvViewers.Name = "lvViewers";
            lvViewers.Size = new Size(1058, 517);
            lvViewers.TabIndex = 1;
            lvViewers.UseCompatibleStateImageBehavior = false;
            lvViewers.View = View.Details;
            // 
            // colDevice
            // 
            colDevice.Text = "Device";
            colDevice.Width = 135;
            // 
            // colIp
            // 
            colIp.Text = "IP Address";
            colIp.Width = 110;
            // 
            // colBrowser
            // 
            colBrowser.Text = "Browser";
            colBrowser.Width = 125;
            // 
            // colMode
            // 
            colMode.Text = "View Mode";
            colMode.Width = 125;
            // 
            // colRtt
            // 
            colRtt.Text = "Latency";
            colRtt.Width = 75;
            // 
            // colUptime
            // 
            colUptime.Text = "Connected";
            colUptime.Width = 85;
            // 
            // colAudio
            // 
            colAudio.Text = "Audio";
            colAudio.Width = 75;
            // 
            // grpAnnouncement
            // 
            grpAnnouncement.Controls.Add(tableAnnouncement);
            grpAnnouncement.Dock = DockStyle.Fill;
            grpAnnouncement.Location = new Point(4, 642);
            grpAnnouncement.Margin = new Padding(4, 0, 4, 5);
            grpAnnouncement.Name = "grpAnnouncement";
            grpAnnouncement.Padding = new Padding(11, 10, 11, 10);
            grpAnnouncement.Size = new Size(1080, 155);
            grpAnnouncement.TabIndex = 1;
            grpAnnouncement.TabStop = false;
            grpAnnouncement.Text = "Send On-Screen Announcement Banner to All Viewers";
            // 
            // tableAnnouncement
            // 
            tableAnnouncement.ColumnCount = 2;
            tableAnnouncement.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableAnnouncement.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 171F));
            tableAnnouncement.Controls.Add(txtAnnouncement, 0, 0);
            tableAnnouncement.Controls.Add(btnSendAnnouncement, 1, 0);
            tableAnnouncement.Controls.Add(lblAnnouncementStatus, 0, 1);
            tableAnnouncement.Dock = DockStyle.Fill;
            tableAnnouncement.Location = new Point(11, 34);
            tableAnnouncement.Margin = new Padding(4, 5, 4, 5);
            tableAnnouncement.Name = "tableAnnouncement";
            tableAnnouncement.RowCount = 2;
            tableAnnouncement.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableAnnouncement.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableAnnouncement.Size = new Size(1058, 111);
            tableAnnouncement.TabIndex = 0;
            // 
            // txtAnnouncement
            // 
            txtAnnouncement.Dock = DockStyle.Fill;
            txtAnnouncement.Location = new Point(0, 5);
            txtAnnouncement.Margin = new Padding(0, 5, 9, 5);
            txtAnnouncement.Name = "txtAnnouncement";
            txtAnnouncement.PlaceholderText = "Type message to display on connected viewer screens...";
            txtAnnouncement.Size = new Size(878, 31);
            txtAnnouncement.TabIndex = 0;
            // 
            // btnSendAnnouncement
            // 
            btnSendAnnouncement.Dock = DockStyle.Fill;
            btnSendAnnouncement.Location = new Point(887, 3);
            btnSendAnnouncement.Margin = new Padding(0, 3, 0, 3);
            btnSendAnnouncement.Name = "btnSendAnnouncement";
            btnSendAnnouncement.Size = new Size(171, 47);
            btnSendAnnouncement.TabIndex = 1;
            btnSendAnnouncement.Text = "Send Banner";
            btnSendAnnouncement.UseVisualStyleBackColor = true;
            // 
            // lblAnnouncementStatus
            // 
            lblAnnouncementStatus.Anchor = AnchorStyles.Left;
            lblAnnouncementStatus.AutoSize = true;
            tableAnnouncement.SetColumnSpan(lblAnnouncementStatus, 2);
            lblAnnouncementStatus.Font = new Font("Segoe UI", 8F);
            lblAnnouncementStatus.ForeColor = SystemColors.GrayText;
            lblAnnouncementStatus.Location = new Point(4, 71);
            lblAnnouncementStatus.Margin = new Padding(4, 0, 4, 0);
            lblAnnouncementStatus.Name = "lblAnnouncementStatus";
            lblAnnouncementStatus.Size = new Size(586, 21);
            lblAnnouncementStatus.TabIndex = 2;
            lblAnnouncementStatus.Text = "Announcements appear as temporary toast notifications on all connected browsers.";
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(tableSettings);
            tabSettings.Location = new Point(4, 36);
            tabSettings.Margin = new Padding(4, 5, 4, 5);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(9, 10, 9, 10);
            tabSettings.Size = new Size(1106, 822);
            tabSettings.TabIndex = 2;
            tabSettings.Text = "Settings & Logs";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // tableSettings
            // 
            tableSettings.ColumnCount = 1;
            tableSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableSettings.Controls.Add(tableSettingsTop, 0, 0);
            tableSettings.Controls.Add(grpLogs, 0, 1);
            tableSettings.Dock = DockStyle.Fill;
            tableSettings.Location = new Point(9, 10);
            tableSettings.Margin = new Padding(4, 5, 4, 5);
            tableSettings.Name = "tableSettings";
            tableSettings.RowCount = 2;
            tableSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, 358F));
            tableSettings.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableSettings.Size = new Size(1088, 802);
            tableSettings.TabIndex = 0;
            // 
            // tableSettingsTop
            // 
            tableSettingsTop.ColumnCount = 2;
            tableSettingsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            tableSettingsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tableSettingsTop.Controls.Add(grpQualityStartup, 0, 0);
            tableSettingsTop.Controls.Add(grpDiagnosticsFirewall, 1, 0);
            tableSettingsTop.Dock = DockStyle.Fill;
            tableSettingsTop.Location = new Point(0, 0);
            tableSettingsTop.Margin = new Padding(0);
            tableSettingsTop.Name = "tableSettingsTop";
            tableSettingsTop.RowCount = 1;
            tableSettingsTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableSettingsTop.Size = new Size(1088, 358);
            tableSettingsTop.TabIndex = 0;
            // 
            // grpQualityStartup
            // 
            grpQualityStartup.Controls.Add(tableQualityStartup);
            grpQualityStartup.Dock = DockStyle.Fill;
            grpQualityStartup.Location = new Point(4, 5);
            grpQualityStartup.Margin = new Padding(4, 5, 9, 10);
            grpQualityStartup.Name = "grpQualityStartup";
            grpQualityStartup.Padding = new Padding(11, 10, 11, 10);
            grpQualityStartup.Size = new Size(552, 343);
            grpQualityStartup.TabIndex = 0;
            grpQualityStartup.TabStop = false;
            grpQualityStartup.Text = "Stream Quality & Startup Behavior";
            // 
            // tableQualityStartup
            // 
            tableQualityStartup.ColumnCount = 2;
            tableQualityStartup.ColumnStyles.Add(new ColumnStyle());
            tableQualityStartup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableQualityStartup.Controls.Add(lblFpsDesc, 0, 0);
            tableQualityStartup.Controls.Add(comboFps, 1, 0);
            tableQualityStartup.Controls.Add(lblQualityDesc, 0, 1);
            tableQualityStartup.Controls.Add(panelQualityRow, 1, 1);
            tableQualityStartup.Controls.Add(cbAutoStart, 0, 2);
            tableQualityStartup.Controls.Add(cbMiniStart, 0, 3);
            tableQualityStartup.Controls.Add(cbCloseToTray, 0, 4);
            tableQualityStartup.Dock = DockStyle.Fill;
            tableQualityStartup.Location = new Point(11, 34);
            tableQualityStartup.Margin = new Padding(4, 5, 4, 5);
            tableQualityStartup.Name = "tableQualityStartup";
            tableQualityStartup.RowCount = 5;
            tableQualityStartup.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableQualityStartup.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableQualityStartup.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableQualityStartup.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableQualityStartup.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableQualityStartup.Size = new Size(530, 299);
            tableQualityStartup.TabIndex = 0;
            // 
            // lblFpsDesc
            // 
            lblFpsDesc.Anchor = AnchorStyles.Left;
            lblFpsDesc.AutoSize = true;
            lblFpsDesc.Location = new Point(0, 14);
            lblFpsDesc.Margin = new Padding(0, 0, 11, 0);
            lblFpsDesc.Name = "lblFpsDesc";
            lblFpsDesc.Size = new Size(148, 25);
            lblFpsDesc.TabIndex = 0;
            lblFpsDesc.Text = "Target Framerate:";
            // 
            // comboFps
            // 
            comboFps.Dock = DockStyle.Fill;
            comboFps.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFps.Items.AddRange(new object[] { "15 FPS (Low Bandwidth)", "30 FPS (Standard)", "60 FPS (Smooth)" });
            comboFps.Location = new Point(159, 5);
            comboFps.Margin = new Padding(0, 5, 0, 5);
            comboFps.Name = "comboFps";
            comboFps.Size = new Size(371, 33);
            comboFps.TabIndex = 1;
            // 
            // lblQualityDesc
            // 
            lblQualityDesc.Anchor = AnchorStyles.Left;
            lblQualityDesc.AutoSize = true;
            lblQualityDesc.Location = new Point(0, 75);
            lblQualityDesc.Margin = new Padding(0, 0, 11, 0);
            lblQualityDesc.Name = "lblQualityDesc";
            lblQualityDesc.Size = new Size(127, 25);
            lblQualityDesc.TabIndex = 2;
            lblQualityDesc.Text = "Image Quality:";
            // 
            // panelQualityRow
            // 
            panelQualityRow.ColumnCount = 2;
            panelQualityRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panelQualityRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            panelQualityRow.Controls.Add(qualitySlider, 0, 0);
            panelQualityRow.Controls.Add(lblQuality, 1, 0);
            panelQualityRow.Dock = DockStyle.Fill;
            panelQualityRow.Location = new Point(159, 53);
            panelQualityRow.Margin = new Padding(0);
            panelQualityRow.Name = "panelQualityRow";
            panelQualityRow.RowCount = 1;
            panelQualityRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelQualityRow.Size = new Size(371, 70);
            panelQualityRow.TabIndex = 3;
            // 
            // qualitySlider
            // 
            qualitySlider.AutoSize = false;
            qualitySlider.Dock = DockStyle.Fill;
            qualitySlider.LargeChange = 10;
            qualitySlider.Location = new Point(4, 5);
            qualitySlider.Margin = new Padding(4, 5, 4, 5);
            qualitySlider.Maximum = 100;
            qualitySlider.Minimum = 20;
            qualitySlider.Name = "qualitySlider";
            qualitySlider.Size = new Size(303, 60);
            qualitySlider.TabIndex = 0;
            qualitySlider.TickFrequency = 10;
            qualitySlider.Value = 80;
            // 
            // lblQuality
            // 
            lblQuality.Anchor = AnchorStyles.Right;
            lblQuality.AutoSize = true;
            lblQuality.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblQuality.Location = new Point(322, 23);
            lblQuality.Margin = new Padding(4, 0, 4, 0);
            lblQuality.Name = "lblQuality";
            lblQuality.Size = new Size(45, 23);
            lblQuality.TabIndex = 1;
            lblQuality.Text = "80%";
            // 
            // cbAutoStart
            // 
            cbAutoStart.AutoSize = true;
            tableQualityStartup.SetColumnSpan(cbAutoStart, 2);
            cbAutoStart.Location = new Point(4, 126);
            cbAutoStart.Margin = new Padding(4, 3, 4, 3);
            cbAutoStart.Name = "cbAutoStart";
            cbAutoStart.Size = new Size(484, 29);
            cbAutoStart.TabIndex = 4;
            cbAutoStart.Text = "Start broadcasting automatically when ScreenTask opens";
            cbAutoStart.UseVisualStyleBackColor = true;
            // 
            // cbMiniStart
            // 
            cbMiniStart.AutoSize = true;
            tableQualityStartup.SetColumnSpan(cbMiniStart, 2);
            cbMiniStart.Location = new Point(4, 169);
            cbMiniStart.Margin = new Padding(4, 3, 4, 3);
            cbMiniStart.Name = "cbMiniStart";
            cbMiniStart.Size = new Size(276, 29);
            cbMiniStart.TabIndex = 5;
            cbMiniStart.Text = "Start minimized in system tray";
            cbMiniStart.UseVisualStyleBackColor = true;
            // 
            // cbCloseToTray
            // 
            cbCloseToTray.AutoSize = true;
            cbCloseToTray.Checked = true;
            cbCloseToTray.CheckState = CheckState.Checked;
            tableQualityStartup.SetColumnSpan(cbCloseToTray, 2);
            cbCloseToTray.Location = new Point(4, 212);
            cbCloseToTray.Margin = new Padding(4, 3, 4, 3);
            cbCloseToTray.Name = "cbCloseToTray";
            cbCloseToTray.Size = new Size(414, 29);
            cbCloseToTray.TabIndex = 6;
            cbCloseToTray.Text = "Minimize to system tray when window is closed";
            cbCloseToTray.UseVisualStyleBackColor = true;
            // 
            // grpDiagnosticsFirewall
            // 
            grpDiagnosticsFirewall.Controls.Add(tableDiagFirewall);
            grpDiagnosticsFirewall.Dock = DockStyle.Fill;
            grpDiagnosticsFirewall.Location = new Point(569, 5);
            grpDiagnosticsFirewall.Margin = new Padding(4, 5, 4, 10);
            grpDiagnosticsFirewall.Name = "grpDiagnosticsFirewall";
            grpDiagnosticsFirewall.Padding = new Padding(11, 10, 11, 10);
            grpDiagnosticsFirewall.Size = new Size(515, 343);
            grpDiagnosticsFirewall.TabIndex = 1;
            grpDiagnosticsFirewall.TabStop = false;
            grpDiagnosticsFirewall.Text = "Audio Diagnostics & Windows Firewall";
            // 
            // tableDiagFirewall
            // 
            tableDiagFirewall.ColumnCount = 1;
            tableDiagFirewall.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableDiagFirewall.Controls.Add(lblAudioDiagInfo, 0, 0);
            tableDiagFirewall.Controls.Add(panelAudioTestButtons, 0, 1);
            tableDiagFirewall.Controls.Add(lblFirewallDesc, 0, 2);
            tableDiagFirewall.Controls.Add(btnFirewallRule, 0, 3);
            tableDiagFirewall.Dock = DockStyle.Fill;
            tableDiagFirewall.Location = new Point(11, 34);
            tableDiagFirewall.Margin = new Padding(4, 5, 4, 5);
            tableDiagFirewall.Name = "tableDiagFirewall";
            tableDiagFirewall.RowCount = 4;
            tableDiagFirewall.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tableDiagFirewall.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableDiagFirewall.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            tableDiagFirewall.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableDiagFirewall.Size = new Size(493, 299);
            tableDiagFirewall.TabIndex = 0;
            // 
            // lblAudioDiagInfo
            // 
            lblAudioDiagInfo.Dock = DockStyle.Fill;
            lblAudioDiagInfo.Font = new Font("Segoe UI", 8.25F);
            lblAudioDiagInfo.ForeColor = SystemColors.GrayText;
            lblAudioDiagInfo.Location = new Point(0, 3);
            lblAudioDiagInfo.Margin = new Padding(0, 3, 0, 3);
            lblAudioDiagInfo.Name = "lblAudioDiagInfo";
            lblAudioDiagInfo.Size = new Size(493, 64);
            lblAudioDiagInfo.TabIndex = 0;
            lblAudioDiagInfo.Text = "WASAPI Audio Engine Ready (48 kHz Low-Latency Stream)";
            // 
            // panelAudioTestButtons
            // 
            panelAudioTestButtons.Controls.Add(btnTestSpeaker);
            panelAudioTestButtons.Controls.Add(btnTestMic);
            panelAudioTestButtons.Dock = DockStyle.Fill;
            panelAudioTestButtons.Location = new Point(0, 70);
            panelAudioTestButtons.Margin = new Padding(0);
            panelAudioTestButtons.Name = "panelAudioTestButtons";
            panelAudioTestButtons.Size = new Size(493, 60);
            panelAudioTestButtons.TabIndex = 1;
            // 
            // btnTestSpeaker
            // 
            btnTestSpeaker.Location = new Point(0, 3);
            btnTestSpeaker.Margin = new Padding(0, 3, 11, 3);
            btnTestSpeaker.Name = "btnTestSpeaker";
            btnTestSpeaker.Size = new Size(186, 47);
            btnTestSpeaker.TabIndex = 0;
            btnTestSpeaker.Text = "Test Speaker Chime";
            btnTestSpeaker.UseVisualStyleBackColor = true;
            // 
            // btnTestMic
            // 
            btnTestMic.Location = new Point(197, 3);
            btnTestMic.Margin = new Padding(0, 3, 0, 3);
            btnTestMic.Name = "btnTestMic";
            btnTestMic.Size = new Size(193, 47);
            btnTestMic.TabIndex = 1;
            btnTestMic.Text = "Test Mic (3s)";
            btnTestMic.UseVisualStyleBackColor = true;
            // 
            // lblFirewallDesc
            // 
            lblFirewallDesc.Dock = DockStyle.Fill;
            lblFirewallDesc.Font = new Font("Segoe UI", 8.25F);
            lblFirewallDesc.ForeColor = SystemColors.GrayText;
            lblFirewallDesc.Location = new Point(0, 143);
            lblFirewallDesc.Margin = new Padding(0, 13, 0, 3);
            lblFirewallDesc.Name = "lblFirewallDesc";
            lblFirewallDesc.Size = new Size(493, 61);
            lblFirewallDesc.TabIndex = 2;
            lblFirewallDesc.Text = "If other devices on Wi-Fi cannot connect, allow the ScreenTask port through Windows Defender Firewall:";
            // 
            // btnFirewallRule
            // 
            btnFirewallRule.Anchor = AnchorStyles.Left;
            btnFirewallRule.AutoSize = true;
            btnFirewallRule.Location = new Point(0, 228);
            btnFirewallRule.Margin = new Padding(0, 3, 0, 3);
            btnFirewallRule.Name = "btnFirewallRule";
            btnFirewallRule.Padding = new Padding(8, 2, 8, 2);
            btnFirewallRule.Size = new Size(348, 50);
            btnFirewallRule.TabIndex = 3;
            btnFirewallRule.Text = "Allow Port in Windows Firewall (Admin)";
            btnFirewallRule.UseVisualStyleBackColor = true;
            // 
            // grpLogs
            // 
            grpLogs.Controls.Add(tableLogs);
            grpLogs.Dock = DockStyle.Fill;
            grpLogs.Location = new Point(4, 358);
            grpLogs.Margin = new Padding(4, 0, 4, 5);
            grpLogs.Name = "grpLogs";
            grpLogs.Padding = new Padding(11, 10, 11, 10);
            grpLogs.Size = new Size(1080, 439);
            grpLogs.TabIndex = 1;
            grpLogs.TabStop = false;
            grpLogs.Text = "Activity & Connection Log";
            // 
            // tableLogs
            // 
            tableLogs.ColumnCount = 1;
            tableLogs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLogs.Controls.Add(txtLog, 0, 0);
            tableLogs.Controls.Add(panelLogButtons, 0, 1);
            tableLogs.Dock = DockStyle.Fill;
            tableLogs.Location = new Point(11, 34);
            tableLogs.Margin = new Padding(4, 5, 4, 5);
            tableLogs.Name = "tableLogs";
            tableLogs.RowCount = 2;
            tableLogs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLogs.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLogs.Size = new Size(1058, 395);
            tableLogs.TabIndex = 0;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 8.5F);
            txtLog.Location = new Point(0, 3);
            txtLog.Margin = new Padding(0, 3, 0, 7);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1058, 332);
            txtLog.TabIndex = 0;
            txtLog.WordWrap = false;
            // 
            // panelLogButtons
            // 
            panelLogButtons.Controls.Add(btnCopyLog);
            panelLogButtons.Controls.Add(btnClearLog);
            panelLogButtons.Controls.Add(btnReportIssue);
            panelLogButtons.Dock = DockStyle.Fill;
            panelLogButtons.FlowDirection = FlowDirection.RightToLeft;
            panelLogButtons.Location = new Point(0, 342);
            panelLogButtons.Margin = new Padding(0);
            panelLogButtons.Name = "panelLogButtons";
            panelLogButtons.Size = new Size(1058, 53);
            panelLogButtons.TabIndex = 1;
            // 
            // btnCopyLog
            // 
            btnCopyLog.Location = new Point(937, 3);
            btnCopyLog.Margin = new Padding(0, 3, 0, 3);
            btnCopyLog.Name = "btnCopyLog";
            btnCopyLog.Size = new Size(121, 43);
            btnCopyLog.TabIndex = 0;
            btnCopyLog.Text = "Copy Log";
            btnCopyLog.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            btnClearLog.Location = new Point(816, 3);
            btnClearLog.Margin = new Padding(9, 3, 0, 3);
            btnClearLog.Name = "btnClearLog";
            btnClearLog.Size = new Size(121, 43);
            btnClearLog.TabIndex = 1;
            btnClearLog.Text = "Clear Log";
            btnClearLog.UseVisualStyleBackColor = true;
            // 
            // btnReportIssue
            // 
            btnReportIssue.Location = new Point(651, 3);
            btnReportIssue.Margin = new Padding(9, 3, 0, 3);
            btnReportIssue.Name = "btnReportIssue";
            btnReportIssue.Size = new Size(156, 43);
            btnReportIssue.TabIndex = 2;
            btnReportIssue.Text = "Report Bug / Issue...";
            btnReportIssue.UseVisualStyleBackColor = true;
            // 
            // statusStripMain
            // 
            statusStripMain.ImageScalingSize = new Size(24, 24);
            statusStripMain.Items.AddRange(new ToolStripItem[] { statusLabelServer, statusLabelViewers, statusLabelFps, statusLabelData, statusLabelUptime, statusLabelAudio, statusLabelCopyright });
            statusStripMain.Location = new Point(0, 897);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Padding = new Padding(1, 0, 20, 0);
            statusStripMain.Size = new Size(1114, 36);
            statusStripMain.TabIndex = 2;
            // 
            // statusLabelServer
            // 
            statusLabelServer.Name = "statusLabelServer";
            statusLabelServer.Size = new Size(431, 29);
            statusLabelServer.Spring = true;
            statusLabelServer.Text = "● Ready to stream";
            statusLabelServer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusLabelViewers
            // 
            statusLabelViewers.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelViewers.BorderStyle = Border3DStyle.Etched;
            statusLabelViewers.IsLink = true;
            statusLabelViewers.Name = "statusLabelViewers";
            statusLabelViewers.ToolTipText = "Click to open Connected Viewers tab";
            statusLabelViewers.Size = new Size(89, 29);
            statusLabelViewers.Text = "0 viewers";
            // 
            // statusLabelFps
            // 
            statusLabelFps.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelFps.BorderStyle = Border3DStyle.Etched;
            statusLabelFps.Name = "statusLabelFps";
            statusLabelFps.Size = new Size(60, 29);
            statusLabelFps.Text = "0 FPS";
            // 
            // statusLabelData
            // 
            statusLabelData.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelData.BorderStyle = Border3DStyle.Etched;
            statusLabelData.Name = "statusLabelData";
            statusLabelData.Size = new Size(71, 29);
            statusLabelData.Text = "0.0 MB";
            // 
            // statusLabelUptime
            // 
            statusLabelUptime.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelUptime.BorderStyle = Border3DStyle.Etched;
            statusLabelUptime.Name = "statusLabelUptime";
            statusLabelUptime.Size = new Size(84, 29);
            statusLabelUptime.Text = "00:00:00";
            // 
            // statusLabelAudio
            // 
            statusLabelAudio.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelAudio.BorderStyle = Border3DStyle.Etched;
            statusLabelAudio.Name = "statusLabelAudio";
            statusLabelAudio.Size = new Size(99, 29);
            statusLabelAudio.Text = "Audio: Off";
            // 
            // statusLabelCopyright
            // 
            statusLabelCopyright.BorderSides = ToolStripStatusLabelBorderSides.Left;
            statusLabelCopyright.BorderStyle = Border3DStyle.Etched;
            statusLabelCopyright.IsLink = true;
            statusLabelCopyright.Name = "statusLabelCopyright";
            statusLabelCopyright.Size = new Size(259, 29);
            statusLabelCopyright.Text = "© 2014-2026 Eslam Hamouda";
            statusLabelCopyright.ToolTipText = "ScreenTask 2.0 • Free & Open Source under GPLv3 • screentask.eslamx.com";
            // 
            // appNotify
            // 
            appNotify.ContextMenuStrip = trayContextMenu;
            appNotify.Icon = (Icon)resources.GetObject("appNotify.Icon");
            appNotify.Text = "ScreenTask 2.0";
            appNotify.Visible = true;
            // 
            // trayContextMenu
            // 
            trayContextMenu.ImageScalingSize = new Size(24, 24);
            trayContextMenu.Name = "trayContextMenu";
            trayContextMenu.Size = new Size(61, 4);
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1460, 960);
            Controls.Add(tabControlMain);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            Font = new Font("Segoe UI", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1240, 860);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ScreenTask 2.0";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            tabControlMain.ResumeLayout(false);
            tabBroadcast.ResumeLayout(false);
            tableBroadcast.ResumeLayout(false);
            grpPreview.ResumeLayout(false);
            tablePreviewInner.ResumeLayout(false);
            panelSourceBar.ResumeLayout(false);
            panelSourceBar.PerformLayout();
            panelCaptureFlags.ResumeLayout(false);
            panelCaptureFlags.PerformLayout();
            tableBroadcastRight.ResumeLayout(false);
            grpSessionControl.ResumeLayout(false);
            tableSessionControl.ResumeLayout(false);
            tableSessionControl.PerformLayout();
            panelSessionSubActions.ResumeLayout(false);
            panelAudioSelector.ResumeLayout(false);
            panelAudioMeterRow.ResumeLayout(false);
            panelAudioMeterRow.PerformLayout();
            grpConnection.ResumeLayout(false);
            tableConnection.ResumeLayout(false);
            tableConnection.PerformLayout();
            panelAdapterRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            panelUrlRow.ResumeLayout(false);
            panelUrlRow.PerformLayout();
            panelPinQrRow.ResumeLayout(false);
            panelPinControls.ResumeLayout(false);
            panelPinControls.PerformLayout();
            panelPinInputRow.ResumeLayout(false);
            panelPinInputRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPairingQr).EndInit();
            tabViewers.ResumeLayout(false);
            tableViewers.ResumeLayout(false);
            grpViewerFleet.ResumeLayout(false);
            tableViewerFleet.ResumeLayout(false);
            panelViewerToolbar.ResumeLayout(false);
            panelViewerToolbar.PerformLayout();
            grpAnnouncement.ResumeLayout(false);
            tableAnnouncement.ResumeLayout(false);
            tableAnnouncement.PerformLayout();
            tabSettings.ResumeLayout(false);
            tableSettings.ResumeLayout(false);
            tableSettingsTop.ResumeLayout(false);
            grpQualityStartup.ResumeLayout(false);
            tableQualityStartup.ResumeLayout(false);
            tableQualityStartup.PerformLayout();
            panelQualityRow.ResumeLayout(false);
            panelQualityRow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)qualitySlider).EndInit();
            grpDiagnosticsFirewall.ResumeLayout(false);
            tableDiagFirewall.ResumeLayout(false);
            tableDiagFirewall.PerformLayout();
            panelAudioTestButtons.ResumeLayout(false);
            grpLogs.ResumeLayout(false);
            tableLogs.ResumeLayout(false);
            tableLogs.PerformLayout();
            panelLogButtons.ResumeLayout(false);
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // MenuStrip
        internal System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        internal System.Windows.Forms.ToolStripMenuItem menuFileToggleBroadcast;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        internal System.Windows.Forms.ToolStripMenuItem menuFileCopyLink;
        internal System.Windows.Forms.ToolStripMenuItem menuFileOpenBrowser;
        private System.Windows.Forms.ToolStripSeparator menuFileSep2;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuView;
        private System.Windows.Forms.ToolStripMenuItem menuViewBroadcast;
        private System.Windows.Forms.ToolStripMenuItem menuViewViewers;
        private System.Windows.Forms.ToolStripMenuItem menuViewSettings;
        private System.Windows.Forms.ToolStripMenuItem menuCapture;
        private System.Windows.Forms.ToolStripMenuItem menuCapturePickSource;
        private System.Windows.Forms.ToolStripMenuItem menuCaptureRefreshSources;
        private System.Windows.Forms.ToolStripSeparator menuCaptureSep1;
        internal System.Windows.Forms.ToolStripMenuItem menuCaptureShowMouse;
        internal System.Windows.Forms.ToolStripMenuItem menuCaptureEnableAudio;
        private System.Windows.Forms.ToolStripMenuItem menuNetwork;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkRefreshAdapters;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkFirewallRule;
        private System.Windows.Forms.ToolStripSeparator menuNetworkSep1;
        internal System.Windows.Forms.ToolStripMenuItem menuNetworkPinProtection;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkGenPin;
        private System.Windows.Forms.ToolStripMenuItem menuNetworkCopyPin;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        internal System.Windows.Forms.ToolStripMenuItem menuHelpReportBug;
        internal System.Windows.Forms.ToolStripMenuItem menuHelpRequestFeature;
        private System.Windows.Forms.ToolStripSeparator menuHelpSepFeedback;
        private System.Windows.Forms.ToolStripMenuItem menuHelpWebsite;
        private System.Windows.Forms.ToolStripMenuItem menuHelpGitHub;
        private System.Windows.Forms.ToolStripMenuItem menuHelpBuyMeCoffee;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep1;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        internal System.Windows.Forms.ToolStripMenuItem menuHelpCheckUpdates;
        internal System.Windows.Forms.ToolStripMenuItem menuFeedbackQuick;

        // Main TabControl
        internal System.Windows.Forms.TabControl tabControlMain;
        internal System.Windows.Forms.TabPage tabBroadcast;
        internal System.Windows.Forms.TabPage tabViewers;
        internal System.Windows.Forms.TabPage tabSettings;

        // Tab 0: Broadcast
        private System.Windows.Forms.TableLayoutPanel tableBroadcast;
        internal System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.TableLayoutPanel tablePreviewInner;
        internal ScreenTask.UI.MonitorPreview monitorPreview;
        private System.Windows.Forms.TableLayoutPanel panelSourceBar;
        private System.Windows.Forms.Label lblSource;
        internal System.Windows.Forms.ComboBox comboScreens;
        internal System.Windows.Forms.Button btnRefreshScreens;
        internal System.Windows.Forms.Button btnSwitchSource;
        private System.Windows.Forms.FlowLayoutPanel panelCaptureFlags;
        internal System.Windows.Forms.CheckBox cbCaptureMouse;
        internal System.Windows.Forms.CheckBox cbAudio;
        internal System.Windows.Forms.Button btnPopout;

        private System.Windows.Forms.TableLayoutPanel tableBroadcastRight;
        internal System.Windows.Forms.GroupBox grpSessionControl;
        private System.Windows.Forms.TableLayoutPanel tableSessionControl;
        internal System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TableLayoutPanel panelSessionSubActions;
        internal System.Windows.Forms.Button btnFreeze;
        internal System.Windows.Forms.Button btnQuickMute;
        private System.Windows.Forms.Label lblAudioDevice;
        private System.Windows.Forms.TableLayoutPanel panelAudioSelector;
        internal System.Windows.Forms.ComboBox comboAudioDevices;
        internal System.Windows.Forms.Button btnRefreshAudio;
        private System.Windows.Forms.TableLayoutPanel panelAudioMeterRow;
        internal System.Windows.Forms.Label lblAudioMeterText;
        internal System.Windows.Forms.ProgressBar pbAudioMeter;

        internal System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.TableLayoutPanel tableConnection;
        private System.Windows.Forms.Label lblAdapter;
        private System.Windows.Forms.TableLayoutPanel panelAdapterRow;
        internal System.Windows.Forms.ComboBox comboIPs;
        internal System.Windows.Forms.NumericUpDown numPort;
        internal System.Windows.Forms.Button btnRefreshIPs;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TableLayoutPanel panelUrlRow;
        internal System.Windows.Forms.TextBox txtURL;
        internal System.Windows.Forms.Button btnCopyUrl;
        internal System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.TableLayoutPanel panelPinQrRow;
        private System.Windows.Forms.FlowLayoutPanel panelPinControls;
        internal System.Windows.Forms.CheckBox cbPrivate;
        private System.Windows.Forms.FlowLayoutPanel panelPinInputRow;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Button btnGenPin;
        internal System.Windows.Forms.Button btnCopyPin;
        private System.Windows.Forms.Label lblQrHint;
        internal System.Windows.Forms.PictureBox picPairingQr;

        // Tab 1: Connected Viewers
        private System.Windows.Forms.TableLayoutPanel tableViewers;
        internal System.Windows.Forms.GroupBox grpViewerFleet;
        private System.Windows.Forms.TableLayoutPanel tableViewerFleet;
        private System.Windows.Forms.TableLayoutPanel panelViewerToolbar;
        internal System.Windows.Forms.Label lblFleetSummary;
        internal System.Windows.Forms.Button btnMuteSelectedViewer;
        internal System.Windows.Forms.Button btnKickSelectedViewer;
        internal System.Windows.Forms.ListView lvViewers;
        private System.Windows.Forms.ColumnHeader colDevice;
        private System.Windows.Forms.ColumnHeader colIp;
        private System.Windows.Forms.ColumnHeader colBrowser;
        private System.Windows.Forms.ColumnHeader colMode;
        private System.Windows.Forms.ColumnHeader colRtt;
        private System.Windows.Forms.ColumnHeader colUptime;
        private System.Windows.Forms.ColumnHeader colAudio;

        internal System.Windows.Forms.GroupBox grpAnnouncement;
        private System.Windows.Forms.TableLayoutPanel tableAnnouncement;
        internal System.Windows.Forms.TextBox txtAnnouncement;
        internal System.Windows.Forms.Button btnSendAnnouncement;
        internal System.Windows.Forms.Label lblAnnouncementStatus;

        // Tab 2: Settings & Logs
        private System.Windows.Forms.TableLayoutPanel tableSettings;
        private System.Windows.Forms.TableLayoutPanel tableSettingsTop;
        internal System.Windows.Forms.GroupBox grpQualityStartup;
        private System.Windows.Forms.TableLayoutPanel tableQualityStartup;
        private System.Windows.Forms.Label lblFpsDesc;
        internal System.Windows.Forms.ComboBox comboFps;
        private System.Windows.Forms.Label lblQualityDesc;
        private System.Windows.Forms.TableLayoutPanel panelQualityRow;
        internal System.Windows.Forms.TrackBar qualitySlider;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.CheckBox cbAutoStart;
        internal System.Windows.Forms.CheckBox cbMiniStart;
        internal System.Windows.Forms.CheckBox cbCloseToTray;

        internal System.Windows.Forms.GroupBox grpDiagnosticsFirewall;
        private System.Windows.Forms.TableLayoutPanel tableDiagFirewall;
        internal System.Windows.Forms.Label lblAudioDiagInfo;
        private System.Windows.Forms.FlowLayoutPanel panelAudioTestButtons;
        internal System.Windows.Forms.Button btnTestSpeaker;
        internal System.Windows.Forms.Button btnTestMic;
        private System.Windows.Forms.Label lblFirewallDesc;
        internal System.Windows.Forms.Button btnFirewallRule;

        internal System.Windows.Forms.GroupBox grpLogs;
        private System.Windows.Forms.TableLayoutPanel tableLogs;
        internal System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.FlowLayoutPanel panelLogButtons;
        internal System.Windows.Forms.Button btnCopyLog;
        internal System.Windows.Forms.Button btnClearLog;
        internal System.Windows.Forms.Button btnReportIssue;

        // Footer & Tray
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
    }
}
