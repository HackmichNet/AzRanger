using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class SQLServer : IReporting
    {
        public string kind { get; set; }
        public SQLServerProperties properties { get; set; }
        public string location { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<SQLServerFirewallRules> firewallRules { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", name, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", name, id);
        }
    }

    public class SQLServerProperties
    {
        public string administratorLogin { get; set; }
        public string version { get; set; }
        public string state { get; set; }
        public string fullyQualifiedDomainName { get; set; }
        public object[] privateEndpointConnections { get; set; }
        public object minimalTlsVersion { get; set; }
        public string publicNetworkAccess { get; set; }
        public string restrictOutboundNetworkAccess { get; set; }
    }


    public class SQLServerFirewallRules
    {
        public SQLServerFirewallRulesProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class SQLServerFirewallRulesProperties
    {
        public string startIpAddress { get; set; }
        public string endIpAddress { get; set; }
    }

}
