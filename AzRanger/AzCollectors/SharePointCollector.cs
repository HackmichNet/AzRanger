using AzRanger.Models;
using AzRanger.Models.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class SharePointCollector : AbstractCollector
    {
        private String BaseAddress; 

        public const String SPOInternalUseOnly = "/_api/SPOInternalUseOnly.Tenant";


        public SharePointCollector(IAuthenticator authenticator, String baseAddress, String tenantId, String proxy)
        {
            this.Authenticator = authenticator;
            this.TenantId = tenantId;
            this.BaseAddress = baseAddress;
            this.additionalHeaders = new List<Tuple<string, string>>
            {
                Tuple.Create("Odata-Version", "4.0")
            };
            String baseScope = baseAddress + "/.default";
            this.Scope = new string[] { baseScope, "offline_access" };
            this.client = Helper.GetDefaultClient(additionalHeaders, proxy);
        }

        public Task<SPOInternalUseOnly> GetSharePointSettings()
        {
            return Get<SPOInternalUseOnly>(SPOInternalUseOnly);
        }
    }
}
