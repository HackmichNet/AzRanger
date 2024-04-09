namespace AzRanger.Models.Teams
{
    // https://admin.teams.microsoft.com/company-wide-settings/external-communications
    public class TenantFederationSettings
    {
        public Alloweddomains AllowedDomains { get; set; }
        public Blockeddomain[] BlockedDomains { get; set; }

        // "Teams and Skype for Business users in external organizations"
        // True => If, all, allowlist or denylist
        // False => All external is blocked
        public bool AllowFederatedUsers { get; set; }

        // "Allow users in my organization to communicate with Skype users."
        public bool AllowPublicUsers { get; set; }

        // "People in my organization can communicate with Teams users whose accounts aren't managed by an organization."
        public bool AllowTeamsConsumer { get; set; }

        // "External users with Teams accounts not managed by an organization can contact users in my organization." => No effect if AllowTeamsConsumer=False
        public bool AllowTeamsConsumerInbound { get; set; }
        public bool TreatDiscoveredPartnersAsUnverified { get; set; }
        public bool SharedSipAddressSpace { get; set; }
        public Key Key { get; set; }
        public string Identity { get; set; }
    }

    public class Tenantfederationsettings
    {
        public string xmlns { get; set; }
        public Alloweddomains AllowedDomains { get; set; }
        public object BlockedDomains { get; set; }
    }

    public class Blockeddomain
    {
        public string Domain { get; set; }
    }
}
