using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class AcceptedDomain : IReporting
    {
        public string Identity { get; set; }
        public bool AddressBookEnabled { get; set; }
        public string AdminDisplayName { get; set; }
        public string AuthenticationType { get; set; }
        public bool CanHaveCloudCache { get; set; }
        public object CatchAllRecipientID { get; set; }
        public bool Default { get; set; }
        public string DistinguishedName { get; set; }
        public string DomainName { get; set; }
        public string DomainType { get; set; }
        public bool EmailOnly { get; set; }
        public bool EnableNego2Authentication { get; set; }
        public string ExchangeObjectId { get; set; }
        public string ExchangeVersion { get; set; }
        public bool ExternallyManaged { get; set; }
        public string FederatedOrganizationLink { get; set; }
        public string Guid { get; set; }
        public string Id { get; set; }
        public bool InitialDomain { get; set; }
        public bool IsCoexistenceDomain { get; set; }
        public bool IsDefaultFederatedDomain { get; set; }
        public bool IsValid { get; set; }
        public object Item { get; set; }
        public string LiveIdInstanceType { get; set; }
        public object MailFlowPartner { get; set; }
        public bool MatchSubDomains { get; set; }
        public string Name { get; set; }
        public string ObjectCategory { get; set; }
        public string[] ObjectClass { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string OriginatingServer { get; set; }
        public bool OutboundOnly { get; set; }
        public bool PendingCompletion { get; set; }
        public bool PendingFederatedAccountNamespace { get; set; }
        public bool PendingFederatedDomain { get; set; }
        public bool PendingRemoval { get; set; }
        public bool PerimeterDuplicateDetected { get; set; }
        public DateTime WhenChanged { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public DateTime WhenCreated { get; set; }
        public DateTime WhenCreatedUTC { get; set; }
        //Custom Properties
        public bool HasSPF { get; set; }
        public bool HasDMARC { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0}", DomainName);
        }

        public string PrintCSV()
        {
            return String.Format("{0};", DomainName);
        }
    }

}
