using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserBitLockerSelfService", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_Devices/DevicesMenuBlade/~/DeviceSettings/menuId/recoveryKeys")]
    [RuleInfo("UserBitLockerSelfService")]
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
