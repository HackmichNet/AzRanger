using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserAllowAdHocSubscriptions", Scope.O365, MaturityLevel.Mature)]
    [RuleScore("User can create trial subscriptions.", "Allowing user to create their own trial subscriptions can lead to numerous subscriptions without control", 1, "https://docs.microsoft.com/en-us/azure/active-directory/authentication/tutorial-enable-sspr")]
    class UserAllowAdHocSubscriptions : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // 0 => Self service password reset enabled = None
            if (tenant.MSOLCompanyInformation.AllowAdHocSubscriptions == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
