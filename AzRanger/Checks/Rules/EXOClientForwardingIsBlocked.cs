using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOClientForwardingIsBlocked", Scope.EXO, MaturityLevel.Mature, "https://admin.exchange.microsoft.com/#/transportrules")]
    [RuleScore("No rule is detected, that blocks auto forwarding on the client site", "User can automatically forward mails outside the organization", 4)]
    class EXOClientForwardingIsBlocked : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
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
                                return CheckResult.Passed;
                            }
                        }
                    }
                }
            }
            return CheckResult.Failed;
        }
    }
}
