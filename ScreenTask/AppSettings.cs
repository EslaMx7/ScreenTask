using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenTask
{
    [Serializable]
    public class AppSettings
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public int ScreenshotsSpeed { get; set; }
        public bool IsPrivateSession { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int SelectedScreenIndex { get; set; }
        public bool IsShowMouseEnabled { get; set; }
        public bool IsAutoStartServerEnabled { get; set; }
        public bool IsStartMinimizedEnabled { get; set; }
    }
}
