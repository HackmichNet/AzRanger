using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AdminCenter
{
    // https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/Calendar
    public class Calendarsharing
    {
        public bool EnableCalendarSharing { get; set; }
        public bool EnableAnonymousCalendarSharing { get; set; }
        public bool HydrationRequired { get; set; }
        public string SharingOption { get; set; }
        public string ContractIdentity { get; set; }
        public string ServiceDomain { get; set; }
        public string TenantDomain { get; set; }
        public string UserDomain { get; set; }
        public string Locale { get; set; }
        public bool EnableAoboScenario { get; set; }
    }

}
