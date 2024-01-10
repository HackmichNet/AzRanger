using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSecurityContactsNotifiedForHigh", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/?l=en.en-us#view/Microsoft_Azure_Policy/PolicyMenuBlade/~/Overview")]
    [CISAZ("2.14", "Ensure That 'Notify about alerts with the following severity' is Set to 'High'", Level.L1, "v1.4")]
    [RuleInfo("The owner is not notified in case of a 'High' security alert", "This can increase the time when an incident could be handled.", 2)]
    internal class AzSecurityContactsNotifiedForHigh : BaseCheck
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
                    if (sub.SecurityContact.Any(
                        x => x.properties.alertNotifications.state == "Off" | x.properties.alertNotifications.minimalSeverity != "High")
                        )
                    {
                        passed = false;
                        this.AddAffectedEntity(sub);
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
