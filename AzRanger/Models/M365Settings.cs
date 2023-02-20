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
    public class M365Settings
    {
        public List<Domain> Domains;
        public SecurityDefaults SecurityDefaults { get; set; }
        public DirectoryProperties DirectoryProperties { get; set; }
        public PasswordResetPolicies PasswordResetPolicies { get; set; }
        public AzureADPasswordPolicy PasswordPolicy { get; set; }
        public ADConnectStatus ADConnectStatus { get; set; }
        public MsolCompanyInformation MSOLCompanyInformation { get; set; }
        public B2BPolicy B2BPolicy { get; set; }
        public LCMSettings LCMSettings { get; set; }
        public UserSettings UserSettings { get; set; }
        public List<DlpCompliancePolicy> OfficeDLPPolicies { get; set; }
        public AdminCenterSettings AdminCenterSettings { get; set; }
        public TenantSkuInfo TenantSkuInfo { get; set; }
        public AuthorizationPolicy AuthorizationPolicy { get; set; }
        public SsgmProperties SsgmProperties { get; set; }
        public DeviceRegistrationPolicy DeviceRegistrationPolicy { get; set; }
        public List<DlpLabel> DlpLabels { get; set; }
        public AuthenticationMethodsPolicy AuthenticationMethodsPolicy { get; set; }
        public DirSyncFeatures DirSyncFeatures { get; set; }
        public List<LoginTenantBranding> LoginTenantBrandings { get; set; }
    }
}
