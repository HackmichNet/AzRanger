using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("SPOGuestSharing", Scope.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [RuleScore("Guest can share data, they do not own", "Only internal user should be allowed to share data, if wanted.", 4)]
    class SPOGuestSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.PreventExternalUsersFromResharing == true)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
