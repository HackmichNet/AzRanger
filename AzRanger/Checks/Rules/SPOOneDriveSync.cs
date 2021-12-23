using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("SPOOneDriveSync", Scope.SPO, MaturityLevel.Mature, "https://<YOUR-DOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/settings/ODBSync")]
    [RuleScore("OneDrive can be synced with unmanaged devices", "The security of unmanaged devices cannot be verified", 7)]
    class SPOOneDriveSync : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.IsUnmanagedSyncClientForTenantRestricted == true &&
               tenant.SharepointInformation.SharepointInternalInfos.BlockMacSync == true)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
