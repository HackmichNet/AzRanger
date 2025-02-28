using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzSQLDatabaseAuditRetention : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.Resources.SQLServers == null)
                {
                    this.SetReason("You do not have SQLServers or the user cannot access them.");
                    return CheckResult.NotApplicable;
                }
                foreach (SQLServer server in sub.Resources.SQLServers)
                {
                    foreach (SQLDatabase sQLDatabase in server.SQLDatabases)
                    {
                        if (sQLDatabase.auditingSettings.properties.state != "Disabled")
                        {
                            if (sQLDatabase.auditingSettings.properties.storageAccountSubscriptionId.Length > 0 & !sQLDatabase.auditingSettings.properties.storageAccountSubscriptionId.Equals("00000000-0000-0000-0000-000000000000"))
                            {
                                if (sQLDatabase.auditingSettings.properties.retentionDays < 90 & sQLDatabase.auditingSettings.properties.retentionDays > 0)
                                {
                                    passed = false;
                                    this.AddAffectedEntity(sQLDatabase);
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
