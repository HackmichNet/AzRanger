using AzRanger.Models;

namespace AzRanger.Checks.Rules
{    
    class UserBitLockerSelfService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AuthorizationPolicy.defaultUserRolePermissions.allowedToReadBitlockerKeysForOwnedDevice == true)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
