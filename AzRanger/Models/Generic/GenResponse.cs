using System.Text.Json.Serialization;

namespace AzRanger.Models.Generic
{
    public class GenResponse
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        [JsonPropertyName("@odata.nextLink")]
        public string odatanextLink { get; set; }
        public object[] value { get; set; }
    }

}
