using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzPasswordPolicy", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/SecurityPrivacy/:/Settings/L1/PasswordPolicy")]
    [CISM365("1.4", "", Level.L1, "v1.5")]
    [RuleInfo("Passwords can expire", "Users tend to use weak passwords, when they have to change password to often.", 5, "https://pages.nist.gov/800-63-3/sp800-63b.html", null, @"Go to <a href=""https://admin.microsoft.com/#/Settings/SecurityPrivacy/:/Settings/L1/PasswordPolicy"">https://admin.microsoft.com/#/Settings/SecurityPrivacy/:/Settings/L1/PasswordPolicy</a> and mark the box for ""Set passwords to never expire (recommended)""")]
    class AzPasswordPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AdminCenterSettings.O365PasswordPolicy.NeverExpire == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
