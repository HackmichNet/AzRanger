using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    // TODO
    class MDMLockScreen : BaseCheck
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
                if ((config.passwordMinutesOfInactivityBeforeScreenTimeout != null && (int)config.passwordMinutesOfInactivityBeforeScreenTimeout <= 5))
                {
                    androidPassed = true;
                }
            }

            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if (config.passwordMinutesOfInactivityBeforeScreenTimeout != null && (int)config.passwordMinutesOfInactivityBeforeScreenTimeout <= 5)
                {
                    androidPrivate = true;
                }
            }

            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if ((config.passcodeMinutesOfInactivityBeforeScreenTimeout != null && (int)config.passcodeMinutesOfInactivityBeforeScreenTimeout <= 5) &&
                    (config.passcodeMinutesOfInactivityBeforeLock != null && (int)config.passcodeMinutesOfInactivityBeforeLock == 0))
                {
                    iosPassed = true;
                }
            }
            foreach (MacOSGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations())
            {
                if ((config.passwordMinutesOfInactivityBeforeScreenTimeout != null && (int)config.passwordMinutesOfInactivityBeforeScreenTimeout <= 5)&&
                    (config.passwordMinutesOfInactivityBeforeLock != null && (int)config.passwordMinutesOfInactivityBeforeLock == 0))
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
