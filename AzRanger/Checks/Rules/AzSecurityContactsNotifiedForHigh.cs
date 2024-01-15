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
