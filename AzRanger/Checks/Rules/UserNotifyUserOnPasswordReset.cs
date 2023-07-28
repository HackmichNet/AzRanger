using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserNotifyUserOnPasswordReset", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/PasswordResetMenuBlade/~/Notifications")]
    [CISAZ("1.9", "", Level.L1, "v2.0")]
    [RuleInfo("User are not notified when their password is changed", "A malicious password reset might stay unnoticed.", 1, null, null, @"Open the link under Portal URL and set ""Notify users on password resets?"" to Yes")]
    class UserNotifyUserOnPasswordReset : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.notifyUsersOnPasswordReset == false)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
