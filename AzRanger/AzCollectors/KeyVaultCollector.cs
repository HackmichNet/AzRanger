﻿using AzRanger.Models.AzMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class KeyVaultCollector : AbstractCollector
    {
        private const String Keys = "/keys/?api-version=7.3";
        private const String Secrets = "/secrets/?api-version=7.3";
        public KeyVaultCollector(MainCollector scanner, String vaultUri)
        {
            this.Scanner = scanner;
            this.BaseAdresse = vaultUri;
            this.Scope = new string[] { "https://vault.azure.net/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, scanner.Proxy);
        }

        public Task<List<KeyVaultKey>> GetKeyVaultKeys()
        {
            return GetAllOf<KeyVaultKey>(Keys);
        }

        public Task<List<KeyVaultSecret>> GetKeyVaultSecrets()
        {
            return GetAllOf<KeyVaultSecret>(Secrets);
        }
    }
}
