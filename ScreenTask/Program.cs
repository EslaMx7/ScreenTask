using System;
using System.Threading;
using System.Windows.Forms;
using ScreenTask.UI;

namespace ScreenTask
{
    static class Program
    {
        private static frmMain? _mainForm;
        private static int _isHandlingCrash;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += OnUiThreadException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledDomainException;

            _mainForm = new frmMain();
            Application.Run(_mainForm);
        }

        private static void OnUiThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowCrashRecoveryDialog(e.Exception);
        }

        private static void OnUnhandledDomainException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                ShowCrashRecoveryDialog(ex);
            }
        }

        internal static void ShowCrashRecoveryDialog(Exception ex)
        {
            if (Interlocked.Exchange(ref _isHandlingCrash, 1) == 1)
            {
                return;
            }

            try
            {
                _mainForm?.Log($"Unhandled error: {ex.GetType().Name} - {ex.Message}");
                FeedbackDiagnosticsSnapshot? snapshot = null;
                try
                {
                    if (_mainForm is { IsDisposed: false })
                    {
                        snapshot = _mainForm.BuildFeedbackDiagnosticsSnapshot();
                    }
                }
                catch { }

                using var dialog = FeedbackForm.CreateCrashReportDialog(ex, snapshot);
                if (_mainForm is { IsDisposed: false, Visible: true })
                {
                    dialog.ShowDialog(_mainForm);
                }
                else
                {
                    dialog.StartPosition = FormStartPosition.CenterScreen;
                    dialog.ShowDialog();
                }
            }
            catch
            {
                try
                {
                    MessageBox.Show(
                        $"An unexpected error occurred in ScreenTask 2.0:\n\n{ex.Message}\n\nPlease report this issue at {Constants.GitHubIssuesUrl} or {Constants.SupportEmail}.",
                        "ScreenTask 2.0 – Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch { }
            }
            finally
            {
                Interlocked.Exchange(ref _isHandlingCrash, 0);
            }
        }
    }
}
