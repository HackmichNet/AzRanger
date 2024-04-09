namespace AzRanger.Models.MSGraph
{
    public class LicenseDetails
    {
        public string id { get; set; }
        public string skuId { get; set; }
        public string skuPartNumber { get; set; }
        public Serviceplan[] servicePlans { get; set; }
    }

    public class Serviceplan
    {
        public string servicePlanId { get; set; }
        public string servicePlanName { get; set; }
        public string provisioningStatus { get; set; }
        public string appliesTo { get; set; }
    }


}
