using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzKeyVKeyExpiration", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", Service.KeyVault)]
    [RuleScore("This key will never expire", "The key can be used until it is manually deactivated.", 1)]
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
                        if(key.attributes.exp == null)
                        {
                            passed = false;
                            this.AddAffectedEntity(key);
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
