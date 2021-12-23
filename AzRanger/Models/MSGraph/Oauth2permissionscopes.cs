using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Oauth2permissionscopes
    {
        public string adminConsentDescription { get; set; }
        public string adminConsentDisplayName { get; set; }
        public string id { get; set; }
        public bool isEnabled { get; set; }
        public string type { get; set; }
        public string userConsentDescription { get; set; }
        public string userConsentDisplayName { get; set; }
        public string value { get; set; }
    }
}
