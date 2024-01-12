using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMPasswordComplexity", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesMenu/configurationProfiles")]
    // TODO
    class MDMPasswordComplexity : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            //TODO: Check if all devices are present + check if policies assigned
            bool androidPassed = false;
            bool androidPrivate = false;
            bool iosPassed = false;
            bool MacPassed = false;

            foreach (AndroidDeviceOwnerGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidDeviceOwnerGeneralDeviceConfigurations())
            {
                if ((config.passwordMinimumLength != null && (int)config.passwordMinimumLength >= 6))
                {
                    androidPassed = true;
                }
            }

            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if (config.passwordMinimumLength != null && (int)config.passwordMinimumLength >= 6)
                {
                    androidPrivate = true;
                }
            }

            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if (config.passcodeMinimumLength != null && (int)config.passcodeMinimumLength >= 6)
                {
                    iosPassed = true;
                }
            }
            foreach (MacOSGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations())
            {
                if (config.passwordMinimumLength != null && (int)config.passwordMinimumLength >= 6)
                {
                    MacPassed = true;
                }
            }

            // Check Assignemts in further development... I know I miss it here.
            if (androidPrivate && androidPassed && iosPassed && MacPassed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
