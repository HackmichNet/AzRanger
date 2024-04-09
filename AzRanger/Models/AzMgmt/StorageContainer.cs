using AzRanger.Output;
using System;

namespace AzRanger.Models.AzMgmt
{
    public class StorageContainer : IReporting
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string etag { get; set; }
        public StorageContainerProperties properties { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", name, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", name, id);
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id, this.name);
        }
    }

    public class StorageContainerProperties
    {
        public Immutablestoragewithversioning immutableStorageWithVersioning { get; set; }
        public bool deleted { get; set; }
        public int remainingRetentionDays { get; set; }
        public string defaultEncryptionScope { get; set; }
        public bool denyEncryptionScopeOverride { get; set; }
        // None => Private
        // StorageAccountBlob => StorageAccountBlob
        // Container => Container
        public string publicAccess { get; set; }
        public string leaseStatus { get; set; }
        public string leaseState { get; set; }
        public DateTime lastModifiedTime { get; set; }
        public bool hasImmutabilityPolicy { get; set; }
        public bool hasLegalHold { get; set; }
    }

    public class Immutablestoragewithversioning
    {
        public bool enabled { get; set; }
    }

}
