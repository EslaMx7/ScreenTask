using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI
{
    public partial class MiniBarForm : Form
    {
        private Point _dragStart;

        public event EventHandler? ExpandRequested;
        public event EventHandler? ToggleBroadcastRequested;
        public event EventHandler? CopyUrlRequested;

        public MiniBarForm()
        {
            InitializeComponent();

            btnCompactExpand.Click += (s, e) => ExpandRequested?.Invoke(this, EventArgs.Empty);
            btnCompactStop.Click += (s, e) => ToggleBroadcastRequested?.Invoke(this, EventArgs.Empty);
            btnCompactCopy.Click += (s, e) => CopyUrlRequested?.Invoke(this, EventArgs.Empty);

            this.DoubleClick += (s, e) => ExpandRequested?.Invoke(this, EventArgs.Empty);
            lblCompactDot.DoubleClick += (s, e) => ExpandRequested?.Invoke(this, EventArgs.Empty);
            lblCompactStatus.DoubleClick += (s, e) => ExpandRequested?.Invoke(this, EventArgs.Empty);
            lblCompactUrl.DoubleClick += (s, e) => ExpandRequested?.Invoke(this, EventArgs.Empty);

            // Enable window dragging
            this.MouseDown += OnFormMouseDown;
            lblCompactStatus.MouseDown += OnFormMouseDown;
            lblCompactUrl.MouseDown += OnFormMouseDown;
            lblCompactDot.MouseDown += OnFormMouseDown;

            this.MouseMove += OnFormMouseMove;
            lblCompactStatus.MouseMove += OnFormMouseMove;
            lblCompactUrl.MouseMove += OnFormMouseMove;
            lblCompactDot.MouseMove += OnFormMouseMove;

            toolTip.SetToolTip(btnCompactExpand, "Restore full ScreenTask window");
            toolTip.SetToolTip(btnCompactStop, "Start or stop screen broadcast");
            toolTip.SetToolTip(btnCompactCopy, "Copy stream URL to clipboard");
        }

        private void OnFormMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragStart = e.Location;
            }
        }

        private void OnFormMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - _dragStart.X;
                this.Top += e.Y - _dragStart.Y;
            }
        }

        public void UpdateStatus(bool isBroadcasting, string statusText, string url)
        {
            if (InvokeRequired)
            {
                try { BeginInvoke(() => UpdateStatus(isBroadcasting, statusText, url)); } catch { }
                return;
            }

            lblCompactDot.ForeColor = isBroadcasting ? Color.FromArgb(16, 124, 65) : Color.FromArgb(196, 43, 28);
            lblCompactStatus.Text = isBroadcasting ? $"● {statusText}" : "● Stopped";
            btnCompactStop.Text = isBroadcasting ? "Stop" : "Start";
            lblCompactUrl.Text = url;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // If user closes mini bar, request expand rather than killing app abruptly
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ExpandRequested?.Invoke(this, EventArgs.Empty);
                return;
            }
            base.OnFormClosing(e);
        }
    }
}
