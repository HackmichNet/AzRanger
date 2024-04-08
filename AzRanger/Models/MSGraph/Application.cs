using AzRanger.Models.Generic;
using AzRanger.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Application : IReporting
    {
        public Guid id { get; set; }
        public string displayName { get; set; }
        public bool credentialsCreated = false; 
        public Guid appId { get; set; }
        public string publisherDomain { get; set; }
        public string signInAudience { get; set; }
        public Passwordcredential[] passwordCredentials { get; set; }
        public KeyCredentials[] keyCredentials { get; set; }
        public IDTypeResponse[] owners { get; set; }
        public List<AzurePrincipal> users { get; set; }
        public List<AzurePrincipal> UserAbleToAddCreds = new List<AzurePrincipal>();

        public void AddUserAbleToAddCreds(AzurePrincipal p)
        {
            foreach(AzurePrincipal entry in this.UserAbleToAddCreds)
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
        public string PrintConsole()
        {
            return String.Format("{0} - {1}", this.displayName, id);
        }

        public bool CanAddCredentials(Guid id)
        {
            foreach (IDTypeResponse response in this.owners)
            {
                if (response.id == id)
                {
                    return true;
                }
            }
            foreach (AzurePrincipal user in this.UserAbleToAddCreds)
            {
                if (user.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", id, this.displayName);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id.ToString(), this.displayName);
        }
    }
}
