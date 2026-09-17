using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ScreenTask.UI.Controls
{
    /// <summary>
    /// Safe P/Invoke helper for Desktop Window Manager (dwmapi.dll) attributes,
    /// enabling Windows 11 immersive dark mode titlebars, rounded corners, and accent borders.
    /// </summary>
    public static class DwmHelper
    {
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWA_BORDER_COLOR = 34;
        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_TEXT_COLOR = 36;

        public enum DwmWindowCornerPreference
        {
            Default = 0,
            DoNotRound = 1,
            Round = 2,
            RoundSmall = 3
        }

        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// Applies modern dark mode and rounded corners to the specified window handle.
        /// Gracefully handles older Windows versions without crashing.
        /// </summary>
        public static void ApplyModernDarkTheme(IntPtr hwnd, bool darkMode = true)
        {
            if (hwnd == IntPtr.Zero) return;

            try
            {
                int useDark = darkMode ? 1 : 0;

                // Try modern attribute (Windows 11 & Windows 10 20H1+)
                int res = DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));
                if (res != 0)
                {
                    // Fallback for older Windows 10 builds (18985 - 19041)
                    DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, ref useDark, sizeof(int));
                }

                // Request Windows 11 rounded corners
                int cornerPreference = (int)DwmWindowCornerPreference.Round;
                DwmSetWindowAttribute(hwnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref cornerPreference, sizeof(int));
            }
            catch
            {
                // Silently ignore on unsupported platforms/emulators
            }
        }
    }
}
