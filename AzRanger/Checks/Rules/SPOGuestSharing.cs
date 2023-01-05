using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOGuestSharing", ScopeEnum.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [CISM365("3.6", "", Level.L2, "v1.4")]
    [RuleInfo("Guests can share data, they do not own", "This increases the risk that you lose control over your data in SharePoint.", 4, null, null, @"Go to the Portal URL and under ""More external sharing settings"" unmark the section ""Allow guests to share items they don't own""")]
    class SPOGuestSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.PreventExternalUsersFromResharing == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
