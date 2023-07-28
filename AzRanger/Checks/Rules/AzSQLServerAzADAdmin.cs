using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSQLServerAzADAdmin", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fservers", ServiceEnum.SQLServer)]
    [CISAZ("4.1.4", "", Level.L1, "v2.0")]
    [RuleInfo("Azure AD Authentication is not enabled for SQL-Server", "This could be an addtional risk to the SQL Server. It eases attacks like password bruteforce.", 1, null, null, @"For each SQL-Server go to ""Settings"" and ""Azure Active Directory"". Here click on ""Set Admin"" and choose an admin.")]
    internal class AzSQLServerAzADAdmin : BaseCheck
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
                    if(server.SQLAdministrators.Count == 0)
                    {
                        passed = false;
                        AddAffectedEntity(server);
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
