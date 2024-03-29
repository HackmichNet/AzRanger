﻿using AzRanger.Output;
using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class TransportRule : IReporting
    {
        public string Identity { get; set; }
        public string[] Actions { get; set; }
        public object ActivationDate { get; set; }
        public object ADComparisonAttribute { get; set; }
        public object ADComparisonOperator { get; set; }
        public object AddManagerAsRecipientType { get; set; }
        public object[] AddToRecipients { get; set; }
        public object[] AnyOfCcHeader { get; set; }
        public object[] AnyOfCcHeaderMemberOf { get; set; }
        public object[] AnyOfRecipientAddressContainsWords { get; set; }
        public object[] AnyOfRecipientAddressMatchesPatterns { get; set; }
        public object[] AnyOfToCcHeader { get; set; }
        public object[] AnyOfToCcHeaderMemberOf { get; set; }
        public object[] AnyOfToHeader { get; set; }
        public object[] AnyOfToHeaderMemberOf { get; set; }
        public object ApplyClassification { get; set; }
        public object ApplyHtmlDisclaimerFallbackAction { get; set; }
        public object ApplyHtmlDisclaimerLocation { get; set; }
        public object ApplyHtmlDisclaimerText { get; set; }
        public bool ApplyOME { get; set; }
        public object ApplyRightsProtectionCustomizationTemplate { get; set; }
        public object ApplyRightsProtectionTemplate { get; set; }
        public object[] AttachmentContainsWords { get; set; }
        public object[] AttachmentExtensionMatchesWords { get; set; }
        public bool AttachmentHasExecutableContent { get; set; }
        public bool AttachmentIsPasswordProtected { get; set; }
        public bool AttachmentIsUnsupported { get; set; }
        public object[] AttachmentMatchesPatterns { get; set; }
        public object[] AttachmentNameMatchesPatterns { get; set; }
        public bool AttachmentProcessingLimitExceeded { get; set; }
        public object[] AttachmentPropertyContainsWords { get; set; }
        public object AttachmentSizeOver { get; set; }
        public object[] BetweenMemberOf1 { get; set; }
        public object[] BetweenMemberOf2 { get; set; }
        public object[] BlindCopyTo { get; set; }
        public string Comments { get; set; }
        public string[] Conditions { get; set; }
        public object[] ContentCharacterSetContainsWords { get; set; }
        public object[] CopyTo { get; set; }
        public string CreatedBy { get; set; }
        public bool DeleteMessage { get; set; }
        public string Description { get; set; }
        public bool Disconnect { get; set; }
        public string DistinguishedName { get; set; }
        public object DlpPolicy { get; set; }
        public string DlpPolicyId { get; set; }
        public object ExceptIfADComparisonAttribute { get; set; }
        public object ExceptIfADComparisonOperator { get; set; }
        public object[] ExceptIfAnyOfCcHeader { get; set; }
        public object[] ExceptIfAnyOfCcHeaderMemberOf { get; set; }
        public object[] ExceptIfAnyOfRecipientAddressContainsWords { get; set; }
        public object[] ExceptIfAnyOfRecipientAddressMatchesPatterns { get; set; }
        public object[] ExceptIfAnyOfToCcHeader { get; set; }
        public object[] ExceptIfAnyOfToCcHeaderMemberOf { get; set; }
        public object[] ExceptIfAnyOfToHeader { get; set; }
        public object[] ExceptIfAnyOfToHeaderMemberOf { get; set; }
        public object[] ExceptIfAttachmentContainsWords { get; set; }
        public object[] ExceptIfAttachmentExtensionMatchesWords { get; set; }
        public bool ExceptIfAttachmentHasExecutableContent { get; set; }
        public bool ExceptIfAttachmentIsPasswordProtected { get; set; }
        public bool ExceptIfAttachmentIsUnsupported { get; set; }
        public object[] ExceptIfAttachmentMatchesPatterns { get; set; }
        public object[] ExceptIfAttachmentNameMatchesPatterns { get; set; }
        public bool ExceptIfAttachmentProcessingLimitExceeded { get; set; }
        public object[] ExceptIfAttachmentPropertyContainsWords { get; set; }
        public object ExceptIfAttachmentSizeOver { get; set; }
        public object[] ExceptIfBetweenMemberOf1 { get; set; }
        public object[] ExceptIfBetweenMemberOf2 { get; set; }
        public object[] ExceptIfContentCharacterSetContainsWords { get; set; }
        public object[] ExceptIfFrom { get; set; }
        public object[] ExceptIfFromAddressContainsWords { get; set; }
        public object[] ExceptIfFromAddressMatchesPatterns { get; set; }
        public object[] ExceptIfFromMemberOf { get; set; }
        public object ExceptIfFromScope { get; set; }
        public object ExceptIfHasClassification { get; set; }
        public bool ExceptIfHasNoClassification { get; set; }
        public bool ExceptIfHasSenderOverride { get; set; }
        public object ExceptIfHeaderContainsMessageHeader { get; set; }
        public object[] ExceptIfHeaderContainsWords { get; set; }
        public object ExceptIfHeaderMatchesMessageHeader { get; set; }
        public object[] ExceptIfHeaderMatchesPatterns { get; set; }
        public object[] ExceptIfManagerAddresses { get; set; }
        public object ExceptIfManagerForEvaluatedUser { get; set; }
        public object[] ExceptIfMessageContainsAllDataClassifications { get; set; }
        public object[] ExceptIfMessageContainsDataClassifications { get; set; }
        public object ExceptIfMessageSizeOver { get; set; }
        public object ExceptIfMessageTypeMatches { get; set; }
        public object[] ExceptIfRecipientADAttributeContainsWords { get; set; }
        public object[] ExceptIfRecipientADAttributeMatchesPatterns { get; set; }
        public object[] ExceptIfRecipientAddressContainsWords { get; set; }
        public object[] ExceptIfRecipientAddressMatchesPatterns { get; set; }
        public object[] ExceptIfRecipientDomainIs { get; set; }
        public object[] ExceptIfRecipientInSenderList { get; set; }
        public object ExceptIfSCLOver { get; set; }
        public object[] ExceptIfSenderADAttributeContainsWords { get; set; }
        public object[] ExceptIfSenderADAttributeMatchesPatterns { get; set; }
        public object[] ExceptIfSenderDomainIs { get; set; }
        public object[] ExceptIfSenderInRecipientList { get; set; }
        public object[] ExceptIfSenderIpRanges { get; set; }
        public object ExceptIfSenderManagementRelationship { get; set; }
        public object[] ExceptIfSentTo { get; set; }
        public object[] ExceptIfSentToMemberOf { get; set; }
        public object ExceptIfSentToScope { get; set; }
        public object[] ExceptIfSubjectContainsWords { get; set; }
        public object[] ExceptIfSubjectMatchesPatterns { get; set; }
        public object[] ExceptIfSubjectOrBodyContainsWords { get; set; }
        public object[] ExceptIfSubjectOrBodyMatchesPatterns { get; set; }
        public object ExceptIfWithImportance { get; set; }
        public object[] Exceptions { get; set; }
        public string ExchangeVersion { get; set; }
        public object ExpiryDate { get; set; }
        public string[] From { get; set; }
        public object[] FromAddressContainsWords { get; set; }
        public object[] FromAddressMatchesPatterns { get; set; }
        public object[] FromMemberOf { get; set; }
        public object FromScope { get; set; }
        public object GenerateIncidentReport { get; set; }
        public object GenerateNotification { get; set; }
        public string Guid { get; set; }
        public object HasClassification { get; set; }
        public bool HasNoClassification { get; set; }
        public bool HasSenderOverride { get; set; }
        public object HeaderContainsMessageHeader { get; set; }
        public object[] HeaderContainsWords { get; set; }
        public object HeaderMatchesMessageHeader { get; set; }
        public object[] HeaderMatchesPatterns { get; set; }
        public string ImmutableId { get; set; }
        public object[] IncidentReportContent { get; set; }
        public bool IsValid { get; set; }
        public string LastModifiedBy { get; set; }
        public object LogEventText { get; set; }
        public object[] ManagerAddresses { get; set; }
        public object ManagerForEvaluatedUser { get; set; }
        public bool ManuallyModified { get; set; }
        public object[] MessageContainsAllDataClassifications { get; set; }
        public object[] MessageContainsDataClassifications { get; set; }
        public object MessageSizeOver { get; set; }
        public object MessageTypeMatches { get; set; }
        public string Mode { get; set; }
        public bool ModerateMessageByManager { get; set; }
        public string[] ModerateMessageByUser { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public object PrependSubject { get; set; }
        public int Priority { get; set; }
        public bool Quarantine { get; set; }
        public object[] RecipientADAttributeContainsWords { get; set; }
        public object[] RecipientADAttributeMatchesPatterns { get; set; }
        public object[] RecipientAddressContainsWords { get; set; }
        public object[] RecipientAddressMatchesPatterns { get; set; }
        public string RecipientAddressType { get; set; }
        public object[] RecipientDomainIs { get; set; }
        public object[] RecipientInSenderList { get; set; }
        public object[] RedirectMessageTo { get; set; }
        public object RejectMessageEnhancedStatusCode { get; set; }
        public object RejectMessageReasonText { get; set; }
        public object RemoveHeader { get; set; }
        public bool RemoveOME { get; set; }
        public bool RemoveOMEv2 { get; set; }
        public bool RemoveRMSAttachmentEncryption { get; set; }
        public object RouteMessageOutboundConnector { get; set; }
        public bool RouteMessageOutboundRequireTls { get; set; }
        public string RuleErrorAction { get; set; }
        public string RuleSubType { get; set; }
        public string RuleVersion { get; set; }
        public object SCLOver { get; set; }
        public object[] SenderADAttributeContainsWords { get; set; }
        public object[] SenderADAttributeMatchesPatterns { get; set; }
        public string SenderAddressLocation { get; set; }
        public object[] SenderDomainIs { get; set; }
        public object[] SenderInRecipientList { get; set; }
        public object[] SenderIpRanges { get; set; }
        public object SenderManagementRelationship { get; set; }
        public object SenderNotificationType { get; set; }
        public object[] SentTo { get; set; }
        public object[] SentToMemberOf { get; set; }
        public object SentToScope { get; set; }
        public string SetAuditSeverity { get; set; }
        public object SetHeaderName { get; set; }
        public object SetHeaderValue { get; set; }
        public object SetSCL { get; set; }
        public object SmtpRejectMessageRejectStatusCode { get; set; }
        public object SmtpRejectMessageRejectText { get; set; }
        public string State { get; set; }
        public bool StopRuleProcessing { get; set; }
        public object[] SubjectContainsWords { get; set; }
        public object[] SubjectMatchesPatterns { get; set; }
        public object[] SubjectOrBodyContainsWords { get; set; }
        public object[] SubjectOrBodyMatchesPatterns { get; set; }
        public bool UseLegacyRegex { get; set; }
        public DateTime WhenChanged { get; set; }
        public object WithImportance { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", Identity, Guid);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", Guid, Identity);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.Guid, this.Identity);
        }
    }

}
