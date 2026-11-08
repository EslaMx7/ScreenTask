using System;
using System.Xml.Serialization;

namespace ScreenTask
{
    [Serializable]
    public class AppSettings
    {
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 7070;
        public int TargetFps { get; set; } = 30;
        public int ImageQuality { get; set; } = 80;
        public bool IsPrivateSession { get; set; } = false;
        public string PinCode { get; set; } = "";
        public bool IsShowMouseEnabled { get; set; } = true;
        public bool IsAudioEnabled { get; set; } = false;
        public string CaptureMode { get; set; } = "Screen"; // "Screen" or "Window"
        public int SelectedScreenIndex { get; set; } = 0;
        public long SelectedWindowHandle { get; set; } = 0;
        public bool IsAutoStartServerEnabled { get; set; } = false;
        public bool IsStartMinimizedEnabled { get; set; } = false;
        public bool IsCloseToTrayEnabled { get; set; } = true;
        public string SelectedAudioDeviceId { get; set; } = "";
        public bool DarkMode { get; set; } = true;

        [XmlIgnore]
        public bool IsMinimizeToTrayEnabled
        {
            get => IsCloseToTrayEnabled;
            set => IsCloseToTrayEnabled = value;
        }

        // Legacy compatibility properties
        public int ScreenshotsSpeed
        {
            get => TargetFps > 0 ? 1000 / TargetFps : 33;
            set => TargetFps = value > 0 ? Math.Clamp(1000 / value, 1, 60) : 30;
        }

        public string Username
        {
            get => "admin";
            set { }
        }

        public string Password
        {
            get => PinCode;
            set => PinCode = value;
        }

        public bool AllowPublicAccess { get; set; } = true;
    }
}
