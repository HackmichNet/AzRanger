using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Approleassignment
    {
        public string id { get; set; }
        public object deletedDateTime { get; set; }
        public string appRoleId { get; set; }
        public DateTime createdDateTime { get; set; }
        public string principalDisplayName { get; set; }
        public string principalId { get; set; }
        public string principalType { get; set; }
        public string resourceDisplayName { get; set; }
        public string resourceId { get; set; }
    }
}
