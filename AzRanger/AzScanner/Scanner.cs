using AzRanger.Models;
using AzRanger.Models.Azrbac;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Models.WinGraph;
using AzRanger.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class Scanner
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        internal string Username { get; }
        internal String Password { get; }
        internal String Proxy { get; }

        internal Authenticator Authenticator;
        internal String Domain;
        internal String TenantId;


        internal AdminCenterScanner AdminCenterScanner;
        internal MSGraphScanner MsGraphScanner;
        internal ProvisionAPIScanner ProvisionAPIScanner;
        internal ExchangeOnlineScanner ExchangeOnlineScanner;
        internal MainIamScanner MainIamScanner;
        internal ComplianceCenterScanner ComplianceCenterScanner;
        internal GraphWinScanner GraphWinScanner;
        internal TeamsScanner TeamsScanner;
        internal MDMScanner MDMScanner;
        internal AzrbacScanner AzrbacScanner;


        public Scanner(String username, String password, String proxy, String tenant = null)
        {
            this.Username = username;
            this.Password = password;
            this.Proxy = proxy;
            this.Domain = username.Split("@")[1];
            if (tenant == null)
            {
                this.TenantId = Helper.GetTenantIdToDomain(this.Domain, this.Proxy);
            }
            else
            {
                this.TenantId = tenant;
            }

            if (username == null && password == null)
            { 
                this.Authenticator = new Authenticator(this.TenantId);
            }
            else
            {
                this.Authenticator = new Authenticator(this.TenantId, username, password);
            }
        }

        public Scanner(String proxy, String tenant = null)
        {
            this.Proxy = proxy;
            this.TenantId = tenant;
            this.Authenticator = new Authenticator(this.TenantId);
            if(this.TenantId == null)
            {
                this.TenantId = this.Authenticator.GetTenantId();
            }
        }

        public Tenant ScanTenant()
        {
            if(this.TenantId == null)
            {
                logger.Warn("Scanner.ScanTenant: Cannot retrieve TenantId. Aborting!");
                return null;
            }
            AdminCenterScanner = new AdminCenterScanner(this);
            MsGraphScanner = new MSGraphScanner(this);
            ProvisionAPIScanner = new ProvisionAPIScanner(this);
            ExchangeOnlineScanner = new ExchangeOnlineScanner(this);
            MainIamScanner = new MainIamScanner(this);
            ComplianceCenterScanner = new ComplianceCenterScanner(this);
            GraphWinScanner = new GraphWinScanner(this);
            TeamsScanner = new TeamsScanner(this);
            AzrbacScanner = new AzrbacScanner(this);
                
            Tenant Result = new Tenant();
            Result.TenantId = this.TenantId;

            Result.AllDirectoryRoles = MsGraphScanner.GetAllDirectoryRoles(true);
            
            String currentUserId = this.Authenticator.GetUserId();
            bool sufficientRights = false;
            if(currentUserId != null)
            {
                List<DirectoryRole> neededRoles = new List<DirectoryRole>();
                foreach(DirectoryRole role in Result.AllDirectoryRoles.Values)
                {
                    if(role.roleTemplateId == DirectoryRoleTemplateID.GlobalAdministrator | role.roleTemplateId == DirectoryRoleTemplateID.GlobalReader)
                    {
                        neededRoles.Add(role);
                    }
                }
                if(neededRoles.Count == 0)
                {
                    logger.Warn("Scanner.ScanTenant: No roles found. This should not happen");
                    return null;
                }
                foreach(DirectoryRole role in neededRoles)
                {
                    if (role.Contains(Guid.Parse(currentUserId)))
                    {
                        sufficientRights = true;
                        break;
                    }
                }
            }
            else
            {
                logger.Warn("Scanner.ScanTenant: Cannot get User Id.should not happen");
                return null;
            }

            if (!sufficientRights)
            {
                Console.WriteLine("[-] The current user has not sufficient rights, please choose another one.");
                return null;
            }
            else
            {
                Console.WriteLine("[+] Current user has sufficient rights, continue...");
            }

            Result.domains = MsGraphScanner.GetAzDomains();
            Console.WriteLine("[+] Scanning the tenant: {0}", this.TenantId);

            Result.AllUsers = MsGraphScanner.GetAllUsers();
            Console.WriteLine("[+] Found {0} users.", Result.AllUsers.Count);

            Console.WriteLine("[+] Found {0} roles.", Result.AllDirectoryRoles.Count);

            Result.AllGuests = MsGraphScanner.GetAllGuests();
            Console.WriteLine("[+] Found {0} users.", Result.AllGuests.Count);

            Result.AllApplications = MsGraphScanner.GetAllApplications();
            Console.WriteLine("[+] Found {0} applications.", Result.AllApplications.Count);

            Result.AllServicePrincipals = MsGraphScanner.GetAllServicePrincipals();
            Console.WriteLine("[+] Found {0} serviceprincipals.", Result.AllServicePrincipals.Count);

            Result.AllGroups = MsGraphScanner.GetAllGroups();
            Console.WriteLine("[+] Found {0} groups.", Result.AllGroups.Count);

            Result.RoleDefinitions = GraphWinScanner.GetRoleDefinitons();

            

            foreach (String roleTemplateId in DirectoryRoleTemplateID.RolesAllowingAddCreds)
            {
                RoleDefinition roleToCheck = null;
                foreach (RoleDefinition role in Result.RoleDefinitions)
                {
                    if (role.objectId.ToString() == roleTemplateId)
                    {
                        roleToCheck = role;
                    }
                }

                if (roleToCheck != null)
                {
                    AddUserToAppsAbleAddCreds(roleToCheck.objectId, Result.AllApplications, Result.AllServicePrincipals);
                }
                else
                {
                    logger.Warn("[-] Role definition for {0} not found...", roleTemplateId);
                }
            }

            Result.EnterpriseApplicationUserSettings = MsGraphScanner.GetSettings();

            Result.AllCAPolicies = MsGraphScanner.GetAllCondtionalAccessPolicies();
            Result.SecurityDefaults = MainIamScanner.GetSecurityDefaults();
            Result.DirectoryProperties = MainIamScanner.GetDirectoryProperties();
            Result.PasswordResetPolicies = MainIamScanner.GetPasswordResetPolicies();
            Result.PasswordPolicy = MainIamScanner.GetPasswordPolicy();
            Result.ADConnectStatus = MainIamScanner.GetADConnectStatus();
            Result.B2BPolicy = MainIamScanner.GetB2BPolicy();
            Result.LCMSettings = MainIamScanner.GetLCMSettings();
            Result.UserSettings = MainIamScanner.GetUserSettings();

            Result.TeamsSettings.TeamsClientConfiguration = TeamsScanner.GetTeamsClientConfiguration();
            Result.TeamsSettings.TenantFederationSettings = TeamsScanner.GetTenantFederationSettings();

            SharepointInformation infos = ProvisionAPIScanner.GetSharepointInformation();
            SharePointScanner sharePointScanner = new SharePointScanner(this, infos.AdminUrl);
            infos.SharepointInternalInfos = sharePointScanner.GetSharepointSettings();
            Result.SharepointInformation = infos;
            
            Result.AdminCenterSettings.SkypeTeams = AdminCenterScanner.GetSkypeTeamsSettings();
            Result.AdminCenterSettings.OfficeFormsSettings = AdminCenterScanner.GetOfficeFormsSettings();
            Result.AdminCenterSettings.OfficeStoreSettings = AdminCenterScanner.GetOfficeStoreSettings();
            Result.AdminCenterSettings.O365PasswordPolicy = AdminCenterScanner.GetO365PasswordPolicy();
            Result.AdminCenterSettings.SwaySettings = AdminCenterScanner.GetSwaySettings();
            Result.AdminCenterSettings.Calendarsharing = AdminCenterScanner.GetCalendarsharing();

            Result.ExchangeOnlineSettings.AdminAuditLogConfig = ExchangeOnlineScanner.GetAdminAuditLogConfig();
            Result.ExchangeOnlineSettings.HostedOutboundSpamFilterPolicy = ExchangeOnlineScanner.GetHostedOutboundSpamFilterPolicies();
            Result.ExchangeOnlineSettings.MalwareFilterPolicy = ExchangeOnlineScanner.GetMalwareFilterPolicies();
            Result.ExchangeOnlineSettings.TransportRules = ExchangeOnlineScanner.GetTransportRules();
            Result.ExchangeOnlineSettings.AcceptedDomains = ExchangeOnlineScanner.GetAcceptedDomains();
            Result.ExchangeOnlineSettings.DkimSigningConfigs = ExchangeOnlineScanner.GetDkimSigningConfig();
            Result.ExchangeOnlineSettings.ExchangeModernAutheSettings = AdminCenterScanner.GetExchangeModernAuthSettings();
            Result.ExchangeOnlineSettings.MalwareFilterRule = ExchangeOnlineScanner.GetMalwareFilterRules();
            Result.ExchangeOnlineSettings.Mailboxes = ExchangeOnlineScanner.GetMailboxes();
            Result.ExchangeOnlineSettings.RemoteDomains = ExchangeOnlineScanner.GetRemoteDomains();
            Result.ExchangeOnlineSettings.RoleAssignmentPolicies = ExchangeOnlineScanner.GeRoleAssignmentPolicies();
            Result.ExchangeOnlineSettings.OrganizationConfig = ExchangeOnlineScanner.GetOrganizationConfig();
            Result.ExchangeOnlineSettings.AuthenticationPolicies = ExchangeOnlineScanner.GetAuthenticationPolicies();
            Result.ExchangeOnlineSettings.EXOUsers = ExchangeOnlineScanner.GetEXOUsers();
            Result.ExchangeOnlineSettings.OwaMailboxPolicy = ExchangeOnlineScanner.GetOwaMailboxPolicy();

            Result.OfficeDLPPolicies = ComplianceCenterScanner.GetDLPPolicies();

            MDMScanner = new MDMScanner(this);
            if (MDMScanner.CheckIntunePowerShellAvailable())
            {
                Result.MDMSettings.MobileDeviceConfigurations = MDMScanner.GetMobileDeviceConfigurations();
                Result.MDMSettings.ConfigurationPolicies = MDMScanner.GetConfigurationPolicies();
                Result.MDMSettings.MobileDeviceCompliancePolicies = MDMScanner.GetMobileDeviceCompliancePolicies();
            }

            Console.WriteLine("[+] Finished collecting infos");
            return Result;  
        }

        public TenantSettings ScanSettings()
        {
            if (this.TenantId == null)
            {
                logger.Warn("Scanner.ScanTenant: Cannot retrieve TenantId. Aborting!");
                return null;
            }
            AdminCenterScanner = new AdminCenterScanner(this);
            MsGraphScanner = new MSGraphScanner(this);
            ProvisionAPIScanner = new ProvisionAPIScanner(this);
            ExchangeOnlineScanner = new ExchangeOnlineScanner(this);
            MainIamScanner = new MainIamScanner(this);
            ComplianceCenterScanner = new ComplianceCenterScanner(this);
            GraphWinScanner = new GraphWinScanner(this);
            TeamsScanner = new TeamsScanner(this);
            AzrbacScanner = new AzrbacScanner(this);

            TenantSettings Result = new TenantSettings();
            Result.TenantId = this.TenantId;

            Dictionary<Guid, DirectoryRole> AllRoles = MsGraphScanner.GetAllDirectoryRoles(true);

            String currentUserId = this.Authenticator.GetUserId();
            bool sufficientRights = false;
            if (currentUserId != null)
            {
                List<DirectoryRole> neededRoles = new List<DirectoryRole>();
                foreach (DirectoryRole role in AllRoles.Values)
                {
                    if (role.roleTemplateId == DirectoryRoleTemplateID.GlobalAdministrator | role.roleTemplateId == DirectoryRoleTemplateID.GlobalReader)
                    {
                        neededRoles.Add(role);
                    }
                }
                if (neededRoles.Count == 0)
                {
                    logger.Warn("Scanner.ScanTenant: No roles found. This should not happen");
                    return null;
                }
                foreach (DirectoryRole role in neededRoles)
                {
                    if (role.Contains(Guid.Parse(currentUserId)))
                    {
                        sufficientRights = true;
                        break;
                    }
                }
            }
            else
            {
                logger.Warn("Scanner.ScanTenant: Cannot get User Id.should not happen");
                return null;
            }

            if (!sufficientRights)
            {
                Console.WriteLine("[-] The current user has not sufficient rights, please choose another one.");
                return null;
            }
            else
            {
                Console.WriteLine("[+] Current user has sufficient rights, continue...");
            }

            Result.domains = MsGraphScanner.GetAzDomains();
            Console.WriteLine("[+] Scanning the tenant: {0}", this.TenantId);

            Result.EnterpriseApplicationUserSettings = MsGraphScanner.GetSettings();

            Result.AllCAPolicies = MsGraphScanner.GetAllCondtionalAccessPolicies();
            Result.SecurityDefaults = MainIamScanner.GetSecurityDefaults();
            Result.DirectoryProperties = MainIamScanner.GetDirectoryProperties();
            Result.PasswordResetPolicies = MainIamScanner.GetPasswordResetPolicies();
            Result.PasswordPolicy = MainIamScanner.GetPasswordPolicy();
            Result.ADConnectStatus = MainIamScanner.GetADConnectStatus();
            Result.B2BPolicy = MainIamScanner.GetB2BPolicy();
            Result.LCMSettings = MainIamScanner.GetLCMSettings();
            Result.UserSettings = MainIamScanner.GetUserSettings();

            Result.TeamsSettings.TeamsClientConfiguration = TeamsScanner.GetTeamsClientConfiguration();
            Result.TeamsSettings.TenantFederationSettings = TeamsScanner.GetTenantFederationSettings();

            SharepointInformation infos = ProvisionAPIScanner.GetSharepointInformation();
            SharePointScanner sharePointScanner = new SharePointScanner(this, infos.AdminUrl);
            infos.SharepointInternalInfos = sharePointScanner.GetSharepointSettings();
            Result.SharepointInformation = infos;

            Result.AdminCenterSettings.SkypeTeams = AdminCenterScanner.GetSkypeTeamsSettings();
            Result.AdminCenterSettings.OfficeFormsSettings = AdminCenterScanner.GetOfficeFormsSettings();
            Result.AdminCenterSettings.OfficeStoreSettings = AdminCenterScanner.GetOfficeStoreSettings();
            Result.AdminCenterSettings.O365PasswordPolicy = AdminCenterScanner.GetO365PasswordPolicy();
            Result.AdminCenterSettings.SwaySettings = AdminCenterScanner.GetSwaySettings();
            Result.AdminCenterSettings.Calendarsharing = AdminCenterScanner.GetCalendarsharing();

            Result.ExchangeOnlineSettings.AdminAuditLogConfig = ExchangeOnlineScanner.GetAdminAuditLogConfig();
            Result.ExchangeOnlineSettings.HostedOutboundSpamFilterPolicy = ExchangeOnlineScanner.GetHostedOutboundSpamFilterPolicies();
            Result.ExchangeOnlineSettings.MalwareFilterPolicy = ExchangeOnlineScanner.GetMalwareFilterPolicies();
            Result.ExchangeOnlineSettings.TransportRules = ExchangeOnlineScanner.GetTransportRules();
            Result.ExchangeOnlineSettings.AcceptedDomains = ExchangeOnlineScanner.GetAcceptedDomains();
            Result.ExchangeOnlineSettings.DkimSigningConfigs = ExchangeOnlineScanner.GetDkimSigningConfig();
            Result.ExchangeOnlineSettings.ExchangeModernAutheSettings = AdminCenterScanner.GetExchangeModernAuthSettings();
            Result.ExchangeOnlineSettings.MalwareFilterRule = ExchangeOnlineScanner.GetMalwareFilterRules();
            Result.ExchangeOnlineSettings.Mailboxes = ExchangeOnlineScanner.GetMailboxes();
            Result.ExchangeOnlineSettings.RemoteDomains = ExchangeOnlineScanner.GetRemoteDomains();
            Result.ExchangeOnlineSettings.RoleAssignmentPolicies = ExchangeOnlineScanner.GeRoleAssignmentPolicies();
            Result.ExchangeOnlineSettings.OrganizationConfig = ExchangeOnlineScanner.GetOrganizationConfig();
            Result.ExchangeOnlineSettings.AuthenticationPolicies = ExchangeOnlineScanner.GetAuthenticationPolicies();
            Result.ExchangeOnlineSettings.OwaMailboxPolicy = ExchangeOnlineScanner.GetOwaMailboxPolicy();

            Result.OfficeDLPPolicies = ComplianceCenterScanner.GetDLPPolicies();
            Console.WriteLine("[+] Finished collecting infos");
            return Result;

        }
        public void AddUserToAppsAbleAddCreds(Guid roleId, Dictionary<Guid, Application> applications, Dictionary<Guid, ServicePrincipal> servicePrincipals)
        {
            List<RoleAssignments> roleAssignments = AzrbacScanner.GetRoleAssignemtsForApp(roleId);
            if(roleAssignments == null){
                logger.Debug("Scanner.AddUserToAppsAbleAddCreds: RoleId {0} not found", roleId);
                return;
            }
            foreach (RoleAssignments assignment in roleAssignments)
            {
                AzurePrincipal currentUser = null;
                if (assignment.subject.type == "User")
                {
                    currentUser = new AzurePrincipal(assignment.subjectId, AzurePrincipalType.User);
                }
                else if (assignment.subject.type == "ServicePrincipal")
                {
                    currentUser = new AzurePrincipal(assignment.subjectId, AzurePrincipalType.ServicePrincipal);
                }
                else
                {
                    logger.Debug("Scanner.AddUserToAppsAbleAddCreds: Role {0} has no known type {1}!", assignment.subject.displayName, assignment.subject.type);
                    continue;
                }
                // Is assigned to the "global" CloudAppAdmins => Assign it to every app and servicePrincipal
                if (assignment.scopedResourceId == null)
                {

                    foreach (Application app in applications.Values)
                    {
                        app.userAbleToAddCreds.Add(currentUser);
                    }
                    foreach(ServicePrincipal principal in servicePrincipals.Values)
                    {
                        principal.userAbleToAddCreds.Add(currentUser);
                    }

                }
                else
                {
                    Guid objectId = Guid.Parse((String)assignment.scopedResourceId);
                    if (assignment.scopedResource.type == "ServicePrincipal")
                    {
                        servicePrincipals[objectId].userAbleToAddCreds.Add(currentUser);
                    }
                    else if (assignment.scopedResource.type == "Application")
                    {
                        applications[objectId].userAbleToAddCreds.Add(currentUser);

                    }
                    else
                    {
                        logger.Warn("Scanner.AddUserToAppsAbleAddCreds: Failed to find scopedResource");
                        logger.Debug(assignment.scopedResource.ToString());
                    }
                }
            }
        }
    }
}
