using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllowCreationOfAzureTenants", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
    [CISM365("1.1.22", "", Level.L1, "v2.0")]
    [CISAZ("1.3", "", Level.L1, "v2.0")]
    [RuleInfo("UserAllowCreationOfAzureTenants")]
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
