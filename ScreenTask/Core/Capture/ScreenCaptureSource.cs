using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ScreenTask.Core.Capture
{
    public class ScreenCaptureSource : ICaptureSource
    {
        private readonly int _screenIndex;

        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        private const int CURSOR_SHOWING = 0x0001;
        private const int DI_NORMAL = 0x0003;

        public ScreenCaptureSource(int screenIndex = 0)
        {
            _screenIndex = screenIndex;
        }

        public string Id => $"Screen_{_screenIndex}";
        public int ScreenIndex => _screenIndex;

        public bool IsPrimary
        {
            get
            {
                var screens = Screen.AllScreens;
                return _screenIndex >= 0 && _screenIndex < screens.Length && screens[_screenIndex].Primary;
            }
        }

        public string DisplayName
        {
            get
            {
                var screens = Screen.AllScreens;
                if (_screenIndex >= 0 && _screenIndex < screens.Length)
                {
                    var s = screens[_screenIndex];
                    return $"Display {_screenIndex + 1}: {s.Bounds.Width}x{s.Bounds.Height} {(s.Primary ? "(Primary)" : "")}".Trim();
                }
                return "Primary Display";
            }
        }

        public Rectangle Bounds
        {
            get
            {
                var screens = Screen.AllScreens;
                if (_screenIndex >= 0 && _screenIndex < screens.Length)
                {
                    return screens[_screenIndex].Bounds;
                }
                return Screen.PrimaryScreen?.Bounds ?? new Rectangle(0, 0, 1920, 1080);
            }
        }

        public bool IsValid => true;

        public Bitmap? CaptureFrame(bool captureCursor)
        {
            var bounds = Bounds;
            if (bounds.Width <= 0 || bounds.Height <= 0)
                return null;

            try
            {
                var bitmap = new Bitmap(bounds.Width, bounds.Height);
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

                    if (captureCursor)
                    {
                        DrawCursor(g, bounds);
                    }
                }
                return bitmap;
            }
            catch (Exception)
            {
                // Happens when desktop session is locked (Win+L) or screen saver / UAC desktop
                return CreateSessionLockedBitmap(bounds.Width, bounds.Height);
            }
        }

        private static void DrawCursor(Graphics g, Rectangle bounds)
        {
            try
            {
                var pci = new CURSORINFO { cbSize = Marshal.SizeOf<CURSORINFO>() };
                if (GetCursorInfo(out pci) && pci.flags == CURSOR_SHOWING && pci.hCursor != IntPtr.Zero)
                {
                    var hdc = g.GetHdc();
                    try
                    {
                        DrawIconEx(hdc, pci.ptScreenPos.x - bounds.X, pci.ptScreenPos.y - bounds.Y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                    }
                    finally
                    {
                        g.ReleaseHdc();
                    }
                }
            }
            catch
            {
                // Silently ignore cursor render errors
            }
        }

        private static Bitmap CreateSessionLockedBitmap(int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(20, 24, 39)); // Slate dark background
                using var brush = new SolidBrush(Color.White);
                using var font = new Font("Segoe UI", 18, FontStyle.Bold);
                var text = "🔒 Host Screen Locked (Stream Paused)";
                var size = g.MeasureString(text, font);
                g.DrawString(text, font, brush, (width - size.Width) / 2, (height - size.Height) / 2);
            }
            return bmp;
        }

        public void Dispose()
        {
            // Nothing unmanaged to dispose
        }
    }
}
