using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOCommonAttachmentFilter", Scope.EXO, MaturityLevel.Tentative, "https://security.microsoft.com/antimalwarev2")]
    [CISM365("4.1", "", Level.L1, "v1.4")]
    [RuleInfo("Common Attachment filter is not active", "This increases the risk that your company is compromised with malicious attachments.", 3)]
    class EXOCommonAttachmentFilter : BaseCheck
    {
        //TODO: Check, if it implemented in another policy
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(MalwareFilterPolicy m in tenant.ExchangeOnlineSettings.MalwareFilterPolicy)
            {
                // Because Default policy cannot be disabled everythin is good.
                if (m.Identity == "Default" & m.EnableFileFilter)
                {
                    return CheckResult.NoFinding;
                }
            }

            foreach(MalwareFilterPolicy m in tenant.ExchangeOnlineSettings.MalwareFilterPolicy)
            {
                if (m.EnableFileFilter)
                {
                    // Check if policy is enabled
                    foreach(MalwareFilterRule r in tenant.ExchangeOnlineSettings.MalwareFilterRule)
                    {
                        if(m.Id == r.Identity && r.State == "Enables")
                        {
                            return CheckResult.NoFinding;
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
