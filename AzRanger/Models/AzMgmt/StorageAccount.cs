using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class StorageAccount : IReporting
    {
        public StorageAccountSku sku { get; set; }
        public string kind { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public StorageAccountProperties properties { get; set; }
        // Custom Probs
        public List<StorageContainer> StorageContainers { get; set; }
        public StorageAccountDefault Default { get; set; }
        public Guid Subscription { get; set; }

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

    public class StorageAccountSku
    {
        public string name { get; set; }
        public string tier { get; set; }
    }

    public class StorageAccountProperties
    {
        public bool defaultToOAuthAuthentication { get; set; }
        public StorageAccountKeyPolicy keyPolicy { get; set; }
        public StorageAccountKeycreationtime keyCreationTime { get; set; }
        public bool allowCrossTenantReplication { get; set; }
        public object[] privateEndpointConnections { get; set; }
        public string minimumTlsVersion { get; set; }
        public bool allowBlobPublicAccess { get; set; }
        public bool allowSharedKeyAccess { get; set; }
        public StorageAccountNetworkacls networkAcls { get; set; }
        public bool supportsHttpsTrafficOnly { get; set; }
        public StorageAccountEncryption encryption { get; set; }
        public string accessTier { get; set; }
        public string provisioningState { get; set; }
        public DateTime? creationTime { get; set; }
        public StorageAccountPrimaryendpoints primaryEndpoints { get; set; }
        public string primaryLocation { get; set; }
        public string statusOfPrimary { get; set; }
    }


    public class StorageAccountKeyPolicy
    {
        public int keyExpirationPeriodInDays { get; set; }
        public bool enableAutoRotation { get; set; }
    }


    public class StorageAccountKeycreationtime
    {
        public object key1 { get; set; }
        public object key2 { get; set; }
    }

    public class StorageAccountNetworkacls
    {
        public string bypass { get; set; }
        public object[] virtualNetworkRules { get; set; }
        public object[] ipRules { get; set; }
        public string defaultAction { get; set; }
    }


    public class StorageAccountEncryption
    {
        public Identity identity { get; set; }
        public bool requireInfrastructureEncryption { get; set; }
        public StorageAccountKeyvaultproperties keyvaultproperties { get; set; }
        public StorageAccountServices services { get; set; }
        // Microsoft.Storage => Microsoft Managed
        // Microsoft.Keyvault => Custom Key vault
        public string keySource { get; set; }
    }

    public class Identity
    {
    }

    public class StorageAccountKeyvaultproperties
    {
        public string currentVersionedKeyIdentifier { get; set; }
        public DateTime lastKeyRotationTimestamp { get; set; }
        public DateTime currentVersionedKeyExpirationTimestamp { get; set; }
        public string keyvaulturi { get; set; }
        public string keyname { get; set; }
        public string keyversion { get; set; }
    }

    public class StorageAccountServices
    {
        public StorageAccountFile file { get; set; }
        public StorageAccountBlob blob { get; set; }
    }

    public class StorageAccountFile
    {
        public string keyType { get; set; }
        public bool enabled { get; set; }
        public DateTime lastEnabledTime { get; set; }
    }

    public class StorageAccountBlob
    {
        public string keyType { get; set; }
        public bool enabled { get; set; }
        public DateTime lastEnabledTime { get; set; }
    }
    public class StorageAccountPrimaryendpoints
    {
        public string dfs { get; set; }
        public string web { get; set; }
        public string blob { get; set; }
        public string queue { get; set; }
        public string table { get; set; }
        public string file { get; set; }
    }

}
