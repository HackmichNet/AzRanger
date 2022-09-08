using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOLegacyAuth", Scope.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/accessControl")]
    [CISM365("1.4", "", Level.L1, "v1.4")]
    [RuleInfo("SharePoint Online allows to use legacy authentication protocol", "This increases the attack surface against user credentials.", 10, null, null, @"Go tot the Portal URL and under ""Apps that don't use modern authentication"" set the value to ""Block access"".")]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
