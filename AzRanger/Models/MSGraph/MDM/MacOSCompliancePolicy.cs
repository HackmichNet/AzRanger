using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph.MDM
{
    public class MacOSCompliancePolicy
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string[] roleScopeTagIds { get; set; }
        public string id { get; set; }
        public DateTime createdDateTime { get; set; }
        public object description { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string displayName { get; set; }
        public int version { get; set; }
        public bool passwordRequired { get; set; }
        public bool passwordBlockSimple { get; set; }
        public object passwordExpirationDays { get; set; }
        public object passwordMinimumLength { get; set; }
        public object passwordMinutesOfInactivityBeforeLock { get; set; }
        public object passwordPreviousPasswordBlockCount { get; set; }
        public object passwordMinimumCharacterSetCount { get; set; }
        public string passwordRequiredType { get; set; }
        public object osMinimumVersion { get; set; }
        public object osMaximumVersion { get; set; }
        public object osMinimumBuildVersion { get; set; }
        public object osMaximumBuildVersion { get; set; }
        public object systemIntegrityProtectionEnabled { get; set; }
        public bool deviceThreatProtectionEnabled { get; set; }
        public string deviceThreatProtectionRequiredSecurityLevel { get; set; }
        public string advancedThreatProtectionRequiredSecurityLevel { get; set; }
        public object storageRequireEncryption { get; set; }
        public string gatekeeperAllowedAppSource { get; set; }
        public bool firewallEnabled { get; set; }
        public bool firewallBlockAllIncoming { get; set; }
        public bool firewallEnableStealthMode { get; set; }
        public object[] assignments { get; set; }
    }

}
