using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserCanViewOtherUsersProfile", ScopeEnum.AAD, MaturityLevel.Mature)]
    [RuleInfo("User can see profile information about other users", "This can help to perform further phishing attacks or to collect other relevant data.", 1, "https://docs.microsoft.com/en-us/powershell/module/msonline/set-msolcompanysettings?view=azureadps-1.0")]
    class UserCanViewOtherUsersProfile : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // Need to check what I thought here 
            return CheckResult.NotApplicable;
            // 0 => Self service password reset enabled = None
            //if (tenant.PasswordResetPolicies.enablementType == 0)
            //{
            //    return CheckResult.Finding;
            //}
            //return CheckResult.Passed;
        }
    }
}
