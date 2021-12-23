using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class AdminAuditLogConfig
    {
        public bool AdminAuditLogEnabled { get; set; }
        public string LogLevel { get; set; }
        public bool TestCmdletLoggingEnabled { get; set; }
        public string AdminAuditLogCmdletsodatatype { get; set; }
        public string[] AdminAuditLogCmdlets { get; set; }
        public string AdminAuditLogParametersodatatype { get; set; }
        public string[] AdminAuditLogParameters { get; set; }
        public string AdminAuditLogExcludedCmdletsodatatype { get; set; }
        public object[] AdminAuditLogExcludedCmdlets { get; set; }
        public string AdminAuditLogAgeLimit { get; set; }
        public string LoadBalancerCountdatatype { get; set; }
        public int LoadBalancerCount { get; set; }
        public string RefreshIntervaldatatype { get; set; }
        public int RefreshInterval { get; set; }
        public string PartitionInfoodatatype { get; set; }
        public object[] PartitionInfo { get; set; }
        public string AdminAuditLogMailbox { get; set; }
        public bool UnifiedAuditLogIngestionEnabled { get; set; }
        public string UnifiedAuditLogFirstOptInDatedatatype { get; set; }
        public object UnifiedAuditLogFirstOptInDate { get; set; }
        public string AdminDisplayName { get; set; }
        public object Item { get; set; }
        public string ExchangeVersion { get; set; }
        public string Name { get; set; }
        public string DistinguishedName { get; set; }
        public string Identity { get; set; }
        public string ObjectCategory { get; set; }
        public string ObjectClassodatatype { get; set; }
        public string[] ObjectClass { get; set; }
        public string WhenChangeddatatype { get; set; }
        public DateTime WhenChanged { get; set; }
        public string WhenCreateddatatype { get; set; }
        public DateTime WhenCreated { get; set; }
        public string WhenChangedUTCdatatype { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public string WhenCreatedUTCdatatype { get; set; }
        public DateTime WhenCreatedUTC { get; set; }
        public string ExchangeObjectIddatatype { get; set; }
        public string ExchangeObjectIdodatatype { get; set; }
        public string ExchangeObjectId { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string Id { get; set; }
        public string Guiddatatype { get; set; }
        public string Guidodatatype { get; set; }
        public string Guid { get; set; }
        public string OriginatingServer { get; set; }
        public bool IsValid { get; set; }
    }

}
