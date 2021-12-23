using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.ExchangeOnline
{
    public class RemoteDomain
    {
        public string DomainName { get; set; }
        public bool IsInternal { get; set; }
        public bool TargetDeliveryDomain { get; set; }
        public string ByteEncoderTypeFor7BitCharsets { get; set; }
        public string CharacterSet { get; set; }
        public string NonMimeCharacterSet { get; set; }
        public string AllowedOOFType { get; set; }
        public bool AutoReplyEnabled { get; set; }
        public bool AutoForwardEnabled { get; set; }
        public bool DeliveryReportEnabled { get; set; }
        public bool NDREnabled { get; set; }
        public bool MeetingForwardNotificationEnabled { get; set; }
        public string ContentType { get; set; }
        public bool DisplaySenderName { get; set; }
        public string PreferredInternetCodePageForShiftJis { get; set; }
        public object RequiredCharsetCoverage { get; set; }
        public object TNEFEnabled { get; set; }
        public string LineWrapSize { get; set; }
        public bool TrustedMailOutboundEnabled { get; set; }
        public bool TrustedMailInboundEnabled { get; set; }
        public bool UseSimpleDisplayName { get; set; }
        public bool NDRDiagnosticInfoEnabled { get; set; }
        public string MessageCountThresholddatatype { get; set; }
        public int MessageCountThreshold { get; set; }
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
