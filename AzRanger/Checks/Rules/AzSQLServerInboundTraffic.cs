using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzSQLServerInboundTraffic", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fservers", Service.SQLServer)]
    [RuleScore("Arbitrary ingress traffic is allowed to this SQL Server", "Currently everyone can connect to this SQL Server and bruteforce the login", 1)]
    internal class AzSQLServerInboundTraffic : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
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
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
