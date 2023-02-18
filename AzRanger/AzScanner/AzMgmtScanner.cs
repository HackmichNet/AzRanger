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
        private const String SubscriptionPolicy = "/providers/Microsoft.Subscription/policies/default?api-version=2021-01-01-privatepreview";
        private const String AuditingSettings = "{0}/auditingSettings/default?api-version=2021-11-01-preview";
        private const String SQLAdminitrators = "{0}/administrators?api-version=2019-06-01-preview";
        private const String SQLDatabases = "{0}/databases/?api-version=2022-02-01-preview";
        private const String TransparentDataEncryption = "{0}/transparentDataEncryption/current?api-version=2020-11-01-preview";
        public AzMgmtScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://management.azure.com/";
            this.Scope = new string[] { "https://management.azure.com/.default", "offline_access" };
        }

        public Task<ManagementGroupSettings> GetManagementGroupSettings()
        {
            return Get<ManagementGroupSettings>(String.Format(ManagementGroupSettings, this.Scanner.TenantId));
        }

        public Task<ManagementGroup> GetRootManagementGroup()
        {
            return Get<ManagementGroup>(String.Format(ManagementGroup, this.Scanner.TenantId));
        }

        public Task<ManagementGroup> GetManagementGroup(String name)
        {
            return Get<ManagementGroup>(String.Format(ManagementGroup, name));
        }

        public async Task<Dictionary<String, ManagementGroup>> GetAllManagementGroups()
        {
            Stack<String> GroupsToProcess = new Stack<String>();
            Dictionary<String, ManagementGroup> Result = new Dictionary<string, ManagementGroup>();
            GroupsToProcess.Push(this.Scanner.TenantId);
            while (GroupsToProcess.Count > 0)
            {
                String currentManagementGroup = GroupsToProcess.Pop();
                ManagementGroup group = (ManagementGroup)(await GetManagementGroup(currentManagementGroup));
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

        public async Task<Dictionary<Guid, Subscription>> GetAllSubscriptions()
        {
            var subs = await GetAllOf<Subscription>(Subscriptions);
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

        public async Task<List<StorageAccount>> GetStorageAccounts(String subscription)
        {
            List<StorageAccount> Result = await GetAllOf<StorageAccount>(String.Format(StorageAccounts, subscription));
            if (Result != null)
            {
                foreach (StorageAccount account in Result)
                {
                    account.StorageContainers = await GetAllOf<StorageContainer>(String.Format(StorageContainers, account.id));
                    account.Default = (StorageAccountDefault) (await Get<StorageAccountDefault>(String.Format(StorageDefault, account.id)));
                    account.Subscription = Guid.Parse(subscription);
                }
            }
            return Result;

        }

        public async Task<List<KeyVault>> GetKeyVaults (String subscription)
        {
            List<KeyVault> Result = await GetAllOf<KeyVault>(String.Format(KeyVaults, subscription));
            if (Result != null)
            {
                foreach (KeyVault keyVault in Result)
                {
                    keyVault.DiagnosticSettings = await GetAllOf<DiagnosticSettings>(String.Format(DiagnosticSettings, keyVault.id));
                }
            }
            return Result;
        }
        public Task<List<ActivityLogAlert>> GetActivityLogAlerts(String subscription)
        {
            return GetAllOf<ActivityLogAlert>(String.Format(ActivityLogAlerts, subscription));
        }

        public Task<List<NetworkSecurityGroup>> GetNetworkSecurityGroups(String subscription)
        {
            return GetAllOf<NetworkSecurityGroup>(String.Format(NetworkSecurityGroups, subscription));
        }
        public async Task<List<SQLServer>> GetSQLServers(String subscription)
        {
            List<SQLServer> Result = await GetAllOf<SQLServer>(String.Format(SQLServers, subscription));
            if(Result == null)
            {
                return null;
            }
            foreach (SQLServer server in Result)
            {
                server.firewallRules = await GetAllOf<SQLServerFirewallRules>(String.Format(SQLServerFirewall, server.id));
                server.auditingSettings = (SQLServerAuditingSettings)(await Get<SQLServerAuditingSettings>(String.Format(AuditingSettings, server.id)));
                server.SQLAdministrators = await GetAllOf<SQLAdministrator>(String.Format(SQLAdminitrators, server.id));
                server.SQLDatabases = await GetAllOf<SQLDatabase>(String.Format(SQLDatabases, server.id));
                foreach (SQLDatabase database in server.SQLDatabases)
                {
                    database.transparentDataEncryption = (SQLDatabaseTransparentDataEncryption) (await Get<SQLDatabaseTransparentDataEncryption>(String.Format(TransparentDataEncryption, database.id)));
                }
            }
            return Result;
        }

        public Task<List<AutoProvisioningSettings>> GetProvisioningSettings(String subscription)
        {
            return GetAllOf<AutoProvisioningSettings>(String.Format(AutoProvisioningSettings, subscription));
        }

        public Task<SecurityContact> GetSecurityContacts(String subscription)
        {
            return Get<SecurityContact>(String.Format(SecurityContacts, subscription));
        }

        public Task<List<VirtualMachine>> GetVirtualMachines(String subscription)
        {
            return GetAllOf<VirtualMachine>(String.Format(VirtualMachines, subscription));
        }

        public Task<SubscriptionPolicy> GetSubscriptionPolicy()
        {
            return Get<SubscriptionPolicy>(SubscriptionPolicy);
        }

        internal override String ManipulateResponse(String response, String endPoint)
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

        public Task<SecurityCenterBuiltIn> GetSecurityCenterBuiltIn(String subscription)
        {
            return Get<SecurityCenterBuiltIn>(String.Format(SecurityCenterBuiltIn, subscription));
        }
    }
}
