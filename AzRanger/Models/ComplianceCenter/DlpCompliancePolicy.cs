
using AzRanger.Output;
using System;

namespace AzRanger.Models.ComplianceCenter

{
    public class DlpCompliancePolicy : IReporting
    {
        public string Identity { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public string DistinguishedName { get; set; }
        public bool Enabled { get; set; }
        public string ExchangeObjectId { get; set; }
        public string Mode { get; set; }
        public string Type { get; set; }
        public string Workload { get; set; }
        public string Guid { get; set; }

        public string PrintConsole()
        {
            return Identity;
        }

        public string PrintCSV()
        {
            return Identity + ";";
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.Identity, null);
        }
    }

}
