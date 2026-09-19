using System;
using System.Windows.Forms;

namespace ScreenTask.UI.Views
{
    public partial class PreferencesView : UserControl
    {
        public event EventHandler? AutoStartChanged;
        public event EventHandler? MiniStartChanged;
        public event EventHandler? CloseToTrayChanged;

        public PreferencesView()
        {
            InitializeComponent();

            cbAutoStart.CheckedChanged += (s, e) => AutoStartChanged?.Invoke(this, EventArgs.Empty);
            cbMiniStart.CheckedChanged += (s, e) => MiniStartChanged?.Invoke(this, EventArgs.Empty);
            cbCloseToTray.CheckedChanged += (s, e) => CloseToTrayChanged?.Invoke(this, EventArgs.Empty);

            btnClearLog.Click += (s, e) => txtLog.Clear();
            btnCopyLog.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtLog.Text))
                {
                    try { Clipboard.SetText(txtLog.Text); } catch { }
                }
            };
        }

        public void SetPreferences(bool autoStart, bool miniStart, bool closeToTray)
        {
            cbAutoStart.Checked = autoStart;
            cbMiniStart.Checked = miniStart;
            cbCloseToTray.Checked = closeToTray;
        }

        public void AppendLog(string message)
        {
            if (InvokeRequired)
            {
                try { BeginInvoke(() => AppendLog(message)); } catch { }
                return;
            }

            var entry = $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}";
            txtLog.AppendText(entry);
        }

        public bool AutoStart => cbAutoStart.Checked;
        public bool MiniStart => cbMiniStart.Checked;
        public bool CloseToTray => cbCloseToTray.Checked;
    }
}
