using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOAdminAuditLogConfig", ScopeEnum.EXO, MaturityLevel.Mature, "https://security.microsoft.com/auditlogsearch")]
    [CISM365("5.1", "", Level.L1, "v1.4")]
    [RuleInfo("Unified AuditLog is turned off", "Attacks on Exchange Online can go unnoticed.", 7 , "https://docs.microsoft.com/en-us/microsoft-365/compliance/turn-audit-log-search-on-or-off?view=o365-worldwide", null, "Enable Unified Audit Log like in the Reference Link.")]
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
