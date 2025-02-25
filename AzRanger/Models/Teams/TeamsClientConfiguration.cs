namespace AzRanger.Models.Teams
{
    public class TeamsClientConfiguration
    {
        public bool AllowEmailIntoChannel { get; set; }
        public string RestrictedSenderList { get; set; }
        public bool AllowDropBox { get; set; }
        public bool AllowBox { get; set; }
        public bool AllowGoogleDrive { get; set; }
        public bool AllowShareFile { get; set; }
        public bool AllowEgnyte { get; set; }
        public bool AllowOrganizationTab { get; set; }
        public bool AllowSkypeBusinessInterop { get; set; }
        public string ContentPin { get; set; }
        public bool AllowResourceAccountSendMessage { get; set; }
        public string ResourceAccountContentAccess { get; set; }
        public bool AllowGuestUser { get; set; }
        public bool AllowScopedPeopleSearchandAccess { get; set; }
        public bool AllowRoleBasedChatPermissions { get; set; }
        public Key Key { get; set; }
        public string Identity { get; set; }
    }

    public class Teamsclientconfiguration
    {
        public string xmlns { get; set; }
    }


}
