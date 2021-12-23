using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzPasswordPolicy", Scope.O365, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/SecurityPrivacy/:/Settings/L1/PasswordPolicy")]
    [RuleScore("Password policy expiration is set", "Users tend to use weak passwords, when they have to change password to often", 5, "https://pages.nist.gov/800-63-3/sp800-63b.html")]
    class AzPasswordPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.AdminCenterSettings.O365PasswordPolicy.NeverExpire == true)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
