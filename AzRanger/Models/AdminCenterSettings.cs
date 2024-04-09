using AzRanger.Models.AdminCenter;

namespace AzRanger.Models
{
    public class AdminCenterSettings
    {
        public Calendarsharing Calendarsharing { get; set; }
        public SkypeTeams SkypeTeams { get; set; }
        public SwaySettings SwaySettings { get; set; }
        public O365PasswordPolicy O365PasswordPolicy { get; set; }
        public OfficeFormsSettings OfficeFormsSettings { get; set; }
        public OfficeStoreSettings OfficeStoreSettings { get; set; }
        public DirsyncManagement DirsyncManagement { get; set; }
    }
}
