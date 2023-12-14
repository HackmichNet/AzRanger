using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class MainIamCollector : AbstractCollector
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
        private const String OnPremisesPasswordResetPolicies = "/api/PasswordReset/OnPremisesPasswordResetPolicies";

        public MainIamCollector(MainCollector scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://main.iam.ad.ext.azure.com";
            this.Scope = new string[] { "74658136-14ec-4630-ad9b-26e160ff0fc6/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, scanner.Proxy);
        }

        public Task<OnPremisesPasswordResetPolicy> GetOnPremisesPasswordResetPolicy()
        {
            return Get<OnPremisesPasswordResetPolicy>(MainIamCollector.OnPremisesPasswordResetPolicies);
        }

        public Task<SecurityDefaults> GetSecurityDefaults()
        {
            return Get<SecurityDefaults>(MainIamCollector.SecurityDefaultsEndpoint);
        }

        public Task<SsgmProperties> GetSsgmProperties()
        {
            return Get<SsgmProperties>(MainIamCollector.SsgmProperties);
        }

        public Task<TenantSkuInfo> GetTenantSkuInfo()
        {
            return Get<TenantSkuInfo>(MainIamCollector.TenantSkuInfo);
        }

        public Task<UserSettings> GetUserSettings()
        {
            return Get<UserSettings>(MainIamCollector.UserSettings);
        }

        public Task<LCMSettings> GetLCMSettings()
        {
            return Get<LCMSettings>(MainIamCollector.LCMSettings);
        }

        public Task<B2BPolicy> GetB2BPolicy()
        {
            return Get<B2BPolicy>(MainIamCollector.B2BPolicy);
        }

        public Task<DirectoryProperties> GetDirectoryProperties()
        {
            return Get<DirectoryProperties>(MainIamCollector.DirectoryProperties);
        }

        public Task<PasswordResetPolicies> GetPasswordResetPolicies()
        {
            return Get<PasswordResetPolicies>(MainIamCollector.PasswordsResetPolicies);
        }

        public Task<AzureADPasswordPolicy> GetPasswordPolicy()
        {
            return Get<AzureADPasswordPolicy>(MainIamCollector.PasswordPolicy);
        }

        public async Task<ADConnectStatus> GetADConnectStatus()
        {
            ADConnectStatus status = await Get<ADConnectStatus>(MainIamCollector.ADConnectStatus);
            if(status == null)
            {
                logger.Warn("MainIamScanner.ADConnectStatus: Failed to get status");
                return null;
            }
            status.passwordHashSyncEnabled = (bool) await Get<bool>(MainIamCollector.DirSyncStatus);
            return status;
        }

        public Task<List<LoginTenantBranding>> GetLoginTenantBrandings()
        {
            //return GetAllOf<LoginTenantBranding>(MainIamScanner.LoginTenantBrandings);
            return Get<List<LoginTenantBranding>>(MainIamCollector.LoginTenantBrandings);
        }
    }
}
