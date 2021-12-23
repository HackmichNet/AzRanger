
namespace AzRanger.Models.MainIAM
{
    public class SecurityDefaults
    {
        public bool anyBaselinePolicyEnabled { get; set; }
        public bool anyCAPolicyEnabled { get; set; }
        public bool securityDefaultsEnabled { get; set; }
        public bool ignoreBaselineProtectionPolicies { get; set; }
        public bool anyClassicPolicyEnabled { get; set; }
        public bool anyIPCEnabled { get; set; }
    }

}
