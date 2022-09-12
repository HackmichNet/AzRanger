﻿using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserNotifyAdminOnPasswordReset", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Notifications")]
    [CISAZ("1.8", "", Level.L2, "v1.4")]
    [RuleInfo("Admins are not notified when their password is changed", "A malicious password reset might stay unnoticed.", 1, null, null, @"Open the link under Protal URL and set ""Notify all admins when other admins reset their password?"" to Yes")]
    class UserNotifyAdminOnPasswordReset : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.PasswordResetPolicies.notifyOnAdminPasswordReset == false)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}