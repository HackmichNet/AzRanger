using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeDLPLabels", ScopeEnum.AAD, MaturityLevel.Tentative, "https://compliance.microsoft.com/informationprotection/labelpolicies")]
    [CISM365("3.2", "", Level.L2, "v2.0")]
    [RuleInfo("The tenant has no DLP Labels Policies", "This increases the risk of unwanted data loss.", 0, "https://learn.microsoft.com/en-us/microsoft-365/compliance/dlp-learn-about-dlp?view=o365-worldwide#deploy-your-policies-in-production", null, "Go to the portal URL and DLP label policies according to your needs.")]
    class OfficeDLPLabels : BaseCheck
    {
        // TODO: Maybe we can check if they makes sense
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.DlpLabels != null)
            {
                foreach(DlpLabel label in tenant.TenantSettings.DlpLabels)
                {
                    if (label.Disabled != false & label.Mode.Equals("Enforce"))
                    {
                        return CheckResult.NoFinding;
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
