using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models.MSGraph.MDM
{
    // Personally Owned Device with work profile
    public class AndroidWorkProfileCompliancePolicy
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
        public object passwordMinimumLength { get; set; }
        public string passwordRequiredType { get; set; }
        public object passwordMinutesOfInactivityBeforeLock { get; set; }
        public object passwordExpirationDays { get; set; }
        public object passwordPreviousPasswordBlockCount { get; set; }
        public object passwordSignInFailureCountBeforeFactoryReset { get; set; }
        public bool securityPreventInstallAppsFromUnknownSources { get; set; }
        public bool securityDisableUsbDebugging { get; set; }
        public bool securityRequireVerifyApps { get; set; }
        public bool deviceThreatProtectionEnabled { get; set; }
        public string deviceThreatProtectionRequiredSecurityLevel { get; set; }
        public string advancedThreatProtectionRequiredSecurityLevel { get; set; }
        public bool securityBlockJailbrokenDevices { get; set; }
        public object osMinimumVersion { get; set; }
        public object osMaximumVersion { get; set; }
        public object minAndroidSecurityPatchLevel { get; set; }
        public object storageRequireEncryption { get; set; }
        public object securityRequireSafetyNetAttestationBasicIntegrity { get; set; }
        public object securityRequireSafetyNetAttestationCertifiedDevice { get; set; }
        public bool securityRequireGooglePlayServices { get; set; }
        public bool securityRequireUpToDateSecurityProviders { get; set; }
        public bool securityRequireCompanyPortalAppIntegrity { get; set; }
        public string securityRequiredAndroidSafetyNetEvaluationType { get; set; }
        public string assignmentsodatacontext { get; set; }
        public object[] assignments { get; set; }
    }

}
