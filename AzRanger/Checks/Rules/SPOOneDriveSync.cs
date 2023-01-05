using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOOneDriveSync", ScopeEnum.SPO, MaturityLevel.Mature, "https://<YOUR-DOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/settings/ODBSync")]
    [CISM365("6.2", "", Level.L2, "v1.4")]
    [RuleInfo("OneDrive can be synced with unmanaged devices", "This can lead to data loss.", 7, null, null, @"Go to the Portal URL and add your on-premise domain GUID (Get-ADDomain) to the list of ""Allow syncing only on computers joined to specific domains"". If you do not have an on-premise domain, use conditional access to block access to OneDrive from unmanaged devices.")]
    class SPOOneDriveSync : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.IsUnmanagedSyncClientForTenantRestricted == true &&
               tenant.SharepointInformation.SharepointInternalInfos.BlockMacSync == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
