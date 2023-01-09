using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSubscriptionPolicy", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_Azure_SubscriptionManagement/ManageSubscriptionPoliciesBlade")]
    [CISAZ("1.25", "", Level.L2, "v1.5")]
    [RuleInfo("User can move Subscriptions in and out of Anzure Active Directory", "This increases the risk, that a bad actor moves an subscription to a Tenant, where he has more permissions.", 6, "https://www.cloud-architekt.net/detection-and-mitigation-consent-grant-attacks-azuread/", null, @"Go to the reference lin an set ""Subscription leaving AAD directory"" and ""Subscription entering AAD directory "" to ""Permit no one"".")]
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
