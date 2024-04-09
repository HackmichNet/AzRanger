using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class UserAllowAdHocSubscriptions : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.MSOLCompanyInformation.AllowAdHocSubscriptions == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
