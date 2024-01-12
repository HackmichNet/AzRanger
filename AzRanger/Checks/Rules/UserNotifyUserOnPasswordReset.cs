using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserNotifyUserOnPasswordReset", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/PasswordResetMenuBlade/~/Notifications")]
    [CISAZ("1.9", "", CISLevel.L1, "v2.0")]
    
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
