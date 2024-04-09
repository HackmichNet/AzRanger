namespace AzRanger.Models.AzMgmt
{

    // https://management.azure.com/providers/Microsoft.Subscription/policies/default?api-version=2021-01-01-privatepreview
    public class SubscriptionPolicy
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public SubscriptionPolicyProperties properties { get; set; }
    }

    public class SubscriptionPolicyProperties
    {
        public string policyId { get; set; }
        public bool blockSubscriptionsLeavingTenant { get; set; }
        public bool blockSubscriptionsIntoTenant { get; set; }
        public object[] exemptedPrincipals { get; set; }
    }

}
