using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRestrictAccessToAdminPortal", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
    [CISAZ("1.17", "", Level.L1, "v2.0")]
    [CISM365("1.1.20", "", Level.L1, "v2.0")]
    [RuleInfo("UserRestrictAccessToAdminPortal")]
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
