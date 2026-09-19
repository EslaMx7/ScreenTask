namespace ScreenTask.UI
{
    partial class MiniBarForm
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
            this.lblCompactDot = new System.Windows.Forms.Label();
            this.lblCompactStatus = new System.Windows.Forms.Label();
            this.lblCompactUrl = new System.Windows.Forms.Label();
            this.btnCompactCopy = new ScreenTask.UI.Controls.FluentButton();
            this.btnCompactStop = new ScreenTask.UI.Controls.FluentButton();
            this.btnCompactExpand = new ScreenTask.UI.Controls.FluentButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblCompactDot
            // 
            this.lblCompactDot.AutoSize = true;
            this.lblCompactDot.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCompactDot.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCompactDot.Location = new System.Drawing.Point(12, 18);
            this.lblCompactDot.Name = "lblCompactDot";
            this.lblCompactDot.Size = new System.Drawing.Size(26, 25);
            this.lblCompactDot.TabIndex = 0;
            this.lblCompactDot.Text = "●";
            // 
            // lblCompactStatus
            // 
            this.lblCompactStatus.AutoSize = true;
            this.lblCompactStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompactStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCompactStatus.Location = new System.Drawing.Point(38, 14);
            this.lblCompactStatus.Name = "lblCompactStatus";
            this.lblCompactStatus.Size = new System.Drawing.Size(126, 15);
            this.lblCompactStatus.TabIndex = 1;
            this.lblCompactStatus.Text = "● Ready to Stream";
            // 
            // lblCompactUrl
            // 
            this.lblCompactUrl.AutoSize = true;
            this.lblCompactUrl.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblCompactUrl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCompactUrl.Location = new System.Drawing.Point(38, 36);
            this.lblCompactUrl.Name = "lblCompactUrl";
            this.lblCompactUrl.Size = new System.Drawing.Size(145, 13);
            this.lblCompactUrl.TabIndex = 2;
            this.lblCompactUrl.Text = "http://127.0.0.1:7070";
            // 
            // btnCompactCopy
            // 
            this.btnCompactCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompactCopy.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnCompactCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompactCopy.Location = new System.Drawing.Point(580, 18);
            this.btnCompactCopy.Name = "btnCompactCopy";
            this.btnCompactCopy.Size = new System.Drawing.Size(85, 32);
            this.btnCompactCopy.TabIndex = 3;
            this.btnCompactCopy.Text = "📋 Copy Link";
            this.btnCompactCopy.UseVisualStyleBackColor = true;
            // 
            // btnCompactStop
            // 
            this.btnCompactStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompactStop.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCompactStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompactStop.Location = new System.Drawing.Point(672, 18);
            this.btnCompactStop.Name = "btnCompactStop";
            this.btnCompactStop.Size = new System.Drawing.Size(80, 32);
            this.btnCompactStop.TabIndex = 4;
            this.btnCompactStop.Text = "Start";
            this.btnCompactStop.UseVisualStyleBackColor = true;
            // 
            // btnCompactExpand
            // 
            this.btnCompactExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCompactExpand.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnCompactExpand.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCompactExpand.Location = new System.Drawing.Point(758, 18);
            this.btnCompactExpand.Name = "btnCompactExpand";
            this.btnCompactExpand.Size = new System.Drawing.Size(90, 32);
            this.btnCompactExpand.TabIndex = 5;
            this.btnCompactExpand.Text = "🗖 Full View";
            this.btnCompactExpand.UseVisualStyleBackColor = true;
            // 
            // MiniBarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(860, 68);
            this.Controls.Add(this.lblCompactDot);
            this.Controls.Add(this.lblCompactStatus);
            this.Controls.Add(this.lblCompactUrl);
            this.Controls.Add(this.btnCompactCopy);
            this.Controls.Add(this.btnCompactStop);
            this.Controls.Add(this.btnCompactExpand);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MiniBarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ScreenTask Mini Bar";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.Label lblCompactDot;
        internal System.Windows.Forms.Label lblCompactStatus;
        internal System.Windows.Forms.Label lblCompactUrl;
        internal ScreenTask.UI.Controls.FluentButton btnCompactCopy;
        internal ScreenTask.UI.Controls.FluentButton btnCompactStop;
        internal ScreenTask.UI.Controls.FluentButton btnCompactExpand;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
