using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ScreenTask
{
    public partial class frmMain : Form
    {
        private bool isWorking;

        private object locker = new object();
        private ReaderWriterLock rwl = new ReaderWriterLock();
        private MemoryStream img;
        private List<Tuple<string, string>> _ips;
        HttpListener serv;
        private Version currentVersion;
        private AppSettings _currentSettings = new AppSettings();
        public frmMain()
        {
            InitializeComponent();
            currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            CheckForIllegalCrossThreadCalls = false; // For Visual Studio Debuging Only !
            serv = new HttpListener();
            serv.IgnoreWriteExceptions = true; // Seems Had No Effect :(
            img = new MemoryStream();

            foreach (var screen in Screen.AllScreens)
            {
                comboScreens.Items.Add(screen.DeviceName.Replace("\\", "").Replace(".", ""));
            }
            comboScreens.SelectedIndex = 0;
            this.Text = $"Screen Task v{currentVersion.Major}.{currentVersion.Minor}";
        }

        private async void btnStartServer_Click(object sender, EventArgs e)
        {

            if (btnStartServer.Tag.ToString() != "start")
            {
                btnStartServer.Tag = "start";
                btnStartServer.Text = "Start Server";
                isWorking = false;
                if (serv.IsListening)
                {
                    serv.Stop();
                    serv.Close();
                }
                Log("Server Stoped.");
                appNotify.ShowBalloonTip(1_000, "ScreenTask", "Server Stoped.", ToolTipIcon.Info);

                return;
            }

            try
            {

                serv = new HttpListener();
                serv.IgnoreWriteExceptions = true;
                isWorking = true;
                Log("Starting Server, Please Wait...");
                await AddFirewallRule((int)numPort.Value);
                _ = Task.Factory.StartNew(() => CaptureScreenEvery((int)numShotEvery.Value), TaskCreationOptions.LongRunning);
                btnStartServer.Tag = "stop";
                btnStartServer.Text = "Stop Server";
                await StartServer();

            }
            catch (ObjectDisposedException disObj)
            {
                serv = new HttpListener();
                serv.IgnoreWriteExceptions = true;
            }
            catch (HttpListenerException httpEx)
            {
                if (httpEx.ErrorCode == 32) // Port Already Used
                {
                    btnStartServer.Tag = "start";
                    btnStartServer.Text = "Start Server";
                    isWorking = false;
                    Log($"This port {numPort.Value} is already used");
                    var msgResult = MessageBox.Show($"This port {numPort.Value} is already used, Do you want to use another random one ?", "Port Already Used !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (msgResult == DialogResult.Yes)
                    {
                        numPort.Value += DateTime.Now.Second;
                        Log($"New port is {numPort.Value}");
                        btnStartServer_Click(sender, e);
                    }
                    else
                    {

                        Log("Port Change Request Declined");
                    }

                }
                else if (httpEx.ErrorCode == 183)
                {
                    MessageBox.Show(httpEx.Message);
                }

            }
            catch (Exception ex)
            {
                Log("Error! : " + ex.Message);
            }
        }
        private async Task StartServer()
        {
            //serv = serv??new HttpListener();
            var selectedIP = _ips.ElementAt(comboIPs.SelectedIndex).Item2;

            var url = string.Format("http://{0}:{1}", selectedIP, numPort.Value.ToString());
            txtURL.Text = url;
            serv.Prefixes.Clear();
            serv.Prefixes.Add("http://localhost:" + numPort.Value.ToString() + "/");
            //serv.Prefixes.Add("http://*:" + numPort.Value.ToString() + "/"); // Uncomment this to Allow Public IP Over Internet. [Commented for Security Reasons.]
            serv.Prefixes.Add(url + "/");
            serv.Start();
            Log("Server Started Successfuly!");
            appNotify.ShowBalloonTip(1_000, "ScreenTask", $"Server Started Successfuly!\r\n{url}", ToolTipIcon.Info);

            Log("Network URL : " + url);
            Log("Localhost URL : " + "http://localhost:" + numPort.Value.ToString() + "/");
            while (isWorking)
            {
                var ctx = await serv.GetContextAsync();
                //Screenshot();
                var resPath = ctx.Request.Url.LocalPath;
                if (resPath == "/") // Route The Root Dir to the Index Page
                    resPath += "index.html";
                var page = Application.StartupPath + "/WebServer" + resPath;
                bool fileExist;
                lock (locker)
                    fileExist = File.Exists(page);
                if (!fileExist)
                {
                    var errorPage = Encoding.UTF8.GetBytes("<h1 style=\"color:red\">Error 404 , File Not Found </h1><hr><a href=\".\\\">Back to Home</a>");
                    ctx.Response.ContentType = "text/html";
                    ctx.Response.StatusCode = 404;
                    try
                    {
                        await ctx.Response.OutputStream.WriteAsync(errorPage, 0, errorPage.Length);
                    }
                    catch (Exception ex)
                    {


                    }
                    ctx.Response.Close();
                    continue;
                }


                if (_currentSettings.IsPrivateSession)
                {
                    if (!ctx.Request.Headers.AllKeys.Contains("Authorization"))
                    {
                        ctx.Response.StatusCode = 401;
                        ctx.Response.AddHeader("WWW-Authenticate", "Basic realm=Screen Task Authentication : ");
                        ctx.Response.Close();
                        continue;
                    }
                    else
                    {
                        var auth1 = ctx.Request.Headers["Authorization"];
                        auth1 = auth1.Remove(0, 6); // Remove "Basic " From The Header Value
                        auth1 = Encoding.UTF8.GetString(Convert.FromBase64String(auth1));
                        var auth2 = string.Format("{0}:{1}", txtUser.Text, txtPassword.Text);
                        if (auth1 != auth2)
                        {
                            // MessageBox.Show(auth1+"\r\n"+auth2);
                            Log(string.Format("Bad Login from {0} using {1}", ctx.Request.RemoteEndPoint.Address.ToString(), auth1));
                            var errorPage = Encoding.UTF8.GetBytes("<h1 style=\"color:red\">Not Authorized !!! </h1><hr><a href=\"./\">Back to Home</a>");
                            ctx.Response.ContentType = "text/html";
                            ctx.Response.StatusCode = 401;
                            ctx.Response.AddHeader("WWW-Authenticate", "Basic realm=Screen Task Authentication : ");
                            try
                            {
                                await ctx.Response.OutputStream.WriteAsync(errorPage, 0, errorPage.Length);
                            }
                            catch (Exception ex)
                            {


                            }
                            ctx.Response.Close();
                            continue;
                        }

                    }
                }

                //Everything OK! ??? Then Read The File From HDD as Bytes and Send it to the Client 
                byte[] filedata;

                // Required for One-Time Access of the file {Reader\Writer Problem in OS}
                rwl.AcquireReaderLock(Timeout.Infinite);
                filedata = File.ReadAllBytes(page);
                rwl.ReleaseReaderLock();

                var fileinfo = new FileInfo(page);
                if (fileinfo.Extension == ".css") // important for IE -> Content-Type must be defiend for CSS files unless will ignored !!!
                    ctx.Response.ContentType = "text/css";
                else if (fileinfo.Extension == ".html" || fileinfo.Extension == ".htm")
                    ctx.Response.ContentType = "text/html"; // Important For Chrome Otherwise will display the HTML as plain text.



                ctx.Response.StatusCode = 200;
                try
                {
                    await ctx.Response.OutputStream.WriteAsync(filedata, 0, filedata.Length);
                }
                catch (Exception ex)
                {

                    /*
                        Do Nothing !!! this is the Only Effective Solution for this Exception : 
                        the specified network name is no longer available
                        
                     */

                }

                ctx.Response.Close();
            }

        }
        private async Task CaptureScreenEvery(int msec)
        {
            while (isWorking)
            {
                TakeScreenshot(_currentSettings.IsShowMouseEnabled);
                msec = (int)numShotEvery.Value;
                await Task.Delay(msec);
            }
        }
        private void TakeScreenshot(bool captureMouse)
        {
            if (captureMouse)
            {
                var bmp = ScreenCapturePInvoke.CaptureSelectedScreen(true, comboScreens.SelectedIndex);
                rwl.AcquireWriterLock(Timeout.Infinite);
                bmp.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                rwl.ReleaseWriterLock();

                bmp.Dispose();
                bmp = null;
                return;
            }
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                rwl.AcquireWriterLock(Timeout.Infinite);
                bitmap.Save(Application.StartupPath + "/WebServer" + "/ScreenTask.jpg", ImageFormat.Jpeg);
                rwl.ReleaseWriterLock();


            }
        }
        private string GetIPv4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
        private List<Tuple<string, string>> GetAllIPv4Addresses()
        {
            List<Tuple<string, string>> ipList = new List<Tuple<string, string>>();
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {

                foreach (var ua in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ua.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipList.Add(Tuple.Create(ni.Name, ua.Address.ToString()));
                    }
                }
            }
            return ipList;
        }
        private async Task AddFirewallRule(int port)
        {
            await Task.Run(() =>
            {

                string cmd = RunCMD("netsh advfirewall firewall show rule \"Screen Task\"");
                if (!cmd.Contains("Screen Task"))
                {
                    cmd = RunCMD("netsh advfirewall firewall add rule name=\"Screen Task\" dir=in action=allow remoteip=localsubnet protocol=tcp localport=" + port);
                    cmd = RunCMD("netsh advfirewall firewall show rule \"Screen Task\"");
                    if (cmd.Contains("Screen Task"))
                    {
                        Log("Screen Task Rule added to your firewall");
                    }
                }
                else
                {
                    cmd = RunCMD("netsh advfirewall firewall delete rule name=\"Screen Task\"");
                    cmd = RunCMD("netsh advfirewall firewall add rule name=\"Screen Task\" dir=in action=allow remoteip=localsubnet protocol=tcp localport=" + port);
                    cmd = RunCMD("netsh advfirewall firewall show rule \"Screen Task\"");
                    if (cmd.Contains("Screen Task"))
                    {
                        Log("Screen Task Rule updated to your firewall");
                    }
                }
            });

        }
        private string RunCMD(string cmd)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = "/C " + cmd;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.Start();
            string res = proc.StandardOutput.ReadToEnd();
            proc.StandardOutput.Close();

            proc.Close();
            return res;
        }
        private void Log(string text)
        {
            txtLog.Text += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " : " + text + "\r\n";

        }

        private void cbPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPrivate.Checked == true)
            {
                txtUser.Enabled = true;
                txtPassword.Enabled = true;
                _currentSettings.IsPrivateSession = true;
            }
            else
            {
                txtUser.Enabled = false;
                txtPassword.Enabled = false;
                _currentSettings.IsPrivateSession = false;
            }
        }

        private void cbCaptureMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCaptureMouse.Checked)
            {
                _currentSettings.IsShowMouseEnabled = true;
            }
            else
            {
                _currentSettings.IsShowMouseEnabled = false;
            }
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _ips = GetAllIPv4Addresses();
            foreach (var ip in _ips)
            {
                comboIPs.Items.Add(ip.Item2 + " - " + ip.Item1);
            }
            comboIPs.SelectedIndex = comboIPs.Items.Count - 1;
            var recommendedIP = _ips.FirstOrDefault(ip => ip.Item2.StartsWith("192.") || ip.Item2.StartsWith("10."));
            if (recommendedIP != null)
            {
                this.comboIPs.SelectedIndex = _ips.IndexOf(recommendedIP);
            }

            try
            {
                if (File.Exists("appsettings.xml"))
                {
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    using (var appSettingsFile = File.OpenRead("appsettings.xml"))
                    {
                        _currentSettings = (AppSettings)serializer.Deserialize(appSettingsFile);
                    }

                    this.cbPrivate.Checked = _currentSettings.IsPrivateSession;
                    this.cbCaptureMouse.Checked = _currentSettings.IsShowMouseEnabled;
                    this.cbAutoStart.Checked = _currentSettings.IsAutoStartServerEnabled;
                    this.cbMiniStart.Checked = _currentSettings.IsStartMinimizedEnabled;
                    this.txtUser.Text = _currentSettings.Username;
                    this.txtPassword.Text = _currentSettings.Password;
                    this.numPort.Value = _currentSettings.Port;
                    this.numShotEvery.Value = _currentSettings.ScreenshotsSpeed;
                    this.comboIPs.SelectedIndex = _ips.IndexOf(_ips.FirstOrDefault(ip => ip.Item2.Contains(_currentSettings.IP)));
                    if (_currentSettings.SelectedScreenIndex > -1 && comboScreens.Items.Count > 0 && _currentSettings.SelectedScreenIndex <= comboScreens.Items.Count - 1)
                        this.comboScreens.SelectedIndex = _currentSettings.SelectedScreenIndex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load local appsettings.xml file.\r\n{ex.Message}", "ScreenTask", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (_currentSettings.IsStartMinimizedEnabled)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            if (_currentSettings.IsAutoStartServerEnabled)
            {
                btnStartServer_Click(null, null);
            }

            try
            {
                Task.Factory.StartNew(() =>
            {
                var wc = new WebClient();
                wc.Headers.Add(HttpRequestHeader.UserAgent, "ScreenTask");
                var latestGithubReleaseText = wc.DownloadString("https://api.github.com/repos/EslaMx7/ScreenTask/releases/latest");
                var regex = Regex.Match(latestGithubReleaseText, @"\""tag_name\"":\""(.{2,5})\"",", RegexOptions.Multiline);
                if (regex.Success && !string.IsNullOrEmpty(regex.Value) && regex.Groups.Count > 1)
                {
                    var groupMatch = regex.Groups[1];
                    var newVersion = new Version(groupMatch.Value.Replace("v", ""));

                    if (newVersion > currentVersion)
                    {
                        var msgResult = MessageBox.Show($"There is a new update available for download.\nCurrent Version: {currentVersion}\nLatest Version: {newVersion}\nDo you want to download it now ?", "New Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (msgResult == DialogResult.Yes)
                        {
                            Process.Start("https://screentask.me");
                        }
                    }
                }
            });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to check for updates.\r\n{ex.Message}", "ScreenTask", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void lblMe_Click(object sender, EventArgs e)
        {
            Process.Start("https://screentask.me");
            Process.Start("http://twitter.com/EslaMx7");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _currentSettings.Port = (int)numPort.Value;
                _currentSettings.Username = txtUser.Text;
                _currentSettings.Password = txtPassword.Text;
                _currentSettings.IsShowMouseEnabled = cbCaptureMouse.Checked;
                _currentSettings.IsPrivateSession = cbPrivate.Checked;
                _currentSettings.IsAutoStartServerEnabled = cbAutoStart.Checked;
                _currentSettings.IsStartMinimizedEnabled = cbMiniStart.Checked;
                _currentSettings.ScreenshotsSpeed = (int)numShotEvery.Value;
                _currentSettings.IP = _ips.ElementAt(comboIPs.SelectedIndex).Item2;
                _currentSettings.SelectedScreenIndex = comboScreens.SelectedIndex;

                using (var appSettingsFile = new FileStream("appsettings.xml", FileMode.Create, FileAccess.Write))
                {
                    var serializer = new XmlSerializer(typeof(AppSettings));
                    serializer.Serialize(appSettingsFile, _currentSettings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot save the settings file next to the executable file.\r\n{ex.Message}", "ScreenTask", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (txtURL.Text.StartsWith("http"))
                Process.Start(txtURL.Text);

        }

        private void appNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Focus();
            this.ShowInTaskbar = true;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                appNotify.Visible = true;
                appNotify.ShowBalloonTip(1_000, "ScreenTask", "Running in the background", ToolTipIcon.Info);
            }
        }

        private void appNotify_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Focus();
        }

    }
}
