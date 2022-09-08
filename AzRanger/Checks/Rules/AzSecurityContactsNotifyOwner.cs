using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSecurityContactsNotifyOwner", Scope.Azure, MaturityLevel.Mature, "https://portal.azure.com/?l=en.en-us#view/Microsoft_Azure_Policy/PolicyMenuBlade/~/Overview")]
    [CISAZ("2.15", "Ensure That 'All users with the following roles' is set to 'Owner'", Level.L1, "v1.4")]
    [RuleInfo("The Owner of a subscription does not get security alerts via E-Mail", "This can increase the time when an incident could be handled.", 2)]
    internal class AzSecurityContactsNotifyOwner : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if(sub.SecurityContact == null)
                {
                    passed = false;
                    this.AddAffectedEntity(sub);
                }
                else
                {
                    if(sub.SecurityContact.properties.notificationsByRole.state == "Off"){
                        passed = false;
                        this.AddAffectedEntity(sub);
                    } 
                    else
                    {
                        bool containsOwner = false;
                        foreach (String role in sub.SecurityContact.properties.notificationsByRole.roles)
                        {
                            if(role == "Owner")
                            {
                                containsOwner = true;
                            }
                        }
                        if (!containsOwner)
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
