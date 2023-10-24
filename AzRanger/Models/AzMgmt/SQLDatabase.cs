using AzRanger.Output;
using System;

namespace AzRanger.Models.AzMgmt
{
    public class SQLDatabase : IReporting
    {
        public SQLDatabaseSku sku { get; set; }
        public string kind { get; set; }
        public SQLDatabaseProperties properties { get; set; }
        public string location { get; set; }
        public Tags tags { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public SQLDatabaseTransparentDataEncryption transparentDataEncryption { get; set; }

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

    public class SQLDatabaseSku
    {
        public string name { get; set; }
        public string tier { get; set; }
        public string family { get; set; }
        public int capacity { get; set; }
    }

    public class SQLDatabaseProperties
    {
        public string collation { get; set; }
        public long maxSizeBytes { get; set; }
        public string status { get; set; }
        public string databaseId { get; set; }
        public DateTime creationDate { get; set; }
        public string currentServiceObjectiveName { get; set; }
        public string requestedServiceObjectiveName { get; set; }
        public string defaultSecondaryLocation { get; set; }
        public string catalogCollation { get; set; }
        public bool zoneRedundant { get; set; }
        public long maxLogSizeBytes { get; set; }
        public string readScale { get; set; }
        public SQLDatabaseCurrentsku currentSku { get; set; }
        public int autoPauseDelay { get; set; }
        public string currentBackupStorageRedundancy { get; set; }
        public string requestedBackupStorageRedundancy { get; set; }
        public float minCapacity { get; set; }
        public string maintenanceConfigurationId { get; set; }
        public bool isLedgerOn { get; set; }
        public bool isInfraEncryptionEnabled { get; set; }
    }

    public class SQLDatabaseCurrentsku
    {
        public string name { get; set; }
        public string tier { get; set; }
        public string family { get; set; }
        public int capacity { get; set; }
    }

    public class Tags
    {
    }


    public class SQLDatabaseTransparentDataEncryption
    {
        public SQLDatabaseTransparentDataEncryptionProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class SQLDatabaseTransparentDataEncryptionProperties
    {
        public string state { get; set; }
    }


}
