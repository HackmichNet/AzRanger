using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOModernauthentication", ScopeEnum.EXO, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/ModernAuthentication")]
    [CISM365("1.2", "", Level.L1, "v2.0")]
    
    class EXOModernauthentication : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            if (tenant.ExchangeOnlineSettings.OrganizationConfig.OAuth2ClientProfileEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
