using AzRanger.Models.Azrbac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class AzrbacScanner : IScanner
    {
        private const String RoleAssignemtsForApp = "/api/v2/privilegedAccess/aadroles/roleAssignments";
        public AzrbacScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://api.azrbac.mspim.azure.com";
            this.Scope = new String[] { "01fc33a7-78ba-4d2f-a4b7-768e336e890e/.default", "offline_access" };
        }


        public List<RoleAssignments> GetRoleAssignemtsForApp(Guid tenantID, Guid roleDefinition)
        {
            String query = String.Format(@"$filter=(roleDefinition/resource/id eq '{0}') and (roleDefinition/id eq '{1}')&$expand=subject,scopedResource", tenantID.ToString(), roleDefinition.ToString());
            return GetAllOf<RoleAssignments>(RoleAssignemtsForApp, query); ;
        }
    }
}
