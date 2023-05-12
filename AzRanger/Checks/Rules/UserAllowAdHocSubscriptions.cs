using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllowAdHocSubscriptions", ScopeEnum.AAD, MaturityLevel.Mature)]
    [RuleInfo("User can create trial subscriptions.", "This can result in a high number of uncontrolled subscriptions.", 1, "https://docs.microsoft.com/en-us/azure/active-directory/authentication/tutorial-enable-sspr", null, @"See the link in the reference section.")]
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
