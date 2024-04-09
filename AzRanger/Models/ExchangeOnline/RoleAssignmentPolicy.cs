using AzRanger.Output;
using System;

namespace AzRanger.Models.ExchangeOnline
{
    public class RoleAssignmentPolicy : IReporting
    {
        public bool IsDefault { get; set; }
        public string Description { get; set; }
        public string RoleAssignmentsodatatype { get; set; }
        public string[] RoleAssignments { get; set; }
        public string AssignedRolesodatatype { get; set; }
        public string[] AssignedRoles { get; set; }
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

        public string PrintConsole()
        {
            return Identity;
        }

        public string PrintCSV()
        {
            return Identity + ";";
        }
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.Id, this.Identity);
        }
    }
}
