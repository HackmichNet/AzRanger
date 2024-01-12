using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;


namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOMailboxAudit", ScopeEnum.EXO)]
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
