using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRestrictAccessToAdminPortal", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
    [CISAZ("1.17", "", Level.L1, "v2.0")]
    [CISM365("1.1.20", "", Level.L1, "v2.0")]
    [RuleInfo("User can access the Entra admin portal", "The Entra admin portal may contain sensitive information.", 1, null, "Entra Admin portal contains sensitive information, which not everyone should be able to see.", @"1. Go to Entra admin center</b>2. Go to Users</br>3. Go to User settings</b>4. Set Restrict access to Azure AD administration portal to ""Yes"".")]
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
