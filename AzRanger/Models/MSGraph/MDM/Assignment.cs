using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [JsonProperty(PropertyName = "@odata.type")]
        public string odatatype { get; set; }
        public object deviceAndAppManagementAssignmentFilterId { get; set; }
        public string deviceAndAppManagementAssignmentFilterType { get; set; }
        public string groupId { get; set; }
    }

}