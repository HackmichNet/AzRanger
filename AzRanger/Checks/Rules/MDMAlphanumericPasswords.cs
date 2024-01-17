using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    class MDMAlphanumericPasswords : BaseCheck
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
                if ((config.passwordRequiredType != null && (String)config.passwordRequiredType=="alphanumeric") ||
                    config.workProfilePasswordRequiredType != null && (String)config.workProfilePasswordRequiredType == "alphanumeric")
                {
                    androidPassed = true;
                }
            }

            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if ((config.passwordRequiredType != null && (String)config.passwordRequiredType == "alphanumeric") ||
                    config.workProfilePasswordRequiredType != null && (String)config.workProfilePasswordRequiredType == "alphanumeric")
                {
                    androidPrivate = true;
                }
            }

            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if ((config.passcodeRequiredType != null && (String)config.passcodeRequiredType == "alphanumeric"))
                {
                    iosPassed = true;
                }
            }
            foreach (MacOSGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations())
            {
                if (config.passwordRequiredType != null && (String)config.passwordRequiredType == "alphanumeric")
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
