using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzActLogAlertCreateOrUpdateSecuritySolution", ScopeEnum.Azure, MaturityLevel.Tentative, "https://portal.azure.com/#blade/Microsoft_Azure_Monitoring/AzureMonitoringBrowseBlade/alertsV2", ServiceEnum.StorageAccount)]
    [CISAZ("5.2.5", "", Level.L1, "v2.0")]
    [RuleInfo("No Activity Log Alert for 'Create or Update Security Solutions' ", @"Unwanted changes for in the Security Solution might stay unnoticed.", 0)]
    internal class AzActLogAlertCreateOrUpdateSecuritySolution : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            
            foreach(Subscription sub in tenant.Subscriptions.Values)
            {
                bool wantedAllertRuleExist = false;
                foreach (ActivityLogAlert alert in sub.Resources.ActivityLogAlerts)
                {
                    if(alert.location == "Global" && alert.properties.enabled == true)
                    {
                        bool scopeIsEntireSubscription = false;
                        foreach (String scope in alert.properties.scopes)
                        {
                            if (scope == sub.id)
                            {
                                scopeIsEntireSubscription = true;
                            }
                        }
                        if (scopeIsEntireSubscription)
                        {
                            foreach(ActivityLogAlertAllof allOf in alert.properties.condition.allOf)
                            {
                                if(allOf.field == "operationName" && allOf.equals.ToLower() == "microsoft.security/securitysolutions/write")
                                {
                                    wantedAllertRuleExist = true;
                                }
                            }
                        }
                    }
                }
                if(wantedAllertRuleExist == false)
                {
                    this.AddAffectedEntity(sub);
                    passed = false;
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
