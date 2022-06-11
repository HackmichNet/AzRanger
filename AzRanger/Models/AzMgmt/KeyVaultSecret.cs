using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class KeyVaultSecret : IReporting
    {
        public string id { get; set; }
        public KeyVaultSecretAttributes attributes { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0}", id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};", id);
        }
    }

    public class KeyVaultSecretAttributes
    {
        public bool enabled { get; set; }
        public int? exp { get; set; }
        public int created { get; set; }
        public int updated { get; set; }
        public string recoveryLevel { get; set; }
        public int recoverableDays { get; set; }
    }
}
