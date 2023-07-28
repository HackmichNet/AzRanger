using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzAutoprovisioning", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings")]
    [CISAZ("2.2.15", "", Level.L1, "v2.0")]
    [RuleInfo("LogAnalytics Agent is not automatically deployed on VMs", "If no logAnalytics Agent is deployed on a VM a lot of security related events can go unoticed.", 2)]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;  
        }
    }
}
