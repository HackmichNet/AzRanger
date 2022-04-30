using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("SPOLegacyAuth", Scope.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/accessControl")]
    [RuleScore("SharePoint Online allows to use legacy authentication protocol", "Legacy authentication protocols do not supper strong authentication schems like MFA", 10)]
    class SPOLegacyAuth : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SecurityDefaults.securityDefaultsEnabled == true)
            {
                return CheckResult.NotApplicable;
            }
            if (tenant.SharepointInformation.SharepointInternalInfos.LegacyAuthProtocolsEnabled == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
