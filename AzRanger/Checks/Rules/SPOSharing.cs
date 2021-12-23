using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("SPOSharing", Scope.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [RuleScore("Sharepoint Online does allow sharing documents with arbitrary domains", "This can be lead to data leakage", 4, "https://www.michev.info/Blog/Post/969/you-can-now-control-external-sharing-in-sharepoint-online-per-domain")]
    class SPOSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.SharingDomainRestrictionMode != 0)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
