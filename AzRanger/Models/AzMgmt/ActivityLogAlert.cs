namespace AzRanger.Models.AzMgmt
{
    public class ActivityLogAlert
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public object kind { get; set; }
        public ActivityLogAlertProperties properties { get; set; }
        public object identity { get; set; }
    }

    public class ActivityLogAlertProperties
    {
        public string[] scopes { get; set; }
        public ActivityLogAlertCondition condition { get; set; }
        public ActivityLogAlertActions actions { get; set; }
        public bool enabled { get; set; }
        public string description { get; set; }
    }

    public class ActivityLogAlertCondition
    {
        public ActivityLogAlertAllof[] allOf { get; set; }
        public object odatatype { get; set; }
    }

    public class ActivityLogAlertAllof
    {
        public string field { get; set; }
        public string equals { get; set; }
        public object containsAny { get; set; }
        public object odatatype { get; set; }
    }

    public class ActivityLogAlertActions
    {
        public object[] actionGroups { get; set; }
    }

}
