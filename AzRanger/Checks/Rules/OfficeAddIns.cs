using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeAddIns", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Store")]
    [CISM365("2.9", "", Level.L1, "v2.0")]
    
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
