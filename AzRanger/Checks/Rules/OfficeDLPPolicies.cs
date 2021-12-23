using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("OfficeDLPPolicies", Scope.O365, MaturityLevel.Tentative, "https://compliance.microsoft.com/datalossprevention?viewid=policies")]
    [RuleScore("Your tenant has no DLP Policies", "DLP Policies can help your organiszation preventing data loss", 0)]
    class OfficeDLPPolicies : BaseCheck
    {
        // TODO: Maybe we can check if they makes sense
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.OfficeDLPPolicies != null)
            {
                foreach(DlpCompliancePolicy policy in tenant.OfficeDLPPolicies)
                {
                    if (policy.Enabled)
                    {
                        return CheckResult.Passed;
                    }
                }
            }
            return CheckResult.Failed;
        }
    }
}
