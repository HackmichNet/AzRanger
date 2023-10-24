using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzRanger.Output;

namespace AzRanger.Models.MSGraph.MDM
{
    public class IosGeneralDeviceConfiguration : IReporting
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
        public bool accountBlockModification { get; set; }
        public bool activationLockAllowWhenSupervised { get; set; }
        public bool airDropBlocked { get; set; }
        public bool airDropForceUnmanagedDropTarget { get; set; }
        public bool airPlayForcePairingPasswordForOutgoingRequests { get; set; }
        public bool appleWatchBlockPairing { get; set; }
        public bool appleWatchForceWristDetection { get; set; }
        public bool appleNewsBlocked { get; set; }
        public string appsVisibilityListType { get; set; }
        public bool appStoreBlockAutomaticDownloads { get; set; }
        public bool appStoreBlocked { get; set; }
        public bool appStoreBlockInAppPurchases { get; set; }
        public bool appStoreBlockUIAppInstallation { get; set; }
        public bool appStoreRequirePassword { get; set; }
        public bool autoFillForceAuthentication { get; set; }
        public bool bluetoothBlockModification { get; set; }
        public bool cameraBlocked { get; set; }
        public bool cellularBlockDataRoaming { get; set; }
        public bool cellularBlockGlobalBackgroundFetchWhileRoaming { get; set; }
        public bool cellularBlockPerAppDataModification { get; set; }
        public bool cellularBlockPersonalHotspot { get; set; }
        public bool cellularBlockPlanModification { get; set; }
        public bool cellularBlockVoiceRoaming { get; set; }
        public bool certificatesBlockUntrustedTlsCertificates { get; set; }
        public bool classroomAppBlockRemoteScreenObservation { get; set; }
        public bool classroomAppForceUnpromptedScreenObservation { get; set; }
        public bool classroomForceAutomaticallyJoinClasses { get; set; }
        public bool classroomForceUnpromptedAppAndDeviceLock { get; set; }
        public string compliantAppListType { get; set; }
        public bool configurationProfileBlockChanges { get; set; }
        public bool definitionLookupBlocked { get; set; }
        public bool deviceBlockEnableRestrictions { get; set; }
        public bool deviceBlockEraseContentAndSettings { get; set; }
        public bool deviceBlockNameModification { get; set; }
        public bool diagnosticDataBlockSubmission { get; set; }
        public bool diagnosticDataBlockSubmissionModification { get; set; }
        public bool documentsBlockManagedDocumentsInUnmanagedApps { get; set; }
        public bool documentsBlockUnmanagedDocumentsInManagedApps { get; set; }
        public object[] emailInDomainSuffixes { get; set; }
        public bool enterpriseAppBlockTrust { get; set; }
        public bool enterpriseAppBlockTrustModification { get; set; }
        public bool esimBlockModification { get; set; }
        public bool faceTimeBlocked { get; set; }
        public bool findMyFriendsBlocked { get; set; }
        public bool gamingBlockGameCenterFriends { get; set; }
        public bool gamingBlockMultiplayer { get; set; }
        public bool gameCenterBlocked { get; set; }
        public bool hostPairingBlocked { get; set; }
        public bool iBooksStoreBlocked { get; set; }
        public bool iBooksStoreBlockErotica { get; set; }
        public bool iCloudBlockActivityContinuation { get; set; }
        public bool iCloudBlockBackup { get; set; }
        public bool iCloudBlockDocumentSync { get; set; }
        public bool iCloudBlockManagedAppsSync { get; set; }
        public bool iCloudBlockPhotoLibrary { get; set; }
        public bool iCloudBlockPhotoStreamSync { get; set; }
        public bool iCloudBlockSharedPhotoStream { get; set; }
        public bool iCloudRequireEncryptedBackup { get; set; }
        public bool iTunesBlockExplicitContent { get; set; }
        public bool iTunesBlockMusicService { get; set; }
        public bool iTunesBlockRadio { get; set; }
        public bool keyboardBlockAutoCorrect { get; set; }
        public bool keyboardBlockDictation { get; set; }
        public bool keyboardBlockPredictive { get; set; }
        public bool keyboardBlockShortcuts { get; set; }
        public bool keyboardBlockSpellCheck { get; set; }
        public bool kioskModeAllowAssistiveSpeak { get; set; }
        public bool kioskModeAllowAssistiveTouchSettings { get; set; }
        public bool kioskModeAllowAutoLock { get; set; }
        public bool kioskModeBlockAutoLock { get; set; }
        public bool kioskModeAllowColorInversionSettings { get; set; }
        public bool kioskModeAllowRingerSwitch { get; set; }
        public bool kioskModeBlockRingerSwitch { get; set; }
        public bool kioskModeAllowScreenRotation { get; set; }
        public bool kioskModeBlockScreenRotation { get; set; }
        public bool kioskModeAllowSleepButton { get; set; }
        public bool kioskModeBlockSleepButton { get; set; }
        public bool kioskModeAllowTouchscreen { get; set; }
        public bool kioskModeBlockTouchscreen { get; set; }
        public bool kioskModeEnableVoiceControl { get; set; }
        public bool kioskModeAllowVoiceControlModification { get; set; }
        public bool kioskModeAllowVoiceOverSettings { get; set; }
        public bool kioskModeAllowVolumeButtons { get; set; }
        public bool kioskModeBlockVolumeButtons { get; set; }
        public bool kioskModeAllowZoomSettings { get; set; }
        public object kioskModeAppStoreUrl { get; set; }
        public object kioskModeBuiltInAppId { get; set; }
        public bool kioskModeRequireAssistiveTouch { get; set; }
        public bool kioskModeRequireColorInversion { get; set; }
        public bool kioskModeRequireMonoAudio { get; set; }
        public bool kioskModeRequireVoiceOver { get; set; }
        public bool kioskModeRequireZoom { get; set; }
        public object kioskModeManagedAppId { get; set; }
        public bool lockScreenBlockControlCenter { get; set; }
        public bool lockScreenBlockNotificationView { get; set; }
        public bool lockScreenBlockPassbook { get; set; }
        public bool lockScreenBlockTodayView { get; set; }
        public object mediaContentRatingAustralia { get; set; }
        public object mediaContentRatingCanada { get; set; }
        public object mediaContentRatingFrance { get; set; }
        public object mediaContentRatingGermany { get; set; }
        public object mediaContentRatingIreland { get; set; }
        public object mediaContentRatingJapan { get; set; }
        public object mediaContentRatingNewZealand { get; set; }
        public object mediaContentRatingUnitedKingdom { get; set; }
        public object mediaContentRatingUnitedStates { get; set; }
        public string mediaContentRatingApps { get; set; }
        public bool messagesBlocked { get; set; }
        public bool notificationsBlockSettingsModification { get; set; }
        public bool passcodeBlockFingerprintUnlock { get; set; }
        public bool passcodeBlockFingerprintModification { get; set; }
        public bool passcodeBlockModification { get; set; }
        public bool passcodeBlockSimple { get; set; }
        public object passcodeExpirationDays { get; set; }
        public object passcodeMinimumLength { get; set; }
        public object passcodeMinutesOfInactivityBeforeLock { get; set; }
        public object passcodeMinutesOfInactivityBeforeScreenTimeout { get; set; }
        public object passcodeMinimumCharacterSetCount { get; set; }
        public object passcodePreviousPasscodeBlockCount { get; set; }
        public object passcodeSignInFailureCountBeforeWipe { get; set; }
        public string passcodeRequiredType { get; set; }
        public bool passcodeRequired { get; set; }
        public bool podcastsBlocked { get; set; }
        public bool proximityBlockSetupToNewDevice { get; set; }
        public bool safariBlockAutofill { get; set; }
        public bool safariBlockJavaScript { get; set; }
        public bool safariBlockPopups { get; set; }
        public bool safariBlocked { get; set; }
        public string safariCookieSettings { get; set; }
        public object[] safariManagedDomains { get; set; }
        public object[] safariPasswordAutoFillDomains { get; set; }
        public bool safariRequireFraudWarning { get; set; }
        public bool screenCaptureBlocked { get; set; }
        public bool siriBlocked { get; set; }
        public bool siriBlockedWhenLocked { get; set; }
        public bool siriBlockUserGeneratedContent { get; set; }
        public bool siriRequireProfanityFilter { get; set; }
        public object softwareUpdatesEnforcedDelayInDays { get; set; }
        public bool softwareUpdatesForceDelayed { get; set; }
        public bool spotlightBlockInternetResults { get; set; }
        public bool voiceDialingBlocked { get; set; }
        public bool wallpaperBlockModification { get; set; }
        public bool wiFiConnectOnlyToConfiguredNetworks { get; set; }
        public bool classroomForceRequestPermissionToLeaveClasses { get; set; }
        public bool keychainBlockCloudSync { get; set; }
        public bool pkiBlockOTAUpdates { get; set; }
        public bool privacyForceLimitAdTracking { get; set; }
        public bool enterpriseBookBlockBackup { get; set; }
        public bool enterpriseBookBlockMetadataSync { get; set; }
        public bool airPrintBlocked { get; set; }
        public bool airPrintBlockCredentialsStorage { get; set; }
        public bool airPrintForceTrustedTLS { get; set; }
        public bool airPrintBlockiBeaconDiscovery { get; set; }
        public bool filesNetworkDriveAccessBlocked { get; set; }
        public bool filesUsbDriveAccessBlocked { get; set; }
        public bool wifiPowerOnForced { get; set; }
        public bool blockSystemAppRemoval { get; set; }
        public bool vpnBlockCreation { get; set; }
        public bool appRemovalBlocked { get; set; }
        public bool usbRestrictedModeBlocked { get; set; }
        public bool passwordBlockAutoFill { get; set; }
        public bool passwordBlockProximityRequests { get; set; }
        public bool passwordBlockAirDropSharing { get; set; }
        public bool dateAndTimeForceSetAutomatically { get; set; }
        public bool contactsAllowManagedToUnmanagedWrite { get; set; }
        public bool contactsAllowUnmanagedToManagedRead { get; set; }
        public bool cellularBlockPersonalHotspotModification { get; set; }
        public bool continuousPathKeyboardBlocked { get; set; }
        public bool findMyDeviceInFindMyAppBlocked { get; set; }
        public bool findMyFriendsInFindMyAppBlocked { get; set; }
        public bool iTunesBlocked { get; set; }
        public bool sharedDeviceBlockTemporarySessions { get; set; }
        public bool appClipsBlocked { get; set; }
        public bool applePersonalizedAdsBlocked { get; set; }
        public bool nfcBlocked { get; set; }
        public bool autoUnlockBlocked { get; set; }
        public bool unpairedExternalBootToRecoveryAllowed { get; set; }
        public bool onDeviceOnlyDictationForced { get; set; }
        public bool wiFiConnectToAllowedNetworksOnlyForced { get; set; }
        public bool onDeviceOnlyTranslationForced { get; set; }
        public bool managedPasteboardRequired { get; set; }
        public string kioskModeAppType { get; set; }
        public object[] appsSingleAppModeList { get; set; }
        public object[] appsVisibilityList { get; set; }
        public object[] compliantAppsList { get; set; }
        public Networkusagerule[] networkUsageRules { get; set; }

        public string PrintConsole()
        {
            return this.displayName + " - " + this.id;
        }

        public string PrintCSV()
        {
            return this.id + ";" + this.displayName;
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.displayName);
        }
    }

    public class Networkusagerule
    {
        public bool cellularDataBlockWhenRoaming { get; set; }
        public bool cellularDataBlocked { get; set; }
        public object[] managedApps { get; set; }
    }

}
