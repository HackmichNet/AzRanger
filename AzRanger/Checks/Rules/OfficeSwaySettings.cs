using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("OfficeSwaySettings", Scope.O365, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Sway")]
    [CISM365("2.11", "", Level.L1, "v1.4")]
    [RuleInfo("Users can share Sways with external users", "This increases the risk of data loss.", 3, "https://support.microsoft.com/en-us/office/administrator-settings-for-sway-d298e79b-b6ab-44c6-9239-aa312f5784d4", null, @"Go to the Portal URL and unmark the line ""Let people in your organization share their sways with people outside your organization""")]
    class OfficeSwaySettings : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AdminCenterSettings.SwaySettings.ExternalSharingEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
