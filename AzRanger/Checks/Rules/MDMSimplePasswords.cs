﻿using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    // TODO
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
