using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    class AppsDangerousPerms : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(ServicePrincipal principal in tenant.AllServicePrincipals.Values)
            {
                // f8cdef31-a31e-4b4a-93e4-5f571e91255a # Microsoft Tenant
                if (principal.appOwnerOrganizationId != null && principal.appOwnerOrganizationId.ToString() == "f8cdef31-a31e-4b4a-93e4-5f571e91255a")
                {
                    continue;
                }
                if(principal.appRoleAssignments == null)
                {
                    continue;
                }
                foreach(Approleassignment roles in principal.appRoleAssignments)
                {
                    // 9e3f62cf-ca93-4989-b6ce-bf83c28f9fe8 # RoleManagement.ReadWrite.Directory -> directly promote yourself to GA
                    // 06b708a9-e830-4db3-a914-8e69da51d44f # AppRoleAssignment.ReadWrite.All -> grant yourself the above role, then promote to GA

                    if (roles.appRoleId == "9e3f62cf-ca93-4989-b6ce-bf83c28f9fe8" | roles.appRoleId == "06b708a9-e830-4db3-a914-8e69da51d44f")
                    {
                        passed = false;
                        this.AddAffectedEntity(principal);
                    }
                }
            }

            if(passed){
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;

        }
    }
}
