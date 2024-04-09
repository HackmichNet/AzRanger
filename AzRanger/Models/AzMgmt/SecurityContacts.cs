namespace AzRanger.Models.AzMgmt
{


    public class SecurityContacts
    {
        public SecurityContact[] SecurityContactList { get; set; }
    }

    public class SecurityContact
    {
        public SecurityContactProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string etag { get; set; }
        public string location { get; set; }
    }

    public class SecurityContactProperties
    {
        public SecurityContactNotificationsbyrole notificationsByRole { get; set; }
        public string emails { get; set; }
        public string phone { get; set; }
        public SecurityContactAlertnotifications alertNotifications { get; set; }
    }

    public class SecurityContactNotificationsbyrole
    {
        public string state { get; set; }
        public string[] roles { get; set; }
    }

    public class SecurityContactAlertnotifications
    {
        public string state { get; set; }
        public string minimalSeverity { get; set; }
    }


}
