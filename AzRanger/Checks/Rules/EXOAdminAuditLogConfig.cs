using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOAdminAuditLogConfig", ScopeEnum.EXO, MaturityLevel.Mature, "https://compliance.microsoft.com/auditlogsearch")]
    [CISM365("5.2", "", Level.L1, "v2.0")]
    [RuleInfo("Unified AuditLog is turned off", "Attacks on Exchange Online can go unnoticed.", 7 , "https://learn.microsoft.com/en-us/purview/audit-log-enable-disable?view=o365-worldwide", null, @"Go to Microsoft PureView and select ""Audit"". Afterwards click ""Start recording user and admin activity"". As an alternative you can use the PowerShell Exchange Online Management Module and use the following command: ""Set-AdminAuditLogConfig -UnifiedAuditLogIngestionEnabled $true"".")]
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
