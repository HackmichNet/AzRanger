using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserRestrictAccessToAdminPortal", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings")]
    [RuleScore("User can access the admin portal", "Azure portal might contain sensitive information", 1)]
    class UserRestrictAccessToAdminPortal : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.DirectoryProperties.restrictNonAdminUsers == false)
            {
                return CheckResult.Failed;
            }
            return CheckResult.Passed;
        }
    }
}
