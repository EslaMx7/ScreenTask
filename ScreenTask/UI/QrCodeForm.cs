using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ScreenTask.Core.Network;

namespace ScreenTask.UI
{
    public class QrCodeForm : Form
    {
        private readonly string _url;
        private readonly PictureBox _picQr;
        private readonly Label _lblUrl;
        private readonly Button _btnCopy;
        private readonly Button _btnOpen;
        private readonly Button _btnClose;

        public QrCodeForm(string url)
        {
            _url = url;

            Text = "ScreenTask 2.0 – Instant Mobile Pairing";
            Size = new Size(380, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(15, 23, 42); // Slate dark
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            var titleLabel = new Label
            {
                Text = "📱 Scan to Watch Stream",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241), // Indigo accent
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            var descLabel = new Label
            {
                Text = "Open your iPhone or Android camera and point it at this QR code to view this screen instantly over Wi-Fi without any app.",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Regular),
                ForeColor = Color.FromArgb(148, 163, 184),
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
                ForeColor = Color.FromArgb(248, 250, 252),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 320),
                Size = new Size(ClientSize.Width - 20, 25)
            };

            _btnCopy = new Button
            {
                Text = "📋 Copy Link",
                Size = new Size(110, 34),
                Location = new Point(45, 360),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _btnCopy.FlatAppearance.BorderColor = Color.FromArgb(51, 65, 85);
            _btnCopy.Click += (s, e) =>
            {
                Clipboard.SetText(_url);
                _btnCopy.Text = "✅ Copied!";
                var timer = new System.Windows.Forms.Timer { Interval = 1500 };
                timer.Tick += (ts, te) =>
                {
                    _btnCopy.Text = "📋 Copy Link";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            };

            _btnOpen = new Button
            {
                Text = "🌐 Open Web",
                Size = new Size(110, 34),
                Location = new Point(165, 360),
                BackColor = Color.FromArgb(79, 70, 229),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _btnOpen.FlatAppearance.BorderSize = 0;
            _btnOpen.Click += (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo(_url) { UseShellExecute = true });
                }
                catch { }
            };

            _btnClose = new Button
            {
                Text = "Close",
                Size = new Size(70, 34),
                Location = new Point(285, 360),
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            _btnClose.FlatAppearance.BorderColor = Color.FromArgb(51, 65, 85);
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
