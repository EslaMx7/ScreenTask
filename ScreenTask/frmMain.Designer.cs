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
            this.sideNav = new ScreenTask.UI.Controls.FluentSideNav();

            // Central Content Container hosting the 5 Page Views
            this.panelContentContainer = new System.Windows.Forms.Panel();
            this.viewBroadcast = new ScreenTask.UI.Views.BroadcastView();
            this.viewClients = new ScreenTask.UI.Views.ConnectedViewersView();
            this.viewCapture = new ScreenTask.UI.Views.CaptureSettingsView();
            this.viewNetwork = new ScreenTask.UI.Views.NetworkSettingsView();
            this.viewPreferences = new ScreenTask.UI.Views.PreferencesView();

            // Footer & System Tray
            this.lblMe = new System.Windows.Forms.Label();
            this.appNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.trayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);

            this.panelSideNav.SuspendLayout();
            this.panelContentContainer.SuspendLayout();
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
            this.panelSideNav.Controls.Add(this.sideNav);
            this.panelSideNav.Location = new System.Drawing.Point(12, 12);
            this.panelSideNav.Name = "panelSideNav";
            this.panelSideNav.Size = new System.Drawing.Size(174, 574);

            // 
            // sideNav
            // 
            this.sideNav.AddItem("Broadcast", "📡");
            this.sideNav.AddItem("Connected Viewers & QR", "👥");
            this.sideNav.AddItem("Screen & Audio", "🎥");
            this.sideNav.AddItem("Network & PIN", "🛡️");
            this.sideNav.AddItem("Settings & Logs", "⚙️");
            this.sideNav.Location = new System.Drawing.Point(8, 64);
            this.sideNav.Name = "sideNav";
            this.sideNav.Size = new System.Drawing.Size(156, 218);
            this.sideNav.TabIndex = 0;

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
            this.Controls.Add(this.panelContentContainer);
            this.Controls.Add(this.lblMe);
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

            this.toolTipMain.SetToolTip(this.btnMiniMode, "Switch to compact mini bar");

            this.panelSideNav.ResumeLayout(false);
            this.panelSideNav.PerformLayout();
            this.panelContentContainer.ResumeLayout(false);
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
        private ScreenTask.UI.Controls.FluentSideNav sideNav;

        // Content Container & Views
        private System.Windows.Forms.Panel panelContentContainer;
        internal ScreenTask.UI.Views.BroadcastView viewBroadcast;
        internal ScreenTask.UI.Views.ConnectedViewersView viewClients;
        internal ScreenTask.UI.Views.CaptureSettingsView viewCapture;
        internal ScreenTask.UI.Views.NetworkSettingsView viewNetwork;
        internal ScreenTask.UI.Views.PreferencesView viewPreferences;

        // Footer & System Tray
        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.NotifyIcon appNotify;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.ContextMenuStrip trayContextMenu;
    }
}
