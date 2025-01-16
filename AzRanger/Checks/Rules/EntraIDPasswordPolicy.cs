using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class EntraIDPasswordPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AdminCenterSettings.O365PasswordPolicy.NeverExpire == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
