using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzSQLServerMinimumTLSVersion", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fservers", Service.SQLServer)]
    [RuleScore("These SQL Server allows connections with TLS 1.1 or TLS 1.0", "There exist some theoretical attacks on TLS 1.0 and TLS 1.1", 1)]
    internal class AzSQLServerMinimumTLSVersion : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if(sub.Resources.SQLServers == null)
                {
                    SetReason("You do not have SQLServer or the user cannot see them.");
                    return CheckResult.NotApplicable;
                }
                foreach(SQLServer server in sub.Resources.SQLServers)
                {
                    if(server.properties.minimalTlsVersion != null && (string)server.properties.minimalTlsVersion.ToString() != "1.2" ||
                        server.properties.minimalTlsVersion == null)
                    { 
                        passed = false;
                        this.AddAffectedEntity(server); 
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
