using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenTask.Core.Capture
{
    public record WindowInfo(IntPtr Handle, string Title, string ProcessName, Icon? Icon)
    {
        public override string ToString() => string.IsNullOrWhiteSpace(Title) ? ProcessName : $"{Title} ({ProcessName})";
    }

    public static class WindowEnumerator
    {
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);

        private const int DWMWA_CLOAKED = 14;

        public static List<WindowInfo> GetShareableWindows()
        {
            var list = new List<WindowInfo>();
            var shellWindow = GetShellWindow();

            EnumWindows((hWnd, lParam) =>
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                // Ignore cloaked (virtual desktop / minimized modern apps)
                if (DwmGetWindowAttribute(hWnd, DWMWA_CLOAKED, out bool isCloaked, sizeof(bool)) == 0 && isCloaked)
                    return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                var builder = new StringBuilder(length + 1);
                GetWindowText(hWnd, builder, builder.Capacity);
                string title = builder.ToString().Trim();
                if (string.IsNullOrEmpty(title)) return true;

                GetWindowThreadProcessId(hWnd, out uint processId);
                string processName = "Unknown";
                Icon? icon = null;

                try
                {
                    using var proc = Process.GetProcessById((int)processId);
                    processName = proc.ProcessName;

                    // Exclude self and system shells
                    if (processName.Equals("ScreenTask", StringComparison.OrdinalIgnoreCase) ||
                        processName.Equals("explorer", StringComparison.OrdinalIgnoreCase) ||
                        processName.Equals("SearchHost", StringComparison.OrdinalIgnoreCase) ||
                        processName.Equals("ShellExperienceHost", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    try
                    {
                        icon = Icon.ExtractAssociatedIcon(proc.MainModule?.FileName ?? "");
                    }
                    catch { }
                }
                catch { }

                list.Add(new WindowInfo(hWnd, title, processName, icon));
                return true;
            }, IntPtr.Zero);

            return list;
        }
    }
}
