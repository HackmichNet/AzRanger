namespace AzRanger.Utilities
{
    public static class DirectoryRoleTemplateID
    {
        // High Priv Accounts
        public static string PrivilegedAuthenticationAdministrator = "7be44c8a-adaf-4e2a-84d6-ab2649e08a13";
        public static string GlobalAdministrator = "62e90394-69f5-4237-9190-012177145e10";

        // Priv Accounts
        public static string ApplicationAdministrator = "9b895d92-2cd3-44c7-9d02-a6ac2d5ea5c3";
        public static string AuthenticationAdministrator = "c4e39bd9-1100-46d3-8c65-fb160da0071f";
        public static string AzureADJoinedDeviceLocalAdministrator = "9f06204d-73c1-4d4c-880a-6edb90606fd8";
        public static string CloudApplicationAdministrator = "158c047a-c907-4556-b7ef-446551a6b5f7";
        public static string CloudDeviceAdministrator = "7698a772-787b-4ac8-901f-60d6b08affd2";
        public static string ExchangeAdministrator = "29232cdf-9323-42fd-ade2-1d097af3e4de";
        public static string GroupsAdministrator = "fdd7a751-b60b-444a-984c-02652fe8fa1c";
        public static string HelpdeskAdministrator = "729827e3-9c14-49f7-bb1b-9608f156bbb8";
        public static string HybridIdentityAdministrator = "8ac3fc64-6eca-42ea-9e69-59f4c7b60eb2";
        public static string IntuneAdministrator = "3a2c62db-5318-420d-8d74-23affee5d9d5";
        public static string PasswordAdministrator = "966707d0-3269-4727-9be2-8c3a10f19b9d";
        public static string UserAdministrator = "fe930be7-5e62-47db-91af-98c3a49a38b1";
        public static string GlobalReader = "f2ef992c-3afb-46b9-b7cf-a126ee74c451";
        public static string SharePointAdmin = "f28a1f50-f6e7-4571-818b-6a12f2af6b6c";

        //https://posts.specterops.io/azure-privilege-escalation-via-service-principal-abuse-210ae2be2a5
        public static string[] RolesAllowingAddCreds = {
                DirectoryRoleTemplateID.CloudApplicationAdministrator,
                DirectoryRoleTemplateID.ApplicationAdministrator,
                DirectoryRoleTemplateID.HybridIdentityAdministrator
        };

        public static string[] HighPrivRoles = {
            DirectoryRoleTemplateID.ApplicationAdministrator,
            DirectoryRoleTemplateID.AuthenticationAdministrator,
            DirectoryRoleTemplateID.AzureADJoinedDeviceLocalAdministrator,
            DirectoryRoleTemplateID.CloudApplicationAdministrator,
            DirectoryRoleTemplateID.CloudDeviceAdministrator,
            DirectoryRoleTemplateID.ExchangeAdministrator,
            DirectoryRoleTemplateID.GroupsAdministrator,
            DirectoryRoleTemplateID.HelpdeskAdministrator,
            DirectoryRoleTemplateID.HybridIdentityAdministrator,
            DirectoryRoleTemplateID.IntuneAdministrator,
            DirectoryRoleTemplateID.PasswordAdministrator,
            DirectoryRoleTemplateID.UserAdministrator,
            DirectoryRoleTemplateID.SharePointAdmin
        };

        // Roles which are Global Admin or can become Global Admins
        public static string[] GlobalAdmins =
        {
            DirectoryRoleTemplateID.GlobalAdministrator,
            DirectoryRoleTemplateID.PrivilegedAuthenticationAdministrator
        };

    }
}
