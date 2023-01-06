using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOLegacyAuth", ScopeEnum.SPO, MaturityLevel.Mature, "https://<YOURDOMAIN>-admin.sharepoint.com/_layouts/15/online/AdminHome.aspx#/accessControl")]
    [CISM365("1.3", "", Level.L1, "v1.5")]
    [RuleInfo("SharePoint Online allows to use legacy authentication protocol", "This increases the attack surface against user credentials.", 10, null, null, @"Go tot the Portal URL and under ""Apps that don't use modern authentication"" set the value to ""Block access"".")]
    class SPOLegacyAuth : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
            {
                this.SetReason("Security Defaults are enabled. This disabled legacy authentication protocols.");
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
