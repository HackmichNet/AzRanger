using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllowCreationOfAzureTenants", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
    [CISAZ("1.3", "", CISLevel.L1, "v2.0")]
    class UserAllowCreationOfAzureTenants : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AuthorizationPolicy.defaultUserRolePermissions.allowedToCreateTenants == true)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
