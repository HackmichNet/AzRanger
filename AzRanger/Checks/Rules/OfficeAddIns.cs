using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeAddIns", ScopeEnum.AAD, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Store")]
    [CISM365("2.9", "", Level.L1, "v1.5")]
    [RuleInfo("Word, Excel and Powerpoint addins are allowed", "Attackers can misuse add-ins for malicious purpose.", 4, null, null, @"Go to the Portal URL and uncheck ""Let users access the Office Store"" and ""Let users start trials on behalf of your organization"".")]
    class OfficeAddIns : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AdminCenterSettings.OfficeStoreSettings.LetUserAccessTheOfficeStore == false & tenant.TenantSettings.AdminCenterSettings.OfficeStoreSettings.LetUserStartTrialsInBehalfOfTheOrg == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
