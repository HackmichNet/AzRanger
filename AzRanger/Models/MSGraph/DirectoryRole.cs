﻿using AzRanger.Models.Generic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class DirectoryRole : IEntity
    {
        [JsonProperty(PropertyName = "@odata.nextLink")]
        public String odatanextLink;
        public string displayName { get; }
        public Guid id { get; }
        private List<AzurePrincipal> members { get; set; }
        public object deletedDateTime { get; set; }
        public string description { get; set; }
        public string roleTemplateId { get; set; }


        public DirectoryRole(Guid id, string displayName, String description, string roleTemplateId)
        {
            this.id = id;
            this.displayName = displayName;
            this.description = description;
            this.roleTemplateId = roleTemplateId;
            this.members = new List<AzurePrincipal>();
        }

        public void AddMember(AzurePrincipal az)
        {
            if (!Contains(az.id))
            {
                this.members.Add(az);
            }
        }

        public void SetMember(List<AzurePrincipal> members)
        {
            this.members = members;
        }

        public List<AzurePrincipal> GetMembers()
        {
            return this.members;
        }

        public bool Contains(Guid id)
        {
            foreach(AzurePrincipal azure in this.members)
            {
                if(azure.id == id)
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
