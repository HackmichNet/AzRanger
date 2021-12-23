using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class HostedOutboundSpamFilterPolicy
    {
        public string Identity { get; set; }
        public string ActionWhenThresholdReached { get; set; }
        public string AdminDisplayName { get; set; }
        public string AutoForwardingMode { get; set; }
        public string[] BccSuspiciousOutboundAdditionalRecipients { get; set; }
        public bool BccSuspiciousOutboundMail { get; set; }
        public string ConfigurationType { get; set; }
        public string DistinguishedName { get; set; }
        public bool Enabled { get; set; }
        public string ExchangeObjectId { get; set; }
        public string ExchangeVersion { get; set; }
        public string Guid { get; set; }
        public string Id { get; set; }
        public bool IsDefault { get; set; }
        public bool IsValid { get; set; }
        public object Item { get; set; }
        public string Name { get; set; }
        public bool NotifyOutboundSpam { get; set; }
        public string[] NotifyOutboundSpamRecipients { get; set; }
        public string ObjectCategory { get; set; }
        public string[] ObjectClass { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string OriginatingServer { get; set; }
        public string RecipientLimitExternalPerHour { get; set; }
        public string RecipientLimitInternalPerHour { get; set; }
        public string RecipientLimitPerDay { get; set; }
        public string RecommendedPolicyType { get; set; }
        public DateTime WhenChanged { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public DateTime WhenCreated { get; set; }
        public DateTime WhenCreatedUTC { get; set; }
    }


}
