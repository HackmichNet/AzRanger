
namespace AzRanger.Models.AdminCenter
{
    public class O365PasswordPolicy
    {
        public int ValidityPeriod { get; set; }
        public int NotificationDays { get; set; }
        public bool NeverExpire { get; set; }
        public int MinimumValidityPeriod { get; set; }
        public int MinimumNotificationDays { get; set; }
        public int MaximumValidityPeriod { get; set; }
        public int MaximumNotificationDays { get; set; }
    }

}
