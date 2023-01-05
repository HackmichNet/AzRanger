using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserNotifyUserOnPasswordReset", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Notifications")]
    [CISAZ("1.7", "", Level.L1, "v1.4")]
    [RuleInfo("User are not notified when their password is changed", "A malicious password reset might stay unnoticed.", 1, null, null, @"Open the link under Protal URL and set ""Notify users on password resets?"" to Yes")]
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
