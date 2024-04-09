using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    class AppsWithHighPrives : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (DirectoryRole role in tenant.DirectoryRoles.Values)
            {
                if (DirectoryRoleTemplateID.HighPrivRoles.Any(role.roleTemplateId.Contains))
                {
                    foreach (AzurePrincipal id in role.GetMembers())
                    {
                        if (id.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            passed = false;
                            this.AddAffectedEntity(tenant.ServicePrincipals[id.id]);
                        }
                    }
                }
            }

            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
