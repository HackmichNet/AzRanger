using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("OfficeSwaySettings", Scope.O365, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Sway")]
    [RuleScore("Users can share Sways with external users", "This can lead to data loss", 3, "https://support.microsoft.com/en-us/office/administrator-settings-for-sway-d298e79b-b6ab-44c6-9239-aa312f5784d4")]
    class OfficeSwaySettings : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AdminCenterSettings.SwaySettings.ExternalSharingEnabled == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
