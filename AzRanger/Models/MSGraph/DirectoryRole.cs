using AzRanger.Models.Azrbac;
using AzRanger.Models.Generic;
using AzRanger.Output;
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
        //                  Id              Scope
        public List<Tuple<AzurePrincipal, AzurePrincipal>> activeMembersScoped { get; set; }
        public List<Tuple<AzurePrincipal, AzurePrincipal>> eligibleMembersScoped { get; set; }
        public List<DirectoryRoleAssignment> pimRoleAssignments { get; set; }
        public List<DirectoryRoleAssignment> pimRoleAssignmentsEligible { get; set; }


        public DirectoryRole(Guid id, string displayName, String description, string roleTemplateId)
        {
            this.id = id;
            this.displayName = displayName;
            this.description = description;
            this.roleTemplateId = roleTemplateId;
            this.activeMembers = new List<AzurePrincipal>();
            this.eligibleMembers = new List<AzurePrincipal>();
            this.activeMembersScoped = new List<Tuple<AzurePrincipal, AzurePrincipal>>();
            this.eligibleMembersScoped = new List<Tuple<AzurePrincipal, AzurePrincipal>>();
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

        public void AddActiveMemberScopes(Tuple<AzurePrincipal, AzurePrincipal> member)
        {
            if (!PricipalIsInActiveMembersScopes(member.Item1.id, member.Item2.id))
            {
                this.activeMembersScoped.Add(member);   
            }
        }
        public void AddEligibleMemberScoped(Tuple<AzurePrincipal, AzurePrincipal> member)
        {
            // First check if in active members, if so ignore the user (active > elligble)
            if (!PricipalIsInActiveMembersScopes(member.Item1.id, member.Item2.id))
            {
                if (!PricipalIsInEligibleMembersScopes(member.Item1.id, member.Item2.id))
                {
                    this.eligibleMembersScoped.Add(member);
                }
            }
        }

        public void SetActiveMember(List<AzurePrincipal> members)
        {
            this.activeMembers = members;
        }

        public List<AzurePrincipal> GetMembers()
        {
            List<AzurePrincipal> allMembers = new List<AzurePrincipal>();
            foreach(var member in this.activeMembers)
            {
                allMembers.Add(member);
            }
            foreach(var member in this.eligibleMembers)
            {
                allMembers.Add(member);
            }
            foreach (var member in this.activeMembersScoped)
            {
                allMembers.Add(member.Item1);
            }
            foreach(var member in this.eligibleMembersScoped)
            {
                allMembers.Add(member.Item1);
            }
            return allMembers;
        }

        public bool PricipalIsInActiveMembersScopes(Guid id, Guid scope)
        {
            foreach (Tuple<AzurePrincipal, AzurePrincipal> entity in this.activeMembersScoped)
            {
                if (entity.Item1.id == id && entity.Item2.id == scope)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PricipalIsInEligibleMembersScopes(Guid id, Guid scope)
        {
            foreach (Tuple<AzurePrincipal, AzurePrincipal> entity in this.eligibleMembersScoped)
            {
                if (entity.Item1.id == id && entity.Item2.id == scope)
                {
                    return true;
                }
            }
            return false;
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
        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.id.ToString(), this.displayName);
        }
    }
}
