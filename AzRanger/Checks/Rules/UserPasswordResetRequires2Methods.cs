using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class UserPasswordResetRequires2Methods : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.enablementType == 0)
            {
                this.SetReason("Password Reset is not configured");
                return CheckResult.NotApplicable;
            }
            if (tenant.TenantSettings.PasswordResetPolicies.numberOfAuthenticationMethodsRequired < 2)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}