using AzRanger.Models.AzMgmt;
using System.Collections.Generic;

namespace AzRanger.Models
{
    public class AzResources
    {
        public List<StorageAccount> StorageAccounts;
        public List<KeyVault> KeyVaults;
        public List<ActivityLogAlert> ActivityLogAlerts;
        public List<NetworkSecurityGroup> NetworkSecurityGroups;
        public List<SQLServer> SQLServers;
        public List<VirtualMachine> VirtualMachines;
        public List<PostgreSQLFlexibleServers> PostgreSQLs;
    }
}
