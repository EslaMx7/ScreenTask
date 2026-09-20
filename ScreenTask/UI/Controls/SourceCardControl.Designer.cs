namespace ScreenTask.UI.Controls
{
    partial class SourceCardControl
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
            this.pnlBorder = new System.Windows.Forms.Panel();
            this.pnlInner = new System.Windows.Forms.Panel();
            this.picThumbnail = new System.Windows.Forms.PictureBox();
            this.lblSelectedBadge = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.pnlBorder.SuspendLayout();
            this.pnlInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBorder
            // 
            this.pnlBorder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlBorder.Controls.Add(this.pnlInner);
            this.pnlBorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBorder.Location = new System.Drawing.Point(0, 0);
            this.pnlBorder.Name = "pnlBorder";
            this.pnlBorder.Padding = new System.Windows.Forms.Padding(1);
            this.pnlBorder.Size = new System.Drawing.Size(188, 142);
            this.pnlBorder.TabIndex = 0;
            // 
            // pnlInner
            // 
            this.pnlInner.BackColor = System.Drawing.SystemColors.Window;
            this.pnlInner.Controls.Add(this.lblSelectedBadge);
            this.pnlInner.Controls.Add(this.picThumbnail);
            this.pnlInner.Controls.Add(this.picIcon);
            this.pnlInner.Controls.Add(this.lblTitle);
            this.pnlInner.Controls.Add(this.lblSub);
            this.pnlInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInner.Location = new System.Drawing.Point(1, 1);
            this.pnlInner.Name = "pnlInner";
            this.pnlInner.Size = new System.Drawing.Size(186, 140);
            this.pnlInner.TabIndex = 0;
            // 
            // picThumbnail
            // 
            this.picThumbnail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this.picThumbnail.Location = new System.Drawing.Point(4, 4);
            this.picThumbnail.Name = "picThumbnail";
            this.picThumbnail.Size = new System.Drawing.Size(178, 92);
            this.picThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picThumbnail.TabIndex = 0;
            this.picThumbnail.TabStop = false;
            // 
            // lblSelectedBadge
            // 
            this.lblSelectedBadge.AutoSize = true;
            this.lblSelectedBadge.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblSelectedBadge.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSelectedBadge.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblSelectedBadge.Location = new System.Drawing.Point(8, 8);
            this.lblSelectedBadge.Name = "lblSelectedBadge";
            this.lblSelectedBadge.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.lblSelectedBadge.Size = new System.Drawing.Size(65, 15);
            this.lblSelectedBadge.TabIndex = 1;
            this.lblSelectedBadge.Text = "✓ Selected";
            this.lblSelectedBadge.Visible = false;
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(6, 100);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(20, 20);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 2;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTitle.Location = new System.Drawing.Point(30, 98);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 18);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Source Title";
            // 
            // lblSub
            // 
            this.lblSub.AutoEllipsis = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblSub.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblSub.Location = new System.Drawing.Point(6, 118);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(174, 16);
            this.lblSub.TabIndex = 4;
            this.lblSub.Text = "Process Name • Resolution";
            // 
            // SourceCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBorder);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SourceCardControl";
            this.Size = new System.Drawing.Size(188, 142);
            this.pnlBorder.ResumeLayout(false);
            this.pnlInner.ResumeLayout(false);
            this.pnlInner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        public System.Windows.Forms.Panel pnlBorder;
        public System.Windows.Forms.Panel pnlInner;
        public System.Windows.Forms.PictureBox picThumbnail;
        public System.Windows.Forms.Label lblSelectedBadge;
        public System.Windows.Forms.PictureBox picIcon;
        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblSub;
    }
}
