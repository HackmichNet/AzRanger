using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class UserLinkedInConnection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.DirectoryProperties.enableLinkedInAppFamily == 1)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
