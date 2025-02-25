using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsExternalCommunicationWithConsumer : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TenantFederationSettings.AllowTeamsConsumer == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
