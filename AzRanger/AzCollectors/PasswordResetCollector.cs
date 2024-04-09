using AzRanger.Models.PasswordReset;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class PasswordResetCollector : AbstractCollector
    {
        private const String AdminPasswordResetPolicy = "/v1.0/policies/adminPasswordResetPolicy";
        public PasswordResetCollector(IAuthenticator authenticator, String tenantId, String proxy)
        {
            this.Authenticator = authenticator;
            this.TenantId = tenantId;
            this.BaseAddress = "https://passwordreset.microsoftonline.com";
            this.Scope = new String[] { "https://passwordreset.microsoftonline.com/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, proxy);
            this.additionalHeaders = new List<Tuple<string, string>>
            {
                Tuple.Create("X-Ms-Client-Session-Id", "42")
            };
        }

        public Task<AdminPasswordResetPolicy> GetAdminPasswordResetPolicy()
        {
            return Get<AdminPasswordResetPolicy>(AdminPasswordResetPolicy);
        }
    }
}
