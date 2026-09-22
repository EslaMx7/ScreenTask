using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI
{
    public class NetworkSettingsForm : Form
    {
        public ComboBox comboIPs = null!;
        public Button btnRefreshIPs = null!;
        public NumericUpDown numPort = null!;
        public Label lblPortStatus = null!;
        public CheckBox cbPrivate = null!;
        public TextBox txtPassword = null!;
        public Button btnGenPin = null!;
        public Button btnCopyPin = null!;
        public Button btnApply = null!;
        public Button btnCancel = null!;

        public int SelectedAdapterIndex => comboIPs.SelectedIndex;
        public int Port => (int)numPort.Value;
        public bool RequirePin => cbPrivate.Checked;
        public string Pin => txtPassword.Text.Trim();

        public event EventHandler? RefreshAdaptersRequested;
        public event EventHandler? SettingsApplied;

        public NetworkSettingsForm(IEnumerable<string> adapters, int selectedIndex, int port, bool requirePin, string pin)
        {
            InitializeCustomComponents();

            PopulateAdapters(adapters, selectedIndex);
            numPort.Value = Math.Clamp(port, 1, 65535);
            cbPrivate.Checked = requirePin;
            txtPassword.Text = pin;

            UpdatePinControlsState();
        }

        private void InitializeCustomComponents()
        {
            Text = "ScreenTask 2.0 – Network & PIN Settings";
            Size = new Size(490, 460);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // ── Title Header ────────────────────────────────────────────────
            var lblTitle = new Label
            {
                Text = "🛡️ Network & PIN Security",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(18, 14),
                AutoSize = true
            };
            Controls.Add(lblTitle);

            var lblDesc = new Label
            {
                Text = "Configure the network adapter, port, and viewer PIN protection.",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(20, 38),
                AutoSize = true
            };
            Controls.Add(lblDesc);

            // ── GroupBox: Network Connection ────────────────────────────────
            var gbNetwork = new GroupBox
            {
                Text = "Network Interface (IP & Port)",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(18, 68),
                Size = new Size(440, 142)
            };

            var lblIp = new Label
            {
                Text = "Network Connection (IP Address):",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 26),
                AutoSize = true
            };
            gbNetwork.Controls.Add(lblIp);

            comboIPs = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(18, 48),
                Size = new Size(310, 24)
            };
            gbNetwork.Controls.Add(comboIPs);

            btnRefreshIPs = new Button
            {
                Text = "🔄 Refresh",
                Font = new Font("Segoe UI", 8F),
                Location = new Point(336, 47),
                Size = new Size(88, 26),
                UseVisualStyleBackColor = true
            };
            btnRefreshIPs.Click += (s, e) => RefreshAdaptersRequested?.Invoke(this, EventArgs.Empty);
            gbNetwork.Controls.Add(btnRefreshIPs);

            var lblPort = new Label
            {
                Text = "Port Number:",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 84),
                AutoSize = true
            };
            gbNetwork.Controls.Add(lblPort);

            numPort = new NumericUpDown
            {
                Font = new Font("Segoe UI", 9F),
                Location = new Point(18, 104),
                Size = new Size(120, 24),
                Minimum = 1,
                Maximum = 65535,
                Value = 7070
            };
            gbNetwork.Controls.Add(numPort);

            lblPortStatus = new Label
            {
                Text = "Default: 7070 (Recommended)",
                Font = new Font("Segoe UI", 8F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(150, 107),
                AutoSize = true
            };
            gbNetwork.Controls.Add(lblPortStatus);

            Controls.Add(gbNetwork);

            // ── GroupBox: PIN Security ──────────────────────────────────────
            var gbSecurity = new GroupBox
            {
                Text = "Stream PIN Code Protection",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = SystemColors.ControlText,
                Location = new Point(18, 222),
                Size = new Size(440, 136)
            };

            cbPrivate = new CheckBox
            {
                Text = "Protect stream with a 4-digit PIN code",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Location = new Point(16, 26),
                AutoSize = true
            };
            cbPrivate.CheckedChanged += (s, e) => UpdatePinControlsState();
            gbSecurity.Controls.Add(cbPrivate);

            var lblPinHelp = new Label
            {
                Text = "Viewers must enter this PIN in their browser before video unlocks:",
                Font = new Font("Segoe UI", 8F),
                ForeColor = SystemColors.GrayText,
                Location = new Point(16, 52),
                AutoSize = true
            };
            gbSecurity.Controls.Add(lblPinHelp);

            txtPassword = new TextBox
            {
                Font = new Font("Consolas", 12F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Center,
                Location = new Point(18, 76),
                Size = new Size(120, 26),
                MaxLength = 8,
                PasswordChar = '•'
            };
            gbSecurity.Controls.Add(txtPassword);

            btnGenPin = new Button
            {
                Text = "🎲 New PIN",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(150, 76),
                Size = new Size(95, 27),
                UseVisualStyleBackColor = true
            };
            btnGenPin.Click += (s, e) =>
            {
                txtPassword.Text = new Random().Next(1000, 9999).ToString();
            };
            gbSecurity.Controls.Add(btnGenPin);

            btnCopyPin = new Button
            {
                Text = "📋 Copy PIN",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(252, 76),
                Size = new Size(95, 27),
                UseVisualStyleBackColor = true
            };
            btnCopyPin.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    try { Clipboard.SetText(txtPassword.Text); } catch { }
                }
            };
            gbSecurity.Controls.Add(btnCopyPin);

            Controls.Add(gbSecurity);

            // ── Bottom Buttons ──────────────────────────────────────────────
            btnApply = new Button
            {
                Text = "Save & Apply",
                DialogResult = DialogResult.OK,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(245, 372),
                Size = new Size(115, 32),
                UseVisualStyleBackColor = true
            };
            btnApply.Click += (s, e) =>
            {
                SettingsApplied?.Invoke(this, EventArgs.Empty);
                Close();
            };
            Controls.Add(btnApply);

            btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(368, 372),
                Size = new Size(90, 32),
                UseVisualStyleBackColor = true
            };
            btnCancel.Click += (s, e) => Close();
            Controls.Add(btnCancel);
        }

        public void PopulateAdapters(IEnumerable<string> items, int selectedIndex)
        {
            comboIPs.Items.Clear();
            foreach (var item in items)
            {
                comboIPs.Items.Add(item);
            }

            if (selectedIndex >= 0 && selectedIndex < comboIPs.Items.Count)
            {
                comboIPs.SelectedIndex = selectedIndex;
            }
            else if (comboIPs.Items.Count > 0)
            {
                comboIPs.SelectedIndex = 0;
            }
        }

        private void UpdatePinControlsState()
        {
            bool enabled = cbPrivate.Checked;
            txtPassword.Enabled = enabled;
            btnGenPin.Enabled = enabled;
            btnCopyPin.Enabled = enabled;

            if (enabled && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = new Random().Next(1000, 9999).ToString();
            }
        }
    }
}
