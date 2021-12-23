using Newtonsoft.Json;
using System;

namespace AzRanger.Models.Generic
{
    public class IDTypeResponse
    {

        [JsonProperty(PropertyName = "@odata.type")]
        public string odatatype { get; set; }
        public Guid id { get; set; }

    }
}
