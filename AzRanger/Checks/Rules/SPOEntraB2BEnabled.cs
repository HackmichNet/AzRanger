using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOEntraB2BEnabled", ScopeEnum.SPO, MaturityLevel.Mature)]
    [CISM365("2.12", "", Level.L1, "v2.0")]
    [RuleInfo("SharePoint and OneDrive are not integrated to Entra ID B2B", "This makes the auditing of Guest access harder and does not allow to apply Azure Access Policies to them.", 3, "https://learn.microsoft.com/en-us/sharepoint/sharepoint-azureb2b-integration#enabling-the-integration", null,  @"You can enable Azure AD B2B Integration by using the following command ""Set-SPOTenant -EnableAzureADB2BIntegration $true"".")]
    class SPOEntraB2BEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
            {
                this.SetReason("Security Defaults are enabled. This disabled legacy authentication protocols.");
                return CheckResult.NotApplicable;
            }
            if (tenant.SharePointInformation.SharePointInternalInfos.LegacyAuthProtocolsEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
