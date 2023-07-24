using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllowCreationOfAzureTenants", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
    [CISM365("1.1.22", "", Level.L1, "v2.0")]
    [RuleInfo("User can create their own tenant", "User can create their own tenants.", 2, "https://twitter.com/JeffreyAppel7/status/1593219049215127555?s=20&t=LH-3XsLy4td6QumFXe9AnA", null, @"Go to the ""User Settings"" in the Entra Admin Portal and set ""Restrict non-admin users from creating tenants"" to ""No"".")]
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
