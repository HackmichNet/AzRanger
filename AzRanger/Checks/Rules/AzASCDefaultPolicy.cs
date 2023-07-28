using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzASCDefaultPolicy", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/?l=en.en-us#view/Microsoft_Azure_Policy/PolicyMenuBlade/~/Overview")]
    [CISAZ("2.1.14", null, Level.L1, "v2.0")]
    [RuleInfo("The Default Azure Policy is disabled", "Having the Security Default Policy enabled could increase the security of a subscription.", 2, null, "The Default Azure Policy is disabled builts a security baseline for each subscriptions", " 1. Go to Azure Policy </br> 2. On Policy Overview blade, Click on Policy ASC Default  (Subscription: Subscription_ID) </br> 3.On ASC Default blade, Click on Edit Assignments </br> 4.In section Basics, drab down to Policy Enforcements setting and check if it is set to Enabled")]
    internal class AzASCDefaultPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if(sub.SecurityCenterBuiltIn.properties.enforcementMode != "Default")
                {
                    passed = false;
                    this.AddAffectedEntity(sub);
                }
            }

            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;  
        }
    }
}
