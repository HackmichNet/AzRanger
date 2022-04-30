using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph.MDM
{
    // Personally Owned Device with work profile
    public class AndroidWorkProfileGeneralDeviceConfiguration : IReporting
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string[] roleScopeTagIds { get; set; }
        public bool supportsScopeTags { get; set; }
        public object deviceManagementApplicabilityRuleOsEdition { get; set; }
        public object deviceManagementApplicabilityRuleOsVersion { get; set; }
        public object deviceManagementApplicabilityRuleDeviceMode { get; set; }
        public DateTime createdDateTime { get; set; }
        public object description { get; set; }
        public string displayName { get; set; }
        public int version { get; set; }
        public bool passwordBlockFaceUnlock { get; set; }
        public bool passwordBlockFingerprintUnlock { get; set; }
        public bool passwordBlockIrisUnlock { get; set; }
        public bool passwordBlockTrustAgents { get; set; }
        public object passwordExpirationDays { get; set; }
        public object passwordMinimumLength { get; set; }
        public object passwordMinutesOfInactivityBeforeScreenTimeout { get; set; }
        public object passwordPreviousPasswordBlockCount { get; set; }
        public object passwordSignInFailureCountBeforeFactoryReset { get; set; }
        public string passwordRequiredType { get; set; }
        public bool workProfileAllowAppInstallsFromUnknownSources { get; set; }
        public string workProfileDataSharingType { get; set; }
        public bool workProfileBlockNotificationsWhileDeviceLocked { get; set; }
        public bool workProfileBlockAddingAccounts { get; set; }
        public bool workProfileBluetoothEnableContactSharing { get; set; }
        public bool workProfileBlockScreenCapture { get; set; }
        public bool workProfileBlockCrossProfileCallerId { get; set; }
        public bool workProfileBlockCamera { get; set; }
        public bool workProfileBlockCrossProfileContactsSearch { get; set; }
        public bool workProfileBlockCrossProfileCopyPaste { get; set; }
        public string workProfileDefaultAppPermissionPolicy { get; set; }
        public bool workProfilePasswordBlockFaceUnlock { get; set; }
        public bool workProfilePasswordBlockFingerprintUnlock { get; set; }
        public bool workProfilePasswordBlockIrisUnlock { get; set; }
        public bool workProfilePasswordBlockTrustAgents { get; set; }
        public object workProfilePasswordExpirationDays { get; set; }
        public object workProfilePasswordMinimumLength { get; set; }
        public object workProfilePasswordMinNumericCharacters { get; set; }
        public object workProfilePasswordMinNonLetterCharacters { get; set; }
        public object workProfilePasswordMinLetterCharacters { get; set; }
        public object workProfilePasswordMinLowerCaseCharacters { get; set; }
        public object workProfilePasswordMinUpperCaseCharacters { get; set; }
        public object workProfilePasswordMinSymbolCharacters { get; set; }
        public object workProfilePasswordMinutesOfInactivityBeforeScreenTimeout { get; set; }
        public object workProfilePasswordPreviousPasswordBlockCount { get; set; }
        public object workProfilePasswordSignInFailureCountBeforeFactoryReset { get; set; }
        public string workProfilePasswordRequiredType { get; set; }
        public bool workProfileRequirePassword { get; set; }
        public bool securityRequireVerifyApps { get; set; }
        public object vpnAlwaysOnPackageIdentifier { get; set; }
        public bool vpnEnableAlwaysOnLockdownMode { get; set; }
        public bool workProfileAllowWidgets { get; set; }
        public bool workProfileBlockPersonalAppInstallsFromUnknownSources { get; set; }
        public string assignmentsodatacontext { get; set; }
        public object[] assignments { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", displayName, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", id, displayName);
        }
    }

}
