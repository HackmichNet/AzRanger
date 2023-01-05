using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzActNSGSUnrestrictedDestionationPort", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Network%2FNetworkSecurityGroups", ServiceEnum.NetworksSecurityGroup)]
    [RuleInfo("Network Security Group without inbpund port restriction", "The service protected by the Network Security Group is exposed to the internet.", 0, null, null, "Configure the Network Security Group that only needed inbound ports are allowed.")]
    internal class AzActNSGSUnrestrictedDestionationPort : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                
                foreach (NetworkSecurityGroup nsg in sub.Resources.NetworkSecurityGroups)
                {
                    foreach(NetworkSecurityGroupSecurityrule rule in nsg.properties.securityRules)
                    {
                        if (rule.properties.direction == "Inbound")
                        {
                            if(rule.properties.destinationPortRange == "*")
                            {
                                    passed = false;
                                    this.AddAffectedEntity(nsg);
                            }                           
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
