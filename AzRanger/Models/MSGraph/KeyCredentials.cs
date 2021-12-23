using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class KeyCredentials
    {
        public string customKeyIdentifier { get; set; }
        public string displayName { get; set; }
        public DateTime endDateTime { get; set; }
        public object key { get; set; }
        public string keyId { get; set; }
        public DateTime startDateTime { get; set; }
        public string type { get; set; }
        public string usage { get; set; }
    }

}
