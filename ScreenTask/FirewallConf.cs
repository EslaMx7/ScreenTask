using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetFwTypeLib;

namespace ScreenTask
{
    class FirewallConf
    {
        public void AddRule(String name, String Description,
           NET_FW_ACTION_ Action, NET_FW_RULE_DIRECTION_ Direction, String LocalPort,
           bool Enabled = true, int Protocole = 6, String RemoteAdresses = "localsubnet", String ApplicationName = "ScreenTask")
        {
            Type Policy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", false);
            INetFwPolicy2 FwPolicy = (INetFwPolicy2)Activator.CreateInstance(Policy2);
            INetFwRules rules = FwPolicy.Rules;
            //Delete if exist to avoid deplicated rules
            DeleteRule(name);
            Type RuleType = Type.GetTypeFromProgID("HNetCfg.FWRule");
            INetFwRule rule = (INetFwRule)Activator.CreateInstance(RuleType);

            rule.Name = name;
            rule.Description = Description;
            rule.Protocol = Protocole;// TCP/IP
            rule.LocalPorts = LocalPort;
            rule.RemoteAddresses = RemoteAdresses;
            rule.Action = Action;
            rule.Direction = Direction;
            rule.ApplicationName = ApplicationName;
            rule.Enabled = true;
            //Add Rule
            rules.Add(rule);
        }
        public void DeleteRule(String RuleName)
        {
            Type Policy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", false);
            INetFwPolicy2 FwPolicy = (INetFwPolicy2)Activator.CreateInstance(Policy2);
            INetFwRules rules = FwPolicy.Rules;

            rules.Remove(RuleName);
        }
    }
}
