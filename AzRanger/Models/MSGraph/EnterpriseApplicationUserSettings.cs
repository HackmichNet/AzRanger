namespace AzRanger.Models.MSGraph
{

    //https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/
    public class EnterpriseApplicationUserSettings
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string templateId { get; set; }
        public Value[] values { get; set; }
    }

    public class Value
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}
