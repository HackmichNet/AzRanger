using AzRanger.Output;
using System;
using System.Collections.Generic;

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

        //Custome Attributes
        public List<SQLServerFirewallRules> firewallRules { get; set; }
        public SQLServerAuditingSettings auditingSettings { get; set; }
        public List<SQLAdministrator> SQLAdministrators { get; set; }
        public List<SQLDatabase> SQLDatabases { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", name, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", name, id);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.name);
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
    public class SQLServerAuditingSettings
    {
        public SQLServerAuditingSettingsProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class SQLServerAuditingSettingsProperties
    {
        public bool isDevopsAuditEnabled { get; set; }
        public int retentionDays { get; set; }
        public string[] auditActionsAndGroups { get; set; }
        public bool isStorageSecondaryKeyInUse { get; set; }
        public bool isAzureMonitorTargetEnabled { get; set; }
        public string state { get; set; }
        public string storageEndpoint { get; set; }
        public string storageAccountSubscriptionId { get; set; }
    }


}
