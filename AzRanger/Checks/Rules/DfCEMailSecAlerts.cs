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
    [CISAZ("2.1.18", "", CISLevel.L1, "v2.0")]
    
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
