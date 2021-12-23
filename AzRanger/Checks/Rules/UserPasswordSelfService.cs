using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserPasswordSelfService", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Properties")]
    [RuleScore("Password Self-Service is disabled", "Allowing to use a secure password reset mechanism reduces the amount of calls in your self service.", 1, "https://docs.microsoft.com/en-us/azure/active-directory/authentication/tutorial-enable-sspr")]
    class UserPasswordSelfService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.enablementType != 2)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}
