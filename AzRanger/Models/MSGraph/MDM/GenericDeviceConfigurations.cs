using System.Text.Json.Serialization;

namespace AzRanger.Models.MSGraph.MDM
{
    // https://graph.microsoft.com/beta/deviceManagement/deviceConfigurations?$select=id,displayName&$expand=assignments
    public class GenericDeviceConfigurations
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public string assignmentsodatacontext { get; set; }
        public Assignment[] assignments { get; set; }
    }
}
