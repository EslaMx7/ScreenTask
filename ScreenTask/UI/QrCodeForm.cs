using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ScreenTask.Core.Network;
using ScreenTask.UI.Controls;

namespace ScreenTask.UI
{
    public class QrCodeForm : Form
    {
        private readonly string _url;
        private readonly PictureBox _picQr;
        private readonly Label _lblUrl;
        private readonly FluentButton _btnCopy;
        private readonly FluentButton _btnOpen;
        private readonly FluentButton _btnClose;

        public QrCodeForm(string url)
        {
            _url = url;

            Text = "ScreenTask 2.0 – Connect via QR Code";
            Size = new Size(380, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            var titleLabel = new Label
            {
                Text = "📱 Scan with Phone or Tablet",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            var descLabel = new Label
            {
                Text = "Point your phone or tablet camera at this QR code to connect and watch instantly — no app installation needed.",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                ForeColor = SystemColors.GrayText,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 42,
                Padding = new Padding(12, 0, 12, 0)
            };

            _picQr = new PictureBox
            {
                Size = new Size(220, 220),
                Location = new Point((ClientSize.Width - 220) / 2, 90),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Padding = new Padding(8)
            };

            try
            {
                _picQr.Image = QrCodeHelper.GenerateQrCodeBitmap(url, 8);
            }
            catch { }

            _lblUrl = new Label
            {
                Text = url,
                Font = new Font("Consolas", 10.5f, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 320),
                Size = new Size(ClientSize.Width - 20, 25)
            };

            _btnCopy = new FluentButton
            {
                Text = "📋 Copy Link",
                Size = new Size(110, 34),
                Location = new Point(45, 360),
                UseVisualStyleBackColor = true,
                ForeColor = SystemColors.ControlText
            };
            _btnCopy.Click += (s, e) =>
            {
                Clipboard.SetText(_url);
                _btnCopy.Text = "✓ Copied!";
                var timer = new System.Windows.Forms.Timer { Interval = 1500 };
                timer.Tick += (ts, te) =>
                {
                    _btnCopy.Text = "📋 Copy Link";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            };

            _btnOpen = new FluentButton
            {
                Text = "🌐 Open Browser",
                Size = new Size(120, 34),
                Location = new Point(160, 360),
                UseVisualStyleBackColor = true,
                ForeColor = SystemColors.ControlText
            };
            _btnOpen.Click += (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo(_url) { UseShellExecute = true });
                }
                catch { }
            };

            _btnClose = new FluentButton
            {
                Text = "Close",
                Size = new Size(70, 34),
                Location = new Point(285, 360),
                UseVisualStyleBackColor = true,
                ForeColor = SystemColors.ControlText
            };
            _btnClose.Click += (s, e) => Close();

            Controls.Add(_btnClose);
            Controls.Add(_btnOpen);
            Controls.Add(_btnCopy);
            Controls.Add(_lblUrl);
            Controls.Add(_picQr);
            Controls.Add(descLabel);
            Controls.Add(titleLabel);
        }
    }
}
