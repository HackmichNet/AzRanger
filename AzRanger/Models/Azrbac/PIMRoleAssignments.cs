using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzRanger.Models.Azrbac
{
    public class PIMRoleAssignments
    {
        [JsonPropertyName("@odata.id")]
        public string odataid { get; set; }
        public string id { get; set; }
        public Guid resourceId { get; set; }
        public Guid roleDefinitionId { get; set; }
        public Guid subjectId { get; set; }
        public object scopedResourceId { get; set; }
        public object linkedEligibleRoleAssignmentId { get; set; }
        public string externalId { get; set; }
        public bool isPermanent { get; set; }
        public object startDateTime { get; set; }
        public object endDateTime { get; set; }
        public string memberType { get; set; }
        public string assignmentState { get; set; }
        public string status { get; set; }
        public object condition { get; set; }
        public object conditionVersion { get; set; }
        public object conditionDescription { get; set; }
        public Subject subject { get; set; }
        public Scopedresource scopedResource { get; set; }
    }
}
