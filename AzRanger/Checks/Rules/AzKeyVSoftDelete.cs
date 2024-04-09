using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzKeyVSoftDelete : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                foreach (KeyVault vault in sub.Resources.KeyVaults)
                {

                    if (vault.properties.enablePurgeProtection & vault.properties.enableSoftDelete)
                    {
                        continue;
                    }
                    else
                    {
                        passed = false;
                        this.AddAffectedEntity(vault);
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
