using AzRanger.Models.AdminCenter;
using AzRanger.Models.AzMgmt;
using AzRanger.Models.Azrbac;
using AzRanger.Models.ComplianceCenter;
using AzRanger.Models.MainIAM;
using AzRanger.Models.MSGraph;
using AzRanger.Models.Provision;
using AzRanger.Models.WinGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models
{
    public class Tenant
    {
        public string TenantId { get; set; }
        public string Username { get; set; }
        public List<Domain> Domains;
        public Dictionary<Guid, User> AllUsers { get; set; }
        public Dictionary<Guid, User> AllGuests { get; set; }
        public Dictionary<Guid, MSGraph.DirectoryRole> AllDirectoryRoles { get; set; }
        public Dictionary<Guid, Application> AllApplications { get; set; }
        public Dictionary<Guid, ServicePrincipal> AllServicePrincipals { get; set; }
        public Dictionary<Guid, Group> AllGroups { get; set; }
        public List<EnterpriseApplicationUserSettings> EnterpriseApplicationUserSettings { get; set; }
        public List<PIMRoleAssignments> PIMRoleAssignments { get; set; }
        public M365Settings TenantSettings { get; set; }
        public Dictionary<Guid, ConditionalAccessPolicy> AllCAPolicies { get; set; }
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
