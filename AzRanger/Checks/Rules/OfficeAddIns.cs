using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeAddIns", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Store")]
    
    class OfficeAddIns : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AdminCenterSettings.OfficeStoreSettings.LetUserAccessTheOfficeStore == false & tenant.TenantSettings.AdminCenterSettings.OfficeStoreSettings.LetUserStartTrialsInBehalfOfTheOrg == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
