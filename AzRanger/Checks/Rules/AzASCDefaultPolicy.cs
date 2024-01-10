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
    [RuleInfo("AzASCDefaultPolicy")]
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
