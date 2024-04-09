namespace AzRanger.Models.MainIAM
{
    // https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/Lifecycle
    public class LCMSettings
    {
        public int expiresAfterInDays { get; set; }   // If custom value is set = 2, if 365 days = 1, if 180 days = 0
        public int groupLifetimeCustomValueInDays { get; set; } // Number of days (if custom)
        public int managedGroupTypesEnum { get; set; }
        public int managedGroupTypes { get; set; }
        public string adminNotificationEmails { get; set; }
        public string[] groupIdsToMonitorExpirations { get; set; }
        public string policyIdentifier { get; set; } // if not set = 00000000-0000-0000-0000-000000000000
    }

}
