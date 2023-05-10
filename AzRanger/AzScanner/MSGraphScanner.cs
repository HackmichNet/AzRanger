using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using AzRanger.Models.WinGraph;

namespace AzRanger.AzScanner
{

    public class MSGraphScanner : IScanner
    {
        public const String ConditionalAccessPoliciesBeta = "/beta/identity/conditionalAccess/policies";
        public const String SecureScoreBeta = "/beta/security/secureScores";
        public const String UsersBeta = "/beta/users";
        public const String Users = "/v1.0/users";
        public const String DirectoryRoles = "/v1.0/directoryRoles";
        public const String DirectoryRoleTemplate = "/v1.0/directoryRoleTemplates";
        public const String DirectoryRolesMembersUsers = "/v1.0/directoryRoles/{0}/members/microsoft.graph.user";
        public const String DirectoryRolesMembersAll = "/beta/directoryRoles/{0}/members";
        public const String DirectoryRolesMembersGroups = "/v1.0/directoryRoles/{0}/members/microsoft.graph.group";
        public const String CredentialUserRegistrationDetailsBeta = "/beta/reports/credentialUserRegistrationDetails";
        public const String GroupsBeta = "/beta/groups";
        public const String GroupMemberTransitiv = "/beta/groups/{0}/transitiveMembers";
        public const String Applications = "/v1.0/applications";
        public const String ApplicationOwners = "/v1.0/applications/{0}/owners";
        public const String ServicePrincipals = "/v1.0/servicePrincipals";
        public const String ServicePrincipalsOwners = "/v1.0/servicePrincipals/{0}/owners";
        public const String GetDomains = "/beta/domains";
        public const String ServiceConfigurationRecords = "/v1.0/domains/{0}/serviceConfigurationRecords";
        public const String SettingsBeta = "/beta/settings";
        public const String DevicesBeta = "/beta/devices";
        public const String LicenseDetailBeta = "/beta/me/licenseDetails";
        public const String DeviceRegistrationPolicy = "/beta/policies/deviceRegistrationPolicy";
        public const String AuthorizationPolicy = "/beta/policies/authorizationPolicy/authorizationPolicy";
        public const String AuthenticationMethodsPolicy = "/beta/policies/authenticationmethodspolicy";
        public const String OAuth2PermissionGrants = "/beta/oauth2PermissionGrants";
        //https://learn.microsoft.com/en-us/graph/api/authentication-list-methods?view=graph-rest-1.0&tabs=http
        public const String AuthenticationMethods = "/{0}/authentication/methods";

        public MSGraphScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://graph.microsoft.com";
            this.Scope = new String[] { "https://graph.microsoft.com/.default", "offline_access" };
        }

        public Task<AuthorizationPolicy> GetAuthorizationPolicy()
        {
            return Get<AuthorizationPolicy>(MSGraphScanner.AuthorizationPolicy);
        }

        public Task<DeviceRegistrationPolicy> GetDeviceRegistrationPolicy()
        {
            return Get<DeviceRegistrationPolicy>(MSGraphScanner.DeviceRegistrationPolicy);
        }

        public Task<List<LicenseDetails>> GetLicenses()
        {
            return GetAllOf<LicenseDetails>(LicenseDetailBeta);
        }

