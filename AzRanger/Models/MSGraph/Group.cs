using AzRanger.Models.Generic;
using AzRanger.Output;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzRanger.Models
{
    public class Group : IReporting
    {
        [JsonPropertyName("@odata.nextLink")]
        public String odatanextLink;
        public Guid id { get; set; }
        public string displayName { get; set; }
        public bool securityEnabled { get; set; }
        public object visibility { get; set; }
        public List<AzurePrincipal> members = new List<AzurePrincipal>();

        public string PrintConsole()
        {
            return String.Format(@"{0} - {1}", this.displayName, this.id);
        }

        public string PrintCSV()
        {
            return String.Format(@"{0};{1}", this.id, this.displayName);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id.ToString(), this.displayName);
        }
    }
}
