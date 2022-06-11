using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserCanViewOtherUsersProfile", Scope.O365, MaturityLevel.Mature)]
    [RuleScore("User can see profile information about other users", "This can help to perform further phishing attacks or to collect other relevant data", 1, "https://docs.microsoft.com/en-us/powershell/module/msonline/set-msolcompanysettings?view=azureadps-1.0")]
    class UserCanViewOtherUsersProfile : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // 0 => Self service password reset enabled = None
            if (tenant.PasswordResetPolicies.enablementType == 0)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}
