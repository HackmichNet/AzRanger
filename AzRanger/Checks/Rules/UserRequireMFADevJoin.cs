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
    [RuleInfo("UserRequireMFADevJoin")]
    internal class UserRequireMFADevJoin : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            SetReason("Data collection is not possible at the moment");
            return CheckResult.NotApplicable;
            /** TODO
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
