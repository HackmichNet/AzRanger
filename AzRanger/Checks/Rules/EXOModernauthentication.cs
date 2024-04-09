using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class EXOModernauthentication : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
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
