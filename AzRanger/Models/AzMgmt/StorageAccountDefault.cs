namespace AzRanger.Models.AzMgmt
{
    public class StorageAccountDefault
    {
        public StorageAccountDefaultSku sku { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public StorageAccountDefaultProperties properties { get; set; }
    }

    public class StorageAccountDefaultSku
    {
        public string name { get; set; }
        public string tier { get; set; }
    }

    public class StorageAccountDefaultProperties
    {
        public Changefeed changeFeed { get; set; }
        public Restorepolicy restorePolicy { get; set; }
        public Deleteretentionpolicy containerDeleteRetentionPolicy { get; set; }
        public Cors cors { get; set; }
        public Deleteretentionpolicy deleteRetentionPolicy { get; set; }
        public bool isVersioningEnabled { get; set; }
    }

    public class Changefeed
    {
        public bool enabled { get; set; }
    }

    public class Restorepolicy
    {
        public bool enabled { get; set; }
    }

    public class Cors
    {
        public object[] corsRules { get; set; }
    }

    public class Deleteretentionpolicy
    {
        public bool enabled { get; set; }
        public int? days { get; set; }
    }

}
