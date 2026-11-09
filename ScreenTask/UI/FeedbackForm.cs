using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenTask.UI
{
    public enum FeedbackType
    {
        BugReport,
        FeatureRequest
    }

    public sealed class FeedbackDiagnosticsSnapshot
    {
        public string AppVersion { get; init; } = Constants.AppVersion;
        public string CaptureSourceType { get; init; } = "Screen";
        public int TargetFps { get; init; } = 30;
        public int ImageQuality { get; init; } = 80;
        public bool IsAudioEnabled { get; init; }
        public string AudioDeviceType { get; init; } = "None";
        public bool IsServerRunning { get; init; }
        public bool IsPinProtected { get; init; }
        public int ActiveViewers { get; init; }
        public string RecentLogLines { get; init; } = string.Empty;
        public string? CrashExceptionDetails { get; init; }
    }

    public class FeedbackForm : Form
    {
        private readonly FeedbackDiagnosticsSnapshot _diagnostics;
        private FeedbackType _currentType;

        internal RadioButton rbBugReport = null!;
        internal RadioButton rbFeatureRequest = null!;
        internal TextBox txtTitle = null!;
        internal TextBox txtDetails = null!;
        internal CheckBox cbIncludeDiagnostics = null!;
        internal TextBox txtDiagnostics = null!;
        internal Button btnOpenGitHub = null!;
        internal Button btnSendEmail = null!;
        internal Button btnCopyMessage = null!;
        internal Button btnClose = null!;
        internal Label lblStatus = null!;

        public const string DefaultBugTemplate =
            "## Bug Description\r\n" +
            "Describe what went wrong clearly and concisely...\r\n\r\n" +
            "## Steps to Reproduce\r\n" +
            "1. Launch ScreenTask 2.0\r\n" +
            "2. Select capture source / settings...\r\n" +
            "3. Click 'Start Broadcast'\r\n" +
            "4. Observe issue...\r\n\r\n" +
            "## Expected vs. Actual Behavior\r\n" +
            "- Expected: \r\n" +
            "- Actual: ";

        public const string DefaultFeatureTemplate =
            "## Problem or Use Case\r\n" +
            "What problem or workflow would this feature solve?\r\n\r\n" +
            "## Proposed Solution\r\n" +
            "Describe how you would like this feature or UI to work...\r\n\r\n" +
            "## Additional Context\r\n" +
            "Any extra notes or examples...";

        public FeedbackForm(FeedbackDiagnosticsSnapshot? diagnostics = null, FeedbackType initialType = FeedbackType.BugReport)
        {
            _diagnostics = diagnostics ?? new FeedbackDiagnosticsSnapshot();
            _currentType = initialType;

            InitializeLayout();
            ApplyFeedbackType(initialType, forceResetTemplate: true);
        }

        public static FeedbackForm CreateCrashReportDialog(Exception exception, FeedbackDiagnosticsSnapshot? baseSnapshot = null)
        {
            var baseDiag = baseSnapshot ?? new FeedbackDiagnosticsSnapshot();
            var crashSnapshot = new FeedbackDiagnosticsSnapshot
            {
                AppVersion = baseDiag.AppVersion,
                CaptureSourceType = baseDiag.CaptureSourceType,
                TargetFps = baseDiag.TargetFps,
                ImageQuality = baseDiag.ImageQuality,
                IsAudioEnabled = baseDiag.IsAudioEnabled,
                AudioDeviceType = baseDiag.AudioDeviceType,
                IsServerRunning = baseDiag.IsServerRunning,
                IsPinProtected = baseDiag.IsPinProtected,
                ActiveViewers = baseDiag.ActiveViewers,
                RecentLogLines = baseDiag.RecentLogLines,
                CrashExceptionDetails = exception.ToString()
            };

            var form = new FeedbackForm(crashSnapshot, FeedbackType.BugReport);
            form.Text = "Unexpected Error – Report Crash (ScreenTask 2.0)";
            form.txtTitle.Text = $"Unhandled {exception.GetType().Name}: {exception.Message}";
            return form;
        }

        private void InitializeLayout()
        {
            Text = "Report a Bug or Request a Feature – ScreenTask 2.0";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 9F);
            ClientSize = new Size(1000, 800);
            MinimumSize = new Size(900, 700);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            ShowIcon = false;

            var rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 6,
                Padding = new Padding(14, 12, 14, 12)
            };
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 0: Header banner
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 1: Type & Title
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F)); // 2: Details template
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F)); // 3: Diagnostics preview
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 4: Status feedback label
            rootLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // 5: Action buttons

            // 0: Intro Header
            var lblIntro = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 8),
                Text = $"Help improve ScreenTask 2.0! Submit an issue on GitHub (recommended for open-source tracking) or send an email to {Constants.SupportEmail}."
            };
            rootLayout.Controls.Add(lblIntro, 0, 0);

            // 1: Type & Title Group
            var grpHeader = new GroupBox
            {
                Text = "1. Feedback Type && Summary",
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(10, 6, 10, 10),
                Margin = new Padding(0, 0, 0, 8)
            };

            var headerTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 2
            };
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            headerTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            headerTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var lblType = new Label
            {
                Text = "Type:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 4, 10, 6)
            };

            var typeFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, 4)
            };

            rbBugReport = new RadioButton
            {
                Text = "Report a Bug / Issue",
                AutoSize = true,
                Checked = _currentType == FeedbackType.BugReport,
                Margin = new Padding(0, 2, 18, 2)
            };

            rbFeatureRequest = new RadioButton
            {
                Text = "Request a Feature / Idea",
                AutoSize = true,
                Checked = _currentType == FeedbackType.FeatureRequest,
                Margin = new Padding(0, 2, 0, 2)
            };

            rbBugReport.CheckedChanged += (s, e) =>
            {
                if (rbBugReport.Checked)
                {
                    ApplyFeedbackType(FeedbackType.BugReport, forceResetTemplate: false);
                }
            };

            rbFeatureRequest.CheckedChanged += (s, e) =>
            {
                if (rbFeatureRequest.Checked)
                {
                    ApplyFeedbackType(FeedbackType.FeatureRequest, forceResetTemplate: false);
                }
            };

            typeFlow.Controls.Add(rbBugReport);
            typeFlow.Controls.Add(rbFeatureRequest);

            var lblTitle = new Label
            {
                Text = "Title:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Margin = new Padding(0, 4, 10, 0)
            };

            txtTitle = new TextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "Short summary (e.g., Audio stutters when switching monitor)",
                Margin = new Padding(0, 2, 0, 0)
            };

            headerTable.Controls.Add(lblType, 0, 0);
            headerTable.Controls.Add(typeFlow, 1, 0);
            headerTable.Controls.Add(lblTitle, 0, 1);
            headerTable.Controls.Add(txtTitle, 1, 1);
            grpHeader.Controls.Add(headerTable);
            rootLayout.Controls.Add(grpHeader, 0, 1);

            // 2: Editable Template Details
            var grpDetails = new GroupBox
            {
                Text = "2. Description && Details (Pre-filled Template – Editable)",
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 8, 10, 10),
                Margin = new Padding(0, 0, 0, 8)
            };

            txtDetails = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                AcceptsReturn = true,
                Font = new Font("Consolas", 9F)
            };
            grpDetails.Controls.Add(txtDetails);
            rootLayout.Controls.Add(grpDetails, 0, 2);

            // 3: Diagnostics Section
            var grpDiagnostics = new GroupBox
            {
                Text = "3. Diagnostic Information (Editable – Review or Redact Before Sending)",
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 6, 10, 10),
                Margin = new Padding(0, 0, 0, 6)
            };

            var diagTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            diagTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            diagTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            diagTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            cbIncludeDiagnostics = new CheckBox
            {
                Text = "Include system environment && recent session log in report",
                AutoSize = true,
                Checked = true,
                Margin = new Padding(0, 0, 0, 6)
            };

            txtDiagnostics = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 8.5F),
                Text = FormatDiagnosticsBlock(_diagnostics)
            };

            cbIncludeDiagnostics.CheckedChanged += (s, e) =>
            {
                txtDiagnostics.Enabled = cbIncludeDiagnostics.Checked;
            };

            diagTable.Controls.Add(cbIncludeDiagnostics, 0, 0);
            diagTable.Controls.Add(txtDiagnostics, 0, 1);
            grpDiagnostics.Controls.Add(diagTable);
            rootLayout.Controls.Add(grpDiagnostics, 0, 3);

            // 4: Status Label
            lblStatus = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(20, 110, 50),
                Margin = new Padding(2, 2, 2, 6),
                Text = $"Tip: Clicking 'Open on GitHub' or 'Send Email' also copies the full report to your clipboard."
            };
            rootLayout.Controls.Add(lblStatus, 0, 4);

            // 5: Bottom Action Buttons
            var buttonBar = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0)
            };
            buttonBar.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            buttonBar.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            buttonBar.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            buttonBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            btnOpenGitHub = new Button
            {
                Text = "Submit GitHub Issue",
                AutoSize = true,
                MinimumSize = new Size(0, 34),
                Padding = new Padding(10, 2, 10, 2),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(0, 0, 8, 0),
                UseVisualStyleBackColor = true
            };
            btnOpenGitHub.Click += btnOpenGitHub_Click;

            btnSendEmail = new Button
            {
                Text = $"Send Email",
                AutoSize = true,
                MinimumSize = new Size(0, 34),
                Padding = new Padding(10, 2, 10, 2),
                Margin = new Padding(0, 0, 8, 0),
                UseVisualStyleBackColor = true
            };
            btnSendEmail.Click += btnSendEmail_Click;

            btnCopyMessage = new Button
            {
                Text = "Copy Report",
                AutoSize = true,
                MinimumSize = new Size(0, 34),
                Padding = new Padding(10, 2, 10, 2),
                Margin = new Padding(0, 0, 8, 0),
                UseVisualStyleBackColor = true
            };
            btnCopyMessage.Click += btnCopyMessage_Click;

            btnClose = new Button
            {
                Text = "Close",
                AutoSize = true,
                MinimumSize = new Size(80, 34),
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0),
                UseVisualStyleBackColor = true
            };
            btnClose.Click += (s, e) => Close();

            buttonBar.Controls.Add(btnOpenGitHub, 0, 0);
            buttonBar.Controls.Add(btnSendEmail, 1, 0);
            buttonBar.Controls.Add(btnCopyMessage, 2, 0);
            buttonBar.Controls.Add(btnClose, 3, 0);
            rootLayout.Controls.Add(buttonBar, 0, 5);

            Controls.Add(rootLayout);
            CancelButton = btnClose;
        }

        internal void ApplyFeedbackType(FeedbackType type, bool forceResetTemplate)
        {
            _currentType = type;

            string currentDetails = txtDetails.Text.Trim();
            bool isUnmodifiedTemplate = string.IsNullOrEmpty(currentDetails) ||
                                        currentDetails == DefaultBugTemplate.Trim() ||
                                        currentDetails == DefaultFeatureTemplate.Trim();

            if (forceResetTemplate || isUnmodifiedTemplate)
            {
                txtDetails.Text = type == FeedbackType.BugReport
                    ? DefaultBugTemplate
                    : DefaultFeatureTemplate;
            }

            if (type == FeedbackType.BugReport)
            {
                txtTitle.PlaceholderText = "Short bug summary (e.g., Audio stutters when switching monitor)";
                cbIncludeDiagnostics.Checked = true;
            }
            else
            {
                txtTitle.PlaceholderText = "Short feature idea (e.g., Add hotkey to freeze/resume broadcast)";
            }
        }

        public static string FormatDiagnosticsBlock(FeedbackDiagnosticsSnapshot snapshot)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"- ScreenTask Version: {snapshot.AppVersion}");
            sb.AppendLine($"- OS: {RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})");
            sb.AppendLine($"- .NET Runtime: {RuntimeInformation.FrameworkDescription}");

            try
            {
                int screenCount = Screen.AllScreens.Length;
                var primaryBounds = Screen.PrimaryScreen?.Bounds ?? Rectangle.Empty;
                sb.AppendLine($"- Displays: {screenCount} screen(s), Primary {primaryBounds.Width}x{primaryBounds.Height}");
            }
            catch
            {
                sb.AppendLine("- Displays: N/A");
            }

            sb.AppendLine($"- Capture Mode: {snapshot.CaptureSourceType} @ {snapshot.TargetFps} FPS (Quality: {snapshot.ImageQuality}%)");
            sb.AppendLine($"- Audio Streaming: {(snapshot.IsAudioEnabled ? $"Enabled ({snapshot.AudioDeviceType})" : "Muted (Opt-in default)")}");
            sb.AppendLine($"- Broadcast State: {(snapshot.IsServerRunning ? "Live" : "Stopped")}, PIN Protected: {snapshot.IsPinProtected}, Viewers: {snapshot.ActiveViewers}");

            if (!string.IsNullOrWhiteSpace(snapshot.CrashExceptionDetails))
            {
                sb.AppendLine();
                sb.AppendLine("Unhandled Exception Details:");
                sb.AppendLine("```text");
                sb.AppendLine(snapshot.CrashExceptionDetails.Trim());
                sb.AppendLine("```");
            }

            if (!string.IsNullOrWhiteSpace(snapshot.RecentLogLines))
            {
                sb.AppendLine();
                sb.AppendLine("Recent Session Log:");
                sb.AppendLine("```text");
                sb.AppendLine(snapshot.RecentLogLines.Trim());
                sb.AppendLine("```");
            }

            return sb.ToString().TrimEnd();
        }

        public string BuildSubjectLine()
        {
            string prefix = _currentType == FeedbackType.BugReport ? "[Bug]" : "[Feature]";
            string userTitle = txtTitle.Text.Trim();
            if (string.IsNullOrEmpty(userTitle))
            {
                userTitle = _currentType == FeedbackType.BugReport
                    ? $"ScreenTask v{_diagnostics.AppVersion} Bug Report"
                    : $"ScreenTask v{_diagnostics.AppVersion} Feature Request";
            }
            return $"{prefix}: {userTitle}";
        }

        public string BuildFullReportText()
        {
            var sb = new StringBuilder();
            sb.AppendLine(txtDetails.Text.Trim());

            if (cbIncludeDiagnostics.Checked && !string.IsNullOrWhiteSpace(txtDiagnostics.Text))
            {
                sb.AppendLine();
                sb.AppendLine("## System & Session Diagnostics");
                sb.AppendLine(txtDiagnostics.Text.Trim());
            }

            return sb.ToString().Trim();
        }

        public string BuildGitHubIssueUrl()
        {
            string label = _currentType == FeedbackType.BugReport ? "bug" : "enhancement";
            string title = BuildSubjectLine();
            string body = BuildFullReportText();

            // Keep query string within safe browser URL limits (~2000 chars total)
            const int maxBodyChars = 1400;
            if (body.Length > maxBodyChars)
            {
                body = body[..maxBodyChars] + "\r\n\r\n*(Full diagnostics copied to clipboard – press Ctrl+V to paste)*";
            }

            return $"{Constants.GitHubIssuesUrl}/new?labels={Uri.EscapeDataString(label)}&title={Uri.EscapeDataString(title)}&body={Uri.EscapeDataString(body)}";
        }

        public string BuildMailtoUri()
        {
            string subject = $"[ScreenTask {_diagnostics.AppVersion}] {BuildSubjectLine()}";
            string body = BuildFullReportText();

            const int maxMailtoBodyChars = 1400;
            if (body.Length > maxMailtoBodyChars)
            {
                body = body[..maxMailtoBodyChars] + "\r\n\r\n[Full report & diagnostics copied to clipboard – press Ctrl+V to paste]";
            }

            return $"mailto:{Constants.SupportEmail}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
        }

        private bool TryCopyReportToClipboard(bool includeHeaderMetadata)
        {
            try
            {
                var sb = new StringBuilder();
                if (includeHeaderMetadata)
                {
                    sb.AppendLine($"To: {Constants.SupportEmail}");
                    sb.AppendLine($"GitHub Issues: {Constants.GitHubIssuesUrl}");
                    sb.AppendLine($"Subject: {BuildSubjectLine()}");
                    sb.AppendLine(new string('-', 50));
                    sb.AppendLine();
                }

                sb.AppendLine(BuildFullReportText());
                Clipboard.SetText(sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnOpenGitHub_Click(object? sender, EventArgs e)
        {
            TryCopyReportToClipboard(includeHeaderMetadata: false);
            string url = BuildGitHubIssueUrl();
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                lblStatus.ForeColor = Color.FromArgb(20, 110, 50);
                lblStatus.Text = "✓ Opened GitHub Issues in your browser (full report also copied to clipboard).";
            }
            catch
            {
                lblStatus.ForeColor = Color.FromArgb(180, 60, 20);
                lblStatus.Text = $"Could not launch browser automatically. Report copied to clipboard — visit {Constants.GitHubIssuesUrl}";
            }
        }

        private void btnSendEmail_Click(object? sender, EventArgs e)
        {
            TryCopyReportToClipboard(includeHeaderMetadata: true);
            string mailto = BuildMailtoUri();
            try
            {
                Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
                lblStatus.ForeColor = Color.FromArgb(20, 110, 50);
                lblStatus.Text = $"✓ Opened default mail client for {Constants.SupportEmail} (also copied to clipboard if you use webmail).";
            }
            catch
            {
                lblStatus.ForeColor = Color.FromArgb(180, 60, 20);
                lblStatus.Text = $"No default mail app found. Full report copied to clipboard — paste and email it to {Constants.SupportEmail}.";
            }
        }

        private void btnCopyMessage_Click(object? sender, EventArgs e)
        {
            if (TryCopyReportToClipboard(includeHeaderMetadata: true))
            {
                lblStatus.ForeColor = Color.FromArgb(20, 110, 50);
                lblStatus.Text = $"✓ Copied full report, diagnostics, and {Constants.SupportEmail} address to clipboard!";
            }
            else
            {
                lblStatus.ForeColor = Color.FromArgb(180, 60, 20);
                lblStatus.Text = "Could not access system clipboard. You can select and copy the text above manually.";
            }
        }
    }
}
