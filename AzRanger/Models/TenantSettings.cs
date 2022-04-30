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
    public class TenantSettings
    {
        public TenantSettings()
        {
            this.ExchangeOnlineSettings = new ExchangeOnlineSettings();
            this.AdminCenterSettings = new AdminCenterSettings();
            this.TeamsSettings = new TeamsSettings();
        }
        public string TenantId { get; set; }

        public List<Domain> Domains;
        public List<EnterpriseApplicationUserSettings> EnterpriseApplicationUserSettings { get; set; }
        public SecurityDefaults SecurityDefaults { get; set; }
        public DirectoryProperties DirectoryProperties { get; set; }
        public PasswordResetPolicies PasswordResetPolicies { get; set; }
        public AzureADPasswordPolicy PasswordPolicy { get; set; }
        public Dictionary<Guid, ConditionalAccessPolicy> AllCAPolicies { get; set; }
        public ADConnectStatus ADConnectStatus { get; set; }
        public SharepointInformation SharepointInformation { get; set; }
        public MsolCompanyInformation MSOLCompanyInformation { get; set; }
        public B2BPolicy B2BPolicy { get; set; }
        public LCMSettings LCMSettings { get; set; }
        public UserSettings UserSettings { get; set; }
        public List<DlpCompliancePolicy> OfficeDLPPolicies { get; set; }
        public ExchangeOnlineSettings ExchangeOnlineSettings { get; set; }
        public AdminCenterSettings AdminCenterSettings { get; set; }
        public TeamsSettings TeamsSettings { get; set; }
        public TenantSkuInfo TenantSkuInfo { get; set; }
        public AuthorizationPolicy AuthorizationPolicy { get; set; }
        public SsgmProperties SsgmProperties { get; set; }
        public List<ManagementGroup> ManagementGroups { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
