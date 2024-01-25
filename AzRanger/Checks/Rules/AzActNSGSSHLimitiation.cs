﻿using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    internal class AzActNSGSSHLimitiation : BaseCheck
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
                            bool isPort22 = false;
                            if(rule.properties.destinationPortRange != null && rule.properties.destinationPortRange == "22")
                            {
                                isPort22 = true;
                            }
                            if(rule.properties.destinationPortRanges != null)
                            {
                                foreach(var port in rule.properties.destinationPortRanges)
                                {
                                    if(port.ToString() == "22")
                                    {
                                        isPort22 = true;
                                    }
                                }
                            }
                            if(isPort22)
                            {
                                if (rule.properties.sourceAddressPrefix != null)
                                {
                                    if (rule.properties.sourceAddressPrefix == "*" ||
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
                
            }
            
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
