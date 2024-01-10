using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzPasswordPolicy", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/SecurityPrivacy/:/Settings/L1/PasswordPolicy")]
    [CISM365("1.4", "", Level.L1, "v2.0")]
    [RuleInfo("AzPasswordPolicy")]
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
