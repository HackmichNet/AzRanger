using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class PolicyAssignment
    {
        public Sku sku { get; set; }
        public PolicyAssignmentProperties properties { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
    }

    public class Sku
    {
        public string name { get; set; }
        public string tier { get; set; }
    }

    public class PolicyAssignmentProperties
    {
        public string displayName { get; set; }
        public string policyDefinitionId { get; set; }
        public string scope { get; set; }
        public object[] notScopes { get; set; }
        public Parameters parameters { get; set; }
        public string description { get; set; }
        public Metadata metadata { get; set; }
        public string enforcementMode { get; set; }
    }

    public class PolicyAssignmentParameters
    {
    }

    public class PolicyAssignmentMetadata
    {
        public string assignedBy { get; set; }
        public PolicyAssignmentParameterscopes parameterScopes { get; set; }
        public string createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object updatedBy { get; set; }
        public object updatedOn { get; set; }
    }

    public class PolicyAssignmentParameterscopes
    {
    }

}
