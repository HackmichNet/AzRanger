using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class OfficeFormsPhishingProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AdminCenterSettings.OfficeFormsSettings.InOrgFormsPhishingScanEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
