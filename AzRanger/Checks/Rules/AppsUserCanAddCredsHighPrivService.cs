using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AppsUserCanAddCredsHighPrivService", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/AllApps/menuId/")]
    [RuleScore("Service Principal with Global Admin role or similar role where a low priv user/app can add credentials.", "This could lead privileges escalation within your tenant, please check 'Roles and administrators | Preview'.", 9, "https://posts.specterops.io/azure-privilege-escalation-via-azure-api-permissions-abuse-74aee1006f48")]
    class AppsUserCanAddCredsHighPrivService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            // Get global admin roles
            List<DirectoryRole> globalAdminRoles = new List<DirectoryRole>();
            foreach (String highPrivRole in DirectoryRoleTemplateID.GlobalAdmins)
            {
                foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
                {
                    if (role.roleTemplateId == highPrivRole)
                    {
                        globalAdminRoles.Add(role);
                        break;
                    }
                }
            }

            // Get Apps with global Admins role 
            // Get User with global admin role
            List<ServicePrincipal> serviceWithGlobalAdminRole = new List<ServicePrincipal>();
            List<Guid> globalAdminEntities = new List<Guid>();
            foreach (DirectoryRole role in globalAdminRoles)
            {
                foreach (AzurePrincipal principal in role.GetMembers())
                {
                    if (principal.PrincipalType == AzurePrincipalType.ServicePrincipal)
                    {
                        serviceWithGlobalAdminRole.Add(tenant.AllServicePrincipals[principal.id]);
                    }
                    globalAdminEntities.Add(principal.id);
                }
            }
            // Check if userAbleToAddCreds is in Global Admin group for servicePrincipals
            foreach (ServicePrincipal servicePrincipal in serviceWithGlobalAdminRole)
            {
                foreach (AzurePrincipal principal in servicePrincipal.userAbleToAddCreds)
                {
                    if (!globalAdminEntities.Contains(principal.id))
                    {
                        passed = false;
                        this.AddAffectedEntity(servicePrincipal);
                        break;
                    }
                }
                foreach(IDTypeResponse response in servicePrincipal.owners)
                {
                    if (!globalAdminEntities.Contains(response.id))
                    {
                        passed = false;
                        this.AddAffectedEntity(servicePrincipal);
                        break;
                    }
                }
            }
            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
