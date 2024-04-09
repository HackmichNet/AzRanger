using AzRanger.Models.AzMgmt;
using AzRanger.Models.MSGraph;
using AzRanger.Models.WinGraph;
using System;
using System.Collections.Generic;

namespace AzRanger.Models
{
    public class Tenant
    {
        public string TenantId { get; set; }
        public string Username { get; set; }
        public List<Domain> Domains;
        public Dictionary<Guid, User> Users { get; set; }
        public Dictionary<Guid, User> Guests { get; set; }
        public Dictionary<Guid, MSGraph.DirectoryRole> DirectoryRoles { get; set; }
        public Dictionary<Guid, Application> Applications { get; set; }
        public Dictionary<Guid, ServicePrincipal> ServicePrincipals { get; set; }
        public Dictionary<Guid, Group> Groups { get; set; }
        public List<EnterpriseApplicationUserSettings> EnterpriseApplicationUserSettings { get; set; }
        public M365Settings TenantSettings { get; set; }
        public Dictionary<Guid, ConditionalAccessPolicy> CAPolicies { get; set; }
        public SharePointInformation SharePointInformation { get; set; }
        public ExchangeOnlineSettings ExchangeOnlineSettings { get; set; }
        public TeamsSettings TeamsSettings { get; set; }
        public List<RoleDefinition> RoleDefinitions { get; set; }
        public Dictionary<String, ManagementGroup> ManagementGroups { get; set; }
        public Dictionary<Guid, Subscription> Subscriptions { get; set; }
        public MDMSettings MDMSettings { get; set; }
        public ManagementGroupSettings ManagementGroupSettings { get; set; }
        public SubscriptionPolicy SubscriptionPolicy { get; set; }
    }
}
