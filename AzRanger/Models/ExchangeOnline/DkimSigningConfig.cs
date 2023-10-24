using AzRanger.Output;
using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class DkimSigningConfig : IReporting
    {
        public string Identity { get; set; }
        public string AdminDisplayName { get; set; }
        public string Algorithm { get; set; }
        public string BodyCanonicalization { get; set; }
        public string DistinguishedName { get; set; }
        public string Domain { get; set; }
        public bool Enabled { get; set; }
        public string ExchangeObjectId { get; set; }
        public string ExchangeVersion { get; set; }
        public string Guid { get; set; }
        public string HeaderCanonicalization { get; set; }
        public string Id { get; set; }
        public bool IncludeKeyExpiration { get; set; }
        public bool IncludeSignatureCreationTime { get; set; }
        public bool IsDefault { get; set; }
        public bool IsValid { get; set; }
        public object Item { get; set; }
        public DateTime KeyCreationTime { get; set; }
        public DateTime LastChecked { get; set; }
        public string Name { get; set; }
        public string NumberOfBytesToSign { get; set; }
        public string ObjectCategory { get; set; }
        public string[] ObjectClass { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string OriginatingServer { get; set; }
        public DateTime RotateOnDate { get; set; }
        public string Selector1CNAME { get; set; }
        public int Selector1KeySize { get; set; }
        public string Selector1PublicKey { get; set; }
        public string Selector2CNAME { get; set; }
        public int Selector2KeySize { get; set; }
        public string Selector2PublicKey { get; set; }
        public string SelectorAfterRotateOnDate { get; set; }
        public string SelectorBeforeRotateOnDate { get; set; }
        public string Status { get; set; }
        public DateTime WhenChanged { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public DateTime WhenCreated { get; set; }
        public DateTime WhenCreatedUTC { get; set; }

        public string PrintConsole()
        {
            return String.Format("{0}", Domain );
        }

        public string PrintCSV()
        {
            return String.Format("{0};", Domain);
        }

        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(null, this.Domain);
        }
    }

}
