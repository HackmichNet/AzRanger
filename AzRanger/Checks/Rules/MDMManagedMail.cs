using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMManagedMail", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesComplianceMenu/policies")]
    // TODO
    class MDMManagedMail : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool iosPass = false;



            foreach (IosCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetIosCompliancePolicies())
            {
                if (policy.managedEmailProfileRequired)
                {
                    iosPass = true;
                }
            }

            if (iosPass)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
