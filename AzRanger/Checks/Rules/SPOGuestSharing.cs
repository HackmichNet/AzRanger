using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOGuestSharing", ScopeEnum.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]    
    class SPOGuestSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SharePointInformation.SharePointInternalInfos.PreventExternalUsersFromResharing == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