        public async Task<Dictionary<Guid, Device>> GetAllDevices()
        {
            List<Device> allDevices = await GetAllOf<Device>(MSGraphScanner.DevicesBeta, "$select=id,displayname,isCompliant,isManaged,operatingSystem,enrollmentType,profileType,deviceId,deviceOwnership,onPremisesSyncEnabled&$expand=registeredOwners($select=id)");
            Dictionary<Guid, Device> Result = new Dictionary<Guid, Device>();
            foreach(Device device in allDevices)
            {
                Result.Add(device.id, device);
            }
            return Result;
        }
        public async Task<Dictionary<Guid, User>> GetAllUsers()
        {
            List<User> allUsers;
            if (this.Scanner.HasP1License)
            {
                allUsers = await GetAllOf<User>(MSGraphScanner.UsersBeta, "?$Filter=UserType eq 'Member'&$select=id,userPrincipalName,displayName,userType,CreatedDateTime,AccountEnabled,signInActivity,onPremisesSyncEnabled");
            }
            else
            {
                allUsers = await GetAllOf<User>(MSGraphScanner.UsersBeta, "?$Filter=UserType eq 'Member'&$select=id,userPrincipalName,displayName,userType,CreatedDateTime,AccountEnabled,onPremisesSyncEnabled");
            }
            if(allUsers == null)
            {
                return null;
            }
            Dictionary<Guid, User> resultingUsers = new Dictionary<Guid, User>();
            
            foreach (User user in allUsers)
            {
                resultingUsers.Add(user.id, user);
            }

            int maxConcurrentRequests = 5;
            int concurrentRequests = 0;
            List<Task<StrongAuthenticationDetail>> tasks = new List<Task<StrongAuthenticationDetail>>();

            foreach (User user in allUsers)
            {
                if (concurrentRequests < maxConcurrentRequests)
                {
                    tasks.Add(this.Scanner.GraphWinScanner.GetStrongAuthenticationDetail(user.id));
                    concurrentRequests++;
                }
                else 
                {
                    IEnumerable<StrongAuthenticationDetail> tempResults = await Task.WhenAll(tasks);
                    concurrentRequests = 0;
                    foreach (StrongAuthenticationDetail resultObject in tempResults)
                    {
                        resultingUsers[resultObject.objectId].strongAuthenticationDetail = resultObject.strongAuthenticationDetail;

                        if (resultObject.strongAuthenticationDetail.methods != null && resultObject.strongAuthenticationDetail.methods.Length > 0)
                        {
                            resultingUsers[resultObject.objectId].isMFAEnabled = true;
                        }
                        else
                        {
                            resultingUsers[resultObject.objectId].isMFAEnabled = false;
                        }
                    }
                    tasks = new List<Task<StrongAuthenticationDetail>>();
                    await Task.Delay(100);
                }

            }            
            return resultingUsers;
        }
        public async Task<Dictionary<Guid, User>> GetAllGuests()
        {
            List<User> allUsers;
            if (this.Scanner.HasP1License)
            {
                allUsers = await GetAllOf<User>(MSGraphScanner.UsersBeta, "?$Filter=UserType eq 'Guest'&$select=id,userPrincipalName,displayName,userType,ExternalUserState,ExternalUserStateChangeDateTime,CreatedDateTime,CreationType,AccountEnabled,signInActivity");
            }
            else
            {
                allUsers = await GetAllOf<User>(MSGraphScanner.UsersBeta, "?$Filter=UserType eq 'Guest'&$select=id,userPrincipalName,displayName,userType,ExternalUserState,ExternalUserStateChangeDateTime,CreatedDateTime,CreationType,AccountEnabled");
            }
            if(allUsers == null)
            {
                return null;
            }
            Dictionary<Guid, User> Result = new Dictionary<Guid, User>();
            foreach (User user in allUsers)
            { 
                Result.Add(user.id, user);
            }
            return Result;
        }

        public Task<List<EnterpriseApplicationUserSettings>> GetSettings()
        {
            return GetAllOf<EnterpriseApplicationUserSettings>(SettingsBeta);
        }
        public async Task<Dictionary<Guid, DirectoryRole>> GetAllDirectoryRoles()
        {
            List<DirectoryRole> roles = await base.GetAllOf<DirectoryRole>(MSGraphScanner.DirectoryRoles);
            Dictionary<Guid, DirectoryRole> Result = new Dictionary<Guid, DirectoryRole>();
            foreach (DirectoryRole role in roles)
            {
                role.SetMember( await GetAllRoleMember(role.id));
                Result.Add(role.id, role);
            }
            return Result;
        }

