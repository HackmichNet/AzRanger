using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;


namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOMailboxAudit", ScopeEnum.EXO)]
    [CISM365("5.3", "", Level.L1, "v2.0")]
    [RuleInfo("Not all mailboxes have audit enabled", "Malicious behavior can go unnoticed.", 6, "https://docs.microsoft.com/en-us/microsoft-365/compliance/enable-mailbox-auditing?view=o365-worldwide", null, "To enable logging, see the reference link.")]
    class EXOMailboxAudit : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(Mailbox mailbox in tenant.ExchangeOnlineSettings.Mailboxes)
            {
                if(mailbox.AuditEnabled == false)
                {
                    passed = false;
                    this.AddAffectedEntity(mailbox);
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
