using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    // TODO
    class MDMDevicePolicySet : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {

            if ((tenant.MDMSettings.ConfigurationPolicies != null && tenant.MDMSettings.ConfigurationPolicies.Count > 0) &&
                (tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidDeviceOwnerGeneralDeviceConfigurations().Count > 0) &&
                (tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations().Count > 0) &&
                (tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations().Count > 0) &&
                (tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration().Count > 0)
                )
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
