using AzRanger.Output;
using System;

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

        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, null);
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
