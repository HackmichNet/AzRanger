﻿using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models.MSGraph.MDM
{
    public class AndroidDeviceOwnerCompliancePolicy
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
        public bool deviceThreatProtectionEnabled { get; set; }
        public string deviceThreatProtectionRequiredSecurityLevel { get; set; }
        public string advancedThreatProtectionRequiredSecurityLevel { get; set; }
        public object securityRequireSafetyNetAttestationBasicIntegrity { get; set; }
        public object securityRequireSafetyNetAttestationCertifiedDevice { get; set; }
        public object osMinimumVersion { get; set; }
        public object osMaximumVersion { get; set; }
        public object minAndroidSecurityPatchLevel { get; set; }
        public object passwordRequired { get; set; }
        public object passwordMinimumLength { get; set; }
        public object passwordMinimumLetterCharacters { get; set; }
        public object passwordMinimumLowerCaseCharacters { get; set; }
        public object passwordMinimumNonLetterCharacters { get; set; }
        public object passwordMinimumNumericCharacters { get; set; }
        public object passwordMinimumSymbolCharacters { get; set; }
        public object passwordMinimumUpperCaseCharacters { get; set; }
        public string passwordRequiredType { get; set; }
        public object passwordMinutesOfInactivityBeforeLock { get; set; }
        public object passwordExpirationDays { get; set; }
        public object passwordPreviousPasswordCountToBlock { get; set; }
        public object storageRequireEncryption { get; set; }
        public object securityRequireIntuneAppIntegrity { get; set; }
        public Assignment[] assignments { get; set; }
    }
}
