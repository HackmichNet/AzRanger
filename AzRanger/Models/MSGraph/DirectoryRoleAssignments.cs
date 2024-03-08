using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class DirectoryRoleAssignments
    {
        public String id { get; set; }
        public string resourceId { get; set; }
        public string roleDefinitionId { get; set; }
        public Guid subjectId { get; set; }
        public object linkedEligibleRoleAssignmentId { get; set; }
        public string externalId { get; set; }
        public DateTime? startDateTime { get; set; }
        public object endDateTime { get; set; }
        public string memberType { get; set; }
        public string assignmentState { get; set; }
        public string status { get; set; }
        public string subjectodatacontext { get; set; }
        public DirectoryRoleAssignmentsSubject subject { get; set; }
    }

    public class DirectoryRoleAssignmentsSubject
    {
        public string id { get; set; }
        public string type { get; set; }
        public string displayName { get; set; }
        public string principalName { get; set; }
        public string email { get; set; }
    }


}
