using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
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
