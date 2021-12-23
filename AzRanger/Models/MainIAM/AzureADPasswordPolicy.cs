
namespace AzRanger.Models.MainIAM
{

    //https://portal.azure.com/#blade/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/PasswordProtection
    // Authentication Method for onprem AD

    public class AzureADPasswordPolicy
    {
        public int lockoutThreshold { get; set; }
        public int lockoutDurationInSeconds { get; set; }
        public bool enforceCustomBannedPasswords { get; set; }
        public object[] customBannedPasswords { get; set; }
        public bool enableBannedPasswordCheckOnPremises { get; set; }
        public int bannedPasswordCheckOnPremisesMode { get; set; }
    }

}
