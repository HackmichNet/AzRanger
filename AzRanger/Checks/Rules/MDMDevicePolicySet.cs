using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMDevicePolicySet", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesMenu/configurationProfiles")]
    // TODO
    class MDMDevicePolicySet : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {

            if((tenant.MDMSettings.ConfigurationPolicies != null && tenant.MDMSettings.ConfigurationPolicies.Count > 0) &&
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
