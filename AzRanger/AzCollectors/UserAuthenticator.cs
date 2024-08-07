﻿using AzRanger.Utilities;
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
        public UserAuthenticator(string tenantId, string proxy, string clientID)
        {
            this.ClientId = clientID;
            if (tenantId == null)
            {
                if (proxy != null)
                {
                    // This is a dirty hack, because some redirect errors in the authentication occurs, have to fix that later
                    if (clientID.Equals("386ce8c0-7421-48c9-a1df-2a532400339f"))
                    {
                        IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithHttpClientFactory(httpClientFactory).WithRedirectUri("ms-appx-web://microsoft.aad.brokerplugin/386ce8c0-7421-48c9-a1df-2a532400339f").WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
                    }
                    else if (clientID.Equals("14d82eec-204b-4c2f-b7e8-296a70dab67e"))
                    {
                        IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithHttpClientFactory(httpClientFactory).WithDefaultRedirectUri().WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();

                    }
                    else if (clientID.Equals("9bc3ab49-b65d-410a-85ad-de819febfddc"))
                    {
                        IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithHttpClientFactory(httpClientFactory).WithRedirectUri("https://oauth.spops.microsoft.com/").WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
                    }
                    else
                    {
                        IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithHttpClientFactory(httpClientFactory).WithDefaultRedirectUri().WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
                    }
                }
                else
                {
                    if (clientID.Equals("386ce8c0-7421-48c9-a1df-2a532400339f"))
                    {
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithRedirectUri("ms-appx-web://microsoft.aad.brokerplugin/386ce8c0-7421-48c9-a1df-2a532400339f").WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();

                    }
                    else if (clientID.Equals("14d82eec-204b-4c2f-b7e8-296a70dab67e"))
                    {
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithDefaultRedirectUri().WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();

                    }
                    else
                    {
                        App = PublicClientApplicationBuilder.Create(this.ClientId).WithDefaultRedirectUri().WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
                    }
                }
            }
            else
            {
                this.Authority = Authority + "/" + tenantId + "/";
                if (proxy != null)
                {
                    IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                    App = PublicClientApplicationBuilder.Create(this.ClientId).WithHttpClientFactory(httpClientFactory).WithAuthority(Authority).WithTenantId(tenantId).WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
                }
                else
                {
                    App = PublicClientApplicationBuilder.Create(this.ClientId).WithAuthority(Authority).WithTenantId(tenantId).WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
                }
            }
        }

        public UserAuthenticator(String Username, String Password, string tenantId, string proxy, string clientID)
        {
            this.ClientId = clientID;
            this.Authority = Authority + "/" + tenantId + "/";
            this.Username = Username;
            this.Password = new SecureString();
            foreach (char c in Password)
            {
                this.Password.AppendChar(c);
            }
            if (proxy != null)
            {
                IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                App = PublicClientApplicationBuilder.Create(this.ClientId).WithHttpClientFactory(httpClientFactory).WithAuthority(Authority).WithTenantId(tenantId).WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
            }
            else
            {
                App = PublicClientApplicationBuilder.Create(this.ClientId).WithAuthority(Authority).WithTenantId(tenantId).WithCacheOptions(CacheOptions.EnableSharedCacheOptions).Build();
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
