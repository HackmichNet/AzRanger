using AzRanger.AzScanner;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace AzRanger.Utilities.EnrichmentEngine
{
    public static class AssignUserCanAddCreds
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Enrich(Tenant tenant)
        {
            foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
            {
                if (DirectoryRoleTemplateID.RolesAllowingAddCreds.Contains(role.roleTemplateId))
                {
                    foreach (AzurePrincipal principal in role.activeMembers)
                    {
                        foreach (Application a in tenant.AllApplications.Values)
                        {
                            a.AddUserAbleToAddCreds(principal);
                        }

                        foreach (ServicePrincipal s in tenant.AllServicePrincipals.Values)
                        {
                            if (s.appOwnerOrganizationId == tenant.TenantId)
                            {
                                s.AddUserAbleToAddCreds(principal);
                            }
                        }
                    }
                    foreach (AzurePrincipal principal in role.eligibleMembers)
                    {
                        foreach (Application a in tenant.AllApplications.Values)
                        {
                            a.AddUserAbleToAddCreds(principal);
                        }

                        foreach (ServicePrincipal s in tenant.AllServicePrincipals.Values)
                        {
                            if (s.appOwnerOrganizationId == tenant.TenantId)
                            {
                                s.AddUserAbleToAddCreds(principal);
                            }
                        }
                    }
                    foreach(Tuple<AzurePrincipal, AzurePrincipal> entry in role.activeMembersScoped)
                    {
                        if(entry.Item2.PrincipalType == AzurePrincipalType.Application)
                        {
                            tenant.AllApplications[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                        if (entry.Item2.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            tenant.AllServicePrincipals[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                    }
                    foreach (Tuple<AzurePrincipal, AzurePrincipal> entry in role.eligibleMembersScoped)
                    {
                        if (entry.Item2.PrincipalType == AzurePrincipalType.Application)
                        {
                            tenant.AllApplications[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                        if (entry.Item2.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            tenant.AllServicePrincipals[entry.Item2.id].AddUserAbleToAddCreds(new AzurePrincipal(entry.Item1.id, entry.Item1.PrincipalType));
                        }
                    }
                }
            }
        }
    }
}
