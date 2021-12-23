using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;


namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOMailboxAudit", Scope.EXO)]
    [RuleScore("Not all mailboxes have audit enabled", "Logging and monitoring helps you to perfomr IR or to find malicious behaviour", 6, "https://docs.microsoft.com/en-us/microsoft-365/compliance/enable-mailbox-auditing?view=o365-worldwide")]
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
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
