using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using NLog;
using System;
using System.Linq;

namespace AzRanger.Utilities.EnrichmentEngine
{
    public static class AssignUserCanAddCreds
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Enrich(Tenant tenant)
        {
            foreach (DirectoryRole role in tenant.DirectoryRoles.Values)
            {
                if (DirectoryRoleTemplateID.RolesAllowingAddCreds.Contains(role.roleTemplateId))
                {
                    foreach (AzurePrincipal principal in role.activeMembers)
                    {
                        foreach (Application a in tenant.Applications.Values)
                        {
                            a.AddUserAbleToAddCreds(principal);
                        }

                        foreach (ServicePrincipal s in tenant.ServicePrincipals.Values)
                        {
                            if (s.appOwnerOrganizationId == tenant.TenantId)
                            {
                                s.AddUserAbleToAddCreds(principal);
                            }
                        }
                    }
                    foreach (AzurePrincipal principal in role.eligibleMembers)
                    {
                        foreach (Application a in tenant.Applications.Values)
                        {
                            a.AddUserAbleToAddCreds(principal);
                        }

                        foreach (ServicePrincipal s in tenant.ServicePrincipals.Values)
                        {
                            if (s.appOwnerOrganizationId == tenant.TenantId)
                            {
                                s.AddUserAbleToAddCreds(principal);
                            }
                        }
                    }
                    foreach (Tuple<AzurePrincipal, AzurePrincipal> entry in role.activeMembersScoped)
                    {
                        if (entry.Item2.PrincipalType == AzurePrincipalType.Application)
                        {
                            tenant.Applications[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                        if (entry.Item2.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            tenant.ServicePrincipals[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                    }
                    foreach (Tuple<AzurePrincipal, AzurePrincipal> entry in role.eligibleMembersScoped)
                    {
                        if (entry.Item2.PrincipalType == AzurePrincipalType.Application)
                        {
                            tenant.Applications[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                        if (entry.Item2.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            tenant.ServicePrincipals[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                    }
                }
            }
        }
    }
}
