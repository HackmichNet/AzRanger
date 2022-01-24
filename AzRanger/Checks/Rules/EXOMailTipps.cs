using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOMailTipps", Scope.EXO, MaturityLevel.Mature)]
    [RuleScore("Not all Mailbox Tips are enabled", "Mailbox can prevent a user to sent mails to too many recipients or sending mails outside of the organisation", 1)]
    class EXOMailTipps : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            OrganizationConfig config = tenant.ExchangeOnlineSettings.OrganizationConfig;

            if(config.MailTipsAllTipsEnabled &&
                config.MailTipsExternalRecipientsTipsEnabled && 
                config.MailTipsGroupMetricsEnabled &&
                config.MailTipsLargeAudienceThreshold <= 25)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
