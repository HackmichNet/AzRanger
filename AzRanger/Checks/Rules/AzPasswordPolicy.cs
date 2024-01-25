using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class AzPasswordPolicy : BaseCheck
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
