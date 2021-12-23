using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("SPOExpireExternalLinks", Scope.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [RuleScore("Anonymous shared link will not expire or the expire time is too long", "User or attack can persist access to data", 4, "https://docs.microsoft.com/en-us/sharepoint/turn-external-sharing-on-or-off")]
    class SPOExpireExternalLinks : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // RequireAnonymousLinksExpireInDays = -1 => Link will never expire
            // Mark "Choose expiration and permissions options for Anyone links." is not set
            if (tenant.SharepointInformation.SharepointInternalInfos.RequireAnonymousLinksExpireInDays > 0 && 
                tenant.SharepointInformation.SharepointInternalInfos.RequireAnonymousLinksExpireInDays <= 30)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
