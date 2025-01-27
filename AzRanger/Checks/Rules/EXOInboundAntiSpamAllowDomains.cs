using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class EXOInboundAntiSpamAllowDomains : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool finding = false;
            foreach(var item in tenant.ExchangeOnlineSettings.HostedContentFilterPolicies)
            {
                if (item.AllowedSenderDomains != null && item.AllowedSenderDomains.Length > 0) { 
                    finding = true;
                    AddAffectedEntity(item);
                }
            }

            if (finding)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
