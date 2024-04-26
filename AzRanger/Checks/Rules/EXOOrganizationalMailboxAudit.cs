using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class EXOOrganizationalMailboxAudit : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ExchangeOnlineSettings.OrganizationConfig.AuditDisabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
