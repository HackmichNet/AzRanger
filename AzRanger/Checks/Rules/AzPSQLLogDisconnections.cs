using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzPSQLLogDisconnections", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.DBforPostgreSQL%2Fservers", ServiceEnum.PSQLServer)]
    [RuleInfo("AzPSQLLogDisconnections")]
    [CISAZ("4.3.4", "", Level.L1, "v2.0")]
    internal class AzPSQLLogDisconnections : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if(sub.Resources.PostgreSQLs == null)
                {
                    SetReason("You do not have SQLServer or the user cannot see them.");
                    return CheckResult.NotApplicable;
                }
                foreach(PostgreSQLFlexibleServers server in sub.Resources.PostgreSQLs)
                {
                    foreach (PostgreSQLFlexibleServersParameters param in server.Paramters)
                    {
                        if (param.name == "log_disconnections")
                        {
                            if (param.properties.value == "off")
                            {
                                passed = false;
                                this.AddAffectedEntity(server);
                                break;
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
