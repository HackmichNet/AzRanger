using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class Subscription : IReporting
    {
        public string id { get; set; }
        public string subscriptionId { get; set; }
        public string tenantId { get; set; }
        public string displayName { get; set; }
        public string state { get; set; }
        public Subscriptionpolicies subscriptionPolicies { get; set; }
        public string authorizationSource { get; set; }
        public Managedbytenant[] managedByTenants { get; set; }
        public Dictionary<String, String> tags { get; set; }
        public AzResources Resources = new AzResources();
        public List<AutoProvisioningSettings> AutoProvisioningSettings { get; set; }
        public SecurityCenterBuiltIn SecurityCenterBuiltIn { get; set; }
        public List<SecurityContact> SecurityContact { get; set;  }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", displayName, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", displayName, id);
        }
    }

    public class Subscriptionpolicies
    {
        public string locationPlacementId { get; set; }
        public string quotaId { get; set; }
        public string spendingLimit { get; set; }
    }
    public class Managedbytenant
    {
        public string tenantId { get; set; }
    }

}
