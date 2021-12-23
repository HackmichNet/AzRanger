using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.WinGraph
{
    public class RoleDefinition
    {
        [JsonProperty(PropertyName = "@odata.type")]
        public string odatatype { get; set; }
        public string inheritsPermissionsFromodatanavigationLinkUrl { get; set; }
        public Inheritspermissionsfrom[] inheritsPermissionsFrom { get; set; }
        public Guid objectId { get; set; }
        public string displayName { get; set; }
    }

    public class Inheritspermissionsfrom
    {
        [JsonProperty(PropertyName = "@odata.type")]
        public string odatatype { get; set; }
        public string objectId { get; set; }
    }

}
