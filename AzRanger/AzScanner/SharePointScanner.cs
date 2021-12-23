using AzRanger.Models;
using AzRanger.Models.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class SharePointScanner : IScanner
    {

        public const String SPOInternalUseOnly = "/_api/SPOInternalUseOnly.Tenant";


        public SharePointScanner(Scanner scanner, String BaseAdresse)
        {
            this.Scanner = scanner;
            this.BaseAdresse = BaseAdresse;
            this.additionalHeaders = new List<Tuple<string, string>>
            {
                Tuple.Create("Odata-Version", "4.0")
            };
            String baseScope = BaseAdresse + "/.default";
            this.Scope = new string[] {"offline_access", baseScope};
        }

        public SPOInternalUseOnly GetSharepointSettings()
        {
            return (SPOInternalUseOnly)Get<SPOInternalUseOnly>(SPOInternalUseOnly);
        }
    }
}
