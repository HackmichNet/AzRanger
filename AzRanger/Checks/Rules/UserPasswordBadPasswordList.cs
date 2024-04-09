using AzRanger.Models;

namespace AzRanger.Checks
{
    class UserPasswordBadPasswordList : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.enablementType == 0)
            {
                this.SetReason("Password Reset is not configured");
                return CheckResult.NotApplicable;
            }
            if (tenant.TenantSettings.PasswordPolicy.enforceCustomBannedPasswords == false)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
