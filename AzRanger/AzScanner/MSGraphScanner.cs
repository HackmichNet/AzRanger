using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Models.MSGraph.MDM;
using System.Collections.Generic;
using System;

namespace AzRanger.AzScanner
{

    public class MSGraphScanner : IScanner
    {
        public const String ConditionalAccessPolicies = "/beta/identity/conditionalAccess/policies";
        public const String SecureScore = "/beta/security/secureScores";
        public const String UsersBeta = "/beta/users";
        public const String DirectoryRolesV1 = "/v1.0/directoryRoles";
        public const String DirectoryRoleTemplateV1 = "/v1.0/directoryRoleTemplates";
        public const String DirectoryRolesMembersUsers = "/v1.0/directoryRoles/{0}/members/microsoft.graph.user";
        public const String DirectoryRolesMembersAll = "/beta/directoryRoles/{0}/members";
        public const String DirectoryRolesMembersGroups = "/v1.0/directoryRoles/{0}/members/microsoft.graph.group";
        public const String CredentialUserRegistrationDetails = "/beta/reports/credentialUserRegistrationDetails";
        public const String GroupsBeta = "/beta/groups";
        public const String GroupMemberTransitiv = "/beta/groups/{0}/transitiveMembers";
        public const String Applications = "/v1.0/applications";
        public const String ApplicationOwners = "/v1.0/applications/{0}/owners";
        public const String ServicePrincipals = "/v1.0/servicePrincipals";
        public const String ServicePrincipalsOwners = "/v1.0/servicePrincipals/{0}/owners";
        public const String GetDomains = "/beta/domains";
        public const String ServiceConfigurationRecords = "/v1.0/domains/{0}/serviceConfigurationRecords";
        public const String Settings = "/beta/settings";
        public const String Devices = "/beta/devices";

        public MSGraphScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://graph.microsoft.com";
            this.Scope = new String[] { "https://graph.microsoft.com/.default", "offline_access" };
        }

        public Dictionary<Guid, Device> GetAllDevices()
        {
            List<Device> allDevices = GetAllOf<Device>(MSGraphScanner.Devices, "$select=id,displayname,isCompliant,isManaged,operatingSystem,enrollmentType,profileType,deviceId,deviceOwnership,onPremisesSyncEnabled&$expand=registeredOwners($select=id)");
            Dictionary<Guid, Device> Result = new Dictionary<Guid, Device>();
            foreach(Device device in allDevices)
            {
                Result.Add(device.id, device);
            }
            return Result;
        }
        public Dictionary<Guid, User> GetAllUsers()
        {
            List<User> allUsers = GetAllOf<User>(MSGraphScanner.UsersBeta, "?$Filter=UserType eq 'Member'&$select=id,userPrincipalName,displayName,userType,CreatedDateTime,AccountEnabled,signInActivity");
            Dictionary<Guid, User> Result = new Dictionary<Guid, User>();
            foreach (User user in allUsers)
            {
                 user.isMFAEnabled = this.Scanner.GraphWinScanner.HasMFA(user.id);              
                Result.Add(user.id, user);
            }
            return Result;
        }

        public Dictionary<Guid, User> GetAllGuests()
        {
            List<User> allUsers = GetAllOf<User>(MSGraphScanner.UsersBeta, "?$Filter=UserType eq 'Guest'&$select=id,userPrincipalName,displayName,userType,ExternalUserState,ExternalUserStateChangeDateTime,CreatedDateTime,CreationType,AccountEnabled,signInActivity");
            Dictionary<Guid, User> Result = new Dictionary<Guid, User>();
            foreach (User user in allUsers)
            { 
                Result.Add(user.id, user);
            }
            return Result;
        }

        public bool HasDomainSPF(string domain)
        {
            var result = Get<String>(String.Format(ServiceConfigurationRecords,domain));
            return false;
        }

        public List<EnterpriseApplicationUserSettings> GetSettings()
        {
            return GetAllOf<EnterpriseApplicationUserSettings>(Settings);
        }
        public Dictionary<Guid, DirectoryRole> GetAllDirectoryRoles(bool includeMember)
        {
            List<DirectoryRole> roles = base.GetAllOf<DirectoryRole>(MSGraphScanner.DirectoryRolesV1);
            Dictionary<Guid, DirectoryRole> Result = new Dictionary<Guid, DirectoryRole>();
            foreach (DirectoryRole role in roles)
            {
                if (includeMember)
                {
                    role.SetMember(GetAllRoleMember(role.id));
                }

                Result.Add(role.id, role);
            }
            return Result;
        }

