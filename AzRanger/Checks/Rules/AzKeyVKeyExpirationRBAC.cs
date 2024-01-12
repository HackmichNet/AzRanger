using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzKeyVKeyExpirationRBAC", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", ServiceEnum.KeyVault)]
    [CISAZ("8.1", "", CISLevel.L1, "v2.0")]
    // TODO
    internal class AzKeyVKeyExpirationRBAC : BaseCheck
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
