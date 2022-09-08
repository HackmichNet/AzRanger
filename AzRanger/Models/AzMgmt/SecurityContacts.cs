using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{


    public class SecurityContacts
    {
        public SecurityContact[] SecurityContactList { get; set; }
    }

    public class SecurityContact
    {
        public SecurtiyContactProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string etag { get; set; }
        public string location { get; set; }
    }

    public class SecurtiyContactProperties
    {
        public SecurtiyContactNotificationsbyrole notificationsByRole { get; set; }
        public string emails { get; set; }
        public string phone { get; set; }
        public SecurtiyContactAlertnotifications alertNotifications { get; set; }
    }

    public class SecurtiyContactNotificationsbyrole
    {
        public string state { get; set; }
        public string[] roles { get; set; }
    }

    public class SecurtiyContactAlertnotifications
    {
        public string state { get; set; }
        public string minimalSeverity { get; set; }
    }


}
