using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeDLPLabel", Scope.O365, MaturityLevel.Tentative, "https://compliance.microsoft.com/informationprotection?viewid=sensitivitylabels")]
    [CISM365("3.2", "Ensure SharePoint Online Information Protection policies are set up and used", Level.L2, "v1.4")]
    [RuleInfo("Your tenant has no DLP Labels", "This increases the risk of unwanted data loss.", 0, "https://chris-brumm.medium.com/using-sensitivity-labels-in-office-365-dlp-a41618e39474", null, "Configure the DLP Labels according to your needs.")]
    class OfficeDLPLabel : BaseCheck
    {
        // TODO: Maybe we can check if they makes sense
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.DlpLabels != null)
            {
                foreach(DlpLabel label in tenant.DlpLabels)
                {
                    if (label.Disabled == false)
                    {
                        return CheckResult.NoFinding;
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
