using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class AutoProvisioningSettings
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public AutoProvisioningSettingsProperties properties { get; set; }
    }

    public class AutoProvisioningSettingsProperties
    {
        public string autoProvision { get; set; }
    }

}
