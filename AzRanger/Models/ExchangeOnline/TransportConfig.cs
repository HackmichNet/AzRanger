using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.ExchangeOnline
{
    public class TransportConfig
    {
        public string Name { get; set; }
        public string TLSReceiveDomainSecureListodatatype { get; set; }
        public object[] TLSReceiveDomainSecureList { get; set; }
        public string TLSSendDomainSecureListodatatype { get; set; }
        public object[] TLSSendDomainSecureList { get; set; }
        public string GenerateCopyOfDSNForodatatype { get; set; }
        public object[] GenerateCopyOfDSNFor { get; set; }
        public string InternalSMTPServersodatatype { get; set; }
        public object[] InternalSMTPServers { get; set; }
        public string JournalingReportNdrTo { get; set; }
        public string OrganizationFederatedMailbox { get; set; }
        public string MaxDumpsterSizePerDatabase { get; set; }
        public string MaxDumpsterTime { get; set; }
        public bool VerifySecureSubmitEnabled { get; set; }
        public bool ClearCategories { get; set; }
        public bool AddressBookPolicyRoutingEnabled { get; set; }
        public bool ConvertDisclaimerWrapperToEml { get; set; }
        public bool PreserveReportBodypart { get; set; }
        public bool ConvertReportToMessage { get; set; }
        public string DSNConversionMode { get; set; }
        public bool VoicemailJournalingEnabled { get; set; }
        public string HeaderPromotionModeSetting { get; set; }
        public bool Xexch50Enabled { get; set; }
        public bool Rfc2231EncodingEnabled { get; set; }
        public bool OpenDomainRoutingEnabled { get; set; }
        public string MaxReceiveSize { get; set; }
        public string MaxRecipientEnvelopeLimit { get; set; }
        public string MaxSendSize { get; set; }
        public bool ExternalDelayDsnEnabled { get; set; }
        public object ExternalDsnDefaultLanguage { get; set; }
        public bool ExternalDsnLanguageDetectionEnabled { get; set; }
        public string ExternalDsnMaxMessageAttachSize { get; set; }
        public object ExternalDsnReportingAuthority { get; set; }
        public bool ExternalDsnSendHtml { get; set; }
        public object ExternalPostmasterAddress { get; set; }
        public bool InternalDelayDsnEnabled { get; set; }
        public object InternalDsnDefaultLanguage { get; set; }
        public bool InternalDsnLanguageDetectionEnabled { get; set; }
        public string InternalDsnMaxMessageAttachSize { get; set; }
        public object InternalDsnReportingAuthority { get; set; }
        public bool InternalDsnSendHtml { get; set; }
        public string SupervisionTagsodatatype { get; set; }
        public string[] SupervisionTags { get; set; }
        public string HygieneSuite { get; set; }
        public bool MigrationEnabled { get; set; }
        public bool LegacyJournalingMigrationEnabled { get; set; }
        public bool LegacyArchiveJournalingEnabled { get; set; }
        public bool RedirectDLMessagesForLegacyArchiveJournaling { get; set; }
        public bool RedirectUnprovisionedUserMessagesForLegacyArchiveJournaling { get; set; }
        public bool LegacyArchiveLiveJournalingEnabled { get; set; }
        public bool JournalArchivingEnabled { get; set; }
        public string SafetyNetHoldTime { get; set; }
        public string TransportRuleConfigodatatype { get; set; }
        public string[] TransportRuleConfig { get; set; }
        public string TransportRuleCollectionAddedRecipientsLimitdatatype { get; set; }
        public int TransportRuleCollectionAddedRecipientsLimit { get; set; }
        public string TransportRuleLimitdatatype { get; set; }
        public int TransportRuleLimit { get; set; }
        public string TransportRuleCollectionRegexCharsLimit { get; set; }
        public string TransportRuleSizeLimit { get; set; }
        public string TransportRuleAttachmentTextScanLimit { get; set; }
        public string TransportRuleRegexValidationTimeout { get; set; }
        public string TransportRuleMinProductVersion { get; set; }
        public string AnonymousSenderToRecipientRatePerHourdatatype { get; set; }
        public int AnonymousSenderToRecipientRatePerHour { get; set; }
        public string QueueDiagnosticsAggregationInterval { get; set; }
        public bool JournalReportDLMemberSubstitutionEnabled { get; set; }
        public string DiagnosticsAggregationServicePortdatatype { get; set; }
        public int DiagnosticsAggregationServicePort { get; set; }
        public bool AgentGeneratedMessageLoopDetectionInSubmissionEnabled { get; set; }
        public bool AgentGeneratedMessageLoopDetectionInSmtpEnabled { get; set; }
        public string MaxAllowedAgentGeneratedMessageDepthdatatype { get; set; }
        public string MaxAllowedAgentGeneratedMessageDepthodatatype { get; set; }
        public int MaxAllowedAgentGeneratedMessageDepth { get; set; }
        public string MaxAllowedAgentGeneratedMessageDepthPerAgentdatatype { get; set; }
        public string MaxAllowedAgentGeneratedMessageDepthPerAgentodatatype { get; set; }
        public int MaxAllowedAgentGeneratedMessageDepthPerAgent { get; set; }
        public bool AttributionRejectConsumerMessages { get; set; }
        public bool AttributionRejectBeforeMServRequest { get; set; }
        public bool SmtpClientAuthenticationDisabled { get; set; }
        public string JournalMessageExpirationDaysdatatype { get; set; }
        public int JournalMessageExpirationDays { get; set; }
        public bool PreventDuplicateJournalingEnabled { get; set; }
        public bool ReplyAllStormProtectionEnabled { get; set; }
        public string ReplyAllStormDetectionMinimumRecipientsdatatype { get; set; }
        public int ReplyAllStormDetectionMinimumRecipients { get; set; }
        public string ReplyAllStormDetectionMinimumRepliesdatatype { get; set; }
        public int ReplyAllStormDetectionMinimumReplies { get; set; }
        public object AllowLegacyTLSClients { get; set; }
        public string ReplyAllStormBlockDurationHoursdatatype { get; set; }
        public int ReplyAllStormBlockDurationHours { get; set; }
        public string MessageExpiration { get; set; }
        public bool EnableExternalHTTPMailDelivery { get; set; }
        public string OtherWellKnownObjectsodatatype { get; set; }
        public object[] OtherWellKnownObjects { get; set; }
        public string AdminDisplayName { get; set; }
        public string ExchangeVersion { get; set; }
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
