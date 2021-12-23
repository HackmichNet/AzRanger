using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Teams
{
    public class Key
    {
        public string ScopeClass { get; set; }
        public Schemaid SchemaId { get; set; }
        public Authorityid AuthorityId { get; set; }
        public Defaultxml DefaultXml { get; set; }
        public Xmlroot XmlRoot { get; set; }
    }

    public class Schemaid
    {
        public Xname XName { get; set; }
    }

    public class Xname
    {
        public string name { get; set; }
    }

    public class Xmlroot
    {
        public string name { get; set; }
    }


    public class Authorityid
    {
        public string Class { get; set; }
        public string InstanceId { get; set; }
        public Xmlroot XmlRoot { get; set; }
    }

    public class Defaultxml
    {
        public Schemaid SchemaId { get; set; }
        public Data Data { get; set; }
        public string Signature { get; set; }
        public bool IsModified { get; set; }
    }

    public class Alloweddomains
    {
        public Alloweddomain[] AllowedDomain { get; set; }
    }

    public class Alloweddomain
    {
        public string Domain { get; set; }
    }

    // Not needed at the moment
    public class Data
    {
        public object Settings { get; set; }
    }

}
