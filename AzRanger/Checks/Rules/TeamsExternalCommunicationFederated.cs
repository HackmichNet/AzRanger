using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class TeamsExternalCommunicationFederated : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TeamsSettings.TenantFederationSettings.AllowFederatedUsers == false)
            {
                return CheckResult.NoFinding;
            }
            else
            {
                if (tenant.TeamsSettings.TenantFederationSettings.AllowedDomains.AllowedDomain != null)
                {
                    return CheckResult.NoFinding;
                }
            }
            return CheckResult.Finding;
        }
    }
}
