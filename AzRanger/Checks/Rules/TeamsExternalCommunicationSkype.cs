using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsExternalCommunicationSkype : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TenantFederationSettings.AllowPublicUsers == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
