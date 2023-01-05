using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("CABlockLegacyAuth", ScopeEnum.AAD, MaturityLevel.Tentative, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ConditionalAccessBlade/Policies")]
    [CISM365("1.1.6", "", Level.L1, "v1.4")]
    [RuleInfo("Legacy Authentication methods is not blocked by Conditional Access", "This can expose your tenant to password brute force or password spraying attacks.", 7, "Legacy authentication does not support techniques like MFA.")]
    class CABlockLegacyAuth : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
            {
                this.SetReason("Security Defaults are enabled.");
                return CheckResult.NotApplicable;
            }
            foreach(ConditionalAccessPolicy policy in tenant.AllCAPolicies.Values)
            {
                if (policy.state == "enabled")
                {
                    bool exchangeActiveSyncMissing = true;
                    bool otherMissing = true;
                    if (policy.conditions.clientAppTypes != null)
                    {
                        foreach (string clientApp in policy.conditions.clientAppTypes)
                        {
                            if (clientApp == "exchangeActiveSync")
                            {
                                exchangeActiveSyncMissing = false;
                            }
                            if (clientApp == "other")
                            {
                                otherMissing = false;
                            }
                        }
                    }
                    if (!(exchangeActiveSyncMissing | otherMissing ))
                    {
                        if (policy.grantControls != null && policy.grantControls.builtInControls[0] == "block")
                        {
                            return CheckResult.NoFinding;
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
