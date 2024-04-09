using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;

namespace AzRanger.Checks.Rules
{
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
