using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOClientForwardingIsBlocked", ScopeEnum.EXO, MaturityLevel.Tentative, "https://admin.exchange.microsoft.com/#/transportrules")]
    [CISM365("4.3", "", Level.L1, "v1.5-3")]
    [RuleInfo("Autoforwarding on the client site is not prevent by Exchange Online", "This can lead to an unwanted data loss.", 4, null, "No mail flow rule detected, that blocks auto forwarding on the client site. User might be able automatically forward mails outside the organization")]
    class EXOClientForwardingIsBlocked : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(HostedOutboundSpamFilterPolicy policy in tenant.ExchangeOnlineSettings.HostedOutboundSpamFilterPolicy)
            {
                /*
                    You can use outbound spam filter policies to control automatic forwarding to external recipients. Three settings are available:

                    Automatic - System-controlled: Automatic external forwarding is blocked. Internal automatic forwarding of messages will continue to work. This is the default setting.
                    On: Automatic external forwarding is allowed and not restricted.
                    Off: Automatic external forwarding is disabled and will result in a non-delivery report (also known as an NDR or bounce message) to the sender.
                    Source: https://docs.microsoft.com/en-us/microsoft-365/security/office-365-security/external-email-forwarding?view=o365-worldwide
                 */
                // Rule "Default" is always on
                if (policy.Name == "Default")
                {
                    if(policy.AutoForwardingMode == "Automatic" || policy.AutoForwardingMode == "Off")
                    {
                        return CheckResult.NoFinding;
                    }
                }
            }
            foreach(TransportRule rule in tenant.ExchangeOnlineSettings.TransportRules)
            {
                if (rule.State == "Enabled" & rule.Mode == "Enforce")
                {
                    if ((rule.FromScope != null && (string) rule.FromScope.ToString() == "InOrganization" ) &&
                        (rule.SentTo != null && (string)rule.SentToScope.ToString() == "NotInOrganization") &&
                        (rule.MessageTypeMatches != null && (string) rule.MessageTypeMatches.ToString() == "AutoForward") &&
                        (rule.State != null && (string) rule.State.ToString() == "Enabled"))
                    {
                        foreach (string action in rule.Actions)
                        {
                            if (action.Equals("Microsoft.Exchange.MessagingPolicies.Rules.Tasks.RejectMessageAction"))
                            {
                                return CheckResult.NoFinding;
                            }
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
