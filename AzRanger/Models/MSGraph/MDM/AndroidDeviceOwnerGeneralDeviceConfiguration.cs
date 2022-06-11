using System;
using System.Text.Json.Serialization;


namespace AzRanger.Models.MSGraph.MDM
{

    //Android Enterprise - Device restrictions
    public class AndroidDeviceOwnerGeneralDeviceConfiguration : IReporting
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
        public object accountsBlockModification { get; set; }
        public object appsAllowInstallFromUnknownSources { get; set; }
        public object appsAutoUpdatePolicy { get; set; }
        public object appsDefaultPermissionPolicy { get; set; }
        public object appsRecommendSkippingFirstUseHints { get; set; }
        public object bluetoothBlockConfiguration { get; set; }
        public object bluetoothBlockContactSharing { get; set; }
        public object cameraBlocked { get; set; }
        public object cellularBlockWiFiTethering { get; set; }
        public object certificateCredentialConfigurationDisabled { get; set; }
        public object microsoftLauncherConfigurationEnabled { get; set; }
        public object microsoftLauncherCustomWallpaperEnabled { get; set; }
        public object microsoftLauncherCustomWallpaperImageUrl { get; set; }
        public object microsoftLauncherCustomWallpaperAllowUserModification { get; set; }
        public object microsoftLauncherFeedEnabled { get; set; }
        public object microsoftLauncherFeedAllowUserModification { get; set; }
        public object microsoftLauncherDockPresenceConfiguration { get; set; }
        public object microsoftLauncherDockPresenceAllowUserModification { get; set; }
        public object microsoftLauncherSearchBarPlacementConfiguration { get; set; }
        public string enrollmentProfile { get; set; }
        public object dataRoamingBlocked { get; set; }
        public object dateTimeConfigurationBlocked { get; set; }
        public object[] factoryResetDeviceAdministratorEmails { get; set; }
        public object factoryResetBlocked { get; set; }
        public object globalProxy { get; set; }
        public object googleAccountsBlocked { get; set; }
        public object kioskCustomizationDeviceSettingsBlocked { get; set; }
        public object kioskCustomizationPowerButtonActionsBlocked { get; set; }
        public string kioskCustomizationStatusBar { get; set; }
        public object kioskCustomizationSystemErrorWarnings { get; set; }
        public string kioskCustomizationSystemNavigation { get; set; }
        public object kioskModeScreenSaverConfigurationEnabled { get; set; }
        public object kioskModeScreenSaverImageUrl { get; set; }
        public object kioskModeScreenSaverDisplayTimeInSeconds { get; set; }
        public object kioskModeScreenSaverStartDelayInSeconds { get; set; }
        public object kioskModeScreenSaverDetectMediaDisabled { get; set; }
        public object kioskModeWallpaperUrl { get; set; }
        public object kioskModeExitCode { get; set; }
        public object kioskModeVirtualHomeButtonEnabled { get; set; }
        public object kioskModeVirtualHomeButtonType { get; set; }
        public object kioskModeBluetoothConfigurationEnabled { get; set; }
        public object kioskModeWiFiConfigurationEnabled { get; set; }
        public object kioskModeFlashlightConfigurationEnabled { get; set; }
        public object kioskModeMediaVolumeConfigurationEnabled { get; set; }
        public object kioskModeShowDeviceInfo { get; set; }
        public object kioskModeManagedSettingsEntryDisabled { get; set; }
        public object kioskModeDebugMenuEasyAccessEnabled { get; set; }
        public object kioskModeShowAppNotificationBadge { get; set; }
        public object kioskModeScreenOrientation { get; set; }
        public object kioskModeIconSize { get; set; }
        public object kioskModeFolderIcon { get; set; }
        public object[] kioskModeWifiAllowedSsids { get; set; }
        public object kioskModeAppOrderEnabled { get; set; }
        public object kioskModeAppsInFolderOrderedByName { get; set; }
        public object kioskModeGridHeight { get; set; }
        public object kioskModeGridWidth { get; set; }
        public object kioskModeLockHomeScreen { get; set; }
        public object kioskModeManagedHomeScreenAutoSignout { get; set; }
        public object kioskModeManagedHomeScreenInactiveSignOutDelayInSeconds { get; set; }
        public object kioskModeManagedHomeScreenInactiveSignOutNoticeInSeconds { get; set; }
        public object kioskModeManagedHomeScreenPinComplexity { get; set; }
        public object kioskModeManagedHomeScreenPinRequired { get; set; }
        public object kioskModeManagedHomeScreenPinRequiredToResume { get; set; }
        public object kioskModeManagedHomeScreenSignInBackground { get; set; }
        public object kioskModeManagedHomeScreenSignInBrandingLogo { get; set; }
        public object kioskModeManagedHomeScreenSignInEnabled { get; set; }
        public object microphoneForceMute { get; set; }
        public object networkEscapeHatchAllowed { get; set; }
        public object nfcBlockOutgoingBeam { get; set; }
        public object passwordBlockKeyguard { get; set; }
        public object[] passwordBlockKeyguardFeatures { get; set; }
        public object passwordExpirationDays { get; set; }
        public object passwordMinimumLength { get; set; }
        public object passwordMinimumLetterCharacters { get; set; }
        public object passwordMinimumLowerCaseCharacters { get; set; }
        public object passwordMinimumNonLetterCharacters { get; set; }
        public object passwordMinimumNumericCharacters { get; set; }
        public object passwordMinimumSymbolCharacters { get; set; }
        public object passwordMinimumUpperCaseCharacters { get; set; }
        public object passwordMinutesOfInactivityBeforeScreenTimeout { get; set; }
        public object passwordPreviousPasswordCountToBlock { get; set; }
        public string passwordRequiredType { get; set; }
        public object passwordSignInFailureCountBeforeFactoryReset { get; set; }
        public object playStoreMode { get; set; }
        public object safeBootBlocked { get; set; }
        public object screenCaptureBlocked { get; set; }
        public object securityAllowDebuggingFeatures { get; set; }
        public object securityDeveloperSettingsEnabled { get; set; }
        public bool securityRequireVerifyApps { get; set; }
        public object statusBarBlocked { get; set; }
        public object[] stayOnModes { get; set; }
        public object storageAllowUsb { get; set; }
        public object storageBlockExternalMedia { get; set; }
        public object storageBlockUsbFileTransfer { get; set; }
        public object systemUpdateWindowStartMinutesAfterMidnight { get; set; }
        public object systemUpdateWindowEndMinutesAfterMidnight { get; set; }
        public object systemUpdateInstallType { get; set; }
        public object systemWindowsBlocked { get; set; }
        public object usersBlockAdd { get; set; }
        public object usersBlockRemove { get; set; }
        public object volumeBlockAdjustment { get; set; }
        public bool vpnAlwaysOnLockdownMode { get; set; }
        public string vpnAlwaysOnPackageIdentifier { get; set; }
        public object wifiBlockEditConfigurations { get; set; }
        public object wifiBlockEditPolicyDefinedConfigurations { get; set; }
        public object personalProfileAppsAllowInstallFromUnknownSources { get; set; }
        public object personalProfileCameraBlocked { get; set; }
        public object personalProfileScreenCaptureBlocked { get; set; }
        public string personalProfilePlayStoreMode { get; set; }
        public object workProfilePasswordExpirationDays { get; set; }
        public object workProfilePasswordMinimumLength { get; set; }
        public object workProfilePasswordMinimumNumericCharacters { get; set; }
        public object workProfilePasswordMinimumNonLetterCharacters { get; set; }
        public object workProfilePasswordMinimumLetterCharacters { get; set; }
        public object workProfilePasswordMinimumLowerCaseCharacters { get; set; }
        public object workProfilePasswordMinimumUpperCaseCharacters { get; set; }
        public object workProfilePasswordMinimumSymbolCharacters { get; set; }
        public object workProfilePasswordPreviousPasswordCountToBlock { get; set; }
        public object workProfilePasswordSignInFailureCountBeforeFactoryReset { get; set; }
        public string workProfilePasswordRequiredType { get; set; }
        public object[] azureAdSharedDeviceDataClearApps { get; set; }
        public object[] kioskModeApps { get; set; }
        public object[] kioskModeManagedFolders { get; set; }
        public object[] kioskModeAppPositions { get; set; }
        public object[] personalProfilePersonalApplications { get; set; }
        public object[] assignments { get; set; }

        public string PrintConsole()
        {
            return this.displayName + " - " + this.id;
        }

        public string PrintCSV()
        {
            return this.displayName + ";" + this.id;
        }
    }

}
