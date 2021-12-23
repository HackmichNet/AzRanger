using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOSpamPolicy", Scope.EXO, MaturityLevel.Mature, "https://security.microsoft.com/antispam")]
    [RuleScore("Exchange Online Spam policy does not notify admins if a mail is blocked", "Admins can be notified when an outgoing mail is blocked", 1)]
    class EXOSpamPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(HostedOutboundSpamFilterPolicy policy in tenant.ExchangeOnlineSettings.HostedOutboundSpamFilterPolicy)
            {
                if (policy.BccSuspiciousOutboundMail & policy.NotifyOutboundSpam & policy.IsDefault)
                {
                    return CheckResult.Passed;
                }
            }
            return CheckResult.Failed;
        }
    }
}
