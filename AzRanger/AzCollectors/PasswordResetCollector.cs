using AzRanger.Models.PasswordReset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class PasswordResetCollector : AbstractCollector
    {
        private const String AdminPasswordResetPolicy = "/v1.0/policies/adminPasswordResetPolicy";
        public PasswordResetCollector(MainCollector scanner) {
            this.Scanner = scanner;
            this.BaseAdresse = "https://passwordreset.microsoftonline.com";
            this.Scope = new String[] { "https://passwordreset.microsoftonline.com/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, scanner.Proxy);
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
