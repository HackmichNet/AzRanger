using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("ADPasswordProtection", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/PasswordProtection")]
    [RuleScore("Azure Active Directory Password Protection is not enabled", "Azure Active Directory Password Protection protects on-premise user using weak passwords", 3)]
    class ADPasswordProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordPolicy.bannedPasswordCheckOnPremisesMode == 1)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
