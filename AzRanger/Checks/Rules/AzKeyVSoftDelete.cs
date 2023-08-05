using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzKeyVSoftDelete", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", ServiceEnum.KeyVault)]
    [CISAZ("8.5", "", Level.L1, "v2.0")]
    [RuleInfo("Key Vault without purge protection", "In case a user inadvertently deletes a Azure KeyVault Key, it is not possible to recover it.", 1, null, null, @"Go to each Azure KeyVaul and ensure that ""Soft delete has been enabled on this key vault"" is checked.")]
    internal class AzKeyVSoftDelete : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(KeyVault vault in sub.Resources.KeyVaults)
                {

                    if(vault.properties.enablePurgeProtection & vault.properties.enableSoftDelete)
                    {
                        continue;
                    }else
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
