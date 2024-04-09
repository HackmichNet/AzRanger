namespace AzRanger.Models.AzMgmt
{
    public class DiagnosticSettings
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public object location { get; set; }
        public object kind { get; set; }
        public object tags { get; set; }
        public DiagnosticSettingsProperties properties { get; set; }
        public object identity { get; set; }
    }

    public class DiagnosticSettingsProperties
    {
        public string storageAccountId { get; set; }
        public object serviceBusRuleId { get; set; }
        public object workspaceId { get; set; }
        public object eventHubAuthorizationRuleId { get; set; }
        public object eventHubName { get; set; }
        public DiagnosticSettingsMetric[] metrics { get; set; }
        public DiagnosticSettingsLog[] logs { get; set; }
        public object logAnalyticsDestinationType { get; set; }
    }

    public class DiagnosticSettingsMetric
    {
        public string category { get; set; }
        public bool enabled { get; set; }
        public DiagnosticSettingsRetentionpolicy retentionPolicy { get; set; }
    }

    public class DiagnosticSettingsRetentionpolicy
    {
        public bool enabled { get; set; }
        public int days { get; set; }
    }

    public class DiagnosticSettingsLog
    {
        public string category { get; set; }
        public object categoryGroup { get; set; }
        public bool enabled { get; set; }
        public DiagnosticSettingsRetentionpolicy retentionPolicy { get; set; }
    }
}
