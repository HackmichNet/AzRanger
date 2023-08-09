using AzRanger.Utilities;
using Microsoft.Identity.Client;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class AppAuthenticator : IAuthenticator
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger();
        private IConfidentialClientApplication app;
        private String ClientId;
        private String TenantId;

        public AppAuthenticator(String ClientId, String ClientSecret, string tenantId, string proxy)
        {
            this.ClientId = ClientId;
            this.TenantId = tenantId;
            if (proxy != null)
            {
                IMsalHttpClientFactory httpClientFactory = new HttpFactoryWithProxy(proxy);
                app = ConfidentialClientApplicationBuilder.Create(ClientId)
                    .WithClientSecret(ClientSecret)
                    .WithTenantId(tenantId)
                    .WithHttpClientFactory(httpClientFactory)
                    .Build();
            }
            else
            {
                app = ConfidentialClientApplicationBuilder.Create(ClientId)
                    .WithClientSecret(ClientSecret)
                    .WithTenantId(tenantId)
                    .Build();
            }
        }
        public async Task<string> GetAccessToken(string[] scopes)
        {
            var authResult = await app.AcquireTokenForClient(scopes).ExecuteAsync();
            return authResult.AccessToken;
        }

        public async Task<string> GetTenantId()
        {
            return await Task.FromResult(this.TenantId);
        }

        public async Task<string> GetUserId()
        {
            return await Task.FromResult(this.ClientId);
        }

        public async Task<string> GetUsername()
        {
            return await Task.FromResult(this.ClientId);
        }
    }
}
