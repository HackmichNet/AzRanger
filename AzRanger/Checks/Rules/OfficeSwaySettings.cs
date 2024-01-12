using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeSwaySettings", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Sway")]    
    class OfficeSwaySettings : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.AdminCenterSettings.SwaySettings.ExternalSharingEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
