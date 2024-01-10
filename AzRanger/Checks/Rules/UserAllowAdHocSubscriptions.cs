using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllowAdHocSubscriptions", ScopeEnum.AAD, MaturityLevel.Mature)]
    [RuleInfo("UserAllowAdHocSubscriptions")]
    class UserAllowAdHocSubscriptions : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.MSOLCompanyInformation.AllowAdHocSubscriptions == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
