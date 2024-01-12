using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOAdminAuditLogConfig", ScopeEnum.EXO, MaturityLevel.Mature, "https://compliance.microsoft.com/auditlogsearch")]
    [CISM365("5.2", "", Level.L1, "v2.0")]
    
    class EXOAdminAuditLogConfig : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.ExchangeOnlineSettings.AdminAuditLogConfig.AdminAuditLogEnabled && tenant.ExchangeOnlineSettings.AdminAuditLogConfig.UnifiedAuditLogIngestionEnabled)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
