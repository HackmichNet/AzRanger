using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzKeyVSecretExpirationRBAC", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", ServiceEnum.KeyVault)]
    [CISAZ("8.3", "", Level.L1, "v2.0")]
    internal class AzKeyVSecretExpirationRBAC : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(KeyVault vault in sub.Resources.KeyVaults)
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
