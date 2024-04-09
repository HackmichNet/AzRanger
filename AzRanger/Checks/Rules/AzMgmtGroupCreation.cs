using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    internal class AzMgmtGroupCreation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ManagementGroupSettings == null)
            {
                return CheckResult.NotApplicable;
            }
            if (tenant.ManagementGroupSettings.properties.requireAuthorizationForGroupCreation == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
