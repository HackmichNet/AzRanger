using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOModernauthentication", Scope.EXO, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/ModernAuthentication")]
    [RuleScore("Exchange Online Modern Authentication is not enabled", "Exchange Online Modern authentication allows your users to use MFA", 10, "https://docs.microsoft.com/en-us/exchange/clients-and-mobile-in-exchange-online/enable-or-disable-modern-authentication-in-exchange-online")]
    class EXOModernauthentication : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SecurityDefaults.securityDefaultsEnabled == true)
            {
                return CheckResult.NotApplicable;
            }
            if (tenant.ExchangeOnlineSettings.ExchangeModernAutheSettings.DisableModernAuth == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
