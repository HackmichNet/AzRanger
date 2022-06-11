using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserLinkedInConnection", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings")]
    [RuleScore("User can connect their account sto LinkedIn", "Allowing user syncen their account witk LinkedIn could disclose usefull information for an attacker", 7, "https://docs.microsoft.com/en-us/azure/active-directory/enterprise-users/linkedin-integration")]
    class UserLinkedInConnection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.DirectoryProperties.enableLinkedInAppFamily == 1)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
