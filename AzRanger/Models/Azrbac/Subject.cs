using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Azrbac
{
    public class Subject
    {
        public string id { get; set; }
        public string type { get; set; }
        public string displayName { get; set; }
        public string principalName { get; set; }
        public string email { get; set; }
    }
}
