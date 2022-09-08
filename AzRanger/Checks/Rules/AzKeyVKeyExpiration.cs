using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzKeyVKeyExpiration", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", Service.KeyVault)]
    [CISAZ("8.1", "", Level.L1, "v1.4")]
    [RuleInfo("KeyVault key will never expire", "If the key is lost or stolen, the attacker can use the key as long as someone changes it. What can be a very long time.", 1, null, "You can set the expiration date either manually in the Portal (Portal Link) for each Key Vault or you can use the followin code: </br> az keyvault key set-attributes --name <keyName> --vault-name <vaultName> --expires Y - m - d'T'H: M:S'Z' ")]
    internal class AzKeyVKeyExpiration : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(KeyVault vault in sub.Resources.KeyVaults)
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
            
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
