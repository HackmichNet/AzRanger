using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMDeviceWipe", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesMenu/configurationProfiles")]
    [CISM365("7.5", "Ensure mobile devices are set to wipe on multiple sign-in failures to prevent brute force compromise", Level.L2, "v1.4")]
    class MDMDeviceWipe : BaseCheck
    {
        // Only checks if a configuration exist. 
        public override CheckResult Audit(Tenant tenant)
        {
            bool androidPrivate = false;
            bool androidPass = false;
            bool iosPass = false;
            
            foreach (AndroidDeviceOwnerGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidDeviceOwnerGeneralDeviceConfigurations())
            {
                if ((config.passwordSignInFailureCountBeforeFactoryReset != null && (long)config.passwordSignInFailureCountBeforeFactoryReset <= 10))
                {
                    androidPass = true;
                }
            }
            foreach (AndroidWorkProfileGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetAndroidWorkProfileGeneralDeviceConfiguration())
            {
                if ((config.passwordSignInFailureCountBeforeFactoryReset != null && (int)config.passwordSignInFailureCountBeforeFactoryReset <= 10))
                {
                    androidPrivate = true;
                }
            }
            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if (config.passcodeSignInFailureCountBeforeWipe != null && (int)config.passcodeSignInFailureCountBeforeWipe<=10)
                {
                    iosPass = true;
                }
            }
            
            if (androidPass & androidPrivate & iosPass)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
            
            
        
        }
    }
}
