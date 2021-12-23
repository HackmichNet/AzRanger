using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph.MDM
{
    public class MobileDeviceCompliancePolicies
    {
        private readonly List<AndroidDeviceOwnerCompliancePolicy> androidDeviceOwnerCompliancePolicy;
        private readonly List<AndroidWorkProfileCompliancePolicy> androidWorkProfileCompliancePolicies;
        private readonly List<IosCompliancePolicy> iosCompliancePolicy;
        private readonly List<MacOSCompliancePolicy> macOSCompliancePolicy;

        public MobileDeviceCompliancePolicies(List<AndroidWorkProfileCompliancePolicy> androidWorkProfileCompliancePolicies, List<AndroidDeviceOwnerCompliancePolicy> androidDeviceOwnerCompliancePolicy, List<IosCompliancePolicy> iosCompliancePolicy, List<MacOSCompliancePolicy> macOSCompliancePolicy)
        {
            this.androidWorkProfileCompliancePolicies = androidWorkProfileCompliancePolicies;
            this.androidDeviceOwnerCompliancePolicy = androidDeviceOwnerCompliancePolicy;
            this.iosCompliancePolicy = iosCompliancePolicy;
            this.macOSCompliancePolicy = macOSCompliancePolicy;
        }

        public List<AndroidWorkProfileCompliancePolicy> GetAndroidWorkProfileCompliancePolicies()
        {
            return this.androidWorkProfileCompliancePolicies;
        }

        public List<AndroidDeviceOwnerCompliancePolicy> GetAndroidDeviceOwnerCompliancePolicies()
        {
            return this.androidDeviceOwnerCompliancePolicy;
        }

        public List<IosCompliancePolicy> GetIosCompliancePolicies()
        {
            return this.iosCompliancePolicy;
        }

        public List<MacOSCompliancePolicy> GetMacOSCompliancePolicies()
        {
            return this.macOSCompliancePolicy;
        }
    }
}
