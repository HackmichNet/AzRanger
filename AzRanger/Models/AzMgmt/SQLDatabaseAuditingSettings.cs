namespace AzRanger.Models.AzMgmt
{
    public class SQLDatabaseAuditingSettings
    {
        public SQLDatabaseAuditingSettingsProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class SQLDatabaseAuditingSettingsProperties
    {
        public int retentionDays { get; set; }
        public string[] auditActionsAndGroups { get; set; }
        public bool isStorageSecondaryKeyInUse { get; set; }
        public bool isAzureMonitorTargetEnabled { get; set; }
        public bool isManagedIdentityInUse { get; set; }
        public string state { get; set; }
        public string storageEndpoint { get; set; }
        public string storageAccountSubscriptionId { get; set; }
    }

}