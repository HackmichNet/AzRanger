using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class AzSecurityDefaultsO365 : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.TenantSkuInfo.aadPremium)
            {
                this.SetReason("Not applicable because Tenant has Premium License");
                return CheckResult.NotApplicable;
            }
            if (tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
