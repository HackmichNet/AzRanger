using System.Text.Json.Serialization;

namespace AzRanger.Models.MSGraph.MDM
{
    public class Assignment
    {
        public string id { get; set; }
        public string source { get; set; }
        public string sourceId { get; set; }
        public string intent { get; set; }
        public Target target { get; set; }
    }

    public class Target
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public object deviceAndAppManagementAssignmentFilterId { get; set; }
        public string deviceAndAppManagementAssignmentFilterType { get; set; }
        public string groupId { get; set; }
    }

}