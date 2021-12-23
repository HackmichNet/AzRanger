using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Domain
    {
        public string authenticationType { get; set; }
        public object availabilityStatus { get; set; }
        public string id { get; set; }
        public bool isAdminManaged { get; set; }
        public bool isDefault { get; set; }
        public bool isInitial { get; set; }
        public bool isRoot { get; set; }
        public bool isVerified { get; set; }
        public string[] supportedServices { get; set; }
        public object passwordValidityPeriodInDays { get; set; }
        public int passwordNotificationWindowInDays { get; set; }
        public object state { get; set; }
    }

}
