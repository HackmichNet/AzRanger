using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzSQLDatabaseEncryption : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.Resources.SQLServers == null)
                {
                    SetReason("You do not have SQLServer or the user cannot see them.");
                    return CheckResult.NotApplicable;
                }
                foreach (SQLServer server in sub.Resources.SQLServers)
                {
                    foreach(SQLDatabase db in server.SQLDatabases)
                    {
                        // Check if the database is not master and the encryption is disabled
                        if (!db.id.ToLower().Contains("databases/master") && db.transparentDataEncryption.properties.state.ToLower().Equals("disabled"))
                        {
                            passed = false;
                            this.AddAffectedEntity(db);
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
