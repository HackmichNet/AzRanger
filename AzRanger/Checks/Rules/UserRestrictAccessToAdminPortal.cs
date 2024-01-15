using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRestrictAccessToAdminPortal", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
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
