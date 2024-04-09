using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    internal class AzGuestInvite : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AuthorizationPolicy.allowInvitesFrom == "adminsAndGuestInviters" | tenant.TenantSettings.AuthorizationPolicy.allowInvitesFrom == "none")
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
