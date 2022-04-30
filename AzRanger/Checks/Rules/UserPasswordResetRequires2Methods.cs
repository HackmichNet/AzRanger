using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserPasswordResetRequires2Methods", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/AuthenticationMethods")]
    [RuleScore("Password Self-Service does not require two methods for reset", "Requring two methods for password reset increases the security", 1)]
    class UserPasswordResetRequires2Methods : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.enablementType == 0)
            {
                this.SetReason("Password Reset is not configured");
                return CheckResult.NotApplicable;
            }
            if (tenant.PasswordResetPolicies.numberOfAuthenticationMethodsRequired != 2)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}