namespace AzRanger.Models.MainIAM
{
    // https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/
    public class UserSettings
    {
        public bool? usersCanAllowAppsToAccessData { get; set; }
        public bool? usersCanAddGalleryApps { get; set; }
        public bool? hideOffice365Apps { get; set; }
    }

}
