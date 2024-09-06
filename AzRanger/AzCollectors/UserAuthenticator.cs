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
        private String ClientId;
        private String Username;
        private SecureString Password;
        private int FailedInteractiveLogonCounter = 0;
        public const string CacheFileName = "azranger.cache";
        public readonly static string CacheDir = MsalCacheHelper.UserRootDirectory;
        // https://blog.cdemi.io/async-waiting-inside-c-sharp-locks/#:~:text=The%20lock%20keyword%20can%20only,is%20used%20pretty%20much%20everywhere.
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public UserAuthenticator(string tenantId, string proxy, string clientID, string redirectUrl = null)
        {
            this.ClientId = clientID;
            PublicClientApplicationBuilder builder = PublicClientApplicationBuilder.Create(this.ClientId);
            if(tenantId != null)
            {
                this.Authority = Authority + "/" + tenantId + "/";
                builder.WithTenantId(tenantId);
            }
            
            if(proxy != null)
            {
                IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                builder.WithHttpClientFactory(httpClientFactory);
            }
            if(redirectUrl != null)
            {
                builder.WithRedirectUri(redirectUrl);
            }
            builder.WithCacheOptions(CacheOptions.EnableSharedCacheOptions);
            App = builder.Build();

        }
                
        public UserAuthenticator(String Username, String Password, string tenantId, string proxy, string clientID, string redirectUrl = null)
        {
            this.ClientId = clientID;
            this.Authority = Authority + "/" + tenantId + "/";
            this.Username = Username;
            this.Password = new SecureString();
            foreach (char c in Password)
            {
                this.Password.AppendChar(c);
            }
            PublicClientApplicationBuilder builder = PublicClientApplicationBuilder.Create(this.ClientId);
            if (proxy != null)
            {
                IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                builder.WithHttpClientFactory(httpClientFactory);
            }

            builder.WithTenantId(tenantId);
            builder.WithCacheOptions(CacheOptions.EnableSharedCacheOptions);
            if(redirectUrl != null)
            {
                builder.WithRedirectUri(redirectUrl);
            }

            App = builder.Build();
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
            AuthenticationResult result;
            if (accounts.Any())
            {
                try
                {
                    result = await App.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync();
                    semaphoreSlim.Release();
                    return result;
                }
                catch (MsalException ex)
                {
                    // Under some circumstances, a lot of logons failing, then we don't logon anymore and skip the rest.
                    if (FailedInteractiveLogonCounter > 4)
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
                        }
                        catch (MsalServiceException ex2)
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
                    result = await App.AcquireTokenByUsernamePassword(scopes, Username, Password).ExecuteAsync();
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
