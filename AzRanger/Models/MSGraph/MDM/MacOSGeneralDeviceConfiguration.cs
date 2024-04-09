using AzRanger.Output;
using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models.MSGraph.MDM
{
    public class MacOSGeneralDeviceConfiguration : IReporting
    {
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
        public string compliantAppListType { get; set; }
        public object[] emailInDomainSuffixes { get; set; }
        public bool passwordBlockSimple { get; set; }
        public object passwordExpirationDays { get; set; }
        public object passwordMinimumCharacterSetCount { get; set; }
        public object passwordMinimumLength { get; set; }
        public object passwordMinutesOfInactivityBeforeLock { get; set; }
        public object passwordMinutesOfInactivityBeforeScreenTimeout { get; set; }
        public object passwordPreviousPasswordBlockCount { get; set; }
        public string passwordRequiredType { get; set; }
        public bool passwordRequired { get; set; }
        public object passwordMaximumAttemptCount { get; set; }
        public object passwordMinutesUntilFailedLoginReset { get; set; }
        public bool keychainBlockCloudSync { get; set; }
        public bool safariBlockAutofill { get; set; }
        public bool cameraBlocked { get; set; }
        public bool iTunesBlockMusicService { get; set; }
        public bool spotlightBlockInternetResults { get; set; }
        public bool keyboardBlockDictation { get; set; }
        public bool definitionLookupBlocked { get; set; }
        public bool appleWatchBlockAutoUnlock { get; set; }
        public bool iTunesBlockFileSharing { get; set; }
        public bool iCloudBlockDocumentSync { get; set; }
        public bool iCloudBlockMail { get; set; }
        public bool iCloudBlockAddressBook { get; set; }
        public bool iCloudBlockCalendar { get; set; }
        public bool iCloudBlockReminders { get; set; }
        public bool iCloudBlockBookmarks { get; set; }
        public bool iCloudBlockNotes { get; set; }
        public bool airDropBlocked { get; set; }
        public bool passwordBlockModification { get; set; }
        public bool passwordBlockFingerprintUnlock { get; set; }
        public bool passwordBlockAutoFill { get; set; }
        public bool passwordBlockProximityRequests { get; set; }
        public bool passwordBlockAirDropSharing { get; set; }
        public object softwareUpdatesEnforcedDelayInDays { get; set; }
        public object updateDelayPolicy { get; set; }
        public bool contentCachingBlocked { get; set; }
        public bool iCloudBlockPhotoLibrary { get; set; }
        public bool screenCaptureBlocked { get; set; }
        public bool classroomAppBlockRemoteScreenObservation { get; set; }
        public bool classroomAppForceUnpromptedScreenObservation { get; set; }
        public bool classroomForceAutomaticallyJoinClasses { get; set; }
        public bool classroomForceRequestPermissionToLeaveClasses { get; set; }
        public bool classroomForceUnpromptedAppAndDeviceLock { get; set; }
        public bool iCloudBlockActivityContinuation { get; set; }
        public bool addingGameCenterFriendsBlocked { get; set; }
        public bool gameCenterBlocked { get; set; }
        public bool multiplayerGamingBlocked { get; set; }
        public bool wallpaperModificationBlocked { get; set; }
        public bool eraseContentAndSettingsBlocked { get; set; }
        public object softwareUpdateMajorOSDeferredInstallDelayInDays { get; set; }
        public object softwareUpdateMinorOSDeferredInstallDelayInDays { get; set; }
        public object softwareUpdateNonOSDeferredInstallDelayInDays { get; set; }
        public object[] compliantAppsList { get; set; }
        public object[] privacyAccessControls { get; set; }
        public object[] assignments { get; set; }

        public string PrintConsole()
        {
            return this.displayName + " - " + this.id;
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", this.id, this.displayName);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.displayName);
        }
    }
}
