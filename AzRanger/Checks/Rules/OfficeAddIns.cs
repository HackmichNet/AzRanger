using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("OfficeAddIns", Scope.O365, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Store")]
    [RuleScore("Word, Excel and Powerpoint Addins are allowed", "Attackers can missues add-ins for their purpose", 4)]
    class OfficeAddIns : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.AdminCenterSettings.OfficeStoreSettings.LetUserAccessTheOfficeStore == false & tenant.AdminCenterSettings.OfficeStoreSettings.LetUserStartTrialsInBehalfOfTheOrg == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
