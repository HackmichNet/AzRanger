using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;

namespace AzRanger.AzScanner
{
    class MainIamScanner : IScanner
    {
        private const String SecurityDefaultsEndpoint = "/api/SecurityDefaults/GetSecurityDefaultStatus";
        private const String DirectoryProperties = "/api/Directories/Properties";
        private const String PasswordsResetPolicies = "/api/PasswordReset/PasswordResetPolicies?getPasswordResetEnabledGroup=true";
        private const String PasswordPolicy = "/api/AuthenticationMethods/PasswordPolicy";
        private const String ADConnectStatus = "/api/Directories/ADConnectStatus";
        private const String DirSyncStatus = "/api/Directories/GetPasswordSyncStatus";
        private const String B2BPolicy = "/api/B2B/b2bPolicy";
        private const String LCMSettings = "/api/Directories/LcmSettings";
        private const String UserSettings = "/api/EnterpriseApplications/UserSettings";
        private const String TenantSkuInfo = "/api/TenantSkuInfo";
        private const String SsgmProperties = "/api/Directories/SsgmProperties";
        private const String LoginTenantBrandings = "/api/LoginTenantBrandings";


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

        public SsgmProperties GetSsgmProperties()
        {
            return (SsgmProperties)Get<SsgmProperties>(MainIamScanner.SsgmProperties);
        }

        public TenantSkuInfo GetTenantSkuInfo()
        {
            return (TenantSkuInfo)Get<TenantSkuInfo>(MainIamScanner.TenantSkuInfo);
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

        public List<LoginTenantBranding> GetLoginTenantBrandings()
        {
            return GetAllOf<LoginTenantBranding>(MainIamScanner.LoginTenantBrandings);
        }

        internal override String ManipulateResponse(String response, String endPoint){
            if (endPoint == MainIamScanner.LoginTenantBrandings)
            {
                response = @"{""value"":" + response + "}";
            }
            return response;
        }
    }
}
