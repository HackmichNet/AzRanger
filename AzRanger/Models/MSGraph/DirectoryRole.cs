using AzRanger.Models.Generic;
using System;
using System.Collections.Generic;

namespace AzRanger.Models.MSGraph
{
    public class DirectoryRole : IReporting
    {
        public string displayName { get; }
        public Guid id { get; }
        public List<AzurePrincipal> activeMembers { get; set; }
        public List<AzurePrincipal> eligibleMembers { get; set; }
        public object deletedDateTime { get; set; }
        public string description { get; set; }
        public string roleTemplateId { get; set; }


        public DirectoryRole(Guid id, string displayName, String description, string roleTemplateId)
        {
            this.id = id;
            this.displayName = displayName;
            this.description = description;
            this.roleTemplateId = roleTemplateId;
            this.activeMembers = new List<AzurePrincipal>();
            this.eligibleMembers = new List<AzurePrincipal>();
        }

        public void AddActiveMember(AzurePrincipal az)
        {
            if (!PricipalIsInActiveMembers(az.id))
            {
                this.activeMembers.Add(az);
            }
        }

        public void AddEligibleMember(AzurePrincipal az)
        {
            // First check if in active members, if so ignore the user (active > elligble)
            if (!PricipalIsInActiveMembers(az.id))
            {
                if (!PricipalIsInEligibleMembers(az.id))
                {
                    this.eligibleMembers.Add(az);
                }
            }
        }

        public void SetMember(List<AzurePrincipal> members)
        {
            this.activeMembers = members;
        }

        public List<AzurePrincipal> GetMembers()
        {
            return this.activeMembers;
        }

        public bool PricipalIsInActiveMembers(Guid id)
        {
            foreach(AzurePrincipal azure in this.activeMembers)
            {
                if(azure.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PricipalIsInEligibleMembers(Guid id)
        {
            if(this.eligibleMembers == null)
            {
                this.eligibleMembers = new List<AzurePrincipal>();
            }
            foreach (AzurePrincipal azure in this.eligibleMembers)
            {
                if (azure.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Equals(DirectoryRole role)
        {
            return this.id == role.id;
        }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", displayName, id);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", id, displayName);
        }
    }
}
