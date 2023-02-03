using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Provision
{

    // PricipalIsInActiveMembers relevant information from Get-MsolCompanyInformation
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

        // Indicates if a user can create security groups, not AAD Groups https://techcommunity.microsoft.com/t5/microsoft-365-groups/users-can-create-office-365-groups-again/m-p/94979
        public bool UsersPermissionToCreateGroupsEnabled;

        // https://docs.microsoft.com/en-us/azure/active-directory/enterprise-users/directory-self-service-signup
        // Indicates if user can create subscriptions => https://blog.nviso.eu/2022/05/18/detecting-preventing-rogue-azure-subscriptions/
        public bool AllowAdHocSubscriptions;

        // Controls whether users can join the tenant by email validation.
        public bool AllowEmailVerifiedUsers;
    }
}
