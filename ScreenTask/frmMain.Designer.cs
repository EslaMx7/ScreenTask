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

            // Header labels
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblStatusBadge = new System.Windows.Forms.Label();

            // Card 1: Capture Source
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboScreens = new System.Windows.Forms.ComboBox();
            this.btnRefreshScreens = new System.Windows.Forms.Button();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.cbAudio = new System.Windows.Forms.CheckBox();
            this.comboAudioDevices = new System.Windows.Forms.ComboBox();
            this.btnRefreshAudio = new System.Windows.Forms.Button();
            this.pbAudioMeter = new System.Windows.Forms.ProgressBar();
            this.btnTestAudio = new System.Windows.Forms.Button();
            this.btnAudioDiag = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.comboFps = new System.Windows.Forms.ComboBox();
            this.lblQuality = new System.Windows.Forms.Label();
            this.qualitySlider = new System.Windows.Forms.TrackBar();

            // Card 2: Network & Security
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboIPs = new System.Windows.Forms.ComboBox();
            this.btnRefreshIPs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);

            // Card 3: Broadcast Control
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnCopyUrl = new System.Windows.Forms.Button();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnQr = new System.Windows.Forms.Button();
            this.lblLiveStats = new System.Windows.Forms.Label();

            // Card 4: Preferences
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.cbMiniStart = new System.Windows.Forms.CheckBox();

            // Log & Footer
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblMe = new System.Windows.Forms.Label();
            this.appNotify = new System.Windows.Forms.NotifyIcon(this.components);

            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.lblAppTitle.Location = new System.Drawing.Point(16, 12);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(155, 28);
            this.lblAppTitle.Text = "ScreenTask 2.0";

            // 
            // lblAppSubtitle
            // 
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblAppSubtitle.Location = new System.Drawing.Point(18, 42);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(225, 15);
            this.lblAppSubtitle.Text = "Free & Open Source LAN Screen Sharing";

            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStatusBadge.Location = new System.Drawing.Point(280, 18);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(154, 25);
            this.lblStatusBadge.Text = "● READY";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // groupBox1 (Capture Source & Display)
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboScreens);
            this.groupBox1.Controls.Add(this.btnRefreshScreens);
            this.groupBox1.Controls.Add(this.cbCaptureMouse);
            this.groupBox1.Controls.Add(this.cbAudio);
            this.groupBox1.Controls.Add(this.comboAudioDevices);
            this.groupBox1.Controls.Add(this.btnRefreshAudio);
            this.groupBox1.Controls.Add(this.pbAudioMeter);
            this.groupBox1.Controls.Add(this.btnTestAudio);
            this.groupBox1.Controls.Add(this.btnAudioDiag);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboFps);
            this.groupBox1.Controls.Add(this.lblQuality);
            this.groupBox1.Controls.Add(this.qualitySlider);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.groupBox1.Location = new System.Drawing.Point(16, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 135);
            this.groupBox1.Text = "Capture Source && Display";

            // label7 (Source:)
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.Text = "Source:";

            // comboScreens
            this.comboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboScreens.FormattingEnabled = true;
            this.comboScreens.Location = new System.Drawing.Point(68, 22);
            this.comboScreens.Name = "comboScreens";
            this.comboScreens.Size = new System.Drawing.Size(304, 21);

            // btnRefreshScreens
            this.btnRefreshScreens.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnRefreshScreens.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshScreens.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnRefreshScreens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshScreens.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshScreens.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.btnRefreshScreens.Location = new System.Drawing.Point(376, 20);
            this.btnRefreshScreens.Name = "btnRefreshScreens";
            this.btnRefreshScreens.Size = new System.Drawing.Size(28, 25);
            this.btnRefreshScreens.Text = "🔄";
            this.btnRefreshScreens.UseVisualStyleBackColor = false;

            // cbCaptureMouse
            this.cbCaptureMouse.AutoSize = true;
            this.cbCaptureMouse.Checked = true;
            this.cbCaptureMouse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCaptureMouse.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbCaptureMouse.ForeColor = System.Drawing.Color.White;
            this.cbCaptureMouse.Location = new System.Drawing.Point(15, 58);
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.Size = new System.Drawing.Size(90, 19);
            this.cbCaptureMouse.Text = "Show Cursor";
            this.cbCaptureMouse.UseVisualStyleBackColor = false;

            // cbAudio
            this.cbAudio.AutoSize = true;
            this.cbAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAudio.ForeColor = System.Drawing.Color.White;
            this.cbAudio.Location = new System.Drawing.Point(108, 58);
            this.cbAudio.Name = "cbAudio";
            this.cbAudio.Size = new System.Drawing.Size(58, 19);
            this.cbAudio.Text = "Audio:";
            this.cbAudio.UseVisualStyleBackColor = false;

            // comboAudioDevices
            this.comboAudioDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioDevices.Enabled = false;
            this.comboAudioDevices.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboAudioDevices.FormattingEnabled = true;
            this.comboAudioDevices.Location = new System.Drawing.Point(168, 56);
            this.comboAudioDevices.Name = "comboAudioDevices";
            this.comboAudioDevices.Size = new System.Drawing.Size(108, 21);

            // btnRefreshAudio
            this.btnRefreshAudio.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnRefreshAudio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshAudio.Enabled = false;
            this.btnRefreshAudio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnRefreshAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshAudio.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.btnRefreshAudio.Location = new System.Drawing.Point(280, 54);
            this.btnRefreshAudio.Name = "btnRefreshAudio";
            this.btnRefreshAudio.Size = new System.Drawing.Size(26, 25);
            this.btnRefreshAudio.Text = "🔄";
            this.btnRefreshAudio.UseVisualStyleBackColor = false;

            // pbAudioMeter
            this.pbAudioMeter.Enabled = false;
            this.pbAudioMeter.Location = new System.Drawing.Point(310, 59);
            this.pbAudioMeter.Maximum = 100;
            this.pbAudioMeter.Minimum = 0;
            this.pbAudioMeter.Name = "pbAudioMeter";
            this.pbAudioMeter.Size = new System.Drawing.Size(26, 15);
            this.pbAudioMeter.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAudioMeter.Value = 0;

            // btnTestAudio
            this.btnTestAudio.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnTestAudio.Enabled = false;
            this.btnTestAudio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnTestAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestAudio.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnTestAudio.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.btnTestAudio.Location = new System.Drawing.Point(340, 54);
            this.btnTestAudio.Name = "btnTestAudio";
            this.btnTestAudio.Size = new System.Drawing.Size(30, 25);
            this.btnTestAudio.Text = "🔊";
            this.btnTestAudio.UseVisualStyleBackColor = false;

            // btnAudioDiag
            this.btnAudioDiag.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnAudioDiag.Enabled = false;
            this.btnAudioDiag.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnAudioDiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAudioDiag.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnAudioDiag.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.btnAudioDiag.Location = new System.Drawing.Point(374, 54);
            this.btnAudioDiag.Name = "btnAudioDiag";
            this.btnAudioDiag.Size = new System.Drawing.Size(30, 25);
            this.btnAudioDiag.Text = "⚙";
            this.btnAudioDiag.UseVisualStyleBackColor = false;

            // label8 (Framerate:)
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(12, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 15);
            this.label8.Text = "Framerate:";

            // comboFps
            this.comboFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFps.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboFps.FormattingEnabled = true;
            this.comboFps.Items.AddRange(new object[] {
                "60 FPS (Ultra)",
                "30 FPS (Smooth)",
                "15 FPS (Balanced)",
                "5 FPS (Saver)"
            });
            this.comboFps.SelectedIndex = 1;
            this.comboFps.Location = new System.Drawing.Point(82, 92);
            this.comboFps.Name = "comboFps";
            this.comboFps.Size = new System.Drawing.Size(115, 21);

            // lblQuality
            this.lblQuality.AutoSize = true;
            this.lblQuality.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblQuality.ForeColor = System.Drawing.Color.White;
            this.lblQuality.Location = new System.Drawing.Point(212, 95);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(77, 15);
            this.lblQuality.Text = "Quality: 80%";

            // qualitySlider
            this.qualitySlider.Location = new System.Drawing.Point(292, 90);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 20;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(115, 30);
            this.qualitySlider.TickFrequency = 20;
            this.qualitySlider.Value = 80;

            // 
            // gbOptions (Network & Security)
            // 
            this.gbOptions.Controls.Add(this.label5);
            this.gbOptions.Controls.Add(this.comboIPs);
            this.gbOptions.Controls.Add(this.btnRefreshIPs);
            this.gbOptions.Controls.Add(this.label2);
            this.gbOptions.Controls.Add(this.numPort);
            this.gbOptions.Controls.Add(this.cbPrivate);
            this.gbOptions.Controls.Add(this.txtPassword);
            this.gbOptions.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.gbOptions.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.gbOptions.Location = new System.Drawing.Point(16, 212);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(418, 100);
            this.gbOptions.Text = "Network && Security";

            // label5 (Network:)
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.Text = "Network:";

            // comboIPs
            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPs.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.comboIPs.FormattingEnabled = true;
            this.comboIPs.Location = new System.Drawing.Point(75, 22);
            this.comboIPs.Name = "comboIPs";
            this.comboIPs.Size = new System.Drawing.Size(195, 21);

            // btnRefreshIPs
            this.btnRefreshIPs.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnRefreshIPs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshIPs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.btnRefreshIPs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshIPs.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshIPs.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.btnRefreshIPs.Location = new System.Drawing.Point(274, 20);
            this.btnRefreshIPs.Name = "btnRefreshIPs";
            this.btnRefreshIPs.Size = new System.Drawing.Size(28, 25);
            this.btnRefreshIPs.Text = "🔄";
            this.btnRefreshIPs.UseVisualStyleBackColor = false;

            // label2 (Port:)
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(310, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.Text = "Port:";

            // numPort
            this.numPort.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.numPort.Location = new System.Drawing.Point(346, 22);
            this.numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(58, 23);
            this.numPort.Value = new decimal(new int[] { 7070, 0, 0, 0 });

            // cbPrivate
            this.cbPrivate.AutoSize = true;
            this.cbPrivate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbPrivate.ForeColor = System.Drawing.Color.White;
            this.cbPrivate.Location = new System.Drawing.Point(15, 62);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(125, 19);
            this.cbPrivate.Text = "Require PIN / Pass:";
            this.cbPrivate.UseVisualStyleBackColor = false;

            // txtPassword
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtPassword.Location = new System.Drawing.Point(145, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(259, 23);

            // 
            // groupBox3 (Broadcast Control)
            // 
            this.groupBox3.Controls.Add(this.btnStartServer);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtURL);
            this.groupBox3.Controls.Add(this.btnCopyUrl);
            this.groupBox3.Controls.Add(this.btnLaunch);
            this.groupBox3.Controls.Add(this.btnQr);
            this.groupBox3.Controls.Add(this.lblLiveStats);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.groupBox3.Location = new System.Drawing.Point(16, 320);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 155);
            this.groupBox3.Text = "Broadcast Control";

            // btnStartServer
            this.btnStartServer.BackColor = System.Drawing.Color.FromArgb(99, 102, 241);
            this.btnStartServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartServer.FlatAppearance.BorderSize = 0;
            this.btnStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartServer.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.Location = new System.Drawing.Point(15, 22);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(389, 42);
            this.btnStartServer.Text = "▶  START BROADCAST";
            this.btnStartServer.UseVisualStyleBackColor = false;

            // label1 (URL:)
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.Text = "URL:";

            // txtURL
            this.txtURL.BackColor = System.Drawing.Color.FromArgb(2, 6, 23);
            this.txtURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtURL.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtURL.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.txtURL.Location = new System.Drawing.Point(48, 75);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(185, 22);
            this.txtURL.Text = "http://127.0.0.1:7070";

            // btnCopyUrl
            this.btnCopyUrl.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCopyUrl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyUrl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnCopyUrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyUrl.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCopyUrl.ForeColor = System.Drawing.Color.White;
            this.btnCopyUrl.Location = new System.Drawing.Point(239, 73);
            this.btnCopyUrl.Name = "btnCopyUrl";
            this.btnCopyUrl.Size = new System.Drawing.Size(50, 26);
            this.btnCopyUrl.Text = "Copy";
            this.btnCopyUrl.UseVisualStyleBackColor = false;

            // btnLaunch
            this.btnLaunch.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnLaunch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLaunch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaunch.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnLaunch.ForeColor = System.Drawing.Color.White;
            this.btnLaunch.Location = new System.Drawing.Point(294, 73);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(52, 26);
            this.btnLaunch.Text = "Open";
            this.btnLaunch.UseVisualStyleBackColor = false;

            // btnQr
            this.btnQr.BackColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnQr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQr.FlatAppearance.BorderSize = 0;
            this.btnQr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQr.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnQr.ForeColor = System.Drawing.Color.White;
            this.btnQr.Location = new System.Drawing.Point(351, 73);
            this.btnQr.Name = "btnQr";
            this.btnQr.Size = new System.Drawing.Size(53, 26);
            this.btnQr.Text = "📱 QR";
            this.btnQr.UseVisualStyleBackColor = false;

            // lblLiveStats
            this.lblLiveStats.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLiveStats.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblLiveStats.Location = new System.Drawing.Point(15, 118);
            this.lblLiveStats.Name = "lblLiveStats";
            this.lblLiveStats.Size = new System.Drawing.Size(389, 22);
            this.lblLiveStats.Text = "⚡ Stopped • Ready to broadcast";
            this.lblLiveStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // groupBox2 (Preferences)
            // 
            this.groupBox2.Controls.Add(this.cbAutoStart);
            this.groupBox2.Controls.Add(this.cbMiniStart);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(129, 140, 248);
            this.groupBox2.Location = new System.Drawing.Point(16, 483);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 55);
            this.groupBox2.Text = "Preferences";

            // cbAutoStart
            this.cbAutoStart.AutoSize = true;
            this.cbAutoStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbAutoStart.ForeColor = System.Drawing.Color.White;
            this.cbAutoStart.Location = new System.Drawing.Point(25, 22);
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.Size = new System.Drawing.Size(125, 19);
            this.cbAutoStart.Text = "Auto-Start Server";
            this.cbAutoStart.UseVisualStyleBackColor = false;

            // cbMiniStart
            this.cbMiniStart.AutoSize = true;
            this.cbMiniStart.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cbMiniStart.ForeColor = System.Drawing.Color.White;
            this.cbMiniStart.Location = new System.Drawing.Point(210, 22);
            this.cbMiniStart.Name = "cbMiniStart";
            this.cbMiniStart.Size = new System.Drawing.Size(150, 19);
            this.cbMiniStart.Text = "Start Minimized to Tray";
            this.cbMiniStart.UseVisualStyleBackColor = false;

            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(2, 6, 23);
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.txtLog.Location = new System.Drawing.Point(16, 548);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(418, 80);

            // 
            // lblMe
            // 
            this.lblMe.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMe.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblMe.Location = new System.Drawing.Point(16, 638);
            this.lblMe.Name = "lblMe";
            this.lblMe.Size = new System.Drawing.Size(418, 20);
            this.lblMe.Text = "© Eslam Hamouda 2014 - 2026 • screentask.eslamx.com • GitHub";
            this.lblMe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // appNotify
            // 
            this.appNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.appNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("appNotify.Icon")));
            this.appNotify.Text = "ScreenTask 2.0";
            this.appNotify.Visible = true;

            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.ClientSize = new System.Drawing.Size(450, 670);
            this.Controls.Add(this.lblMe);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblStatusBadge);
            this.Controls.Add(this.lblAppSubtitle);
            this.Controls.Add(this.lblAppTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScreenTask 2.0 – Offline & LAN Screen Sharing";

            // toolTipMain
            this.toolTipMain.SetToolTip(this.btnRefreshScreens, "Refresh active screens and open application windows");
            this.toolTipMain.SetToolTip(this.btnRefreshIPs, "Refresh available network adapters");
            this.toolTipMain.SetToolTip(this.btnRefreshAudio, "Refresh connected audio input and output devices");
            this.toolTipMain.SetToolTip(this.btnTestAudio, "Play speaker test chime");
            this.toolTipMain.SetToolTip(this.btnAudioDiag, "Open Audio Diagnostics & Microphone Tester");
            this.toolTipMain.SetToolTip(this.btnCopyUrl, "Copy stream URL to clipboard");
            this.toolTipMain.SetToolTip(this.btnLaunch, "Open stream in default web browser");
            this.toolTipMain.SetToolTip(this.btnQr, "Show QR Code for mobile phones");

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboScreens;
        private System.Windows.Forms.Button btnRefreshScreens;
        private System.Windows.Forms.CheckBox cbCaptureMouse;
        private System.Windows.Forms.CheckBox cbAudio;
        private System.Windows.Forms.ComboBox comboAudioDevices;
        private System.Windows.Forms.Button btnRefreshAudio;
        private System.Windows.Forms.ProgressBar pbAudioMeter;
        private System.Windows.Forms.Button btnTestAudio;
        private System.Windows.Forms.Button btnAudioDiag;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboFps;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.TrackBar qualitySlider;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboIPs;
        private System.Windows.Forms.Button btnRefreshIPs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.CheckBox cbPrivate;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnCopyUrl;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnQr;
        private System.Windows.Forms.Label lblLiveStats;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbAutoStart;
        private System.Windows.Forms.CheckBox cbMiniStart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.NotifyIcon appNotify;
        private System.Windows.Forms.ToolTip toolTipMain;
    }
}
