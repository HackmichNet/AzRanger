using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOMailTipps", Scope.EXO, MaturityLevel.Mature)]
    [CISM365("4.12", "", Level.L2, "v1.4")]
    [RuleInfo("Not all Mailbox Tips are enabled", "Mailbox Tips can prevent a user to sent mails to too many recipients or sending mails outside the organization.", 1, null, null, @"Use the Exchange Online PowerShell Module to connect to Exchange Online. Then run the following code ""Set-OrganizationConfig -MailTipsAllTipsEnabled $true -MailTipsExternalRecipientsTipsEnabled $true -MailTipsGroupMetricsEnabled $true -MailTipsLargeAudienceThreshold '25'"".")]
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
