namespace ScreenTask.UI.Controls
{
    partial class MonitorPreviewControl
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
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.lblSourceTitle = new System.Windows.Forms.Label();
            this.lblLiveBadge = new System.Windows.Forms.Label();
            this.lblAudioInfo = new System.Windows.Forms.Label();
            this.lblFps = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.Color.Black;
            this.picPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPreview.Location = new System.Drawing.Point(0, 0);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(360, 175);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            // 
            // lblSourceTitle
            // 
            this.lblSourceTitle.AutoSize = true;
            this.lblSourceTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblSourceTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSourceTitle.ForeColor = System.Drawing.Color.White;
            this.lblSourceTitle.Location = new System.Drawing.Point(6, 6);
            this.lblSourceTitle.Name = "lblSourceTitle";
            this.lblSourceTitle.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.lblSourceTitle.Size = new System.Drawing.Size(185, 17);
            this.lblSourceTitle.TabIndex = 1;
            this.lblSourceTitle.Text = "Display 1 (Primary) • 1920 × 1080";
            // 
            // lblLiveBadge
            // 
            this.lblLiveBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLiveBadge.AutoSize = true;
            this.lblLiveBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblLiveBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblLiveBadge.ForeColor = System.Drawing.Color.Silver;
            this.lblLiveBadge.Location = new System.Drawing.Point(298, 6);
            this.lblLiveBadge.Name = "lblLiveBadge";
            this.lblLiveBadge.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.lblLiveBadge.Size = new System.Drawing.Size(56, 17);
            this.lblLiveBadge.TabIndex = 2;
            this.lblLiveBadge.Text = "● IDLE";
            // 
            // lblAudioInfo
            // 
            this.lblAudioInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAudioInfo.AutoSize = true;
            this.lblAudioInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblAudioInfo.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblAudioInfo.ForeColor = System.Drawing.Color.Silver;
            this.lblAudioInfo.Location = new System.Drawing.Point(6, 153);
            this.lblAudioInfo.Name = "lblAudioInfo";
            this.lblAudioInfo.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblAudioInfo.Size = new System.Drawing.Size(78, 14);
            this.lblAudioInfo.TabIndex = 3;
            this.lblAudioInfo.Text = "Audio: Active";
            // 
            // lblFps
            // 
            this.lblFps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFps.AutoSize = true;
            this.lblFps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblFps.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblFps.ForeColor = System.Drawing.Color.White;
            this.lblFps.Location = new System.Drawing.Point(286, 153);
            this.lblFps.Name = "lblFps";
            this.lblFps.Padding = new System.Windows.Forms.Padding(4, 1, 4, 1);
            this.lblFps.Size = new System.Drawing.Size(68, 15);
            this.lblFps.TabIndex = 4;
            this.lblFps.Text = "⚡ 30.0 FPS";
            // 
            // MonitorPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblSourceTitle);
            this.Controls.Add(this.lblLiveBadge);
            this.Controls.Add(this.lblAudioInfo);
            this.Controls.Add(this.lblFps);
            this.Controls.Add(this.picPreview);
            this.Name = "MonitorPreviewControl";
            this.Size = new System.Drawing.Size(360, 175);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.PictureBox picPreview;
        internal System.Windows.Forms.Label lblSourceTitle;
        internal System.Windows.Forms.Label lblLiveBadge;
        internal System.Windows.Forms.Label lblAudioInfo;
        internal System.Windows.Forms.Label lblFps;
    }
}
