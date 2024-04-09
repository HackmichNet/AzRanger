using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    internal class GroupAllowUserToCreateSecGroups : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AuthorizationPolicy.defaultUserRolePermissions.allowedToCreateSecurityGroups == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
