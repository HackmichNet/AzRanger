namespace AzRanger.Models.AzMgmt
{
    public class SQLAdministrator
    {
        public SQLAdministratorsProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class SQLAdministratorsProperties
    {
        public string administratorType { get; set; }
        public string login { get; set; }
        public string sid { get; set; }
        public string tenantId { get; set; }
        public bool azureADOnlyAuthentication { get; set; }
    }

}
