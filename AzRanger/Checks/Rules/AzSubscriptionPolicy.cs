using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class AzSubscriptionPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SubscriptionPolicy == null)
            {
                // No policy created so far.
                return CheckResult.Finding;
            }
            if (tenant.SubscriptionPolicy.properties.blockSubscriptionsIntoTenant == true && tenant.SubscriptionPolicy.properties.blockSubscriptionsLeavingTenant == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
