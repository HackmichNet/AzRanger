using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class KeyVaultKey : IReporting
    {
        public string kid { get; set; }
        public KeyVaultKeyAttributes attributes { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0}", kid);
        }

        public string PrintCSV()
        {
            return String.Format("{0};", kid);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.name);
        }
    }

    public class KeyVaultKeyAttributes
    {
        public bool enabled { get; set; }
        public int? exp { get; set; }
        public int created { get; set; }
        public int updated { get; set; }
        public string recoveryLevel { get; set; }
        public int recoverableDays { get; set; }
    }
}
