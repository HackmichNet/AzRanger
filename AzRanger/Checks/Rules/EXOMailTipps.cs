using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOMailTipps", ScopeEnum.EXO, MaturityLevel.Mature)]
    [CISM365("4.11", "", Level.L2, "v2.0")]
    [RuleInfo("EXOMailTipps")]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
