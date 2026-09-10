using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ScreenTask.Core.Capture
{
    public class WindowCaptureSource : ICaptureSource
    {
        private readonly IntPtr _hWnd;
        private readonly string _title;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }

        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBmp, uint nFlags);

        private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;
        private const uint PW_RENDERFULLCONTENT = 0x00000002;

        public WindowCaptureSource(IntPtr hWnd, string title)
        {
            _hWnd = hWnd;
            _title = title;
        }

        public string Id => $"Window_{(long)_hWnd}";

        public string DisplayName => $"App Window: {_title}";

        public Rectangle Bounds
        {
            get
            {
                if (!IsWindow(_hWnd)) return Rectangle.Empty;

                if (DwmGetWindowAttribute(_hWnd, DWMWA_EXTENDED_FRAME_BOUNDS, out RECT frame, Marshal.SizeOf<RECT>()) == 0)
                {
                    return new Rectangle(frame.Left, frame.Top, Math.Max(1, frame.Width), Math.Max(1, frame.Height));
                }

                if (GetWindowRect(_hWnd, out RECT rect))
                {
                    return new Rectangle(rect.Left, rect.Top, Math.Max(1, rect.Width), Math.Max(1, rect.Height));
                }

                return Rectangle.Empty;
            }
        }

        public bool IsValid => IsWindow(_hWnd) && !IsIconic(_hWnd);

        public Bitmap? CaptureFrame(bool captureCursor)
        {
            if (!IsValid) return null;

            var bounds = Bounds;
            if (bounds.Width <= 0 || bounds.Height <= 0) return null;

            var bmp = new Bitmap(bounds.Width, bounds.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                var hdc = g.GetHdc();
                try
                {
                    bool printed = PrintWindow(_hWnd, hdc, PW_RENDERFULLCONTENT);
                    if (!printed)
                    {
                        // Fallback to standard PrintWindow
                        PrintWindow(_hWnd, hdc, 0);
                    }
                }
                finally
                {
                    g.ReleaseHdc(hdc);
                }
            }

            return bmp;
        }

        public void Dispose()
        {
        }
    }
}
