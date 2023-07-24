using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeDLPPolicies", ScopeEnum.AAD, MaturityLevel.Tentative, "https://compliance.microsoft.com/datalossprevention?viewid=policies")]
    [CISM365("3.4", "", Level.L1, "v2.0")]
    [RuleInfo("The tenant has no DLP Policies", "This increases the risk of unwanted data loss.", 0, "https://learn.microsoft.com/en-us/microsoft-365/compliance/dlp-learn-about-dlp?view=o365-worldwide#deploy-your-policies-in-production", null, "Configure DLP policies according to your needs.")]
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
