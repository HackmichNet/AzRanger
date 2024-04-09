namespace AzRanger.Models.MSGraph
{
    // https://main.iam.ad.ext.azure.com/api/RequestApprovals/V2/PolicyTemplates?type=AdminConsentFlow
    public class AdminConsentFlow
    {
        public string id { get; set; }
        public string[] approvers { get; set; }
        public Approversv2 approversV2 { get; set; }
        public int requestExpiresInDays { get; set; }
        public bool notificationsEnabled { get; set; }
        public bool remindersEnabled { get; set; }
    }

    public class Approversv2
    {
        public string[] user { get; set; }
        public object[] group { get; set; }
        public object[] role { get; set; }
    }

}
