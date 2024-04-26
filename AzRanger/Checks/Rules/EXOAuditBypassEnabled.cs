using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class EXOAuditBypassEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool finding = false;
            foreach(var item in tenant.ExchangeOnlineSettings.MailboxAuditBypassAssociations)
            {
                if(item.AuditBypassEnabled) { 
                    finding = true;
                    this.AddAffectedEntity(item);
                } 
            }

            if (finding)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
