using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserReconfirmAuthenticationInformation", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/PasswordResetMenuBlade/~/Registration")]
    [CISAZ("1.8", "", Level.L1, "v2.0")]
    [RuleInfo("User must never reconfirm authentication information", "User must never update their authentication information. This increases the risk, that stolen information are valid forever.", 1, null, null, @"Go to Entra Portal -> Protection -> Password reset -> Registration and set the value for ""Number of days before users are asked to re-confirm their authentication information"" to a value that fits for you.")]
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
