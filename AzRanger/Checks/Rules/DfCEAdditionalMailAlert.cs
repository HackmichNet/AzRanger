using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("DfCEAdditionalMailAlert", ScopeEnum.Azure, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings")]
    [CISAZ("2.3.2", "", Level.L1, "v1.5")]
    [RuleInfo("No additional E-Mail Address for Defender Alert is configured", "Only the owner or noone does receive E-Mail Notification from Microsoft if Defender for Cloud detects sometinhg malicious.", 4, null, null, @"To ensure that your secrutiy team receives notficiations from Defender for Cloud go to <a href=""https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings"">https://portal.azure.com/#view/Microsoft_Azure_Security/SecurityMenuBlade/~/EnvironmentSettings</a> and choose a Subscription or Resource Group. Then click on ""Email notification"" and enter in the field ""Additional email addresses"" the addresses you want.")]
    class DfCEAdditionalMailAlert : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.SecurityContact.properties.notificationsByRole.state.Equals("On") && sub.SecurityContact.properties.alertNotifications.state.Equals("On"))
                {
                    if (sub.SecurityContact.properties.emails.Length == 0)
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
