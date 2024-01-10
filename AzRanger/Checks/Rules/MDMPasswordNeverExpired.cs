using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMPasswordNeverExpired", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesMenu/configurationProfiles")]
    [CISM365("7.3", "Ensure that mobile devices are set to never expire passwords", Level.L1, "v1.4")]
    // TODO
    class MDMPasswordNeverExpired : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            //TODO: Check if all devices are present + check if policies assigned
            bool androidPassed = true;
            bool androidPrivate = true;
            bool iosPassed = true;
            bool MacPassed = true;

            foreach (AndroidDeviceOwnerGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidDeviceOwnerGeneralDeviceConfigurations())
            {
                if (config.passwordExpirationDays != null | config.workProfilePasswordExpirationDays != null)
                {
                    androidPassed = false;
                    this.AddAffectedEntity(config);
                }
            }

            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if (config.passwordExpirationDays != null | config.workProfilePasswordExpirationDays != null)
                {
                    androidPrivate = false;
                    this.AddAffectedEntity(config);
                }
            }

            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if (config.passcodeExpirationDays != null)
                {
                    iosPassed = false;
                    this.AddAffectedEntity(config);
                }
            }
            foreach (MacOSGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations())
            {
                if (config.passwordExpirationDays != null)
                {
                    MacPassed = false;
                    this.AddAffectedEntity(config);
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
