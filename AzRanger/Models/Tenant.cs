using AzRanger.Models.AdminCenter;
using AzRanger.Models.AzMgmt;
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
        public Tenant()
        {
            this.ExchangeOnlineSettings = new ExchangeOnlineSettings();
            this.MDMSettings = new MDMSettings();
            this.AdminCenterSettings = new AdminCenterSettings();
            this.TeamsSettings = new TeamsSettings();
        }
        public string TenantId { get; set; }
        public string Username { get; set; }
        public List<Domain> domains;
        public Dictionary<Guid, User> AllUsers { get; set; }
        public Dictionary<Guid, User> AllGuests { get; set; }
        public Dictionary<Guid, MSGraph.DirectoryRole> AllDirectoryRoles { get; set; }
        public Dictionary<Guid, Application> AllApplications { get; set; }
        public Dictionary<Guid, ServicePrincipal> AllServicePrincipals { get; set; }
        public Dictionary<Guid, Group> AllGroups { get; set; }
        public List<EnterpriseApplicationUserSettings> EnterpriseApplicationUserSettings { get; set; }
        public SecurityDefaults SecurityDefaults { get; set; }
        public DirectoryProperties DirectoryProperties { get; set; }
        public PasswordResetPolicies PasswordResetPolicies { get; set; }
        public AzureADPasswordPolicy PasswordPolicy { get; set; }
        public Dictionary<Guid, ConditionalAccessPolicy> AllCAPolicies { get; set; }
        public ADConnectStatus ADConnectStatus { get; set; }
        public SharepointInformation SharepointInformation{ get; set; }
        public B2BPolicy B2BPolicy { get; set; }
        public LCMSettings LCMSettings { get; set; }
        public UserSettings UserSettings { get; set; }
        public List<DlpCompliancePolicy> OfficeDLPPolicies { get; set; }
        public ExchangeOnlineSettings ExchangeOnlineSettings { get; set; }
        public MDMSettings MDMSettings { get; set; }
        public AdminCenterSettings AdminCenterSettings { get; set; }
        public TeamsSettings TeamsSettings { get; set; }
        public List<RoleDefinition> RoleDefinitions { get; set; }
        public TenantSkuInfo TenantSkuInfo { get; set; }
        public AuthorizationPolicy AuthorizationPolicy { get; set; }
        public SsgmProperties SsgmProperties { get; set; }
        public MsolCompanyInformation MSOLCompanyInformation { get; set; }
        public Dictionary<String, ManagementGroup> ManagementGroups{ get; set; }
        public ManagementGroupSettings ManagementGroupSettings { get; set; }
        public Dictionary<Guid, Subscription> Subscriptions { get; set; }
        public DeviceRegistrationPolicy DeviceRegistrationPolicy { get; set; }
        public List<DlpLabel> DlpLabels { get; set; }  
        public AuthenticationMethodsPolicy AuthenticationMethodsPolicy { get; set; }
    }
}
