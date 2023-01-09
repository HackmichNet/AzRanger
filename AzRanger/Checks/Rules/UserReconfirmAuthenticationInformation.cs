using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserReconfirmAuthenticationInformation", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Registration")]
    [CISAZ("1.8", "", Level.L1, "v1.5")]
    [RuleInfo("User must never reconfirm authentication information", "User must never update their authentication information. This increases the risk, that stolen information are valid forever.", 1)]
    class UserReconfirmAuthenticationInformation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.registrationReconfirmIntevalInDays == 0)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
