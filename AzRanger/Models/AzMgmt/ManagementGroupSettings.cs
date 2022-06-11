using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class ManagementGroupSettings
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public ManagementGroupSettingsProperties properties { get; set; }
    }

    public class ManagementGroupSettingsProperties
    {
        public string tenantId { get; set; }
        public bool requireAuthorizationForGroupCreation { get; set; }
        public string defaultManagementGroup { get; set; }
    }

}
