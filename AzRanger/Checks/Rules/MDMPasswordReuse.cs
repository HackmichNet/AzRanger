using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMPasswordReuse", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesMenu/configurationProfiles")]
    // TODO
    class MDMPasswordReuse : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool androidPassed = false;
            bool androidPrivatePass = false;
            bool iosPassed = false;
            bool MacPassed = false;

            foreach(AndroidDeviceOwnerGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidDeviceOwnerGeneralDeviceConfigurations())
            {
                if( (config.passwordPreviousPasswordCountToBlock != null && (int)config.passwordPreviousPasswordCountToBlock >= 5) &&
                    config.workProfilePasswordPreviousPasswordCountToBlock != null && (int)config.workProfilePasswordPreviousPasswordCountToBlock >= 5)
                {
                    androidPassed = true;
                }
            }

            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if ((config.passwordPreviousPasswordBlockCount != null && (int)config.passwordPreviousPasswordBlockCount >= 5) &&
                    config.workProfilePasswordPreviousPasswordBlockCount != null && (int)config.workProfilePasswordPreviousPasswordBlockCount >= 5)
                {
                    androidPassed = true;
                }
            }
            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if(config.passcodePreviousPasscodeBlockCount != null && (int)config.passcodePreviousPasscodeBlockCount >= 5)
                {
                    iosPassed = true;
                }
            }
            foreach(MacOSGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations())
            {
                if(config.passwordPreviousPasswordBlockCount != null && (int)config.passwordPreviousPasswordBlockCount >= 5)
                {
                    MacPassed = true;
                }
            }

            // Check Assignemts in further development... I know I miss it here.
            if(androidPrivatePass && androidPassed && iosPassed && MacPassed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
