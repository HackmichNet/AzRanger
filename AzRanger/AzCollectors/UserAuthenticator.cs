using AzRanger.Utilities;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using NLog;
using System;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Logger = NLog.Logger;

namespace AzRanger.AzScanner
{
    public class UserAuthenticator : IAuthenticator
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly string Authority = "https://login.microsoftonline.com";
        IPublicClientApplication App;
        // AzurePowerShell = "1950a258-227b-4e31-a9cf-717495945fc2"
        // GlobalPowerShell = "1b730954-1685-4b74-9bfd-dac224a7b894"
        private const String ClientId = "1b730954-1685-4b74-9bfd-dac224a7b894";
        private String Username;
        private SecureString Password;
        private int FailedInteractiveLogonCounter = 0;
        public const string CacheFileName = "azranger.cache";
        public readonly static string CacheDir = MsalCacheHelper.UserRootDirectory;
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public UserAuthenticator(string tenantId, string proxy)
        {
            //var storageProperties = new StorageCreationPropertiesBuilder(CacheFileName, CacheDir).Build();
            //var cacheHelper = MsalCacheHelper.CreateAsync(storageProperties).Result;
            if (tenantId == null)
            {
                if (proxy != null)
                {
                    IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                    App = PublicClientApplicationBuilder.Create(ClientId).WithHttpClientFactory(httpClientFactory).WithDefaultRedirectUri().Build();
                }
                else
                {
                    App = PublicClientApplicationBuilder.Create(ClientId).WithDefaultRedirectUri().Build();
                }
            }
            else
            {
                this.Authority = Authority + "/" + tenantId + "/";
                if (proxy != null) {
                    IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                    App = PublicClientApplicationBuilder.Create(ClientId).WithHttpClientFactory(httpClientFactory).WithAuthority(Authority).WithTenantId(tenantId).WithDefaultRedirectUri().Build();
                }
                else
                {
                    App = PublicClientApplicationBuilder.Create(ClientId).WithAuthority(Authority).WithTenantId(tenantId).WithDefaultRedirectUri().Build();
                }
            }
            //cacheHelper.RegisterCache(App.UserTokenCache);
        }

        public UserAuthenticator(String Username, String Password, string tenantId, string proxy)
        {
            this.Authority = Authority + "/" + tenantId + "/";
            this.Username = Username;
            this.Password = new SecureString();
            foreach (char c in Password)
            {       
                this.Password.AppendChar(c);
            }
            if (proxy != null) {
                IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                App = PublicClientApplicationBuilder.Create(ClientId).WithHttpClientFactory(httpClientFactory).WithAuthority(Authority).WithTenantId(tenantId).WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
            }
            else
            {
                App = PublicClientApplicationBuilder.Create(ClientId).WithAuthority(Authority).WithTenantId(tenantId).WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
            }
        }

        public async Task<String> GetUserId()
        {
            String[] scope = new string[] { "offline_access" };
            AuthenticationResult result = await GetAuthenticationResult(scope);
            if (result.UniqueId == null)
            {
                return null;
            }
            return result.UniqueId;
        }

        public async Task<String> GetAccessToken(String[] scopes)
        {
            AuthenticationResult authenticationResult = await GetAuthenticationResult(scopes);
            if (authenticationResult == null)
            {
                return null;
            }
            return authenticationResult.AccessToken;
        }

        public async Task<String> GetTenantId()
        {
            AuthenticationResult result = await GetAuthenticationResult(null);
            return result.TenantId;
        }

        public async Task<String> GetUsername()
        {
            AuthenticationResult result = await GetAuthenticationResult(null);
            return result.Account.Username;
        }
        private async Task<AuthenticationResult> GetAuthenticationResult(String[] scopes)
        {
            await semaphoreSlim.WaitAsync();
            var accounts = await App.GetAccountsAsync();
            AuthenticationResult result = null;
            if (accounts.Any())
            {
                try
                {
                    result = await App.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
                    semaphoreSlim.Release();
                    return result;
                }catch(MsalException ex)
                {
                    // Under some circumstances, a lot of logons failing, then we don't logon anymore and skip the rest.
                    if(FailedInteractiveLogonCounter > 4)
                    {
                        semaphoreSlim.Release();
                        return null;
                    }
                    // May happen that we need do MFA again.
                    if (this.Username == null && this.Password == null)
                    {
                        try
                        {
                            result = await App.AcquireTokenInteractive(scopes).WithUseEmbeddedWebView(true).ExecuteAsync();
                            semaphoreSlim.Release();
                            return result;
                        }catch(MsalServiceException ex2)
                        {
                            logger.Warn(ex2.ErrorCode);
                            logger.Warn(ex2.Message);
                            FailedInteractiveLogonCounter++;
                            semaphoreSlim.Release();
                            return null;
                        }
                    }

                    logger.Warn(ex.ErrorCode);
                    logger.Warn(ex.Message);
                    semaphoreSlim.Release();
                    return null;
                }
            }

            try
            {
                if (this.Username == null && this.Password == null)
                {
                    result = await App.AcquireTokenInteractive(scopes).WithUseEmbeddedWebView(true).ExecuteAsync();
                    semaphoreSlim.Release();
                    return result;
                }
                else
                {
                    result = await App.AcquireTokenByUsernamePassword(scopes, this.Username, this.Password).ExecuteAsync();
                    semaphoreSlim.Release();
                    return result;
                }
            }
            catch (MsalException ex)
            {
                // More errors on: https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-desktop-acquire-token-username-password?tabs=dotnet
                logger.Warn(ex.ErrorCode);
                logger.Warn(ex.Message);
                semaphoreSlim.Release();
                return null;
            }
        }
    }
}
