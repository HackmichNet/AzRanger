using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.AzMgmt
{
    public class AuditingSettings
    {
        public AuditingSettingsProperties properties { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class AuditingSettingsProperties
    {
        public int retentionDays { get; set; }
        public string[] auditActionsAndGroups { get; set; }
        public bool isAzureMonitorTargetEnabled { get; set; }
        public bool isManagedIdentityInUse { get; set; }
        public string state { get; set; }
        public string storageEndpoint { get; set; }
        public string storageAccountSubscriptionId { get; set; }
    }

}
