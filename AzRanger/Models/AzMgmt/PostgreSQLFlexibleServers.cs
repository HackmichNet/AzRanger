using AzRanger.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class PostgreSQLFlexibleServers : IReporting
    {
        public PostgreSQLFlexibleServersSku sku { get; set; }
        public PostgreSQLFlexibleServersSystemdata systemData { get; set; }
        public PostgreSQLFlexibleServersProperties properties { get; set; }
        public string location { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        // Custom properties

        public List<PostgreSQLFlexibleServersParameters> Paramters { get; set; }

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

    public class PostgreSQLFlexibleServersSku
    {
        public string name { get; set; }
        public string tier { get; set; }
    }

    public class PostgreSQLFlexibleServersSystemdata
    {
        public DateTime createdAt { get; set; }
    }

    public class PostgreSQLFlexibleServersProperties
    {
        public PostgreSQLFlexibleServersDataencryption dataEncryption { get; set; }
        public PostgreSQLFlexibleServersAuthconfig authConfig { get; set; }
        public string fullyQualifiedDomainName { get; set; }
        public string version { get; set; }
        public string minorVersion { get; set; }
        public string administratorLogin { get; set; }
        public string state { get; set; }
        public string availabilityZone { get; set; }
        public PostgreSQLFlexibleServersStorage storage { get; set; }
        public PostgreSQLFlexibleServersBackup backup { get; set; }
        public PostgreSQLFlexibleServersNetwork network { get; set; }
        public PostgreSQLFlexibleServersHighavailability highAvailability { get; set; }
        public PostgreSQLFlexibleServersMaintenancewindow maintenanceWindow { get; set; }
        public string replicationRole { get; set; }
        public int replicaCapacity { get; set; }
    }

    public class PostgreSQLFlexibleServersDataencryption
    {
        public string type { get; set; }
    }

    public class PostgreSQLFlexibleServersAuthconfig
    {
        public bool activeDirectoryAuthEnabled { get; set; }
    }

    public class PostgreSQLFlexibleServersStorage
    {
        public int storageSizeGB { get; set; }
    }

    public class PostgreSQLFlexibleServersBackup
    {
        public int backupRetentionDays { get; set; }
        public string geoRedundantBackup { get; set; }
        public DateTime earliestRestoreDate { get; set; }
    }

    public class PostgreSQLFlexibleServersNetwork
    {
        public string publicNetworkAccess { get; set; }
        public string delegatedSubnetResourceId { get; set; }
        public string privateDnsZoneArmResourceId { get; set; }
    }

    public class PostgreSQLFlexibleServersHighavailability
    {
        public string mode { get; set; }
        public string state { get; set; }
    }

    public class PostgreSQLFlexibleServersMaintenancewindow
    {
        public string customWindow { get; set; }
        public int dayOfWeek { get; set; }
        public int startHour { get; set; }
        public int startMinute { get; set; }
    }


    public class PostgreSQLFlexibleServersParameters
    {
        public PostgreSQLFlexibleServersParametersProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class PostgreSQLFlexibleServersParametersProperties
    {
        public string value { get; set; }
        public string description { get; set; }
        public string defaultValue { get; set; }
        public string dataType { get; set; }
        public string allowedValues { get; set; }
        public string source { get; set; }
        public bool isDynamicConfig { get; set; }
        public bool isReadOnly { get; set; }
        public bool isConfigPendingRestart { get; set; }
        public string documentationLink { get; set; }
    }


}
