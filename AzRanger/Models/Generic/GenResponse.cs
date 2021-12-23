using Newtonsoft.Json;

namespace AzRanger.Models.Generic
{
    public class GenResponse
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string odatacontext { get; set; }
        [JsonProperty(PropertyName = "@odata.nextLink")]
        public string odatanextLink { get; set; }
        public object[] value { get; set; }
    }

}
