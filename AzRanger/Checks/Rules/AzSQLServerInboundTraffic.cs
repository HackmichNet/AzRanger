using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSQLServerInboundTraffic", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fservers", ServiceEnum.SQLServer)]
    [CISAZ("4.1.2", "", CISLevel.L1, "v2.0")]
    
    internal class AzSQLServerInboundTraffic : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if(sub.Resources.SQLServers == null)
                {
                    this.SetReason("You do not have SQLServers or the user cannot access them.");
                    return CheckResult.NotApplicable;
                }
                foreach(SQLServer server in sub.Resources.SQLServers)
                {
                    if(server.properties.publicNetworkAccess == "Enabled")
                    {
                        if (server.firewallRules != null)
                        {
                            foreach (SQLServerFirewallRules rules in server.firewallRules)
                            {
                                if (rules.properties.startIpAddress == "0.0.0.0" && rules.properties.endIpAddress != "0.0.0.0")
                                {
                                    passed = false;
                                    this.AddAffectedEntity(server);
                                }
                            }
                        }
                    }
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
