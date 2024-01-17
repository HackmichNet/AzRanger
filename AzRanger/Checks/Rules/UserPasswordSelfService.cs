using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class UserPasswordSelfService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // 0 => Self service password reset enabled = None
            if (tenant.TenantSettings.PasswordResetPolicies.enablementType == 0)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
