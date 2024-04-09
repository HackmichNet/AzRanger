using AzRanger.AzScanner;
using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzRanger.Utilities.EnrichmentEngine
{
    public static class AssignUserToRole
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static async Task<bool> Enrich(Tenant tenant, MSGraphCollector collector)
        {
            foreach (DirectoryRole role in tenant.DirectoryRoles.Values)
            {
                foreach (DirectoryRoleAssignment assignment in role.pimRoleAssignments)
                {
                    // Calculate which user has which role
                    List<AzurePrincipal> principalsToAssign = new List<AzurePrincipal>();
                    AzurePrincipalType aztype;
                    switch (assignment.principal.odatatype)
                    {
                        case "#microsoft.graph.user":
                            aztype = AzurePrincipalType.User;
                            break;
                        case "#microsoft.graph.servicePrincipal":
                            aztype = AzurePrincipalType.ServicePrincipal;
                            break;
                        case "#microsoft.graph.application":
                            aztype = AzurePrincipalType.Application;
                            break;
                        case "#microsoft.graph.group":
                            aztype = AzurePrincipalType.Group;
                            break;
                        default:
                            continue;
                    }
                    if (aztype == AzurePrincipalType.Group)
                    {
                        List<AzurePrincipal> members = await collector.GetAllGroupMemberTransitiv(Guid.Parse(assignment.principalId));
                        foreach (AzurePrincipal member in members)
                        {
                            principalsToAssign.Add(member);
                        }
                    }
                    else
                    {
                        principalsToAssign.Add(new AzurePrincipal(Guid.Parse(assignment.principalId), aztype));
                    }

                    if (assignment.directoryScopeId.Equals("/"))
                    {
                        foreach (AzurePrincipal p in principalsToAssign)
                        {
                            role.AddActiveMember(p);
                        }
                    }
                    else
                    {
                        Guid scopeId = Guid.Parse(assignment.directoryScopeId.Substring(1));
                        if (assignment.directoryScope.odatatype.Equals("#microsoft.graph.application"))
                        {
                            foreach (AzurePrincipal p in principalsToAssign)
                            {
                                role.AddActiveMemberScopes(new Tuple<AzurePrincipal, AzurePrincipal>(p, new AzurePrincipal(scopeId, AzurePrincipalType.Application)));
                            }
                            continue;
                        }
                        if (assignment.directoryScope.odatatype.Equals("#microsoft.graph.servicePrincipal"))
                        {
                            if (tenant.ServicePrincipals[scopeId].appOwnerOrganizationId == tenant.TenantId)
                            {
                                foreach (AzurePrincipal p in principalsToAssign)
                                {
                                    role.AddActiveMemberScopes(new Tuple<AzurePrincipal, AzurePrincipal>(p, new AzurePrincipal(scopeId, AzurePrincipalType.ServicePrincipal)));
                                }
                            }
                            continue;
                        }
                        logger.Warn("[-] AssignUserCanAddCreds: Pim Assignment unknown scope: " + assignment.directoryScope.odatatype);
                    }
                }
            }
            return true;
        }
    }
}
