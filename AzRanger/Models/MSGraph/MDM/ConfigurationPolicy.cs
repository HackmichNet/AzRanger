using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph.MDM
{
    // https://graph.microsoft.com/beta/deviceManagement/configurationPolicies?$select=id,name,description,platforms,technologies,isAssigned
    public class ConfigurationPolicy
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string platforms { get; set; }
        public string technologies { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public int settingCount { get; set; }
        public string[] roleScopeTagIds { get; set; }
        public bool isAssigned { get; set; }
    }

}
