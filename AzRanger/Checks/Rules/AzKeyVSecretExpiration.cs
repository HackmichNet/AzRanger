using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzKeyVSecretExpiration", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", Service.KeyVault)]
    [RuleScore("This secret will never expire", "The secret can be used until it is manully deactivated", 1)]
    internal class AzKeyVSecretExpiration : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(KeyVault vault in sub.Resources.KeyVaults)
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
            
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
