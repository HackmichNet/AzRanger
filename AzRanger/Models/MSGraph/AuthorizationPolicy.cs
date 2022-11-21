using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class AuthorizationPolicy
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        public string id { get; set; }

        // Anyone in the organization can invite guest users including guests and non-admins (most inclusive) => everyone
        // Member users and users assigned to specific admin roles can invite guest users including guests with member permissionsn => adminsGuestInvitersAndAllMembers
        // Only users assigned to specific admin roles can invite guest users => adminsAndGuestInviters
        // No one in the organization can invite guest users including admins (most restrictive) => none
        public string allowInvitesFrom { get; set; }
        public bool allowedToSignUpEmailBasedSubscriptions { get; set; }
        public bool allowedToUseSSPR { get; set; }
        // Indicates whether a user can join the tenant by email validation.
        // AllowEmailVerifiedUsers controls whether users can join the tenant by email validation. To join, the user must have an email address in a domain which matches one of the verified domains in the tenant. This setting is applied company-wide for all domains in the tenant. If you set that parameter to $false, no email-verified user can join the tenant. https://docs.microsoft.com/en-us/azure/active-directory/enterprise-users/directory-self-service-signup
        public bool allowEmailVerifiedUsersToJoinOrganization { get; set; }
        public bool blockMsolPowerShell { get; set; }
        public string description { get; set; }
        public string displayName { get; set; }
        public object[] enabledPreviewFeatures { get; set; }
        public string guestUserRoleId { get; set; }
        public object[] permissionGrantPolicyIdsAssignedToDefaultUserRole { get; set; }
        public Defaultuserrolepermissions defaultUserRolePermissions { get; set; }
    }
    public class Defaultuserrolepermissions
    {
        public bool allowedToCreateApps { get; set; }
        public bool allowedToCreateSecurityGroups { get; set; }
        public bool allowedToCreateTenants { get; set; }
        public bool allowedToReadBitlockerKeysForOwnedDevice { get; set; }
        public bool allowedToReadOtherUsers { get; set; }
    }
}
