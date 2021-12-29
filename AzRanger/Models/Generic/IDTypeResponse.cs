using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models.Generic
{
    public class IDTypeResponse
    {

        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public Guid id { get; set; }

    }
}
