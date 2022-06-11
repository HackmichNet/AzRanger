using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserNotifyUserOnPasswordReset", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Notifications")]
    [RuleScore("Users are not notified when their password is changed", "A malicious password reset might stay unoticed", 1)]
    class UserNotifyUserOnPasswordReset : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.notifyUsersOnPasswordReset == false)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}
