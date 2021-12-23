using AzRanger.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models
{
    public class TeamsSettings
    {
        public TeamsClientConfiguration TeamsClientConfiguration { get; set; }
        public TenantFederationSettings TenantFederationSettings { get; set; }
    }
}
