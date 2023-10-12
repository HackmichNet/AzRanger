using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Approle
    {
        public string[] allowedMemberTypes { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public string id { get; set; }
        public bool isEnabled { get; set; }
        public string origin { get; set; }
        public string value { get; set; }
    }

}
