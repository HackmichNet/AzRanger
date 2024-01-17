using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class ADPasswordProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordPolicy.enableBannedPasswordCheckOnPremises == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
