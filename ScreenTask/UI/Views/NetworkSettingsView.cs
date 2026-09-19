using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScreenTask.UI.Views
{
    public partial class NetworkSettingsView : UserControl
    {
        public event EventHandler? RefreshIpsClicked;
        public event EventHandler? IpSelectionChanged;
        public event EventHandler? PortChanged;
        public event EventHandler? PrivateToggled;
        public event EventHandler? PinChanged;
        public event EventHandler? GenPinClicked;
        public event EventHandler? CopyPinClicked;

        public NetworkSettingsView()
        {
            InitializeComponent();

            btnRefreshIPs.Click += (s, e) => RefreshIpsClicked?.Invoke(this, EventArgs.Empty);
            comboIPs.SelectedIndexChanged += (s, e) => IpSelectionChanged?.Invoke(this, EventArgs.Empty);
            numPort.ValueChanged += (s, e) => PortChanged?.Invoke(this, EventArgs.Empty);

            cbPrivate.CheckedChanged += (s, e) =>
            {
                txtPassword.Enabled = cbPrivate.Checked;
                btnGenPin.Enabled = cbPrivate.Checked;
                btnCopyPin.Enabled = cbPrivate.Checked;
                PrivateToggled?.Invoke(this, EventArgs.Empty);
            };

            txtPassword.TextChanged += (s, e) => PinChanged?.Invoke(this, EventArgs.Empty);
            btnGenPin.Click += (s, e) => GenPinClicked?.Invoke(this, EventArgs.Empty);
            btnCopyPin.Click += (s, e) => CopyPinClicked?.Invoke(this, EventArgs.Empty);
        }

        public void SetAdapters(IEnumerable<string> items, int selectedIndex)
        {
            comboIPs.Items.Clear();
            foreach (var item in items) comboIPs.Items.Add(item);
            if (selectedIndex >= 0 && selectedIndex < comboIPs.Items.Count)
            {
                comboIPs.SelectedIndex = selectedIndex;
            }
        }

        public void SetPort(int port)
        {
            numPort.Value = Math.Clamp(port, (int)numPort.Minimum, (int)numPort.Maximum);
        }

        public void SetPin(bool requirePin, string pin)
        {
            cbPrivate.Checked = requirePin;
            txtPassword.Enabled = requirePin;
            btnGenPin.Enabled = requirePin;
            btnCopyPin.Enabled = requirePin;
            txtPassword.Text = pin;
        }

        public int Port => (int)numPort.Value;
        public bool RequirePin => cbPrivate.Checked;
        public string Pin => txtPassword.Text.Trim();
    }
}
