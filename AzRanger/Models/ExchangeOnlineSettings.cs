using AzRanger.Models.AdminCenter;
using AzRanger.Models.ExchangeOnline;
using System.Collections.Generic;

namespace AzRanger.Models
{
    public class ExchangeOnlineSettings
    {
        public List<MalwareFilterPolicy> MalwareFilterPolicy { get; set; }
        public List<HostedOutboundSpamFilterPolicy> HostedOutboundSpamFilterPolicy { get; set; }
        public List<TransportRule> TransportRules { get; set; }
        public List<AcceptedDomain> AcceptedDomains { get; set; }
        public List<DkimSigningConfig> DkimSigningConfigs { get; set; }
        public ExchangeModernAuthSettings ExchangeModernAutheSettings { get; set; }
        public List<MalwareFilterRule> MalwareFilterRule { get; set; }
        public AdminAuditLogConfig AdminAuditLogConfig { get; set; }
        public List<Mailbox> Mailboxes { get; set; }
        public List<RemoteDomain> RemoteDomains { get; set; }
        public List<RoleAssignmentPolicy> RoleAssignmentPolicies { get; set; }
        public OrganizationConfig OrganizationConfig { get; set; }
        public List<AuthenticationPolicy> AuthenticationPolicies { get; set; }
        public List<EXOUser> EXOUsers { get; set; }
        public OwaMailboxPolicy OwaMailboxPolicy { get; set; }
        public List<MailboxAuditBypassAssociation> MailboxAuditBypassAssociations { get; set; }
        public List<ExternalInOutlook> ExternalInOutlooks { get; set; }
        public List<HostedConnectionFilterPolicy> HostedConnectionFilterPolicy { get; set; }
        public List<HostedContentFilterPolicy> HostedContentFilterPolicies { get; set; }

    }
}
