using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOAdminAuditLogConfig", Scope.EXO, MaturityLevel.Mature, "https://security.microsoft.com/auditlogsearch")]
    [RuleScore("Unified AuditLog is turned off", "Logging is an essential task to observ abnormal behavior and to do incidents response", 7 , "https://docs.microsoft.com/en-us/microsoft-365/compliance/turn-audit-log-search-on-or-off?view=o365-worldwide")]
    class EXOAdminAuditLogConfig : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.ExchangeOnlineSettings.AdminAuditLogConfig.AdminAuditLogEnabled && tenant.ExchangeOnlineSettings.AdminAuditLogConfig.UnifiedAuditLogIngestionEnabled)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
