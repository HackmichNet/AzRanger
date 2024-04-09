namespace AzRanger.Models.MSGraph.MDM
{
    // https://graph.microsoft.com/beta/deviceManagement/deviceCompliancePolicies?$select=id,displayName
    public class GenericCompliancePolicy
    {
        public string odatatype { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
    }


}
