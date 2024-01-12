using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeDLPPolicies", ScopeEnum.AAD, MaturityLevel.Tentative, "https://compliance.microsoft.com/datalossprevention?viewid=policies")]    
    class OfficeDLPPolicies : BaseCheck
    {
        // TODO: Maybe we can check if they makes sense
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.OfficeDLPPolicies != null)
            {
                foreach(DlpCompliancePolicy policy in tenant.TenantSettings.OfficeDLPPolicies)
                {
                    if (policy.Enabled)
                    {
                        return CheckResult.NoFinding;
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
