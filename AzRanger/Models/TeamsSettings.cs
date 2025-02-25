using AzRanger.Models.Teams;

namespace AzRanger.Models
{
    public class TeamsSettings
    {
        public TeamsClientConfiguration TeamsClientConfiguration { get; set; }
        public TenantFederationSettings TenantFederationSettings { get; set; }
        public TeamsMeetingPolicy TeamsMeetingPolicy { get; set; }
    }
}
