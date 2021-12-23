using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Azrbac
{
    public class Scopedresource
    {
        public string id { get; set; }
        public string externalId { get; set; }
        public string type { get; set; }
        public string displayName { get; set; }
        public string status { get; set; }
        public object onboardDateTime { get; set; }
        public object registeredDateTime { get; set; }
        public object managedAt { get; set; }
        public object registeredRoot { get; set; }
        public object originTenantId { get; set; }
    }
}
