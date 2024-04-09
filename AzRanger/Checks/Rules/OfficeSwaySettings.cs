using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class OfficeSwaySettings : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AdminCenterSettings.SwaySettings.ExternalSharingEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
