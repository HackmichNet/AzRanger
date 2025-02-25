using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class ADPasswordHashSyncEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.OrganizationSettings.onPremisesSyncEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
