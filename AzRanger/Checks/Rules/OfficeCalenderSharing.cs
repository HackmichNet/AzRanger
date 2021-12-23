using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("OfficeCalenderSharing", Scope.O365, MaturityLevel.Mature, "https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/Calendar")]
    [RuleScore("User can share their calendar with external users", "External user can gain unintended information about your user", 4)]
    class OfficeCalenderSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AdminCenterSettings.Calendarsharing.EnableCalendarSharing == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
