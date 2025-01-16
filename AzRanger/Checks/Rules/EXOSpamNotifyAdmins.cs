using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class s : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach (HostedOutboundSpamFilterPolicy policy in tenant.ExchangeOnlineSettings.HostedOutboundSpamFilterPolicy)
            {
                if (policy.BccSuspiciousOutboundMail & policy.NotifyOutboundSpam & policy.IsDefault)
                {
                    return CheckResult.NoFinding;
                }
            }
            return CheckResult.Finding;
        }
    }
}
