using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("SPOModernAuthDisabled", Scope.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [RuleScore("Modern auth for Sharepoint is disabled", "User cannot access SharePoint using modern authentication", 1, "")]
    class SPOModernAuthDisabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharepointInformation.SharepointInternalInfos.OfficeClientADALDisabled == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
