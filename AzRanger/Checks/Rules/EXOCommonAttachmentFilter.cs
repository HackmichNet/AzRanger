using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOCommonAttachmentFilter", Scope.EXO, MaturityLevel.Tentative, "https://security.microsoft.com/antimalwarev2")]
    [RuleScore("Common Attachment filter seems not to be active", "Mails with malicious attachament is one of the most common way to infiltrate an company", 3)]
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
                    return CheckResult.Passed;
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
                            return CheckResult.Passed;
                        }
                    }
                }
            }
            return CheckResult.Failed;
        }
    }
}
