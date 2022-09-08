using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMSimplePasswords", Scope.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesMenu/configurationProfiles")]
    [CISM365("7.10", "Ensure that mobile devices require complex passwords (Simple Passwords = Blocked) ", Level.L1, "v1.4")]
    class MDMSimplePasswords : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passIOS = false;
            bool passMac = false;

            foreach (IosGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetIosGeneralDeviceConfigurations())
            {
                if (config.passcodeBlockSimple)
                {
                    passIOS = true;
                }
            }
            foreach (MacOSGeneralDeviceConfiguration config in tenant.MDMSettings.MobileDeviceConfigurations.GetMacOSGeneralDeviceConfigurations())
            {
                if (config.passwordBlockSimple)
                {
                    passIOS = true;
                }
            }
            if(passIOS & passMac)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
