using System;
using System.Text.Json.Serialization;

namespace AzRanger.Models
{
    public class Group : IEntity
    {
        [JsonPropertyName("@odata.nextLink")]
        public String odatanextLink;
        public Guid id { get; set; }
        public string displayName { get; set; }
        public bool securityEnabled { get; set; }
        public object visibility { get; set; }

        public string PrintConsole()
        {
            return String.Format(@"{0} - {1}", this.displayName, this.id);
        }

        public string PrintCSV()
        {
            return String.Format(@"{0};{1}", this.id, this.displayName);
        }
    }
}
