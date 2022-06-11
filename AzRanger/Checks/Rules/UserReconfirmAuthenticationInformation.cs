using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserReconfirmAuthenticationInformation", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Registration")]
    [RuleScore("User must never reconfirm authentication information", "If this is not enabled, it might happen that user authentication information are never updated", 1)]
    class UserReconfirmAuthenticationInformation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.registrationReconfirmIntevalInDays == 0)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}
