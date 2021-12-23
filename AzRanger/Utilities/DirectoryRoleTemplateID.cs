using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Utilities
{
    public static class DirectoryRoleTemplateID
    {
        // High Priv Accounts
        public static String PrivilegedAuthenticationAdministrator = "7be44c8a-adaf-4e2a-84d6-ab2649e08a13";
        public static String GlobalAdministrator = "62e90394-69f5-4237-9190-012177145e10";

        // Priv Accounts
        public static String ApplicationAdministrator = "9b895d92-2cd3-44c7-9d02-a6ac2d5ea5c3";
        public static String AuthenticationAdministrator = "c4e39bd9-1100-46d3-8c65-fb160da0071f";
        public static String AzureADJoinedDeviceLocalAdministrator = "9f06204d-73c1-4d4c-880a-6edb90606fd8";
        public static String CloudApplicationAdministrator = "158c047a-c907-4556-b7ef-446551a6b5f7";
        public static String CloudDeviceAdministrator = "7698a772-787b-4ac8-901f-60d6b08affd2";
        public static String ExchangeAdministrator = "29232cdf-9323-42fd-ade2-1d097af3e4de";
        public static String GroupsAdministrator = "fdd7a751-b60b-444a-984c-02652fe8fa1c";
        public static String HelpdeskAdministrator = "729827e3-9c14-49f7-bb1b-9608f156bbb8";
        public static String HybridIdentityAdministrator = "8ac3fc64-6eca-42ea-9e69-59f4c7b60eb2";
        public static String IntuneAdministrator = "3a2c62db-5318-420d-8d74-23affee5d9d5";
        public static String PasswordAdministrator = "966707d0-3269-4727-9be2-8c3a10f19b9d";
        public static String UserAdministrator = "fe930be7-5e62-47db-91af-98c3a49a38b1";
        public static String GlobalReader = "f2ef992c-3afb-46b9-b7cf-a126ee74c451";

        //https://posts.specterops.io/azure-privilege-escalation-via-service-principal-abuse-210ae2be2a5
        public static string[] RolesAllowingAddCreds = {
                DirectoryRoleTemplateID.CloudApplicationAdministrator,
                DirectoryRoleTemplateID.ApplicationAdministrator,
                DirectoryRoleTemplateID.HybridIdentityAdministrator
        };

        public static String[] HighPrivRoles = {
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
            DirectoryRoleTemplateID.UserAdministrator};

        // Roles which are Global Admin or can become Global Admins
        public static String[] GlobalAdmins =
        {
            DirectoryRoleTemplateID.GlobalAdministrator,
            DirectoryRoleTemplateID.PrivilegedAuthenticationAdministrator
        };

    }
}
