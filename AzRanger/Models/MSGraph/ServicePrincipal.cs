using AzRanger.Models.Generic;
using AzRanger.Output;
using System;
using System.Collections.Generic;

namespace AzRanger.Models.MSGraph
{

    public class ServicePrincipal : IReporting
    {
        public Guid id { get; set; }
        public string appDisplayName { get; set; }
        public Guid appId { get; set; }
        public string appOwnerOrganizationId { get; set; }
        public Passwordcredential[] passwordCredentials { get; set; }
        public KeyCredentials[] keyCredentials { get; set; }
        public Oauth2permissionscopes[] oauth2PermissionScopes { get; set; }
        public Approle[] appRoles { get; set; }
        public IDTypeResponse[] owners { get; set; }
        // Custom Properties
        public Approleassignment[] appRoleAssignments { get; set; }
        public List<AzurePrincipal> UserAbleToAddCreds = new List<AzurePrincipal>();
        // Delegated permissions
        public List<Oauth2PermissionGrant> oauth2PermissionGrants { get; set; }

        public void AddUserAbleToAddCreds(AzurePrincipal p)
        {
            foreach (AzurePrincipal entry in this.UserAbleToAddCreds)
            {
                if (entry.id.Equals(p.id))
                {
                    return;
                }
            }
            this.UserAbleToAddCreds.Add(p);
        }

        public List<AzurePrincipal> GetUserAbleToAddCreds()
        {
            return this.UserAbleToAddCreds;
        }

        public bool CanAddCredentials(Guid id)
        {
            foreach(IDTypeResponse response in this.owners)
            {
                if(response.id == id)
                {
                    return true;
                }
            }
            foreach(AzurePrincipal user in this.UserAbleToAddCreds)
            {
                if(user.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", this.appDisplayName, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", id, this.appDisplayName);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id.ToString(), this.appDisplayName);
        }
    }
}
