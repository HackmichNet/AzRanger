using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Provision
{

    // Contains relevant information from Get-MsolCompanyInformation
    public class MsolCompanyInformation
    {
        /**
         * 
         * Indicates whether to allow users to view the profile info of other users in their company. 
         * This setting is applied company-wide. Set to $False to disable users' ability to use the Azure AD module
         * for Windows PowerShell to access user information for their organization.
         * https://docs.microsoft.com/en-us/powershell/module/msonline/set-msolcompanysettings?view=azureadps-1.0
         */
        public bool UsersPermissionToReadOtherUsersEnabled;

        // Indicates if a user can create security groups, not M365 Groups https://techcommunity.microsoft.com/t5/microsoft-365-groups/users-can-create-office-365-groups-again/m-p/94979
        public bool UsersPermissionToCreateGroupsEnabled;
    }
}
