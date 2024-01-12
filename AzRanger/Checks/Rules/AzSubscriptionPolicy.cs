using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSubscriptionPolicy", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_Azure_SubscriptionManagement/ManageSubscriptionPoliciesBlade")]
    [CISAZ("1.25", "", CISLevel.L2, "v2.0")]
    
    class AzSubscriptionPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SubscriptionPolicy == null)
            {
                // No policy created so far.
                return CheckResult.Finding;
            }
            if(tenant.SubscriptionPolicy.properties.blockSubscriptionsIntoTenant == true && tenant.SubscriptionPolicy.properties.blockSubscriptionsLeavingTenant == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
