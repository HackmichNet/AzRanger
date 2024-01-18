using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class UserReconfirmAuthenticationInformation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.registrationReconfirmIntevalInDays == 0)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
