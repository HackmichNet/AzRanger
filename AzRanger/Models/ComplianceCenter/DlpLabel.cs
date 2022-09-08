using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.ComplianceCenter
{
     public class DlpLabel
    {
        public string Identity { get; set; }
        public object AbacEnabled { get; set; }
        public object[] Capabilities { get; set; }
        public object ColumnAssetCondition { get; set; }
        public string Comment { get; set; }
        public object[] Conditions { get; set; }
        public string ContentType { get; set; }
        public string CreatedBy { get; set; }
        public object CustomSecurityAttributeRequired { get; set; }
        public object DefaultContentLabel { get; set; }
        public bool Disabled { get; set; }
        public string DisplayName { get; set; }
        public string DistinguishedName { get; set; }
        public string ExchangeObjectId { get; set; }
        public string ExchangeVersion { get; set; }
        public string ExternalIdentity { get; set; }
        public string Guid { get; set; }
        public string Id { get; set; }
        public string ImmutableId { get; set; }
        public bool IsParent { get; set; }
        public bool IsValid { get; set; }
        public object Item { get; set; }
        public string[] LabelActions { get; set; }
        public string LastModifiedBy { get; set; }
        public string[] LocaleSettings { get; set; }
        public string Mode { get; set; }
        public string Name { get; set; }
        public object ObjectCategory { get; set; }
        public string[] ObjectClass { get; set; }
        public string ObjectVersion { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string OriginatingServer { get; set; }
        public object ParentId { get; set; }
        public object ParentLabelDisplayName { get; set; }
        public string Policy { get; set; }
        public int Priority { get; set; }
        public bool ReadOnly { get; set; }
        public object SchematizedDataCondition { get; set; }
        public string[] Settings { get; set; }
        public string Tooltip { get; set; }
        public DateTime WhenChanged { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public DateTime WhenCreated { get; set; }
        public DateTime WhenCreatedUTC { get; set; }
        public string Workload { get; set; }
        public object[] ResolvedUsers { get; set; }
        public object Setting { get; set; }
        public object AdvancedSettings { get; set; }
        public object NextLabel { get; set; }
        public object PreviousLabel { get; set; }
        public object MigrationId { get; set; }
        public object SkipValidations { get; set; }
    }

}
