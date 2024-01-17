using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
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
