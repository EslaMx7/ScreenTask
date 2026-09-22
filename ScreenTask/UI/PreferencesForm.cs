using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenTask.UI
{
    public class PreferencesForm : Form
    {
        public TabControl tabControl = null!;
        public TabPage tabGeneral = null!;
        public TabPage tabLogs = null!;
        public CheckBox cbAutoStart = null!;
        public CheckBox cbMiniStart = null!;
        public CheckBox cbCloseToTray = null!;
        public ComboBox comboFps = null!;
        public TrackBar qualitySlider = null!;
        public Label lblQuality = null!;
        public TextBox txtLog = null!;
        public Button btnClearLog = null!;
        public Button btnCopyLog = null!;
        public Button btnClose = null!;

        public void ClearLog() => txtLog.Clear();

        public event EventHandler? SettingsChanged;

        public PreferencesForm(bool autoStart, bool miniStart, bool closeToTray, int fpsIndex, int quality, string logContent)
        {
            InitializeCustomComponents();

            cbAutoStart.Checked = autoStart;
            cbMiniStart.Checked = miniStart;
            cbCloseToTray.Checked = closeToTray;

            if (fpsIndex >= 0 && fpsIndex < comboFps.Items.Count)
            {
                comboFps.SelectedIndex = fpsIndex;
            }
            else
            {
                comboFps.SelectedIndex = 1; // 30 FPS default
            }

            qualitySlider.Value = Math.Clamp(quality, 20, 100);
            UpdateQualityLabel();

            txtLog.Text = logContent;
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();

            cbAutoStart.CheckedChanged += (s, e) => SettingsChanged?.Invoke(this, EventArgs.Empty);
            cbMiniStart.CheckedChanged += (s, e) => SettingsChanged?.Invoke(this, EventArgs.Empty);
            cbCloseToTray.CheckedChanged += (s, e) => SettingsChanged?.Invoke(this, EventArgs.Empty);
            comboFps.SelectedIndexChanged += (s, e) => SettingsChanged?.Invoke(this, EventArgs.Empty);
            qualitySlider.ValueChanged += (s, e) =>
            {
                UpdateQualityLabel();
                SettingsChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        private void InitializeCustomComponents()
        {
            Text = "ScreenTask 2.0 – Preferences & Quality";
            Size = new Size(580, 540);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = SystemColors.Control;
            ForeColor = SystemColors.ControlText;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(12, 6)
            };

            // ── Tab 1: General & Stream Quality ─────────────────────────────
            tabGeneral = new TabPage
            {
                Text = "⚙️ General & Quality",
                Padding = new Padding(12),
                BackColor = SystemColors.Window
            };

            var gbStartup = new GroupBox
            {
                Text = "Application Startup & Tray Behavior",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Location = new Point(14, 12),
                Size = new Size(520, 118)
            };

            cbAutoStart = new CheckBox
            {
                Text = "Start broadcast automatically when ScreenTask opens",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 26),
                AutoSize = true
            };
            gbStartup.Controls.Add(cbAutoStart);

            cbMiniStart = new CheckBox
            {
                Text = "Start minimized to system tray",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 54),
                AutoSize = true
            };
            gbStartup.Controls.Add(cbMiniStart);

            cbCloseToTray = new CheckBox
            {
                Text = "Minimize to system tray when closing window",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 82),
                AutoSize = true,
                Checked = true
            };
            gbStartup.Controls.Add(cbCloseToTray);
            tabGeneral.Controls.Add(gbStartup);

            // Stream Quality Group
            var gbQuality = new GroupBox
            {
                Text = "Stream Video Quality & Framerate",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Location = new Point(14, 142),
                Size = new Size(520, 150)
            };

            var lblFps = new Label
            {
                Text = "Framerate (Smoothness):",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 26),
                AutoSize = true
            };
            gbQuality.Controls.Add(lblFps);

            comboFps = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(18, 48),
                Size = new Size(240, 24)
            };
            comboFps.Items.AddRange(new object[] {
                "60 FPS (Ultra Smooth)",
                "30 FPS (Recommended)",
                "15 FPS (Balanced)",
                "5 FPS (Low Bandwidth)"
            });
            gbQuality.Controls.Add(comboFps);

            lblQuality = new Label
            {
                Text = "Image Quality: 80% (High Detail)",
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(16, 86),
                AutoSize = true
            };
            gbQuality.Controls.Add(lblQuality);

            qualitySlider = new TrackBar
            {
                Location = new Point(12, 106),
                Size = new Size(490, 36),
                Minimum = 20,
                Maximum = 100,
                TickFrequency = 10,
                Value = 80
            };
            gbQuality.Controls.Add(qualitySlider);
            tabGeneral.Controls.Add(gbQuality);

            tabControl.TabPages.Add(tabGeneral);

            // ── Tab 2: Diagnostic Logs ──────────────────────────────────────
            tabLogs = new TabPage
            {
                Text = "📋 Diagnostic Logs",
                Padding = new Padding(12),
                BackColor = SystemColors.Window
            };

            var pnlLogButtons = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36
            };

            btnClearLog = new Button
            {
                Text = "Clear Log",
                Font = new Font("Segoe UI", 8F),
                Location = new Point(0, 4),
                Size = new Size(80, 28),
                UseVisualStyleBackColor = true
            };
            btnClearLog.Click += (s, e) => txtLog.Clear();
            pnlLogButtons.Controls.Add(btnClearLog);

            btnCopyLog = new Button
            {
                Text = "📋 Copy Log",
                Font = new Font("Segoe UI", 8F),
                Location = new Point(88, 4),
                Size = new Size(95, 28),
                UseVisualStyleBackColor = true
            };
            btnCopyLog.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtLog.Text))
                {
                    try { Clipboard.SetText(txtLog.Text); } catch { }
                }
            };
            pnlLogButtons.Controls.Add(btnCopyLog);
            tabLogs.Controls.Add(pnlLogButtons);

            txtLog = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 8.5F),
                BackColor = SystemColors.Window,
                ForeColor = SystemColors.WindowText
            };
            tabLogs.Controls.Add(txtLog);
            txtLog.BringToFront();

            tabControl.TabPages.Add(tabLogs);
            Controls.Add(tabControl);

            // ── Bottom Panel ────────────────────────────────────────────────
            var panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 46,
                Padding = new Padding(12, 6, 12, 6),
                BackColor = SystemColors.Control
            };

            btnClose = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.OK,
                Size = new Size(90, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(ClientSize.Width - 102, 6),
                UseVisualStyleBackColor = true
            };
            btnClose.Click += (s, e) => Close();
            panelBottom.Controls.Add(btnClose);
            Controls.Add(panelBottom);

            tabControl.BringToFront();
        }

        private void UpdateQualityLabel()
        {
            string desc = qualitySlider.Value switch
            {
                >= 90 => "Ultra Detail",
                >= 70 => "High Detail",
                >= 50 => "Medium Detail",
                _ => "Low Bandwidth"
            };
            lblQuality.Text = $"Image Quality: {qualitySlider.Value}% ({desc})";
        }
    }
}
