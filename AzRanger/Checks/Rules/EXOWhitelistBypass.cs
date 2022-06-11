using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOWhitelistBypass", Scope.EXO, MaturityLevel.Mature, "https://admin.exchange.microsoft.com/#/transportrules")]
    [RuleScore("There exist Transport Rules in EXO, that may bypass anti-spam and anti-malware scanning", "This expose your organisation an additional riks", 3, "https://docs.microsoft.com/en-us/Exchange/security-and-compliance/mail-flow-rules/mail-flow-rules")]
    class EXOWhitelistBypass : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(TransportRule rule in tenant.ExchangeOnlineSettings.TransportRules)
            {
                if (rule.State != "Enabled")
                {
                    continue;
                }
                if (rule.SetSCL != null && rule.SetSCL.Equals("-1") && rule.SenderDomainIs != null)
                {
                    passed = false;
                    this.AddAffectedEntity(rule);
                }
            }
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
