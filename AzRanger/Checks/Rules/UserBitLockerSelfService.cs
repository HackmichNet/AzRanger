using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserBitLockerSelfService", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_Devices/DevicesMenuBlade/~/DeviceSettings/menuId~/null")]
    [RuleInfo("BitLocker Self-ServiceEnum is enabled", "User can see their BitLocker Keys in Azure. This gives the user an easy way to escalate their privileges to local Admin.", 8, "https://docs.microsoft.com/en-us/azure/active-directory/devices/device-management-azure-portal#block-users-from-viewing-their-bitlocker-keys-preview", null, "Use the link in the reference below to disable this.")]
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
