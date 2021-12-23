using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Passwordcredential
    {
        public object customKeyIdentifier { get; set; }
        public string displayName { get; set; }
        public DateTime endDateTime { get; set; }
        public string hint { get; set; }
        public string keyId { get; set; }
        public object secretText { get; set; }
        public DateTime startDateTime { get; set; }
    }
}
