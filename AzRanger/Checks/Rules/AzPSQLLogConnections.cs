using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzPSQLLogConnections", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.DBforPostgreSQL%2Fservers", ServiceEnum.PSQLServer)]
    [RuleInfo("Log Connection are disabled", @"You might miss problems and errors in PSQLQueries.", 1, null, null, @"For each PSQLServer go to Settings->Server parameters and search for ""log_connections"". Set the value to ""On"".")]
    [CISAZ("4.3.3", "", Level.L1, "v2.0")]
    internal class AzPSQLLogConnections : BaseCheck
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
                        if (param.name == "log_connections")
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
