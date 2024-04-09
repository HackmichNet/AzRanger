﻿using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class OwaMailboxPolicy
    {
        public bool WacEditingEnabled { get; set; }
        public bool PrintWithoutDownloadEnabled { get; set; }
        public bool OneDriveAttachmentsEnabled { get; set; }
        public bool ThirdPartyFileProvidersEnabled { get; set; }
        public bool AdditionalStorageProvidersAvailable { get; set; }
        public bool ClassicAttachmentsEnabled { get; set; }
        public bool ReferenceAttachmentsEnabled { get; set; }
        public bool SaveAttachmentsToCloudEnabled { get; set; }
        public object InternalSPMySiteHostURL { get; set; }
        public object ExternalSPMySiteHostURL { get; set; }
        public bool ExternalImageProxyEnabled { get; set; }
        public bool NpsSurveysEnabled { get; set; }
        public bool MessagePreviewsDisabled { get; set; }
        public bool PersonalAccountCalendarsEnabled { get; set; }
        public bool TeamsnapCalendarsEnabled { get; set; }
        public bool BookingsMailboxCreationEnabled { get; set; }
        public bool ProjectMocaEnabled { get; set; }
        public bool DirectFileAccessOnPublicComputersEnabled { get; set; }
        public bool DirectFileAccessOnPrivateComputersEnabled { get; set; }
        public bool WebReadyDocumentViewingOnPublicComputersEnabled { get; set; }
        public bool WebReadyDocumentViewingOnPrivateComputersEnabled { get; set; }
        public bool ForceWebReadyDocumentViewingFirstOnPublicComputers { get; set; }
        public bool ForceWebReadyDocumentViewingFirstOnPrivateComputers { get; set; }
        public bool WacViewingOnPublicComputersEnabled { get; set; }
        public bool WacViewingOnPrivateComputersEnabled { get; set; }
        public bool ForceWacViewingFirstOnPublicComputers { get; set; }
        public bool ForceWacViewingFirstOnPrivateComputers { get; set; }
        public string ActionForUnknownFileAndMIMETypes { get; set; }
        public string WebReadyFileTypesodatatype { get; set; }
        public string[] WebReadyFileTypes { get; set; }
        public string WebReadyMimeTypesodatatype { get; set; }
        public string[] WebReadyMimeTypes { get; set; }
        public bool WebReadyDocumentViewingForAllSupportedTypes { get; set; }
        public string WebReadyDocumentViewingSupportedMimeTypesodatatype { get; set; }
        public string[] WebReadyDocumentViewingSupportedMimeTypes { get; set; }
        public string WebReadyDocumentViewingSupportedFileTypesodatatype { get; set; }
        public string[] WebReadyDocumentViewingSupportedFileTypes { get; set; }
        public string AllowedFileTypesodatatype { get; set; }
        public string[] AllowedFileTypes { get; set; }
        public string AllowedMimeTypesodatatype { get; set; }
        public string[] AllowedMimeTypes { get; set; }
        public string ForceSaveFileTypesodatatype { get; set; }
        public string[] ForceSaveFileTypes { get; set; }
        public string ForceSaveMimeTypesodatatype { get; set; }
        public string[] ForceSaveMimeTypes { get; set; }
        public string BlockedFileTypesodatatype { get; set; }
        public string[] BlockedFileTypes { get; set; }
        public string BlockedMimeTypesodatatype { get; set; }
        public string[] BlockedMimeTypes { get; set; }
        public bool FeedbackEnabled { get; set; }
        public bool SMimeSuppressNameChecksEnabled { get; set; }
        public bool PhoneticSupportEnabled { get; set; }
        public string DefaultTheme { get; set; }
        public bool IsDefault { get; set; }
        public string DefaultClientLanguagedatatype { get; set; }
        public int DefaultClientLanguage { get; set; }
        public string LogonAndErrorLanguagedatatype { get; set; }
        public int LogonAndErrorLanguage { get; set; }
        public bool UseGB18030 { get; set; }
        public bool UseISO885915 { get; set; }
        public string OutboundCharset { get; set; }
        public bool GlobalAddressListEnabled { get; set; }
        public bool OrganizationEnabled { get; set; }
        public bool ExplicitLogonEnabled { get; set; }
        public bool OWALightEnabled { get; set; }
        public bool DelegateAccessEnabled { get; set; }
        public bool IRMEnabled { get; set; }
        public bool CalendarEnabled { get; set; }
        public bool ContactsEnabled { get; set; }
        public bool TasksEnabled { get; set; }
        public bool JournalEnabled { get; set; }
        public bool NotesEnabled { get; set; }
        public bool OnSendAddinsEnabled { get; set; }
        public bool RemindersAndNotificationsEnabled { get; set; }
        public bool PremiumClientEnabled { get; set; }
        public bool SpellCheckerEnabled { get; set; }
        public bool SearchFoldersEnabled { get; set; }
        public bool SignaturesEnabled { get; set; }
        public bool ThemeSelectionEnabled { get; set; }
        public bool JunkEmailEnabled { get; set; }
        public bool UMIntegrationEnabled { get; set; }
        public bool WSSAccessOnPublicComputersEnabled { get; set; }
        public bool WSSAccessOnPrivateComputersEnabled { get; set; }
        public bool ChangePasswordEnabled { get; set; }
        public bool UNCAccessOnPublicComputersEnabled { get; set; }
        public bool UNCAccessOnPrivateComputersEnabled { get; set; }
        public bool ActiveSyncIntegrationEnabled { get; set; }
        public bool AllAddressListsEnabled { get; set; }
        public bool RulesEnabled { get; set; }
        public bool PublicFoldersEnabled { get; set; }
        public bool SMimeEnabled { get; set; }
        public bool RecoverDeletedItemsEnabled { get; set; }
        public bool InstantMessagingEnabled { get; set; }
        public bool TextMessagingEnabled { get; set; }
        public bool ForceSaveAttachmentFilteringEnabled { get; set; }
        public bool SilverlightEnabled { get; set; }
        public string InstantMessagingType { get; set; }
        public bool DisplayPhotosEnabled { get; set; }
        public bool SetPhotoEnabled { get; set; }
        public string AllowOfflineOn { get; set; }
        public string SetPhotoURL { get; set; }
        public bool PlacesEnabled { get; set; }
        public bool WeatherEnabled { get; set; }
        public bool LocalEventsEnabled { get; set; }
        public bool InterestingCalendarsEnabled { get; set; }
        public bool AllowCopyContactsToDeviceAddressBook { get; set; }
        public bool PredictedActionsEnabled { get; set; }
        public bool UserDiagnosticEnabled { get; set; }
        public bool FacebookEnabled { get; set; }
        public bool LinkedInEnabled { get; set; }
        public bool WacExternalServicesEnabled { get; set; }
        public bool WacOMEXEnabled { get; set; }
        public bool ReportJunkEmailEnabled { get; set; }
        public bool GroupCreationEnabled { get; set; }
        public bool SkipCreateUnifiedGroupCustomSharepointClassification { get; set; }
        public string WebPartsFrameOptionsType { get; set; }
        public bool UserVoiceEnabled { get; set; }
        public bool SatisfactionEnabled { get; set; }
        public bool FreCardsEnabled { get; set; }
        public string ConditionalAccessPolicy { get; set; }
        public string ConditionalAccessFeaturesodatatype { get; set; }
        public object[] ConditionalAccessFeatures { get; set; }
        public bool OutlookBetaToggleEnabled { get; set; }
        public bool ShowOnlineArchiveEnabled { get; set; }
        public string AdminDisplayName { get; set; }
        public object Item { get; set; }
        public string ExchangeVersion { get; set; }
        public string Name { get; set; }
        public string DistinguishedName { get; set; }
        public string Identity { get; set; }
        public string ObjectCategory { get; set; }
        public string ObjectClassodatatype { get; set; }
        public string[] ObjectClass { get; set; }
        public string WhenChangeddatatype { get; set; }
        public DateTime WhenChanged { get; set; }
        public string WhenCreateddatatype { get; set; }
        public DateTime WhenCreated { get; set; }
        public string WhenChangedUTCdatatype { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public string WhenCreatedUTCdatatype { get; set; }
        public DateTime WhenCreatedUTC { get; set; }
        public string ExchangeObjectIddatatype { get; set; }
        public string ExchangeObjectIdodatatype { get; set; }
        public string ExchangeObjectId { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string Id { get; set; }
        public string Guiddatatype { get; set; }
        public string Guidodatatype { get; set; }
        public string Guid { get; set; }
        public string OriginatingServer { get; set; }
        public bool IsValid { get; set; }
    }

}
