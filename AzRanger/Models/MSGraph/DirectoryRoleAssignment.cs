using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class DirectoryRoleAssignment
    {
        public string id { get; set; }
        public string principalId { get; set; }
        public string roleDefinitionId { get; set; }
        public string directoryScopeId { get; set; }
        public object appScopeId { get; set; }
        public string createdUsing { get; set; }
        public Object createdDateTime { get; set; }
        public object modifiedDateTime { get; set; }
        public string status { get; set; }
        public string assignmentType { get; set; }
        public string memberType { get; set; }
        public DirectoryRoleAssignmentScheduleinfo scheduleInfo { get; set; }
        public DirectoryRoleAssignmentPrincipal principal { get; set; }
        public DirectoryRoleAssignmentDirectoryscope directoryScope { get; set; }
    }

    public class DirectoryRoleAssignmentScheduleinfo
    {
        public Object startDateTime { get; set; }
        public object recurrence { get; set; }
        public DirectoryRoleAssignmentExpiration expiration { get; set; }
    }

    public class DirectoryRoleAssignmentExpiration
    {
        public string type { get; set; }
        public object endDateTime { get; set; }
        public object duration { get; set; }
    }

    public class DirectoryRoleAssignmentPrincipal
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
    }

    public class DirectoryRoleAssignmentDirectoryscope
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
    }


}
