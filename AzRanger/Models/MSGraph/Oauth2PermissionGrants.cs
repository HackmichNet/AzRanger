using System;

namespace AzRanger.Models.MSGraph
{
    // https://docs.microsoft.com/en-us/graph/api/resources/oauth2permissiongrant?view=graph-rest-beta
    // Represents the delegated permissions that have been granted to an application's service principal.

    public class Oauth2PermissionGrant
    {
        // The id of the client service principal for the application which is authorized to act on behalf of a signed-in user when accessing an API (Delegated permissions only!)
        public Guid clientId { get; set; }
        // 'AllPrincipals' indicates authorization to impersonate all users. 'Principal' indicates authorization to impersonate a specific user.
        public string consentType { get; set; }
        public DateTime expiryTime { get; set; }
        public String id { get; set; }
        public string principalId { get; set; }
        public string resourceId { get; set; }
        public string scope { get; set; }
        public DateTime startTime { get; set; }
    }

}
