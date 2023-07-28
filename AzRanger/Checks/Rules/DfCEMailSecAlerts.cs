using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("DfCEMailSecAlerts", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings")]
    [CISAZ("2.1.18", "", Level.L1, "v2.0")]
    [RuleInfo("E-Mail Security Alerts for a subscription are disbaled or not configured", "The owner of a subscription is not configured to receive E-Mail Notification from Microsoft if Defender for Cloud detects sometinhg malicious.", 4, null, null, @"Go to <a href=""https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings"">https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings</a> and choose a Subscription or Resource Group. Then click on ""Email notification"" and set ""All users with the following roles"" to ""Owner"".")]
    class DfCEMailSecAlerts : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.SecurityContact[0].properties.notificationsByRole.state.Equals("On") && sub.SecurityContact[0].properties.alertNotifications.state.Equals("On"))
                {
                    if (!sub.SecurityContact[0].properties.notificationsByRole.roles.Contains("Owner"))
                    {
                        AddAffectedEntity(sub);
                        passed = false;
                    }
                }
                else
                {
                    AddAffectedEntity(sub);
                    passed = false;
                }
            }
            
            if(passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
