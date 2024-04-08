using AzRanger.Models.Azrbac;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class AzrbacCollector : AbstractCollector
    {
        private const String RoleAssignmentForDirectory = "/api/v2/privilegedAccess/aadroles/roleAssignments";
        public AzrbacCollector(IAuthenticator authenticator, String tenantId, String proxy)
        {
            this.Authenticator = authenticator;
            this.TenantId = tenantId;
            this.BaseAddress = "https://api.azrbac.mspim.azure.com";
            this.Scope = new String[] { "01fc33a7-78ba-4d2f-a4b7-768e336e890e/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, proxy);
        }

        public Task<List<PIMRoleAssignments>> GetRoleAssignments(Guid roleDefinition)
        {
            String query = String.Format(@"$filter=(roleDefinition/resource/id eq '{0}') and (roleDefinition/id eq '{1}')&$expand=subject,scopedResource", TenantId.ToString(), roleDefinition.ToString());
            return GetAllOf<PIMRoleAssignments>(RoleAssignmentForDirectory, query); ;
        }
    }
}
