using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    internal class AzMgmtScanner : IScanner
    {
        private const String ManagementGroup = "/providers/Microsoft.Management/managementGroups/{0}?api-version=2020-05-01&$expand=children";
        private const String ManagementGroupSettings = "/providers/Microsoft.Management/managementGroups/{0}/settings/default?api-version=2020-02-01";
        private const String Subscriptions = "/subscriptions/?api-version=2020-10-01";
        private const String StorageAccounts = "/subscriptions/{0}/providers/Microsoft.Storage/storageAccounts?api-version=2021-08-01";
        private const String KeyVaults = "/subscriptions/{0}/providers/Microsoft.KeyVault/vaults?api-version=2021-04-01-preview";
        private const String StorageContainers = "{0}/blobServices/default/containers?api-version=2021-08-01";
        private const String StorageDefault = "{0}/blobServices/default?api-version=2021-08-01";
        private const String DiagnosticSettings = "{0}/providers/microsoft.insights/diagnosticSettings?api-version=2021-05-01-preview";
        private const String ActivityLogAlerts = "/subscriptions/{0}/providers/microsoft.insights/activityLogAlerts?api-version=2020-10-01";
        private const String NetworkSecurityGroups = "/subscriptions/{0}/providers/Microsoft.Network/networkSecurityGroups/?api-version=2021-12-01";
        private const String SQLServers = "/subscriptions/{0}/providers/Microsoft.Sql/servers/?api-version=2021-11-01-preview";
        private const String SQLServerFirewall = "{0}/firewallRules?api-version=2015-05-01-preview";
        private const String AutoProvisioningSettings = "/subscriptions/{0}/providers/Microsoft.Security/autoProvisioningSettings/?api-version=2017-08-01-preview";
        private const String SecurityCenterBuiltIn = "/subscriptions/{0}/providers/Microsoft.Authorization/policyAssignments/SecurityCenterBuiltIn?api-version=2022-06-01";
        private const String SecurityContacts = "/subscriptions/{0}/providers/Microsoft.Security/securityContacts?api-version=2020-01-01-preview";
        private const String VirtualMachines = "/subscriptions/{0}/providers/Microsoft.Compute/virtualMachines?api-version=2022-03-01";
        public AzMgmtScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://management.azure.com/";
            this.Scope = new string[] { "https://management.azure.com/.default", "offline_access" };
        }

        public ManagementGroupSettings GetManagementGroupSettings()
        {
            return (ManagementGroupSettings)Get<ManagementGroupSettings>(String.Format(ManagementGroupSettings, this.Scanner.TenantId));
        }

        public ManagementGroup GetRootManagementGroup()
        {
            return (ManagementGroup)Get<ManagementGroup>(String.Format(ManagementGroup, this.Scanner.TenantId));
        }

        public ManagementGroup GetManagementGroup(String name)
        {
            return (ManagementGroup) Get<ManagementGroup>(String.Format(ManagementGroup, name));
        }

        public Dictionary<String, ManagementGroup> GetAllManagementGroups()
        {
            Stack<String> GroupsToProcess = new Stack<String>();
            Dictionary<String, ManagementGroup> Result = new Dictionary<string, ManagementGroup>();
            GroupsToProcess.Push(this.Scanner.TenantId);
            while (GroupsToProcess.Count > 0)
            {
                String currentManagementGroup = GroupsToProcess.Pop();
                ManagementGroup group = GetManagementGroup(currentManagementGroup);
                if (group == null)
                {
                    continue;
                }
                Result.Add(group.name, group);
                if(group.properties.children == null)
                {
                    continue;
                }
                foreach(ManagementGroupChild childGroup in group.properties.children)
                {
                    if (!Result.ContainsKey(childGroup.name) && childGroup.type == "Microsoft.Management/managementGroups")
                    {
                        GroupsToProcess.Push((String)childGroup.name);  
                    }
                }
            }
            return Result;
        }

        public Dictionary<Guid, Subscription> GetAllSubscriptions()
        {
            var subs = GetAllOf<Subscription>(Subscriptions);
            if (subs != null)
            {
                Dictionary<Guid, Subscription> Result = new Dictionary<Guid, Subscription>();
                foreach (Subscription sub in subs)
                {
                    Result.Add(Guid.Parse(sub.subscriptionId), sub);
                }
                return Result;
            }
            return null;
        }

        public List<StorageAccount> GetStorageAccounts(String subscription)
        {
            List<StorageAccount> Result = GetAllOf<StorageAccount>(String.Format(StorageAccounts, subscription));
            if (Result != null)
            {
                foreach (StorageAccount account in Result)
                {
                    account.StorageContainers = GetAllOf<StorageContainer>(String.Format(StorageContainers, account.id));
                    account.Default = (StorageAccountDefault)Get<StorageAccountDefault>(String.Format(StorageDefault, account.id));
                    account.Subscription = Guid.Parse(subscription);
                }
            }
            return Result;

        }

        public List<KeyVault> GetKeyVaults (String subscription)
        {
            List<KeyVault> Result = GetAllOf<KeyVault>(String.Format(KeyVaults, subscription));
            if (Result != null)
            {
                foreach (KeyVault keyVault in Result)
                {
                    keyVault.DiagnosticSettings = GetAllOf<DiagnosticSettings>(String.Format(DiagnosticSettings, keyVault.id));
                }
            }
            return Result;
        }
        public List<ActivityLogAlert> GetActivityLogAlerts(String subscription)
        {
            return GetAllOf<ActivityLogAlert>(String.Format(ActivityLogAlerts, subscription));
        }

        public List<NetworkSecurityGroup> GetNetworkSecurityGroups(String subscription)
        {
            return GetAllOf<NetworkSecurityGroup>(String.Format(NetworkSecurityGroups, subscription));
        }
        public List<SQLServer> GetSQLServers(String subscription)
        {
            List<SQLServer> Result = GetAllOf<SQLServer>(String.Format(SQLServers, subscription));
            if(Result == null)
            {
                return null;
            }
            foreach (SQLServer server in Result)
            {
                server.firewallRules = GetAllOf<SQLServerFirewallRules>(String.Format(SQLServerFirewall, server.id));
            }
            return Result;
        }

        public List<AutoProvisioningSettings> GetProvisioningSettings(String subscription)
        {
            return GetAllOf<AutoProvisioningSettings>(String.Format(AutoProvisioningSettings, subscription));
        }

        public SecurityContact GetSecurityContacts(String subscription)
        {
            return (SecurityContact)Get<SecurityContact>(String.Format(SecurityContacts, subscription));
        }

        public List<VirtualMachine> GetVirtualMachines(String subscription)
        {
            return GetAllOf<VirtualMachine>(String.Format(VirtualMachines, subscription));
        }

        internal override String ManipulateResponse(String response)
        {
            if (response.Contains("/providers/Microsoft.Security/securityContacts/default"))
            {
                String newResp = response.Substring(1);
                newResp = newResp.Substring(0, newResp.Length - 1);
                return newResp;
            }
            else
            {
                return response;
            }
        }

        public SecurityCenterBuiltIn GetSecurityCenterBuiltIn(String subscription)
        {
            return (SecurityCenterBuiltIn)Get<SecurityCenterBuiltIn>(String.Format(SecurityCenterBuiltIn, subscription));
        }
    }
}
