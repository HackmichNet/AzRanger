using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    // TODO
    internal class AzKeyVKeyExpirationRBAC : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                foreach (KeyVault vault in sub.Resources.KeyVaults)
                {
                    if (vault.properties.enableRbacAuthorization)
                    {
                        foreach (KeyVaultKey key in vault.Keys)
                        {
                            if (key.attributes.exp == null)
                            {
                                passed = false;
                                this.AddAffectedEntity(key);
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
