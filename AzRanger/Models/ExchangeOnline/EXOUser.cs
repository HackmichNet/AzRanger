using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.ExchangeOnline
{
    public class EXOUser : IEntity
    {
        public bool IsSecurityPrincipal { get; set; }
        public string SamAccountName { get; set; }
        public string Sid { get; set; }
        public string SidHistoryodatatype { get; set; }
        public object[] SidHistory { get; set; }
        public string UserPrincipalName { get; set; }
        public bool ResetPasswordOnNextLogon { get; set; }
        public string CertificateSubjectodatatype { get; set; }
        public object[] CertificateSubject { get; set; }
        public bool RemotePowerShellEnabled { get; set; }
        public string WindowsLiveID { get; set; }
        public string MicrosoftOnlineServicesID { get; set; }
        public object NetID { get; set; }
        public bool IsCloudCacheProvisioningComplete { get; set; }
        public bool IsCloudCache { get; set; }
        public string CloudCacheProviderdatatype { get; set; }
        public int CloudCacheProvider { get; set; }
        public string CloudCacheAccountType { get; set; }
        public string CloudCacheScopedatatype { get; set; }
        public int CloudCacheScope { get; set; }
        public string CloudCacheRemoteEmailAddress { get; set; }
        public string CloudCacheUserName { get; set; }
        public bool IsCloudCacheBlocked { get; set; }
        public object ConsumerNetID { get; set; }
        public string UserAccountControl { get; set; }
        public string OrganizationalUnit { get; set; }
        public bool IsLinked { get; set; }
        public string LinkedMasterAccount { get; set; }
        public object LegalAgeGroup { get; set; }
        public string ExternalDirectoryObjectId { get; set; }
        public object SKUAssigned { get; set; }
        public bool IsSoftDeletedByRemove { get; set; }
        public bool IsSoftDeletedByDisable { get; set; }
        public object WhenSoftDeleted { get; set; }
        public string PreviousRecipientTypeDetails { get; set; }
        public string UpgradeRequest { get; set; }
        public string UpgradeStatus { get; set; }
        public object UpgradeDetails { get; set; }
        public object UpgradeMessage { get; set; }
        public object UpgradeStage { get; set; }
        public object UpgradeStageTimeStamp { get; set; }
        public object MailboxRegion { get; set; }
        public object MailboxRegionLastUpdateTime { get; set; }
        public string MailboxRegionSuffix { get; set; }
        public object MailboxProvisioningConstraint { get; set; }
        public string MailboxProvisioningPreferencesodatatype { get; set; }
        public object[] MailboxProvisioningPreferences { get; set; }
        public string MailboxWorkloads { get; set; }
        public object DefaultMailboxWorkloadsMask { get; set; }
        public object DesiredMailboxWorkloads { get; set; }
        public object DesiredMailboxWorkloadsModified { get; set; }
        public object DesiredMailboxWorkloadsGracePeriod { get; set; }
        public string LegacyExchangeDN { get; set; }
        public string InPlaceHoldsRawodatatype { get; set; }
        public object[] InPlaceHoldsRaw { get; set; }
        public string MailboxRelease { get; set; }
        public string ArchiveRelease { get; set; }
        public bool AccountDisabled { get; set; }
        public object AuthenticationPolicy { get; set; }
        public object StsRefreshTokensValidFrom { get; set; }
        public bool IsInactiveMailbox { get; set; }
        public string MailboxLocationsodatatype { get; set; }
        public string[] MailboxLocations { get; set; }
        public string InformationBarrierSegmentsodatatype { get; set; }
        public object[] InformationBarrierSegments { get; set; }
        public object WhenIBSegmentChanged { get; set; }
        public bool CanHaveCloudCache { get; set; }
        public object DataEncryptionPolicy { get; set; }
        public string AdministrativeUnitsodatatype { get; set; }
        public object[] AdministrativeUnits { get; set; }
        public string AssistantName { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public object CountryOrRegion { get; set; }
        public string Department { get; set; }
        public string DirectReportsodatatype { get; set; }
        public object[] DirectReports { get; set; }
        public string DisplayName { get; set; }
        public string Fax { get; set; }
        public string FirstName { get; set; }
        public object GeoCoordinates { get; set; }
        public string HomePhone { get; set; }
        public string Initials { get; set; }
        public bool IsDirSynced { get; set; }
        public string LastName { get; set; }
        public object Manager { get; set; }
        public string MobilePhone { get; set; }
        public string Notes { get; set; }
        public string Office { get; set; }
        public string OtherFaxodatatype { get; set; }
        public object[] OtherFax { get; set; }
        public string OtherHomePhoneodatatype { get; set; }
        public object[] OtherHomePhone { get; set; }
        public string OtherTelephoneodatatype { get; set; }
        public object[] OtherTelephone { get; set; }
        public string Pager { get; set; }
        public string Phone { get; set; }
        public string PhoneticDisplayName { get; set; }
        public string PostalCode { get; set; }
        public string PostOfficeBoxodatatype { get; set; }
        public object[] PostOfficeBox { get; set; }
        public string RecipientType { get; set; }
        public string RecipientTypeDetails { get; set; }
        public string SimpleDisplayName { get; set; }
        public string StateOrProvince { get; set; }
        public string StreetAddress { get; set; }
        public string Title { get; set; }
        public object UMDialPlan { get; set; }
        public string UMDtmfMapodatatype { get; set; }
        public string[] UMDtmfMap { get; set; }
        public string AllowUMCallsFromNonUsers { get; set; }
        public string WebPage { get; set; }
        public string TelephoneAssistant { get; set; }
        public string WindowsEmailAddress { get; set; }
        public string UMCallingLineIdsodatatype { get; set; }
        public object[] UMCallingLineIds { get; set; }
        public object SeniorityIndex { get; set; }
        public string VoiceMailSettingsodatatype { get; set; }
        public object[] VoiceMailSettings { get; set; }
        public string Identity { get; set; }
        public string Id { get; set; }
        public bool IsValid { get; set; }
        public object Item { get; set; }
        public string ExchangeVersion { get; set; }
        public string Name { get; set; }
        public string DistinguishedName { get; set; }
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
        public string Guiddatatype { get; set; }
        public string Guidodatatype { get; set; }
        public string Guid { get; set; }
        public string OriginatingServer { get; set; }

        public string PrintConsole()
        {
            return String.Format(@"{0} - {1}", UserPrincipalName, Guid);
        }

        public string PrintCSV()
        {
            return String.Format(@"{0};{1}", Guid, UserPrincipalName);
        }
    }

}
