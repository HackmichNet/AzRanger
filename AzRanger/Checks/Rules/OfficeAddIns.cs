using AzRanger.Models;

namespace AzRanger.Checks.Rules
{    
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
