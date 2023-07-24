using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOWhitelistBypass", ScopeEnum.EXO, MaturityLevel.Mature, "https://admin.exchange.microsoft.com/#/transportrules")]
    [CISM365("4.4", "", Level.L1, "v2.0")]
    [RuleInfo("It exists Transport Rules, that can bypass anti-spam and anti-malware scanning by whitelisting certain domains", "This expose your organization an additional risk.", 3, "https://docs.microsoft.com/en-us/Exchange/security-and-compliance/mail-flow-rules/mail-flow-rules", null, "Go to the Exchange Admin Center and check the rules.")]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
