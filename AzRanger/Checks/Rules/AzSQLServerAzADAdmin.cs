using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzSQLServerAzADAdmin : BaseCheck
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
                    if (server.SQLAdministrators.Count == 0)
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
