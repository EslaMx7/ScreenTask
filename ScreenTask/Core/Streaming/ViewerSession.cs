using System;
using System.Text.RegularExpressions;

namespace ScreenTask.Core.Streaming
{
    public class ViewerSession
    {
        public string Id { get; init; } = "";
        public string RemoteIp { get; set; } = "127.0.0.1";
        public string UserAgent { get; set; } = "";
        public string DeviceName { get; set; } = "Desktop PC";
        public string DeviceIcon { get; set; } = "💻";
        public string BrowserName { get; set; } = "Web Browser";
        public DateTime ConnectedAt { get; init; } = DateTime.UtcNow;
        public double RttMs { get; set; } = 0.0;
        public string ViewMode { get; set; } = "Fit to Screen (1.0x)";
        public bool IsAudioMuted { get; set; }
        public DateTime LastTelemetryTime { get; set; } = DateTime.UtcNow;

        public TimeSpan Uptime => DateTime.UtcNow - ConnectedAt;

        public string FormattedUptime
        {
            get
            {
                var span = Uptime;
                if (span.TotalHours >= 1)
                {
                    return $"{(int)span.TotalHours:D2}h {span.Minutes:D2}m {span.Seconds:D2}s";
                }
                return $"{span.Minutes:D2}m {span.Seconds:D2}s";
            }
        }
    }

    public static class UserAgentParser
    {
        public static (string DeviceName, string DeviceIcon, string BrowserName) Parse(string? userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent))
            {
                return ("Web Client", "🌐", "Standard Browser");
            }

            string device = "Desktop PC";
            string icon = "💻";

            // Device / OS detection
            if (userAgent.Contains("iPhone", StringComparison.OrdinalIgnoreCase))
            {
                device = "iPhone";
                icon = "📱";
            }
            else if (userAgent.Contains("iPad", StringComparison.OrdinalIgnoreCase))
            {
                device = "iPad";
                icon = "📱";
            }
            else if (userAgent.Contains("Android", StringComparison.OrdinalIgnoreCase))
            {
                if (userAgent.Contains("Mobile", StringComparison.OrdinalIgnoreCase))
                {
                    device = "Android Phone";
                    icon = "📱";
                }
                else
                {
                    device = "Android Tablet";
                    icon = "📱";
                }
            }
            else if (userAgent.Contains("Macintosh", StringComparison.OrdinalIgnoreCase) ||
                     userAgent.Contains("Mac OS X", StringComparison.OrdinalIgnoreCase))
            {
                device = "MacBook / macOS";
                icon = "💻";
            }
            else if (userAgent.Contains("Windows NT", StringComparison.OrdinalIgnoreCase))
            {
                device = "Windows PC";
                icon = "💻";
            }
            else if (userAgent.Contains("Linux", StringComparison.OrdinalIgnoreCase))
            {
                device = "Linux Workstation";
                icon = "💻";
            }

            // Browser detection
            string browser = "Web Browser";

            if (userAgent.Contains("Edg/", StringComparison.OrdinalIgnoreCase) ||
                userAgent.Contains("Edge/", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(userAgent, @"Edg(?:e)?\/([\d\.]+)");
                browser = match.Success ? $"Edge {match.Groups[1].Value}" : "Microsoft Edge";
            }
            else if (userAgent.Contains("Chrome/", StringComparison.OrdinalIgnoreCase) &&
                     !userAgent.Contains("Edg/", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(userAgent, @"Chrome\/([\d\.]+)");
                browser = match.Success ? $"Chrome {match.Groups[1].Value}" : "Google Chrome";
            }
            else if (userAgent.Contains("Firefox/", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(userAgent, @"Firefox\/([\d\.]+)");
                browser = match.Success ? $"Firefox {match.Groups[1].Value}" : "Mozilla Firefox";
            }
            else if (userAgent.Contains("Safari/", StringComparison.OrdinalIgnoreCase) &&
                     !userAgent.Contains("Chrome/", StringComparison.OrdinalIgnoreCase))
            {
                var match = Regex.Match(userAgent, @"Version\/([\d\.]+)");
                browser = match.Success ? (icon == "📱" ? $"Mobile Safari {match.Groups[1].Value}" : $"Safari {match.Groups[1].Value}") : "Apple Safari";
            }

            return (device, icon, browser);
        }
    }
}
