using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
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
