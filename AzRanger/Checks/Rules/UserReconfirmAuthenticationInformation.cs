using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserReconfirmAuthenticationInformation", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/PasswordResetMenuBlade/~/Registration")]
    [CISAZ("1.8", "", Level.L1, "v2.0")]
    [RuleInfo("UserReconfirmAuthenticationInformation")]
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
