using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeCalenderSharing", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/Calendar")]
    [CISM365("2.3", "", Level.L2, "v2.0")]
    [RuleInfo("OfficeCalenderSharing")]
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
