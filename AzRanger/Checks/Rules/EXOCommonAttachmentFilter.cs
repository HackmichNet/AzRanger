using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class EXOCommonAttachmentFilter : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            MalwareFilterRule highestPriority = null;

            foreach (MalwareFilterRule rule in tenant.ExchangeOnlineSettings.MalwareFilterRule)
            {
                if (rule.State == "Enabled" && highestPriority == null)
                {
                    highestPriority = rule;
                }
                else
                {
                    if (rule.State == "Enabled" && highestPriority.Priority < rule.Priority)
                    {
                        highestPriority = rule;
                    }
                }
            }

            // If we have no rule, then it is a finding.
            if (highestPriority == null)
            {
                return CheckResult.Finding;
            }

            foreach (MalwareFilterPolicy m in tenant.ExchangeOnlineSettings.MalwareFilterPolicy)
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
