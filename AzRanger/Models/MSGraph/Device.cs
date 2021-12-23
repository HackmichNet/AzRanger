using AzRanger.Models.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class Device
    {
        public Guid id { get; set; }
        public string displayName { get; set; }
        public object isCompliant { get; set; }
        public object isManaged { get; set; }
        public string operatingSystem { get; set; }
        public object enrollmentType { get; set; }
        public string profileType { get; set; }
        public Guid deviceId { get; set; }
        public object deviceOwnership { get; set; }
        public object onPremisesSyncEnabled { get; set; }
        public IDTypeResponse[] registeredOwners { get; set; }
    }
}