        public async Task<List<AzurePrincipal>> GetAllMemberOf(Guid groupId)
        {
            List<AzurePrincipal> Result = new List<AzurePrincipal>();
            List<IDTypeResponse> All = await GetAllOf<IDTypeResponse>(string.Format(MSGraphScanner.GroupMemberTransitiv, groupId.ToString()), "?$select=id");
            foreach (IDTypeResponse member in All)
            {
                if (member.odatatype == "#microsoft.graph.user")
                {
                    AzurePrincipal p = new AzurePrincipal(member.id, AzurePrincipalType.User);
                    if (!Result.Contains(p))
                    {
                        Result.Add(p);
                    }
                }
                else if (member.odatatype == "#microsoft.graph.servicePrincipal")
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

        internal async Task<List<AzurePrincipal>> GetAllRoleMember(Guid roleId)
        {
            List<AzurePrincipal> Result = new List<AzurePrincipal>();
            List<IDTypeResponse> All = await GetAllOf<IDTypeResponse>(string.Format(MSGraphScanner.DirectoryRolesMembersAll, roleId.ToString()), "?$select=id");
            foreach (IDTypeResponse member in All)
            {
                if (member.odatatype == "#microsoft.graph.group")
                {
                    List<IDTypeResponse> usersInGroups = await GetAllOf<IDTypeResponse>(string.Format(MSGraphScanner.GroupMemberTransitiv, member.id), "?$select=id");
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

        public Task<List<Domain>> GetAzDomains()
        {
            return GetAllOf<Domain>(MSGraphScanner.GetDomains);
        }

        public async Task<Dictionary<Guid, ConditionalAccessPolicy>> GetAllCondtionalAccessPolicies()
        {
            List< ConditionalAccessPolicy> policies = await GetAllOf<ConditionalAccessPolicy>(MSGraphScanner.ConditionalAccessPoliciesBeta);
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

        public async Task<Dictionary<Guid, Application>> GetAllApplications()
        {
            Dictionary<Guid, Application> result = new Dictionary<Guid, Application>();
            List<Application> allApps = await GetAllOf<Application>( MSGraphScanner.Applications, "?$select=id,displayName,appId,passwordCredentials,keyCredentials,publisherDomain,signInAudience&$expand=owners($select=id)");

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

        public async Task<Dictionary<Guid, ServicePrincipal>> GetAllServicePrincipals()
        {
            Dictionary<Guid, ServicePrincipal> result = new Dictionary<Guid, ServicePrincipal>();
            List<ServicePrincipal> allAppsWithOwner = await base.GetAllOf<ServicePrincipal>(MSGraphScanner.ServicePrincipals, "?$select=id,appDisplayName,appId,passwordCredentials,keyCredentials,oauth2PermissionScopes,appOwnerOrganizationId&$expand=owners($select=id)");
            foreach (ServicePrincipal app in allAppsWithOwner)
            {
                result.Add(app.id, app);
            }
            List<ServicePrincipal> allAppsWithAppRoles = await base.GetAllOf<ServicePrincipal>(MSGraphScanner.ServicePrincipals, "?$select=id&$expand=appRoleAssignments");
            foreach (ServicePrincipal app in allAppsWithAppRoles)
            {
                result[app.id].appRoleAssignments = app.appRoleAssignments;
            }

            List<Oauth2PermissionGrant> grants = await GetAllOf<Oauth2PermissionGrant>(OAuth2PermissionGrants);
            foreach(Oauth2PermissionGrant grant in grants)
            {
                // I know not the best implementation....maybe think about later.
                foreach(ServicePrincipal principal in result.Values)
                {
                    if(principal.id == grant.clientId)
                    {
                        if(principal.oauth2PermissionGrants == null)
                        {
                            principal.oauth2PermissionGrants = new List<Oauth2PermissionGrant>();
                        }
                        principal.oauth2PermissionGrants.Add(grant);
                        break;
                    }
                }
            }

            return result;
        }

        internal async Task<Dictionary<Guid, Group>> GetAllGroups()
        {
            List <Group> allGroups = await GetAllOf<Group>(GroupsBeta, "?$select=id,displayName,securityEnabled,Visibility");
            Dictionary<Guid, Group> Result = new Dictionary<Guid, Group>();
            foreach (Group group in allGroups)
            {
                Result.Add(group.id, group);
            }
            return Result;
        }

        // Transitiv, gives only the users or principals back, does not include other groups
        internal async Task<List<AzurePrincipal>> GetAllGroupMemberTransitiv(Guid groupId)
        {
            List<IDTypeResponse> groupMember = await GetAllOf<IDTypeResponse>(string.Format(GroupMemberTransitiv, groupId.ToString()), "?$select=id");
            List<AzurePrincipal> result = new List<AzurePrincipal>();

            foreach (IDTypeResponse user in groupMember)
            {
                result.Add(new AzurePrincipal(user.id, AzurePrincipalType.User));
            }
            return result;
        }

        internal Task<AuthenticationMethodsPolicy> GetAuthenticationMethodsPolicy()
        {
            return Get<AuthenticationMethodsPolicy>(AuthenticationMethodsPolicy);

        }


    }
}
