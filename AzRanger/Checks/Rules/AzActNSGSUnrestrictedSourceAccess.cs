﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzActNSGSUnrestrictedSourceAccess", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.Network%2FNetworkSecurityGroups", Service.NetworksSecurityGroup)]
    [RuleScore("The follwoing NetworkSecurityGroups have no restricted source address", "This can result in an additional threat to the destination services.", 0)]
    internal class AzActNSGSUnrestrictedSourceAccess : BaseCheck
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
            
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
