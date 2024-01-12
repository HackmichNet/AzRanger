using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOOneDriveSync", ScopeEnum.SPO, MaturityLevel.Mature, "https://<YOUR-DOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/settings/ODBSync")]
    [CISM365("6.2", "", Level.L2, "v2.0")]
    
    class SPOOneDriveSync : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharePointInformation.SharePointInternalInfos.IsUnmanagedSyncClientForTenantRestricted == true &&
               tenant.SharePointInformation.SharePointInternalInfos.BlockMacSync == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
