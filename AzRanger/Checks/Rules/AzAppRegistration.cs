using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    internal class AzAppRegistration : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.DirectoryProperties.usersCanRegisterApps == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
