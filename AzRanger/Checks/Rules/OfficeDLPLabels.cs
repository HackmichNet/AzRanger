using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeDLPLabels", ScopeEnum.AAD, MaturityLevel.Tentative, "https://compliance.microsoft.com/informationprotection/labelpolicies")]
    [CISM365("3.2", "", Level.L2, "v2.0")]
    [RuleInfo("OfficeDLPLabels")]
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
