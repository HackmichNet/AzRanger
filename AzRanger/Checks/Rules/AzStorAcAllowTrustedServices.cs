using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzStorAcAllowTrustedServices : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                foreach (StorageAccount account in sub.Resources.StorageAccounts)
                {
                    if (account.properties.networkAcls.defaultAction == "Deny")
                    {
                        if (account.properties.networkAcls.bypass != "AzureServices")
                        {
                            passed = false;
                            this.AddAffectedEntity(account);
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
