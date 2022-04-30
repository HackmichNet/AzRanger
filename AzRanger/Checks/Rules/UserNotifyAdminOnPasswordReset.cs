using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserNotifyAdminOnPasswordReset", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Notifications")]
    [RuleScore("Admins are not notified when their password is changed", "A malicious password reset might stay unoticed", 1)]
    class UserNotifyAdminOnPasswordReset : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.notifyOnAdminPasswordReset == false)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}
