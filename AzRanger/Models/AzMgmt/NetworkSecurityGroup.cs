using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class NetworkSecurityGroup : IReporting
    {
        public string name { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public NetworkSecurityGroupProperties properties { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", name, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", name, id);
        }
    }

    public class NetworkSecurityGroupProperties
    {
        public string provisioningState { get; set; }
        public string resourceGuid { get; set; }
        public NetworkSecurityGroupSecurityrule[] securityRules { get; set; }
        public NetworkSecurityGroupSecurityrule[] defaultSecurityRules { get; set; }
    }

    public class NetworkSecurityGroupSecurityrule
    {
        public string name { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string type { get; set; }
        public NetworkSecurityGroupSecurityruleProperties properties { get; set; }
    }

    public class NetworkSecurityGroupSecurityruleProperties
    {
        public string provisioningState { get; set; }
        public string protocol { get; set; }
        public string sourcePortRange { get; set; }
        public string destinationPortRange { get; set; }
        public string sourceAddressPrefix { get; set; }
        public string destinationAddressPrefix { get; set; }
        public string access { get; set; }
        public int priority { get; set; }
        public string direction { get; set; }
        public object[] sourcePortRanges { get; set; }
        public object[] destinationPortRanges { get; set; }
        public object[] sourceAddressPrefixes { get; set; }
        public object[] destinationAddressPrefixes { get; set; }
    }
}
