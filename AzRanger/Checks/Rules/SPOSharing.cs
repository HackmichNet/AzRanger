using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOSharing", ScopeEnum.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [CISM365("6.1", "", Level.L2, "v2.0")]
    [RuleInfo("SharePoint Online allows sharing documents with arbitrary domains", "This can lead to data leakage.", 4, "https://www.michev.info/Blog/Post/969/you-can-now-control-external-sharing-in-sharepoint-online-per-domain", null, @"Go to your SharePoint Admin Center and check the sharing settings carefully. You can find more information under the reference section.")]
    class SPOSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.SharingCapability == 0)
            {
                return CheckResult.NoFinding;
            }
            if(tenant.SharepointInformation.SharepointInternalInfos.SharingDomainRestrictionMode != 0)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
