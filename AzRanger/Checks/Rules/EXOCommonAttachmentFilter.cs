using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOCommonAttachmentFilter", ScopeEnum.EXO, MaturityLevel.Tentative, "https://security.microsoft.com/antimalwarev2")]
    [CISM365("4.1", "", Level.L1, "v2.0")]
    [RuleInfo("Common Attachment filter is not active", "This increases the risk that your company is compromised with malicious attachments.", 3, null, null, "Go to the link in the reference and ensure that in the policy with the highest priority the value for 'Enable the common attachments filter' is 'On'. ")]
    class EXOCommonAttachmentFilter : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            MalwareFilterRule highestPriority = null;

            foreach(MalwareFilterRule rule in tenant.ExchangeOnlineSettings.MalwareFilterRule)
            {
                if(rule.State == "Enabled" && highestPriority == null)
                {
                    highestPriority = rule;
                }
                else
                {
                    if(rule.State == "Enabled" && highestPriority.Priority < rule.Priority)
                    {
                        highestPriority = rule;
                    }
                }
            }

            // If we have no rule, then it is a finding.
            if(highestPriority == null)
            {
                return CheckResult.Finding;
            }

            foreach(MalwareFilterPolicy m in tenant.ExchangeOnlineSettings.MalwareFilterPolicy)
            {
                if (m.Identity == highestPriority.Identity)
                {
                    if (m.EnableFileFilter)
                    {
                        return CheckResult.NoFinding;
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
