using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Collections.Generic;

namespace AzRanger.Checks.Rules
{
    class AppsUserCanAddCredsHighPrivApp : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            // Get global admin role
            List<DirectoryRole> globalAdminRoles = new List<DirectoryRole>();
            foreach (String highPrivRole in DirectoryRoleTemplateID.GlobalAdmins)
            {
                foreach (DirectoryRole role in tenant.DirectoryRoles.Values)
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
            List<Guid> globalAdminEntity = new List<Guid>();
            foreach (DirectoryRole role in globalAdminRoles)
            {
                foreach (AzurePrincipal principal in role.GetMembers())
                {
                    if (principal.PrincipalType == AzurePrincipalType.ServicePrincipal)
                    {
                        serviceWithGlobalAdminRole.Add(tenant.ServicePrincipals[principal.id]);
                    }
                    globalAdminEntity.Add(principal.id);
                }
            }
            // Check if userAbleToAddCreds is in Global Admin group for app
            foreach (ServicePrincipal servicePrincipal in serviceWithGlobalAdminRole)
            {
                // If corresponding app resides in own tenant
                if (servicePrincipal.appOwnerOrganizationId == tenant.TenantId)
                {
                    foreach (Application app in tenant.Applications.Values)
                    {
                        if (app.appId == servicePrincipal.appId)
                        {
                            foreach (AzurePrincipal principal in app.GetUserAbleToAddCreds())
                            {
                                if (!globalAdminEntity.Contains(principal.id))
                                {
                                    passed = false;
                                    this.AddAffectedEntity(servicePrincipal);
                                    break;
                                }
                            }
                            foreach (IDTypeResponse principal in app.owners)
                            {
                                if (!globalAdminEntity.Contains(principal.id))
                                {
                                    passed = false;
                                    this.AddAffectedEntity(servicePrincipal);
                                    break;
                                }
                            }
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
