using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRestrictAccessToAdminPortal", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings")]
    [CISAZ("1.17", "", Level.L1, "v1.5")]
    [RuleInfo("User can access the admin portal", "The Azure Portal may contain sensitiv information.", 1, null, "Azure portal contains sensitive information, which not everyone shoould be able to see.", @"1. Go to Azure Active Directory</b>2. Go to Users</br>3. Go to User settings</b>4. Set Restrict access to Azure AD administration portal to Yes")]
    class UserRestrictAccessToAdminPortal : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.DirectoryProperties.restrictNonAdminUsers == false)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
