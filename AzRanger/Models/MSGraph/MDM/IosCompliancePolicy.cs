using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph.MDM
{
    public class IosCompliancePolicy
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty(PropertyName = "@odata.type")]
        public string odatatype { get; set; }
        public string[] roleScopeTagIds { get; set; }
        public string id { get; set; }
        public DateTime createdDateTime { get; set; }
        public object description { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string displayName { get; set; }
        public int version { get; set; }
        public bool passcodeBlockSimple { get; set; }
        public object passcodeExpirationDays { get; set; }
        public object passcodeMinimumLength { get; set; }
        public object passcodeMinutesOfInactivityBeforeLock { get; set; }
        public object passcodeMinutesOfInactivityBeforeScreenTimeout { get; set; }
        public object passcodePreviousPasscodeBlockCount { get; set; }
        public object passcodeMinimumCharacterSetCount { get; set; }
        public string passcodeRequiredType { get; set; }
        public bool passcodeRequired { get; set; }
        public object osMinimumVersion { get; set; }
        public object osMaximumVersion { get; set; }
        public object osMinimumBuildVersion { get; set; }
        public object osMaximumBuildVersion { get; set; }
        public object securityBlockJailbrokenDevices { get; set; }
        public bool deviceThreatProtectionEnabled { get; set; }
        public string deviceThreatProtectionRequiredSecurityLevel { get; set; }
        public string advancedThreatProtectionRequiredSecurityLevel { get; set; }
        public bool managedEmailProfileRequired { get; set; }
        public object[] restrictedApps { get; set; }
        public object[] assignments { get; set; }
    }

}
