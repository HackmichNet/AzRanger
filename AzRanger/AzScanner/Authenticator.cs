using Microsoft.Identity.Client;
using NLog;
using System;
using System.Linq;
using System.Security;
using Logger = NLog.Logger;

namespace AzRanger.AzScanner
{
    public class Authenticator
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string Authority = "https://login.microsoftonline.com";
        IPublicClientApplication App;
        //  1950a258-227b-4e31-a9cf-717495945fc2
        private const String ClientId = "1b730954-1685-4b74-9bfd-dac224a7b894";
        private String Username;
        private SecureString Password;
        public Authenticator(string tenantId)
        {
            if (tenantId == null)
            {
                App = PublicClientApplicationBuilder.Create(ClientId).WithDefaultRedirectUri().Build();
            }
            else
            {
                this.Authority = Authority + "/" + tenantId + "/";
                App = PublicClientApplicationBuilder.Create(ClientId).WithAuthority(Authority).WithTenantId(tenantId).WithDefaultRedirectUri().Build();
            }
            
        }

        public Authenticator(string tenantId, String Username, String Password)
        {
            this.Authority = Authority + "/" + tenantId + "/";
            this.Username = Username;
            this.Password = new SecureString();
            foreach (char c in Password)
            {       
                this.Password.AppendChar(c);
            }
            App = PublicClientApplicationBuilder.Create(ClientId).WithAuthority(Authority).WithTenantId(tenantId).Build(); 
        }

        public String GetUserId()
        {
            String id = GetAuthenticationResult(null).UniqueId;
            if(id == null)
            {
                return null;
            }
            return id;
        }

        public String GetAccessToken(String[] scopes)
        {
            return GetAuthenticationResult(scopes).AccessToken;
        }

        public String GetTenantId()
        {
            return GetAuthenticationResult(null).TenantId;
        }
        private AuthenticationResult GetAuthenticationResult(String[] scopes)
        {
            var accounts = App.GetAccountsAsync().GetAwaiter().GetResult();
            AuthenticationResult result = null;
            if (accounts.Any())
            {
                result = App.AcquireTokenSilent(scopes, accounts.FirstOrDefault()).ExecuteAsync().GetAwaiter().GetResult();
                return result;
            }
            try
            {
                if (this.Username == null && this.Password == null)
                {
                    result = App.AcquireTokenInteractive(scopes).WithUseEmbeddedWebView(true).ExecuteAsync().GetAwaiter().GetResult();
                    return result;
                }
                else
                {
                    result = App.AcquireTokenByUsernamePassword(scopes, this.Username, this.Password).ExecuteAsync().GetAwaiter().GetResult();
                    return result;
                }
            }
            catch (MsalException ex)
            {
                // More errors on: https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-desktop-acquire-token-username-password?tabs=dotnet
                logger.Warn(ex.ErrorCode);
                logger.Warn(ex.Message);
                return null;
            }
        }
    }
}
