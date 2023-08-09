using AzRanger.Models;
using AzRanger.Models.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class SharePointScanner : AbstractScannerModule
    {

        public const String SPOInternalUseOnly = "/_api/SPOInternalUseOnly.Tenant";


        public SharePointScanner(Scanner scanner, String baseAdresse)
        {
            this.Scanner = scanner;
            this.BaseAdresse = baseAdresse;
            this.additionalHeaders = new List<Tuple<string, string>>
            {
                Tuple.Create("Odata-Version", "4.0")
            };
            String baseScope = baseAdresse + "/.default";
            this.Scope = new string[] { baseScope, "offline_access" };
            this.client = Helper.GetDefaultClient(additionalHeaders, this.Scanner.Proxy);
        }

        public Task<SPOInternalUseOnly> GetSharePointSettings()
        {
            return Get<SPOInternalUseOnly>(SPOInternalUseOnly);
        }
    }
}
