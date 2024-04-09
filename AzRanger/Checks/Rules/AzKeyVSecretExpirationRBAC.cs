using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    // TODO
    internal class AzKeyVSecretExpirationRBAC : BaseCheck
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
                        foreach (KeyVaultSecret secret in vault.Secrets)
                        {
                            if (secret.attributes.exp == null)
                            {
                                passed = false;
                                this.AddAffectedEntity(secret);
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
