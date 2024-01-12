using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzActNSGRDPLimitiation", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Network%2FNetworkSecurityGroups", ServiceEnum.NetworksSecurityGroup)]
    [CISAZ("6.1", "", CISLevel.L1, "v2.0")]
    
    internal class AzActNSGRDPLimitiation : BaseCheck
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
                            if(rule.properties.destinationPortRange == "3389")
                            {
                                if(rule.properties.sourceAddressPrefix == "*" ||
                                   rule.properties.sourceAddressPrefix == "0.0.0.0" ||
                                   rule.properties.sourceAddressPrefix == "/0" ||
                                   rule.properties.sourceAddressPrefix == "internet" ||
                                   rule.properties.sourceAddressPrefix == "any" ||
                                   rule.properties.sourceAddressPrefix.EndsWith("/0"))
                                {
                                    passed = false;
                                    this.AddAffectedEntity(nsg);
                                }
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
