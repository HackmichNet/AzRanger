using AzRanger.Models;
using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class MainIamScanner : IScanner
    {
        public const String SecurityDefaultsEndpoint = "/api/SecurityDefaults/GetSecurityDefaultStatus";
        public const String DirectoryProperties = "/api/Directories/Properties";
        public const String PasswordsResetPolicies = "/api/PasswordReset/PasswordResetPolicies?getPasswordResetEnabledGroup=true";
        public const String PasswordPolicy = "/api/AuthenticationMethods/PasswordPolicy";
        public const String ADConnectStatus = "/api/Directories/ADConnectStatus";
        public const String DirSyncStatus = "/api/Directories/GetPasswordSyncStatus";
        public const String B2BPolicy = "/api/B2B/b2bPolicy";
        public const String LCMSettings = "/api/Directories/LcmSettings";
        public const String UserSettings = "/api/EnterpriseApplications/UserSettings";


        public MainIamScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://main.iam.ad.ext.azure.com";
            this.Scope = new string[] { "74658136-14ec-4630-ad9b-26e160ff0fc6/.default", "offline_access" };
        }

        public SecurityDefaults GetSecurityDefaults()
        {
            return (SecurityDefaults)Get<SecurityDefaults>( MainIamScanner.SecurityDefaultsEndpoint);
        }

        public UserSettings GetUserSettings()
        {
            return (UserSettings)Get<UserSettings>(MainIamScanner.UserSettings);
        }

        public LCMSettings GetLCMSettings()
        {
            return (LCMSettings)Get<LCMSettings>(MainIamScanner.LCMSettings);
        }

        public B2BPolicy GetB2BPolicy()
        {
            return (B2BPolicy)Get<B2BPolicy>(MainIamScanner.B2BPolicy);
        }

        public DirectoryProperties GetDirectoryProperties()
        {
            return (DirectoryProperties)Get<DirectoryProperties>(MainIamScanner.DirectoryProperties);
        }

        public PasswordResetPolicies GetPasswordResetPolicies()
        {
            return (PasswordResetPolicies)Get<PasswordResetPolicies>(MainIamScanner.PasswordsResetPolicies);
        }

        public AzureADPasswordPolicy GetPasswordPolicy()
        {
            return (AzureADPasswordPolicy)Get<AzureADPasswordPolicy>(MainIamScanner.PasswordPolicy);
        }

        public ADConnectStatus GetADConnectStatus()
        {
            ADConnectStatus status = (ADConnectStatus)Get<ADConnectStatus>( MainIamScanner.ADConnectStatus);
            if(status == null)
            {
                logger.Warn("MainIamScanner.ADConnectStatus: Failed to get status");
                return null;
            }
            status.passwordHashSyncEnabled = (bool)Get<bool>(MainIamScanner.DirSyncStatus);
            return status;
        }
    }
}