        internal List<AzurePrincipal> GetAllRoleMember(Guid roleId)
        {
            List<AzurePrincipal> Result = new List<AzurePrincipal>();
            List<IDTypeResponse> All = GetAllOf<IDTypeResponse>(string.Format(MSGraphScanner.DirectoryRolesMembersAll, roleId.ToString()), "?$select=id");
            foreach (IDTypeResponse member in All)
            {
                if (member.odatatype == "#microsoft.graph.group")
                {
                    List<IDTypeResponse> usersInGroups = GetAllOf<IDTypeResponse>(string.Format(MSGraphScanner.GroupMemberTransitiv, member.id), "?$select=id");
                    foreach (IDTypeResponse user in usersInGroups)
                    {
                        if (user.odatatype == "#microsoft.graph.user")
                        {
                            AzurePrincipal p = new AzurePrincipal(user.id, AzurePrincipalType.User);
                            if (!Result.Contains(p))
                            {
                                Result.Add(p);
                            }    
                        }
                        else if (user.odatatype == "#microsoft.graph.servicePrincipal")
                        {
                            AzurePrincipal p = new AzurePrincipal(user.id, AzurePrincipalType.ServicePrincipal);
                            if (!Result.Contains(p))
                            {
                                Result.Add(p);
                            }
                        }
                        else
                        {
                            logger.Debug("MSGraphScanner.GetAllRoleMember: Find unknown type of member: {}", user.odatatype);
                            continue;
                        }
                    }
                }
                else if (member.odatatype == "#microsoft.graph.user")
                {
                    AzurePrincipal p = new AzurePrincipal(member.id, AzurePrincipalType.User);
                    if (!Result.Contains(p))
                    {
                        Result.Add(p);
                    }
                }else if(member.odatatype == "#microsoft.graph.servicePrincipal")
                {
                    AzurePrincipal p = new AzurePrincipal(member.id, AzurePrincipalType.ServicePrincipal);
                    if (!Result.Contains(p))
                    {
                        Result.Add(p);
                    }
                }
                else
                {
                    logger.Debug("MSGraphScanner.GetAllRoleMember: Find unknown type of member: {}", member.odatatype);
                }                
            }
            return Result;
        }

        public List<Domain> GetAzDomains()
        {
            return GetAllOf<Domain>(MSGraphScanner.GetDomains);
        }

        public Dictionary<Guid, ConditionalAccessPolicy> GetAllCondtionalAccessPolicies()
        {
            List< ConditionalAccessPolicy> policies = GetAllOf<ConditionalAccessPolicy>(MSGraphScanner.ConditionalAccessPolicies);
            if(policies == null)
            {
                logger.Warn("MSGraphScanner.GetAllCondtionalAccessPolicies: Connot find Conditional Access Policies. Do you have th correct rights?");
                return null;
            }
            Dictionary<Guid, ConditionalAccessPolicy> result = new Dictionary<Guid, ConditionalAccessPolicy>();
            foreach (ConditionalAccessPolicy policy in policies)
            {
                result.Add(Guid.Parse(policy.id), policy);
            }
            return result;
        }

        public Dictionary<Guid, Application> GetAllApplications()
        {
            Dictionary<Guid, Application> result = new Dictionary<Guid, Application>();
            List<Application> allApps = GetAllOf<Application>( MSGraphScanner.Applications, "?$select=id,displayName,appId,passwordCredentials,keyCredentials,publisherDomain,signInAudience&$expand=owners($select=id)");

            foreach (Application app in allApps)
            {
                if (app.passwordCredentials.Length > 0 || app.keyCredentials.Length > 0)
                {
                    app.credentialsCreated = true;
                }
                result.Add(app.id, app);
            }

            return result;
        }

        public Dictionary<Guid, ServicePrincipal> GetAllServicePrincipals()
        {
            Dictionary<Guid, ServicePrincipal> result = new Dictionary<Guid, ServicePrincipal>();
            List<ServicePrincipal> allAppsWithOwner = base.GetAllOf<ServicePrincipal>(MSGraphScanner.ServicePrincipals, "?$select=id,appDisplayName,appId,passwordCredentials,keyCredentials,oauth2PermissionScopes,appOwnerOrganizationId&$expand=owners($select=id)");
            foreach (ServicePrincipal app in allAppsWithOwner)
            {
                result.Add(app.id, app);
            }
            List<ServicePrincipal> allAppsWithAppRoles = base.GetAllOf<ServicePrincipal>(MSGraphScanner.ServicePrincipals, "?$select=id&$expand=appRoleAssignments");
            foreach (ServicePrincipal app in allAppsWithAppRoles)
            {
                result[app.id].appRoleAssignments = app.appRoleAssignments;
            }

            return result;
        }

        internal Dictionary<Guid, Group> GetAllGroups()
        {
            List < Group > allGroups= GetAllOf<Group>(GroupsBeta, "?$select=id,displayName,securityEnabled,Visibility");
            Dictionary<Guid, Group> Result = new Dictionary<Guid, Group>();
            foreach (Group group in allGroups)
            {
                Result.Add(group.id, group);
            }
            return Result;
        }

        internal List<AzurePrincipal> GetAllGroupMember(Guid groupId)
        {
            List<IDTypeResponse> groupMember = GetAllOf<IDTypeResponse>(string.Format(GroupMemberTransitiv, groupId.ToString()), "?$select=id");
            List<AzurePrincipal> result = new List<AzurePrincipal>();

            foreach (IDTypeResponse user in groupMember)
            {
                result.Add(new AzurePrincipal(user.id, AzurePrincipalType.User));
            }
            return result;
        }



    }
}
