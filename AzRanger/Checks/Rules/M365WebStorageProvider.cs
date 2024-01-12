using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("M365WebStorageProvider", ScopeEnum.Azure, MaturityLevel.Mature, "https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/OfficeOnline")]    
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
