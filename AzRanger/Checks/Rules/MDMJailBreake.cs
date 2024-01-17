using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Very unreliable, do only check if a policy exist does not check if policy is assigned

namespace AzRanger.Checks.Rules
{
    // TODO
    class MDMJailBreake : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool androidPrivate = false;
            bool androidPass = false;
            bool iosPass = false;
            bool macPass = false;

            foreach (AndroidDeviceOwnerCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetAndroidDeviceOwnerCompliancePolicies())
            {
                if((policy.securityRequireSafetyNetAttestationBasicIntegrity != null && (bool)policy.securityRequireSafetyNetAttestationBasicIntegrity)
                    || (policy.securityRequireSafetyNetAttestationCertifiedDevice != null && (bool)policy.securityRequireSafetyNetAttestationCertifiedDevice))
                {
                    androidPass = true;
                }
            }
            foreach(AndroidWorkProfileCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetAndroidWorkProfileCompliancePolicies())
            {
                if ((policy.securityRequireSafetyNetAttestationBasicIntegrity != null && (bool)policy.securityRequireSafetyNetAttestationBasicIntegrity)
                    || (policy.securityRequireSafetyNetAttestationCertifiedDevice != null && (bool)policy.securityRequireSafetyNetAttestationCertifiedDevice))
                {
                    androidPrivate = true;
                }
            }
            foreach(IosCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetIosCompliancePolicies())
            {
                if(policy.securityBlockJailbrokenDevices != null && (bool)policy.securityBlockJailbrokenDevices)
                {
                    iosPass = true;
                }
            }
            foreach (MacOSCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetMacOSCompliancePolicies())
            {
                if (policy.systemIntegrityProtectionEnabled != null && (bool)policy.systemIntegrityProtectionEnabled)
                {
                    macPass = true;
                }
            }
            if(androidPass & androidPrivate & iosPass & macPass)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
