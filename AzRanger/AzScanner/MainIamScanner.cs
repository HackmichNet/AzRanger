using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class MainIamScanner : IScannerModule
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
            this.client = Helper.GetDefaultClient(additionalHeaders, this.Scanner.Proxy);
        }

        public Task<SecurityDefaults> GetSecurityDefaults()
        {
            return Get<SecurityDefaults>( MainIamScanner.SecurityDefaultsEndpoint);
        }

        public Task<SsgmProperties> GetSsgmProperties()
        {
            return Get<SsgmProperties>(MainIamScanner.SsgmProperties);
        }

        public Task<TenantSkuInfo> GetTenantSkuInfo()
        {
            return Get<TenantSkuInfo>(MainIamScanner.TenantSkuInfo);
        }

        public Task<UserSettings> GetUserSettings()
        {
            return Get<UserSettings>(MainIamScanner.UserSettings);
        }

        public Task<LCMSettings> GetLCMSettings()
        {
            return Get<LCMSettings>(MainIamScanner.LCMSettings);
        }

        public Task<B2BPolicy> GetB2BPolicy()
        {
            return Get<B2BPolicy>(MainIamScanner.B2BPolicy);
        }

        public Task<DirectoryProperties> GetDirectoryProperties()
        {
            return Get<DirectoryProperties>(MainIamScanner.DirectoryProperties);
        }

        public Task<PasswordResetPolicies> GetPasswordResetPolicies()
        {
            return Get<PasswordResetPolicies>(MainIamScanner.PasswordsResetPolicies);
        }

        public Task<AzureADPasswordPolicy> GetPasswordPolicy()
        {
            return Get<AzureADPasswordPolicy>(MainIamScanner.PasswordPolicy);
        }

        public async Task<ADConnectStatus> GetADConnectStatus()
        {
            ADConnectStatus status = await Get<ADConnectStatus>( MainIamScanner.ADConnectStatus);
            if(status == null)
            {
                logger.Warn("MainIamScanner.ADConnectStatus: Failed to get status");
                return null;
            }
            status.passwordHashSyncEnabled = (bool) await Get<bool>(MainIamScanner.DirSyncStatus);
            return status;
        }

        public Task<List<LoginTenantBranding>> GetLoginTenantBrandings()
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
