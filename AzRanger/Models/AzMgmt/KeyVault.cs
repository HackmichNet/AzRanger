using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{

    public class KeyVault : IReporting
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public KeyVaultProperties properties { get; set; }

        // Custom Probs
        public List<DiagnosticSettings> DiagnosticSettings { get; set; }
        public List<KeyVaultKey> Keys { get; set; }
        public List<KeyVaultSecret> Secrets{ get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", name, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", name, id);
        }
    }

     public class KeyVaultProperties
    {
        public KeyVaultSku sku { get; set; }
        public string tenantId { get; set; }
        public KeyVaultAccesspolicy[] accessPolicies { get; set; }
        public bool enabledForDeployment { get; set; }
        public bool enabledForDiskEncryption { get; set; }
        public bool enabledForTemplateDeployment { get; set; }
        public bool enableSoftDelete { get; set; }
        public int softDeleteRetentionInDays { get; set; }
        public bool enableRbacAuthorization { get; set; }
        public bool enablePurgeProtection { get; set; }
        public string vaultUri { get; set; }
        public string provisioningState { get; set; }
        public string publicNetworkAccess { get; set; }
    }

    public class KeyVaultSku
    {
        public string family { get; set; }
        public string name { get; set; }
    }

    public class KeyVaultAccesspolicy
    {
        public string tenantId { get; set; }
        public string objectId { get; set; }
        public KeyVaultPermissions permissions { get; set; }
    }

    public class KeyVaultPermissions
    {
        public string[] keys { get; set; }
        public string[] secrets { get; set; }
        public string[] certificates { get; set; }
    }

}
