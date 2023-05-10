using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOExpireExternalLinks", ScopeEnum.SPO, MaturityLevel.Mature, @"https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/sharing")]
    [CISM365("6.3", "", Level.L1, "v1.5")]
    [RuleInfo("Anonymous shared link will not expire, or the expiry time is too long", "Users with access to the link have unlimited access to the data.", 4, "https://docs.microsoft.com/en-us/sharepoint/turn-external-sharing-on-or-off", null, @"Go to the SharePoint Online Admin panel and mark under ""Choose expiration and permissions options for Anyone links."" the line ""These links must expire within this many days"". Set the value to your needs.")]
    class SPOExpireExternalLinks : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // RequireAnonymousLinksExpireInDays = -1 => Link will never expire
            // Mark "Choose expiration and permissions options for Anyone links." is not set
            if (tenant.SharepointInformation.SharepointInternalInfos.RequireAnonymousLinksExpireInDays > 0 && 
                tenant.SharepointInformation.SharepointInternalInfos.RequireAnonymousLinksExpireInDays <= 30)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
