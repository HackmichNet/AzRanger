using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
