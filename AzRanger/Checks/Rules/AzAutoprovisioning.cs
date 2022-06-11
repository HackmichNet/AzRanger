using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzAutoprovisioning", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings")]
    [RuleScore("LogAnalytics Agents are not automatically deployed on VMs in the following subscriptions:", "A good monitoring is key in IR", 2)]
    internal class AzAutoprovisioning : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                foreach(AutoProvisioningSettings settings in sub.AutoProvisioningSettings)
                {
                    if(settings.name == "default")
                    {
                        if(settings.properties.autoProvision == "Off")
                        {
                            passed = false;
                            this.AddAffectedEntity(sub);
                        }
                    }
                }
            }

            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;  
        }
    }
}
