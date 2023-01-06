using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeCalenderSharing", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/Calendar")]
    [CISM365("2.2", "", Level.L2, "v1.5")]
    [RuleInfo("User can share their calendar with external users", "This increases the risk, that external user gain personal information about your users.", 4, null, null, @"Go to the Portal URL and unmark ""Let your users share their calendars with people outside of your organization who have Office 365 or Exchange""")]
    class OfficeCalenderSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.AdminCenterSettings.Calendarsharing.EnableCalendarSharing == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
