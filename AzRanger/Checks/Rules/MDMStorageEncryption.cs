using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("MDMStorageEncryption", ScopeEnum.MDM, MaturityLevel.Tentative, "https://endpoint.microsoft.com/?ref=AdminCenter#blade/Microsoft_Intune_DeviceSettings/DevicesComplianceMenu/policies")]
    // TODO
    class MDMStorageEncryption : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool androidPrivate = false;
            bool androidPass = false;
            bool macPass = false;

            foreach (AndroidDeviceOwnerCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetAndroidDeviceOwnerCompliancePolicies())
            {
                if (policy.storageRequireEncryption != null && (bool)policy.storageRequireEncryption == true)
                {
                    androidPass = true;
                }
            }
            foreach (AndroidWorkProfileCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetAndroidWorkProfileCompliancePolicies())
            {
                if (policy.storageRequireEncryption != null && (bool)policy.storageRequireEncryption == true)
                {
                    androidPrivate = true;
                }
            }
            
            foreach (MacOSCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetMacOSCompliancePolicies())
            {
                if (policy.storageRequireEncryption != null && (bool)policy.storageRequireEncryption == true)
                {
                    macPass = true;
                }
            }
            if (androidPass & androidPrivate & macPass)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
