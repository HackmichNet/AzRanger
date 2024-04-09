using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class EXOMailTipps : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            OrganizationConfig config = tenant.ExchangeOnlineSettings.OrganizationConfig;

            if (config.MailTipsAllTipsEnabled &&
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
