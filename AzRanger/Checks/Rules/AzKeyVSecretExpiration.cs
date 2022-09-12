﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzKeyVSecretExpiration", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.KeyVault%2Fvaults", Service.KeyVault)]
    [CISAZ("8.3", "", Level.L1, "v1.4")]
    [RuleInfo("Key Vaul secret will never expire", "If the secret is lost or stolen, the attacker can use the secret as long as someone changes it. What can be a very long time.", 1)]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}