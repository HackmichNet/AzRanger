using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("M365WebStorageProvider", ScopeEnum.Azure, MaturityLevel.Mature, "https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/OfficeOnline")]
    [CISM365("6.4", "", Level.L2, "v2.0")]
    [RuleInfo("Third-party storage services are not restriced in M365 web", "This increases the risk of unwanted data loss.", 1, null, null, @"Go to the portal link and ensure that ""Let users open files stored in third-party storage services in Microsoft 365 on the web"" is not checked.")]
    class M365WebStorageProvider : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.Officeonline.Enabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
