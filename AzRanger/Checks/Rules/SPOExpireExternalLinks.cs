using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOExpireExternalLinks", ScopeEnum.SPO, MaturityLevel.Mature, @"https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [CISM365("6.3", "", Level.L1, "v2.0")]
    
    class SPOExpireExternalLinks : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // RequireAnonymousLinksExpireInDays = -1 => Link will never expire
            // Mark "Choose expiration and permissions options for Anyone links." is not set
            if (tenant.SharePointInformation.SharePointInternalInfos.RequireAnonymousLinksExpireInDays > 0 && 
                tenant.SharePointInformation.SharePointInternalInfos.RequireAnonymousLinksExpireInDays <= 30)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
