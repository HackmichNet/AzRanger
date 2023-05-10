using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSQLAuditingRetention", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fservers", ServiceEnum.SQLServer)]
    [CISAZ("4.1.6", "", Level.L1, "v1.5")]
    [RuleInfo("SQL Server Auditretention is smaller than 90 days", "In case of an incident audit logs might help to investigate it. If the retention time is too short some data might be missing.", 1, null, null, @"In the Azure Portal go to ""SQL Servers"" and click on ""Auditing"" on the left menu under ""Security"" for each instance. Under ""Stoarge"" expand ""Advanced properties"" and seht ""Retention (days)"" to higher than 90 or to 0 for unlimited retention.")]
    internal class AzSQLAuditingRetention : BaseCheck
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
                    if (server.auditingSettings.properties.state != "Disabled")
                    {
                        if(server.auditingSettings.properties.storageEndpoint.Length > 0)
                        {
                            if(server.auditingSettings.properties.retentionDays > 0 && server.auditingSettings.properties.retentionDays <= 90)
                            {
                                passed = false;
                                this.AddAffectedEntity(server);
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
