using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    // Credits to https://github.com/soteria-security/365Inspect/blob/main/Inspectors/BypassingSafeAttachments.ps1
    [RuleMeta("EXOBypassSafeAttachments", Scope.EXO, MaturityLevel.Mature, "https://admin.exchange.microsoft.com/#/transportrules")]
    [RuleInfo("Transport Rules in EXO bypasses SafeAttachment checking", "This expose your organisation an additional riks.", 3, "https://www.undocumented-features.com/2018/05/10/atp-safe-attachments-safe-links-and-anti-phishing-policies-or-all-the-policies-you-can-shake-a-stick-at/#Bypass_Safe_Attachments_Processing")]
    class EXOBypassSafeAttachments : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(TransportRule rule in tenant.ExchangeOnlineSettings.TransportRules)
            {
                if(rule.State != "Enabled")
                {
                    continue;
                }
                if(rule.SetHeaderName != null && (string)rule.SetHeaderName.ToString() == "X-MS-Exchange-Organization-SkipSafeAttachmentProcessing")
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
