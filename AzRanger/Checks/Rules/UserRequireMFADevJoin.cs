using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRequireMFADevJoin", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_Devices/DevicesMenuBlade/~/DeviceSettings/menuId~/null")]
    [CISAZ("1.22", "", Level.L1, "v2.0")]
    [RuleInfo("Users do not require MFA to join devices", "Joining a device is often used by threat actors to persist its access.", 0, null, null, @"1. Go to Azure Active Directory</br> 2. Go to Devices</br> 3. Go to Device settings</br> 4. Set Require Multi-Factor Authentication to register or join devices with Azure AD to Yes")]
    internal class UserRequireMFADevJoin : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            SetReason("Data collection is not possible at the moment");
            return CheckResult.NotApplicable;
            /**
            if(tenant.DeviceRegistrationPolicy.azureADJoin.appliesTo == "0")
            {
                this.SetReason("Joining devices for users is disabled.");
                return CheckResult.NotApplicable;
            }
            if (tenant.DeviceRegistrationPolicy.multiFactorAuthConfiguration == "1")
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
            **/
        }
    }
}
