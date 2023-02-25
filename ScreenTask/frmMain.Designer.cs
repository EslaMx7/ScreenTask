namespace ScreenTask
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbAllowPublicAccess = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboIPs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numShotEvery = new System.Windows.Forms.NumericUpDown();
            this.lblMe = new System.Windows.Forms.Label();
            this.comboScreens = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.appNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.cbAutoStart = new System.Windows.Forms.CheckBox();
            this.cbMiniStart = new System.Windows.Forms.CheckBox();
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.lblQuality = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboOutputType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();



            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firewallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();

            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();


            this.gbOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShotEvery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbAllowPublicAccess);
            this.gbOptions.Controls.Add(this.label5);
            this.gbOptions.Controls.Add(this.comboIPs);
            this.gbOptions.Controls.Add(this.label2);
            this.gbOptions.Controls.Add(this.numPort);
            resources.ApplyResources(this.gbOptions, "gbOptions");
            this.gbOptions.ForeColor = System.Drawing.Color.White;

            this.gbOptions.Location = new System.Drawing.Point(12, 90);


            this.gbOptions.Location = new System.Drawing.Point(12, 71);



            this.gbOptions.Name = "gbOptions";
            this.gbOptions.TabStop = false;
            // 
            // cbAllowPublicAccess
            // 
            resources.ApplyResources(this.cbAllowPublicAccess, "cbAllowPublicAccess");
            this.cbAllowPublicAccess.BackColor = System.Drawing.Color.Transparent;
            this.cbAllowPublicAccess.ForeColor = System.Drawing.Color.White;
            this.cbAllowPublicAccess.Name = "cbAllowPublicAccess";
            this.cbAllowPublicAccess.UseVisualStyleBackColor = false;
            this.cbAllowPublicAccess.CheckedChanged += new System.EventHandler(this.cbAllowPublicAccess_CheckedChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Name = "label5";
            // 
            // comboIPs
            // 
            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboIPs, "comboIPs");
            this.comboIPs.FormattingEnabled = true;
            this.comboIPs.Name = "comboIPs";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Name = "label2";
            // 
            // numPort
            // 
            resources.ApplyResources(this.numPort, "numPort");
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Value = new decimal(new int[] {
            7070,
            0,
            0,
            0});
            // 
            // txtLog
            // 

            this.txtLog.Location = new System.Drawing.Point(11, 464);


            this.txtLog.Location = new System.Drawing.Point(11, 445);

            this.txtLog.Multiline = true;

            resources.ApplyResources(this.txtLog, "txtLog");

            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // cbCaptureMouse
            // 
            resources.ApplyResources(this.cbCaptureMouse, "cbCaptureMouse");
            this.cbCaptureMouse.BackColor = System.Drawing.Color.Transparent;
            this.cbCaptureMouse.ForeColor = System.Drawing.Color.White;

            this.cbCaptureMouse.Location = new System.Drawing.Point(217, 85);


            this.cbCaptureMouse.Location = new System.Drawing.Point(196, 65);



            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.UseVisualStyleBackColor = false;
            this.cbCaptureMouse.CheckedChanged += new System.EventHandler(this.cbCaptureMouse_CheckedChanged);
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.btnStartServer, "btnStartServer");
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Tag = "start";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.Name = "txtUser";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Name = "label3";
            // 
            // cbPrivate
            // 
            resources.ApplyResources(this.cbPrivate, "cbPrivate");
            this.cbPrivate.BackColor = System.Drawing.Color.Transparent;
            this.cbPrivate.ForeColor = System.Drawing.Color.White;
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.UseVisualStyleBackColor = false;
            this.cbPrivate.CheckedChanged += new System.EventHandler(this.cbPrivate_CheckedChanged);
            // 
            // txtURL
            // 
            resources.ApplyResources(this.txtURL, "txtURL");
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Name = "label1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Name = "label6";
            // 
            // numShotEvery
            // 
            resources.ApplyResources(this.numShotEvery, "numShotEvery");
            this.numShotEvery.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numShotEvery.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numShotEvery.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numShotEvery.Name = "numShotEvery";
            this.numShotEvery.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lblMe
            // 
            this.lblMe.BackColor = System.Drawing.Color.Transparent;
            this.lblMe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));

            this.lblMe.Location = new System.Drawing.Point(11, 520);


            this.lblMe.Location = new System.Drawing.Point(11, 501);

            this.lblMe.Name = "lblMe";
            this.lblMe.Size = new System.Drawing.Size(350, 23);
            this.lblMe.TabIndex = 32;
            this.lblMe.Text = "© Atumy 2014 - 2022 - screentask.me";
            this.lblMe.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            resources.ApplyResources(this.lblMe, "lblMe");
            this.lblMe.Name = "lblMe";

            this.lblMe.Click += new System.EventHandler(this.lblMe_Click);
            // 
            // comboScreens
            // 
            this.comboScreens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboScreens, "comboScreens");
            this.comboScreens.FormattingEnabled = true;
            this.comboScreens.Name = "comboScreens";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Name = "label7";
            // 
            // btnLaunch
            // 
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.ForeColor = System.Drawing.Color.White;
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Name = "label8";
            // 
            // appNotify
            // 
            this.appNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.appNotify, "appNotify");
            this.appNotify.Click += new System.EventHandler(this.appNotify_Click);
            // 
            // cbAutoStart
            // 
            resources.ApplyResources(this.cbAutoStart, "cbAutoStart");
            this.cbAutoStart.BackColor = System.Drawing.Color.Transparent;
            this.cbAutoStart.ForeColor = System.Drawing.Color.White;
            this.cbAutoStart.Name = "cbAutoStart";
            this.cbAutoStart.UseVisualStyleBackColor = false;
            // 
            // cbMiniStart
            // 
            resources.ApplyResources(this.cbMiniStart, "cbMiniStart");
            this.cbMiniStart.BackColor = System.Drawing.Color.Transparent;
            this.cbMiniStart.ForeColor = System.Drawing.Color.White;
            this.cbMiniStart.Name = "cbMiniStart";
            this.cbMiniStart.UseVisualStyleBackColor = false;
            // 
            // qualitySlider
            // 

            this.qualitySlider.Location = new System.Drawing.Point(231, 50);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 1;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(110, 45);


            this.qualitySlider.Location = new System.Drawing.Point(55, 58);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 1;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(130, 45);

            resources.ApplyResources(this.qualitySlider, "qualitySlider");
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 1;
            this.qualitySlider.Name = "qualitySlider";


            this.qualitySlider.SmallChange = 10;
            this.qualitySlider.TickFrequency = 25;
            this.qualitySlider.Value = 75;
            this.qualitySlider.Scroll += new System.EventHandler(this.qualitySlider_Scroll);
            // 
            // lblQuality
            // 
            resources.ApplyResources(this.lblQuality, "lblQuality");
            this.lblQuality.BackColor = System.Drawing.Color.Transparent;
            this.lblQuality.ForeColor = System.Drawing.Color.White;

            this.lblQuality.Location = new System.Drawing.Point(192, 55);


            this.lblQuality.Location = new System.Drawing.Point(6, 69);



            this.lblQuality.Name = "lblQuality";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboOutputType);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numShotEvery);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblQuality);
            this.groupBox1.Controls.Add(this.comboScreens);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbCaptureMouse);
            this.groupBox1.Controls.Add(this.qualitySlider);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.ForeColor = System.Drawing.Color.White;

            this.groupBox1.Location = new System.Drawing.Point(12, 164);


            this.groupBox1.Location = new System.Drawing.Point(12, 145);



            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 

            // comboOutputType
            // 
            this.comboOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOutputType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboOutputType.FormattingEnabled = true;
            this.comboOutputType.Items.AddRange(new object[] {
            "WebP",
            "JPG"});
            this.comboOutputType.Location = new System.Drawing.Point(56, 55);
            this.comboOutputType.Name = "comboOutputType";
            this.comboOutputType.Size = new System.Drawing.Size(130, 21);
            this.comboOutputType.TabIndex = 45;


            // comboOutputType
            // 
            this.comboOutputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboOutputType, "comboOutputType");
            this.comboOutputType.FormattingEnabled = true;
            this.comboOutputType.Items.AddRange(new object[] {
            resources.GetString("comboOutputType.Items"),
            resources.GetString("comboOutputType.Items1")});
            this.comboOutputType.Name = "comboOutputType";

            this.comboOutputType.SelectedIndexChanged += new System.EventHandler(this.comboOutputType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;

            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(8, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 31);
            this.label9.TabIndex = 44;
            this.label9.Text = "Output Type :";
            // 

            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Name = "label9";
            // 


            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPrivate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.ForeColor = System.Drawing.Color.White;

            this.groupBox2.Location = new System.Drawing.Point(12, 278);


            this.groupBox2.Location = new System.Drawing.Point(12, 259);



            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtURL);
            this.groupBox3.Controls.Add(this.btnLaunch);
            this.groupBox3.Controls.Add(this.cbMiniStart);
            this.groupBox3.Controls.Add(this.btnStartServer);
            this.groupBox3.Controls.Add(this.cbAutoStart);

            this.groupBox3.Location = new System.Drawing.Point(12, 357);


            this.groupBox3.Location = new System.Drawing.Point(12, 338);

            resources.ApplyResources(this.groupBox3, "groupBox3");


            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 



            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.firewallToolStripMenuItem,
            this.aboutToolStripMenuItem});

            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(373, 24);
            this.menuStrip1.TabIndex = 38;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";

            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimizeToTrayToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");

            // 
            // firewallToolStripMenuItem
            // 
            this.firewallToolStripMenuItem.Name = "firewallToolStripMenuItem";

            this.firewallToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.firewallToolStripMenuItem.Text = "Firewall";

            resources.ApplyResources(this.firewallToolStripMenuItem, "firewallToolStripMenuItem");

            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";

            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";

            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");

            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 

            this.pictureBox1.Image = global::ScreenTask.Properties.Resources.screentask_logo;
            this.pictureBox1.Location = new System.Drawing.Point(11, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 

            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Checked = true;
            this.minimizeToTrayToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            resources.ApplyResources(this.minimizeToTrayToolStripMenuItem, "minimizeToTrayToolStripMenuItem");
            this.minimizeToTrayToolStripMenuItem.Click += new System.EventHandler(this.minimizeToTrayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            // 


            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;

            this.ClientSize = new System.Drawing.Size(373, 546);

            this.ClientSize = new System.Drawing.Size(373, 523);



            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.lblMe);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));

            this.MainMenuStrip = this.menuStrip1;


            this.MainMenuStrip = this.menuStrip1;


            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShotEvery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ComboBox comboIPs;
        private System.Windows.Forms.CheckBox cbCaptureMouse;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbPrivate;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numShotEvery;
        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.ComboBox comboScreens;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NotifyIcon appNotify;
        private System.Windows.Forms.CheckBox cbAutoStart;
        private System.Windows.Forms.CheckBox cbMiniStart;
        private System.Windows.Forms.TrackBar qualitySlider;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbAllowPublicAccess;
        private System.Windows.Forms.GroupBox groupBox3;



        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboOutputType;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firewallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;


    }
}

