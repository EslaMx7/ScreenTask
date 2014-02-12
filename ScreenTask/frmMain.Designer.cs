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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.imgPreview = new System.Windows.Forms.PictureBox();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.comboIPs = new System.Windows.Forms.ComboBox();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.cbPreview = new System.Windows.Forms.CheckBox();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbScreenshotEvery = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numShotEvery = new System.Windows.Forms.NumericUpDown();
            this.lblMe = new System.Windows.Forms.Label();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.lblGithub = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShotEvery)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Location = new System.Drawing.Point(723, 14);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(114, 112);
            this.gbOptions.TabIndex = 0;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Server Options";
            // 
            // gbLog
            // 
            this.gbLog.Location = new System.Drawing.Point(723, 337);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(409, 120);
            this.gbLog.TabIndex = 1;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // gbPreview
            // 
            this.gbPreview.Location = new System.Drawing.Point(723, 142);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new System.Drawing.Size(409, 189);
            this.gbPreview.TabIndex = 2;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "Preview";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 236);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(379, 139);
            this.txtLog.TabIndex = 3;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // imgPreview
            // 
            this.imgPreview.Image = global::ScreenTask.Properties.Resources.imgPrev;
            this.imgPreview.InitialImage = global::ScreenTask.Properties.Resources.imgPrev;
            this.imgPreview.Location = new System.Drawing.Point(398, 235);
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.Size = new System.Drawing.Size(210, 140);
            this.imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgPreview.TabIndex = 4;
            this.imgPreview.TabStop = false;
            this.imgPreview.Click += new System.EventHandler(this.imgPreview_Click);
            // 
            // pnlOptions
            // 
            this.pnlOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlOptions.Location = new System.Drawing.Point(878, 28);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(92, 56);
            this.pnlOptions.TabIndex = 5;
            // 
            // comboIPs
            // 
            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPs.FormattingEnabled = true;
            this.comboIPs.Location = new System.Drawing.Point(47, 87);
            this.comboIPs.Name = "comboIPs";
            this.comboIPs.Size = new System.Drawing.Size(215, 21);
            this.comboIPs.TabIndex = 27;
            // 
            // cbCaptureMouse
            // 
            this.cbCaptureMouse.AutoSize = true;
            this.cbCaptureMouse.BackColor = System.Drawing.Color.Transparent;
            this.cbCaptureMouse.Location = new System.Drawing.Point(400, 155);
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.Size = new System.Drawing.Size(136, 17);
            this.cbCaptureMouse.TabIndex = 26;
            this.cbCaptureMouse.Text = "Capture Mouse Pointer";
            this.cbCaptureMouse.UseVisualStyleBackColor = false;
            this.cbCaptureMouse.CheckedChanged += new System.EventHandler(this.cbCaptureMouse_CheckedChanged);
            // 
            // cbPreview
            // 
            this.cbPreview.AutoSize = true;
            this.cbPreview.BackColor = System.Drawing.Color.Transparent;
            this.cbPreview.Location = new System.Drawing.Point(400, 216);
            this.cbPreview.Name = "cbPreview";
            this.cbPreview.Size = new System.Drawing.Size(64, 17);
            this.cbPreview.TabIndex = 25;
            this.cbPreview.Text = "Preview";
            this.cbPreview.UseVisualStyleBackColor = false;
            this.cbPreview.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            // 
            // btnStopServer
            // 
            this.btnStopServer.BackColor = System.Drawing.Color.Maroon;
            this.btnStopServer.Enabled = false;
            this.btnStopServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopServer.ForeColor = System.Drawing.Color.White;
            this.btnStopServer.Location = new System.Drawing.Point(152, 196);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(130, 23);
            this.btnStopServer.TabIndex = 24;
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.UseVisualStyleBackColor = false;
            this.btnStopServer.Visible = false;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // btnStartServer
            // 
            this.btnStartServer.BackColor = System.Drawing.Color.Gray;
            this.btnStartServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartServer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.Location = new System.Drawing.Point(114, 192);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(202, 30);
            this.btnStartServer.TabIndex = 23;
            this.btnStartServer.Tag = "start";
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(319, 156);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(66, 20);
            this.txtPassword.TabIndex = 22;
            this.txtPassword.Text = "task";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(183, 156);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(66, 20);
            this.txtUser.TabIndex = 21;
            this.txtUser.Text = "screen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(255, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Password : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(147, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "User : ";
            // 
            // cbPrivate
            // 
            this.cbPrivate.AutoSize = true;
            this.cbPrivate.BackColor = System.Drawing.Color.Transparent;
            this.cbPrivate.Location = new System.Drawing.Point(47, 158);
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.Size = new System.Drawing.Size(88, 17);
            this.cbPrivate.TabIndex = 18;
            this.cbPrivate.Text = "Private Task ";
            this.cbPrivate.UseVisualStyleBackColor = false;
            this.cbPrivate.CheckedChanged += new System.EventHandler(this.cbPrivate_CheckedChanged);
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(307, 88);
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
            this.numPort.Size = new System.Drawing.Size(78, 20);
            this.numPort.TabIndex = 1;
            this.numPort.Value = new decimal(new int[] {
            7070,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(268, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Port :";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(47, 117);
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            this.txtURL.Size = new System.Drawing.Size(338, 20);
            this.txtURL.TabIndex = 17;
            this.txtURL.Text = "the URL will displayed here after starting the server...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "URL :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(11, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "IP :";
            // 
            // cbScreenshotEvery
            // 
            this.cbScreenshotEvery.AutoSize = true;
            this.cbScreenshotEvery.BackColor = System.Drawing.Color.Transparent;
            this.cbScreenshotEvery.Checked = true;
            this.cbScreenshotEvery.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScreenshotEvery.Location = new System.Drawing.Point(400, 86);
            this.cbScreenshotEvery.Name = "cbScreenshotEvery";
            this.cbScreenshotEvery.Size = new System.Drawing.Size(144, 17);
            this.cbScreenshotEvery.TabIndex = 29;
            this.cbScreenshotEvery.Text = "Take Screenshot Every :";
            this.cbScreenshotEvery.UseVisualStyleBackColor = false;
            this.cbScreenshotEvery.CheckedChanged += new System.EventHandler(this.cbScreenshotEvery_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(501, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Mellisecond";
            // 
            // numShotEvery
            // 
            this.numShotEvery.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numShotEvery.Location = new System.Drawing.Point(421, 116);
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
            this.numShotEvery.Size = new System.Drawing.Size(74, 20);
            this.numShotEvery.TabIndex = 30;
            this.numShotEvery.ThousandsSeparator = true;
            this.numShotEvery.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lblMe
            // 
            this.lblMe.AutoSize = true;
            this.lblMe.BackColor = System.Drawing.Color.Transparent;
            this.lblMe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMe.Location = new System.Drawing.Point(468, 391);
            this.lblMe.Name = "lblMe";
            this.lblMe.Size = new System.Drawing.Size(138, 13);
            this.lblMe.TabIndex = 32;
            this.lblMe.Text = "Coded by : Eslam Hamouda";
            this.lblMe.Click += new System.EventHandler(this.lblMe_Click);
            // 
            // lblWebsite
            // 
            this.lblWebsite.BackColor = System.Drawing.Color.Transparent;
            this.lblWebsite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWebsite.Location = new System.Drawing.Point(33, 384);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(180, 24);
            this.lblWebsite.TabIndex = 33;
            this.lblWebsite.Text = "2014 | eslamx.com";
            this.lblWebsite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWebsite.Click += new System.EventHandler(this.lblWebsite_Click);
            // 
            // lblGithub
            // 
            this.lblGithub.BackColor = System.Drawing.Color.Transparent;
            this.lblGithub.Location = new System.Drawing.Point(538, 9);
            this.lblGithub.Name = "lblGithub";
            this.lblGithub.Size = new System.Drawing.Size(79, 56);
            this.lblGithub.TabIndex = 34;
            this.lblGithub.Click += new System.EventHandler(this.lblGithub_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ScreenTask.Properties.Resources.ScreenTaskBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(617, 410);
            this.Controls.Add(this.imgPreview);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.gbPreview);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPrivate);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.numPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboIPs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numShotEvery);
            this.Controls.Add(this.cbScreenshotEvery);
            this.Controls.Add(this.cbCaptureMouse);
            this.Controls.Add(this.cbPreview);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.lblGithub);
            this.Controls.Add(this.lblWebsite);
            this.Controls.Add(this.lblMe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screen Task";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShotEvery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.PictureBox imgPreview;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.ComboBox comboIPs;
        private System.Windows.Forms.CheckBox cbCaptureMouse;
        private System.Windows.Forms.CheckBox cbPreview;
        private System.Windows.Forms.Button btnStopServer;
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
        private System.Windows.Forms.CheckBox cbScreenshotEvery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numShotEvery;
        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.Label lblGithub;
    }
}

