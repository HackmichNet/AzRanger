using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("CABlockLegacyAuth", Scope.O365, MaturityLevel.Tentative, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ConditionalAccessBlade/Policies")]
    [RuleScore("No Conditional Access Policy blocking legacy authentication was detected (Might be false positiv)", "Legacy Authentication does not support features like MFA", 7)]
    class CABlockLegacyAuth : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SecurityDefaults.securityDefaultsEnabled == true)
            {
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
                            return CheckResult.Passed;
                        }
                    }
                }
            }
            return CheckResult.Failed;
        }
    }
}
