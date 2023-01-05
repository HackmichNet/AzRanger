using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAllowCreationOfAzureTenants", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings")]
    [RuleInfo("User can create their own tenant", "User can create their own tenants.", 2, "https://twitter.com/JeffreyAppel7/status/1593219049215127555?s=20&t=LH-3XsLy4td6QumFXe9AnA", null, "Use the link from the Portal Url below to disable this feature.")]
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
