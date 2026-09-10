using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScreenTask.Core.Network
{
    public static class FirewallHelper
    {
        public static async Task<bool> RuleExistsAsync(int port)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "netsh",
                        Arguments = $"advfirewall firewall show rule name=\"Screen Task On Port {port}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    };
                    using var proc = Process.Start(psi);
                    if (proc != null)
                    {
                        var output = proc.StandardOutput.ReadToEnd();
                        proc.WaitForExit();
                        return output.Contains($"Screen Task On Port {port}");
                    }
                }
                catch { }
                return false;
            });
        }

        public static async Task<bool> TryAddRuleAsync(int port)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var ruleName = $"Screen Task On Port {port}";
                    var psi = new ProcessStartInfo
                    {
                        FileName = "netsh",
                        Arguments = $"advfirewall firewall add rule name=\"{ruleName}\" dir=in action=allow protocol=tcp localport={port}",
                        CreateNoWindow = true,
                        UseShellExecute = true,
                        Verb = "runas" // Elevate only when explicitly needed
                    };
                    using var proc = Process.Start(psi);
                    proc?.WaitForExit();
                    return proc?.ExitCode == 0;
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}
