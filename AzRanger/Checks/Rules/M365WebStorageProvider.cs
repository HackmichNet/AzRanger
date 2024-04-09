using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class M365WebStorageProvider : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.Officeonline.Enabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
