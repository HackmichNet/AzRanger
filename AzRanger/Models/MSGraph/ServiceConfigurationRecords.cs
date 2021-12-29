using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class ServiceConfigurationRecords
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public bool isOptional { get; set; }
        public string label { get; set; }
        public string recordType { get; set; }
        public string supportedService { get; set; }
        public int ttl { get; set; }
        public string text { get; set; }
    }

}
