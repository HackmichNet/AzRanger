using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserNotifyAdminOnPasswordReset", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/PasswordResetMenuBlade/~/Notifications")]
    [CISAZ("1.10", "", Level.L2, "v2.0")]
    [RuleInfo("Admins are not notified when their password is changed", "A malicious password reset might stay unnoticed.", 1, null, null, @"Open the link under Portal URL and set ""Notify all admins when other admins reset their password?"" to Yes")]
    class UserNotifyAdminOnPasswordReset : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.notifyOnAdminPasswordReset == false)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
