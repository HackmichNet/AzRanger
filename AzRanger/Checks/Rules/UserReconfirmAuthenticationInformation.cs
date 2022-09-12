﻿using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserReconfirmAuthenticationInformation", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Registration")]
    [CISAZ("1.6", "", Level.L1, "v1.4")]
    [RuleInfo("User must never reconfirm authentication information", "User must never update their authentication information. This increases the risk, that stolen information are valid forever.", 1)]
    class UserReconfirmAuthenticationInformation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.registrationReconfirmIntevalInDays == 0)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}