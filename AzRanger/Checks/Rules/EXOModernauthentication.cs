using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOModernauthentication", ScopeEnum.EXO, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/ModernAuthentication")]
    [CISM365("1.2", "", Level.L1, "v1.4")]
    [RuleInfo("Exchange Online Modern Authentication is not enabled", "If modern authentication is disabled, the users cannot use a more secure authentication mechanism.", 10, "https://docs.microsoft.com/en-us/exchange/clients-and-mobile-in-exchange-online/enable-or-disable-modern-authentication-in-exchange-online", null, @"Go to Portal URL and check ""Turn on modern authentication for Outlook 2013 for Windows and later (recommended)"".")]
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
