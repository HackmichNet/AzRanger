using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzStorAcKeyRotationReminder : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                foreach (StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if (account.properties.encryption.keySource == "Microsoft.Storage")
                    {
                        passed = false;
                        this.AddAffectedEntity(account);
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
