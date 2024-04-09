using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models.WinGraph
{
    public class RoleDefinition
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string inheritsPermissionsFromodatanavigationLinkUrl { get; set; }
        public Inheritspermissionsfrom[] inheritsPermissionsFrom { get; set; }
        public Guid objectId { get; set; }
        public string displayName { get; set; }
    }

    public class Inheritspermissionsfrom
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string objectId { get; set; }
    }

}
