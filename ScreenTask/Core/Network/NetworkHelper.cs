using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ScreenTask.Core.Network
{
    public record NetworkAdapterInfo(string InterfaceName, string IpAddress, NetworkInterfaceType Type, bool IsRecommended)
    {
        public string DisplayName => $"{InterfaceName} - {IpAddress}{(IsRecommended ? " (Recommended)" : "")}";
        public override string ToString() => DisplayName;
    }

    public static class NetworkHelper
    {
        public static List<NetworkAdapterInfo> GetAvailableAdapters()
        {
            var list = new List<NetworkAdapterInfo>();

            try
            {
                var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up &&
                                 ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                                 ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                                 !ni.Description.Contains("Virtual", StringComparison.OrdinalIgnoreCase) &&
                                 !ni.Description.Contains("WSL", StringComparison.OrdinalIgnoreCase) &&
                                 !ni.Description.Contains("Hyper-V", StringComparison.OrdinalIgnoreCase) &&
                                 !ni.Description.Contains("Bluetooth", StringComparison.OrdinalIgnoreCase));

                foreach (var ni in interfaces)
                {
                    var props = ni.GetIPProperties();
                    foreach (var addr in props.UnicastAddresses)
                    {
                        if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            var ip = addr.Address.ToString();
                            bool isRecommended = ip.StartsWith("192.168.") || ip.StartsWith("10.") || ip.StartsWith("172.");
                            list.Add(new NetworkAdapterInfo(ni.Name, ip, ni.NetworkInterfaceType, isRecommended));
                        }
                    }
                }
            }
            catch { }

            // If empty, add fallback localhost
            if (list.Count == 0)
            {
                list.Add(new NetworkAdapterInfo("Localhost", "127.0.0.1", NetworkInterfaceType.Loopback, true));
            }

            // Order so recommended IPs come first
            return list.OrderByDescending(a => a.IsRecommended).ThenBy(a => a.InterfaceName).ToList();
        }
    }
}
