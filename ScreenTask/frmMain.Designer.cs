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
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.btnMiniMode = new System.Windows.Forms.Button();

            // Side Navigation
            this.panelSideNav = new System.Windows.Forms.Panel();
            this.btnNavBroadcast = new System.Windows.Forms.Button();
            this.btnNavClients = new System.Windows.Forms.Button();
            this.btnNavCapture = new System.Windows.Forms.Button();
            this.btnNavNetwork = new System.Windows.Forms.Button();
            this.btnNavPrefs = new System.Windows.Forms.Button();

            // Central Content Container hosting the 5 Page Views
            this.panelContentContainer = new System.Windows.Forms.Panel();
            this.viewBroadcast = new ScreenTask.UI.Views.BroadcastView();
            this.viewClients = new ScreenTask.UI.Views.ConnectedViewersView();
            this.viewCapture = new ScreenTask.UI.Views.CaptureSettingsView();
            this.viewNetwork = new ScreenTask.UI.Views.NetworkSettingsView();
            this.viewPreferences = new ScreenTask.UI.Views.PreferencesView();

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

            this.panelSideNav.SuspendLayout();
            this.panelContentContainer.SuspendLayout();
            this.statusStripMain.SuspendLayout();
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
            this.panelSideNav.Controls.Add(this.lblAppSubtitle);
            this.panelSideNav.Controls.Add(this.btnNavBroadcast);
            this.panelSideNav.Controls.Add(this.btnNavClients);
            this.panelSideNav.Controls.Add(this.btnNavCapture);
            this.panelSideNav.Controls.Add(this.btnNavNetwork);
            this.panelSideNav.Controls.Add(this.btnNavPrefs);
            this.panelSideNav.Location = new System.Drawing.Point(12, 12);
            this.panelSideNav.Name = "panelSideNav";
            this.panelSideNav.Size = new System.Drawing.Size(174, 574);
            // 
            // btnNavBroadcast
            // 
            this.btnNavBroadcast.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnNavBroadcast.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavBroadcast.Location = new System.Drawing.Point(8, 64);
            this.btnNavBroadcast.Name = "btnNavBroadcast";
            this.btnNavBroadcast.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNavBroadcast.Size = new System.Drawing.Size(156, 38);
            this.btnNavBroadcast.TabIndex = 0;
            this.btnNavBroadcast.Text = "📡 Broadcast";
            this.btnNavBroadcast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBroadcast.UseVisualStyleBackColor = true;
            // 
            // btnNavClients
            // 
            this.btnNavClients.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnNavClients.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavClients.Location = new System.Drawing.Point(8, 106);
            this.btnNavClients.Name = "btnNavClients";
            this.btnNavClients.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNavClients.Size = new System.Drawing.Size(156, 38);
            this.btnNavClients.TabIndex = 1;
            this.btnNavClients.Text = "👥 Viewers & QR";
            this.btnNavClients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavClients.UseVisualStyleBackColor = true;
            // 
            // btnNavCapture
            // 
            this.btnNavCapture.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnNavCapture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavCapture.Location = new System.Drawing.Point(8, 148);
            this.btnNavCapture.Name = "btnNavCapture";
            this.btnNavCapture.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNavCapture.Size = new System.Drawing.Size(156, 38);
            this.btnNavCapture.TabIndex = 2;
            this.btnNavCapture.Text = "🎥 Screen & Audio";
            this.btnNavCapture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCapture.UseVisualStyleBackColor = true;
            // 
            // btnNavNetwork
            // 
            this.btnNavNetwork.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnNavNetwork.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavNetwork.Location = new System.Drawing.Point(8, 190);
            this.btnNavNetwork.Name = "btnNavNetwork";
            this.btnNavNetwork.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNavNetwork.Size = new System.Drawing.Size(156, 38);
            this.btnNavNetwork.TabIndex = 3;
            this.btnNavNetwork.Text = "🛡️ Network & PIN";
            this.btnNavNetwork.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavNetwork.UseVisualStyleBackColor = true;
            // 
            // btnNavPrefs
            // 
            this.btnNavPrefs.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.btnNavPrefs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNavPrefs.Location = new System.Drawing.Point(8, 232);
            this.btnNavPrefs.Name = "btnNavPrefs";
            this.btnNavPrefs.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNavPrefs.Size = new System.Drawing.Size(156, 38);
            this.btnNavPrefs.TabIndex = 4;
            this.btnNavPrefs.Text = "⚙️ Settings & Logs";
            this.btnNavPrefs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavPrefs.UseVisualStyleBackColor = true;

            // 
            // panelContentContainer
            // 
            this.panelContentContainer.Controls.Add(this.viewBroadcast);
            this.panelContentContainer.Controls.Add(this.viewClients);
            this.panelContentContainer.Controls.Add(this.viewCapture);
            this.panelContentContainer.Controls.Add(this.viewNetwork);
            this.panelContentContainer.Controls.Add(this.viewPreferences);
            this.panelContentContainer.Location = new System.Drawing.Point(196, 44);
            this.panelContentContainer.Name = "panelContentContainer";
            this.panelContentContainer.Size = new System.Drawing.Size(652, 544);
            this.panelContentContainer.TabIndex = 1;

            // 
            // viewBroadcast
            // 
            this.viewBroadcast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBroadcast.Location = new System.Drawing.Point(0, 0);
            this.viewBroadcast.Name = "viewBroadcast";
            this.viewBroadcast.Size = new System.Drawing.Size(652, 544);
            this.viewBroadcast.TabIndex = 0;
            this.viewBroadcast.Visible = true;

            // 
            // viewClients
            // 
            this.viewClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewClients.Location = new System.Drawing.Point(0, 0);
            this.viewClients.Name = "viewClients";
            this.viewClients.Size = new System.Drawing.Size(652, 544);
            this.viewClients.TabIndex = 1;
            this.viewClients.Visible = false;

            // 
            // viewCapture
            // 
            this.viewCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewCapture.Location = new System.Drawing.Point(0, 0);
            this.viewCapture.Name = "viewCapture";
            this.viewCapture.Size = new System.Drawing.Size(652, 544);
            this.viewCapture.TabIndex = 2;
            this.viewCapture.Visible = false;

            // 
            // viewNetwork
            // 
            this.viewNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewNetwork.Location = new System.Drawing.Point(0, 0);
            this.viewNetwork.Name = "viewNetwork";
            this.viewNetwork.Size = new System.Drawing.Size(652, 544);
            this.viewNetwork.TabIndex = 3;
            this.viewNetwork.Visible = false;

            // 
            // viewPreferences
            // 
            this.viewPreferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewPreferences.Location = new System.Drawing.Point(0, 0);
            this.viewPreferences.Name = "viewPreferences";
            this.viewPreferences.Size = new System.Drawing.Size(652, 544);
            this.viewPreferences.TabIndex = 4;
            this.viewPreferences.Visible = false;

            // 
            // statusStripMain
            // 
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
            this.statusStripMain.Location = new System.Drawing.Point(0, 596);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.ShowItemToolTips = true;
            this.statusStripMain.Size = new System.Drawing.Size(860, 24);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 2;
            // 
            // statusLabelServer
            // 
            this.statusLabelServer.Name = "statusLabelServer";
            this.statusLabelServer.Size = new System.Drawing.Size(200, 19);
            this.statusLabelServer.Spring = true;
            this.statusLabelServer.Text = "● Ready to stream";
            this.statusLabelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabelServer.ToolTipText = "Server and streaming status";
            // 
            // statusLabelViewers
            // 
            this.statusLabelViewers.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelViewers.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelViewers.Name = "statusLabelViewers";
            this.statusLabelViewers.Size = new System.Drawing.Size(75, 19);
            this.statusLabelViewers.Text = "👥 0 viewers";
            this.statusLabelViewers.ToolTipText = "Connected client sessions";
            // 
            // statusLabelFps
            // 
            this.statusLabelFps.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelFps.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelFps.Name = "statusLabelFps";
            this.statusLabelFps.Size = new System.Drawing.Size(55, 19);
            this.statusLabelFps.Text = "🎞️ 0 FPS";
            this.statusLabelFps.ToolTipText = "Streaming frame rate";
            // 
            // statusLabelData
            // 
            this.statusLabelData.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelData.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelData.Name = "statusLabelData";
            this.statusLabelData.Size = new System.Drawing.Size(65, 19);
            this.statusLabelData.Text = "📊 0.0 MB";
            this.statusLabelData.ToolTipText = "Total data transferred";
            // 
            // statusLabelUptime
            // 
            this.statusLabelUptime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelUptime.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelUptime.Name = "statusLabelUptime";
            this.statusLabelUptime.Size = new System.Drawing.Size(70, 19);
            this.statusLabelUptime.Text = "⏱️ 00:00:00";
            this.statusLabelUptime.ToolTipText = "Broadcast stream time";
            // 
            // statusLabelAudio
            // 
            this.statusLabelAudio.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelAudio.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelAudio.Name = "statusLabelAudio";
            this.statusLabelAudio.Size = new System.Drawing.Size(85, 19);
            this.statusLabelAudio.Text = "🔇 Audio: Off";
            this.statusLabelAudio.ToolTipText = "WASAPI system audio capture";
            // 
            // statusLabelCopyright
            // 
            this.statusLabelCopyright.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelCopyright.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusLabelCopyright.IsLink = true;
            this.statusLabelCopyright.Name = "statusLabelCopyright";
            this.statusLabelCopyright.Size = new System.Drawing.Size(155, 19);
            this.statusLabelCopyright.Text = "© 2014-2026 Eslam Hamouda";
            this.statusLabelCopyright.ToolTipText = "ScreenTask 2.0 • Free & Open Source under GPLv3 • screentask.eslamx.com";
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
            this.Controls.Add(this.panelContentContainer);
            this.Controls.Add(this.panelSideNav);
            this.Controls.Add(this.btnMiniMode);
            this.Controls.Add(this.lblStatusBadge);
            this.Controls.Add(this.statusStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenTask 2.0 – Offline & LAN Screen Sharing";

            this.toolTipMain.SetToolTip(this.btnMiniMode, "Switch to compact mini bar");

            this.panelSideNav.ResumeLayout(false);
            this.panelSideNav.PerformLayout();
            this.panelContentContainer.ResumeLayout(false);
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

        // Side Navigation
        private System.Windows.Forms.Panel panelSideNav;
        internal System.Windows.Forms.Button btnNavBroadcast;
        internal System.Windows.Forms.Button btnNavClients;
        internal System.Windows.Forms.Button btnNavCapture;
        internal System.Windows.Forms.Button btnNavNetwork;
        internal System.Windows.Forms.Button btnNavPrefs;

        // Content Container & Views
        private System.Windows.Forms.Panel panelContentContainer;
        internal ScreenTask.UI.Views.BroadcastView viewBroadcast;
        internal ScreenTask.UI.Views.ConnectedViewersView viewClients;
        internal ScreenTask.UI.Views.CaptureSettingsView viewCapture;
        internal ScreenTask.UI.Views.NetworkSettingsView viewNetwork;
        internal ScreenTask.UI.Views.PreferencesView viewPreferences;

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
    }
}
