using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.SharePoint
{
    public class SPOInternalUseOnly
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        [JsonPropertyName("@odata.id")]
        public string odataid { get; set; }
        [JsonPropertyName("@odata.editLink")]
        public string odataeditLink { get; set; }
        public string AIBuilderDefaultPowerAppsEnvironment { get; set; }
        public bool AIBuilderEnabled { get; set; }
        public object AIBuilderSiteListFileName { get; set; }
        public bool AllowCommentsTextOnEmailEnabled { get; set; }
        public bool AllowDownloadingNonWebViewableFiles { get; set; }
        public object[] AllowedDomainListForSyncClient { get; set; }
        public bool AllowEditing { get; set; }
        public bool AllowGuestUserShareToUsersNotInSiteCollection { get; set; }
        public bool AllowLimitedAccessOnUnmanagedDevices { get; set; }
        public bool AllowOverrideForBlockUserInfoVisibility { get; set; }
        public object AllowSelectSGsInODBListInTenant { get; set; }
        public bool AnyoneLinkTrackUsers { get; set; }
        public bool ApplyAppEnforcedRestrictionsToAdHocRecipients { get; set; }
        public int AuthContextResilienceMode { get; set; }
        public bool BccExternalSharingInvitations { get; set; }
        public object BccExternalSharingInvitationsList { get; set; }
        public bool BlockAccessOnUnmanagedDevices { get; set; }
        public int BlockDownloadLinksFileType { get; set; }
        public bool BlockDownloadOfAllFilesForGuests { get; set; }
        public bool BlockDownloadOfAllFilesOnUnmanagedDevices { get; set; }
        public bool BlockDownloadOfViewableFilesForGuests { get; set; }
        public bool BlockDownloadOfViewableFilesOnUnmanagedDevices { get; set; }
        public bool BlockMacSync { get; set; }
        public bool BlockSendLabelMismatchEmail { get; set; }
        public string BlockUserInfoVisibility { get; set; }
        public int BlockUserInfoVisibilityInOneDrive { get; set; }
        public int BlockUserInfoVisibilityInSharePoint { get; set; }
        public int ChannelMeetingRecordingPermission { get; set; }
        public bool CommentsOnFilesDisabled { get; set; }
        public bool CommentsOnListItemsDisabled { get; set; }
        public bool CommentsOnSitePagesDisabled { get; set; }
        public string CompatibilityRange { get; set; }
        public int ConditionalAccessPolicy { get; set; }
        public string ConditionalAccessPolicyErrorHelpLink { get; set; }
        public object[] ContentTypeSyncSiteTemplatesList { get; set; }
        public string CustomizedExternalSharingServiceUrl { get; set; }
        public object DefaultContentCenterSite { get; set; }
        public int DefaultLinkPermission { get; set; }
        public string DefaultODBMode { get; set; }
        public int DefaultSharingLinkType { get; set; }
        public bool DisableAddToOneDrive { get; set; }
        public bool DisableBackToClassic { get; set; }
        public bool DisableCustomAppAuthentication { get; set; }
        public object[] DisabledModernListTemplateIds { get; set; }
        public object DisabledWebPartIds { get; set; }
        public bool DisableListSync { get; set; }
        public bool DisableOutlookPSTVersionTrimming { get; set; }
        public bool DisablePersonalListCreation { get; set; }
        public bool DisableReportProblemDialog { get; set; }
        public bool DisableSpacesActivation { get; set; }
        public bool DisallowInfectedFileDownload { get; set; }
        public bool DisplayNamesOfFileViewers { get; set; }
        public bool DisplayNamesOfFileViewersInSpo { get; set; }
        public bool DisplayStartASiteOption { get; set; }
        public bool EmailAttestationEnabled { get; set; }
        public int EmailAttestationReAuthDays { get; set; }
        public bool EmailAttestationRequired { get; set; }
        public bool EnableAIPIntegration { get; set; }
        public bool EnableAutoNewsDigest { get; set; }
        public bool EnableAzureADB2BIntegration { get; set; }
        public bool EnabledFlightAllowAADB2BSkipCheckingOTP { get; set; }
        public bool EnableGuestSignInAcceleration { get; set; }
        public bool EnableMinimumVersionRequirement { get; set; }
        public bool EnableMipSiteLabel { get; set; }
        public bool EnablePromotedFileHandlers { get; set; }
        public string[] ExcludedFileExtensionsForSyncClient { get; set; }
        public bool ExternalServicesEnabled { get; set; }
        public bool ExternalUserExpirationRequired { get; set; }
        public int ExternalUserExpireInDays { get; set; }
        public int FileAnonymousLinkType { get; set; }
        public bool FilePickerExternalImageSearchEnabled { get; set; }
        public int FolderAnonymousLinkType { get; set; }
        public string GuestSharingGroupAllowListInTenant { get; set; }
        public object GuestSharingGroupAllowListInTenantByPrincipalIdentity { get; set; }
        public bool HasAdminCompletedCUConfiguration { get; set; }
        public bool HasIntelligentContentServicesCapability { get; set; }
        public bool HasTopicExperiencesCapability { get; set; }
        public bool HideSyncButtonOnDocLib { get; set; }
        public bool HideSyncButtonOnODB { get; set; }
        public bool IBImplicitGroupBased { get; set; }
        public int ImageTaggingOption { get; set; }
        public bool IncludeAtAGlanceInShareEmails { get; set; }
        public bool InformationBarriersSuspension { get; set; }
        public string IPAddressAllowList { get; set; }
        public bool IPAddressEnforcement { get; set; }
        public int IPAddressWACTokenLifetime { get; set; }
        public bool IsAppBarTemporarilyDisabled { get; set; }
        public bool IsCollabMeetingNotesFluidEnabled { get; set; }
        public bool IsFluidEnabled { get; set; }
        public bool IsHubSitesMultiGeoFlightEnabled { get; set; }
        public bool IsMnAFlightEnabled { get; set; }
        public bool IsMultiGeo { get; set; }
        public bool IsUnmanagedSyncClientForTenantRestricted { get; set; }
        public bool IsUnmanagedSyncClientRestrictionFlightEnabled { get; set; }
        public bool IsWBFluidEnabled { get; set; }
        public object LabelMismatchEmailHelpLink { get; set; }
        public bool LegacyAuthProtocolsEnabled { get; set; }
        public int LimitedAccessFileType { get; set; }
        public bool MachineLearningCaptureEnabled { get; set; }
        public int MarkNewFilesSensitiveByDefault { get; set; }
        public int MediaTranscription { get; set; }
        public bool MobileFriendlyUrlEnabledInTenant { get; set; }
        public object NoAccessRedirectUrl { get; set; }
        public bool NotificationsInOneDriveForBusinessEnabled { get; set; }
        public bool NotificationsInSharePointEnabled { get; set; }
        public bool NotifyOwnersWhenInvitationsAccepted { get; set; }
        public bool NotifyOwnersWhenItemsReshared { get; set; }
        public int ODBAccessRequests { get; set; }
        public int ODBMembersCanShare { get; set; }
        public int ODBSharingCapability { get; set; }
        public bool OfficeClientADALDisabled { get; set; }
        public bool OneDriveForGuestsEnabled { get; set; }
        public int OneDriveStorageQuota { get; set; }
        public bool OptOutOfGrooveBlock { get; set; }
        public bool OptOutOfGrooveSoftBlock { get; set; }
        public object OrgNewsSiteUrl { get; set; }
        public int OrphanedPersonalSitesRetentionPeriod { get; set; }
        public bool OwnerAnonymousNotification { get; set; }
        public bool PermissiveBrowserFileHandlingOverride { get; set; }
        public bool PreventExternalUsersFromResharing { get; set; }
        public bool ProvisionSharedWithEveryoneFolder { get; set; }
        public string PublicCdnAllowedFileTypes { get; set; }
        public bool PublicCdnEnabled { get; set; }
        public object[] PublicCdnOrigins { get; set; }
        public bool RequireAcceptingAccountMatchInvitedAccount { get; set; }
        public int RequireAnonymousLinksExpireInDays { get; set; }
        public float ResourceQuota { get; set; }
        public float ResourceQuotaAllocated { get; set; }
        public bool RestrictedOneDriveLicense { get; set; }
        public string RootSiteUrl { get; set; }
        public bool SearchResolveExactEmailOrUPN { get; set; }
        public object SharingAllowedDomainList { get; set; }
        public object SharingBlockedDomainList { get; set; }
        public int SharingCapability { get; set; }

        // 0 => Shareing is allowed to everyone
        // 1 => Shareing is allowed to all mentioned in SharingAllowedDomainList
        // 2 => Shareing is allowed to everyone except the domains in SharingBlockedDomainList
        public int SharingDomainRestrictionMode { get; set; }
        public bool ShowAllUsersClaim { get; set; }
        public bool ShowEveryoneClaim { get; set; }
        public bool ShowEveryoneExceptExternalUsersClaim { get; set; }
        public bool ShowNGSCDialogForSyncOnODB { get; set; }
        public bool ShowPeoplePickerSuggestionsForGuestUsers { get; set; }
        public string SignInAccelerationDomain { get; set; }
        public bool SocialBarOnSitePagesDisabled { get; set; }
        public int SpecialCharactersStateInFileFolderNames { get; set; }
        public object StartASiteFormUrl { get; set; }
        public bool StopNew2010Workflows { get; set; }
        public bool StopNew2013Workflows { get; set; }
        public int StorageQuota { get; set; }
        public int StorageQuotaAllocated { get; set; }
        public int StreamLaunchConfig { get; set; }
        public DateTime StreamLaunchConfigLastUpdated { get; set; }
        public int StreamLaunchConfigUpdateCount { get; set; }
        public bool SyncAadB2BManagementPolicy { get; set; }
        public bool SyncPrivacyProfileProperties { get; set; }
        public bool UseFindPeopleInPeoplePicker { get; set; }
        public bool UsePersistentCookiesForExplorerView { get; set; }
        public bool UserVoiceForFeedbackEnabled { get; set; }
        public bool ViewersCanCommentOnMediaDisabled { get; set; }
        public bool ViewInFileExplorerEnabled { get; set; }
        public string WhoCanShareAllowListInTenant { get; set; }
        public object WhoCanShareAllowListInTenantByPrincipalIdentity { get; set; }
        public bool Workflow2010Disabled { get; set; }
        public int Workflows2013State { get; set; }
    }

}
