using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzStorAcContainerPublicAccess : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                foreach (StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if (account.properties.allowBlobPublicAccess == true)
                    {
                        foreach (StorageContainer container in account.StorageContainers)
                        {
                            if (container.properties.publicAccess != "None")
                            {
                                passed = false;
                                this.AddAffectedEntity(container);
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
