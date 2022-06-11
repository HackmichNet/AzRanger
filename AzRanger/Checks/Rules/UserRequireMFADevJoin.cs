using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserRequireMFADevJoin", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_Devices/DevicesMenuBlade/~/DeviceSettings/menuId~/null")]
    [RuleScore("Users do not require MFA to join devices", "This setting makes it more dificult for an attacker to persist his access with a device join", 0)]
    internal class UserRequireMFADevJoin : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            return CheckResult.Passed;
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
