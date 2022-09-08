using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class SecurityCenterBuiltIn
    {
        public Properties properties { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public Systemdata systemData { get; set; }
    }

    public class Properties
    {
        public string displayName { get; set; }
        public string policyDefinitionId { get; set; }
        public string scope { get; set; }
        public Parameters parameters { get; set; }
        public string description { get; set; }
        public Metadata metadata { get; set; }
        public string enforcementMode { get; set; }
    }

    public class Parameters
    {
    }

    public class Metadata
    {
        public string assignedBy { get; set; }
        public string[] excludedOutOfTheBoxStandards { get; set; }
        public string createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object updatedBy { get; set; }
        public object updatedOn { get; set; }
    }

    public class Systemdata
    {
        public string createdBy { get; set; }
        public string createdByType { get; set; }
        public DateTime createdAt { get; set; }
        public string lastModifiedBy { get; set; }
        public string lastModifiedByType { get; set; }
        public DateTime lastModifiedAt { get; set; }
    }

}
