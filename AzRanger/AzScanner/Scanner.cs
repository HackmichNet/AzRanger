using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Models.AdminCenter;
using AzRanger.Models.AzMgmt;
using AzRanger.Models.Azrbac;
using AzRanger.Models.ComplianceCenter;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.Generic;
using AzRanger.Models.MainIAM;
using AzRanger.Models.MSGraph;
using AzRanger.Models.Provision;
using AzRanger.Models.Teams;
using AzRanger.Utilities;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class Scanner
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        internal string Username { get; }
        internal String Proxy { get; }

        internal IAuthenticator Authenticator;
        internal String Domain;
        internal String TenantId;
        internal bool HasP1License = false;
        internal bool HasP2License = false;


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
        internal AzMgmtScanner AzMgmtScanner;

        public Scanner(IAuthenticator authenticator, String proxy, String tenant = null)
        {
            this.Proxy = proxy;
            this.TenantId = tenant;
            this.Authenticator = authenticator;
            if(this.TenantId == null)
            {
                this.TenantId = this.Authenticator.GetTenantId().Result;
            }
            this.Username = this.Authenticator.GetUsername().Result;
        }

        public async Task<Tenant> ScanTenant(List<ScopeEnum> scopes)
        {
            if(this.TenantId == null)
            {
                logger.Warn("Scanner.ScanTenant: Cannot retrieve TenantId. Aborting!");
                return null;
            }

            Tenant Result = new Tenant();
            Result.TenantId = this.TenantId;   
            Result.Username = this.Username;
            String currentUserId = await this.Authenticator.GetUserId();
            bool isGlobalAdmin = false;
            bool isGlobalReader = false;
            bool isSharePointAdmin = false;
            bool scanAzureOnly = false;

            AdminCenterScanner = new AdminCenterScanner(this);
            MsGraphScanner = new MSGraphScanner(this);
            ProvisionAPIScanner = new ProvisionAPIScanner(this);
            ExchangeOnlineScanner = new ExchangeOnlineScanner(this);
            MainIamScanner = new MainIamScanner(this);
            ComplianceCenterScanner = new ComplianceCenterScanner(this);
            GraphWinScanner = new GraphWinScanner(this);
            TeamsScanner = new TeamsScanner(this);
            AzrbacScanner = new AzrbacScanner(this);
            AzMgmtScanner = new AzMgmtScanner(this);
            
            if (scopes.Count == 1 & scopes.Contains(ScopeEnum.Azure))
            {
                scanAzureOnly = true;
            }

            if (!scanAzureOnly)
            {
                Result.TenantSettings = new M365Settings();
                Result.AllDirectoryRoles = await MsGraphScanner.GetAllDirectoryRoles();
                if (Result.AllDirectoryRoles != null && currentUserId != null)
                {

                    foreach (DirectoryRole role in Result.AllDirectoryRoles.Values)
                    {
                        if (role.roleTemplateId == DirectoryRoleTemplateID.GlobalAdministrator)
                        {
                            if (role.PricipalIsInActiveMembers(Guid.Parse(currentUserId)))
                            {
                                isGlobalAdmin = true;
                            }
                        }

                        if (role.roleTemplateId == DirectoryRoleTemplateID.GlobalReader)
                        {
                            if (role.PricipalIsInActiveMembers(Guid.Parse(currentUserId)))
                            {
                                isGlobalReader = true;
                            }
                        }

                        if (role.roleTemplateId == DirectoryRoleTemplateID.SharePointAdmin)
                        {
                            if (role.PricipalIsInActiveMembers(Guid.Parse(currentUserId)))
                            {
                                isSharePointAdmin = true;
                            }
                        }
                    }
                }
                else
                {
                    logger.Warn("Scanner.ScanTenant: Cannot get User Id. Should not happen!");
                    return null;
                } 
                

                if (!isGlobalAdmin & !isGlobalReader)
                {
                    Console.WriteLine("[-] The current user has not sufficient rights, please choose another one.");
                    return null;
                }
                else
                {
                    Console.WriteLine("[+] Current user has sufficient rights, continue...");
                }

                if (!isGlobalAdmin)
                {
                    if (!isSharePointAdmin)
                    {
                        Console.WriteLine("[-] The current user is no SharePointAdmin, so it cannot read data from SharePoint.");
                    }
                }
            }
            
            if (scopes.Contains(ScopeEnum.AAD))
            {
                
                Result.TenantSettings.TenantSkuInfo = await MainIamScanner.GetTenantSkuInfo();
                if (Result.TenantSettings.TenantSkuInfo != null)
                {
                    if (Result.TenantSettings.TenantSkuInfo.aadPremium)
                    {
                        Console.WriteLine("[+] Tenant has a P1 license.");
                        this.HasP1License = true;
                    }
                    else
                    {
                        Console.WriteLine("[-] Tenant has no P1 license. Not all data can be gathered.");
                    }

                    if (Result.TenantSettings.TenantSkuInfo.aadPremiumP2)
                    {
                        Console.WriteLine("[+] Tenant has a P2 license.");
                        this.HasP2License = true;
                    }
                    else
                    {
                        Console.WriteLine("[-] Tenant has no P2 license. Not all data can be gathered.");
                    }
                }
                else
                {
                    logger.Warn("Scanner.ScanTenant: Cannot get Tenant License. This should not happen.");
                    return null;
                }

                Task<List<Domain>> getDomainTask = MsGraphScanner.GetAzDomains();
                Task<Dictionary<Guid, User>> getUserTask = MsGraphScanner.GetAllUsers();
                Task<Dictionary<Guid, User>> getGuestTask = MsGraphScanner.GetAllGuests();
                Task<Dictionary<Guid, Application>> getAllApplications = MsGraphScanner.GetAllApplications();
                Task<Dictionary<Guid, ServicePrincipal>> getAllServicePrincipals = MsGraphScanner.GetAllServicePrincipals();
                Task<Dictionary<Guid, Group>> getGroupTask = MsGraphScanner.GetAllGroups();

                List<Task> tasks = new List<Task> { getDomainTask, getUserTask, getGuestTask, getAllApplications, getAllServicePrincipals, getGroupTask };

                while (tasks.Any())
                {
                    var result = await Task.WhenAny(tasks);

                    if (result == getDomainTask)
                    {
                        Result.domains = await getDomainTask;
                    }
                    if (result == getUserTask)
                    {
                        Result.AllUsers = await getUserTask;
                    }
                    if (result == getGuestTask)
                    {
                        Result.AllGuests = await getGuestTask;
                    }
                    if (result == getAllApplications)
                    {
                        Result.AllApplications = await getAllApplications;
                    }
                    if (result == getAllServicePrincipals)
                    {
                        Result.AllServicePrincipals = await getAllServicePrincipals;
                    }
                    if (result == getGroupTask)
                    {
                        Result.AllGroups = await getGroupTask;
                    }

                    tasks.Remove(result);
                }
                

                if (Result.AllDirectoryRoles != null)
                {
                    if (HasP2License)
                    {
                        foreach (DirectoryRole role in Result.AllDirectoryRoles.Values)
                        {
                            List<PIMRoleAssignments> roleAssignments = await AzrbacScanner.GetRoleAssignemts(Guid.Parse(this.TenantId), Guid.Parse(role.roleTemplateId));
                            foreach (PIMRoleAssignments assignment in roleAssignments)
                            {
                                List<AzurePrincipal> principalsToAssigne = new List<AzurePrincipal>();
                                AzurePrincipalType aztype;
                                switch (assignment.subject.type)
                                {
                                    case "User":
                                        aztype = AzurePrincipalType.User;
                                        break;
                                    case "ServicePrincipal":
                                        aztype = AzurePrincipalType.ServicePrincipal;
                                        break;
                                    case "Application":
                                        aztype = AzurePrincipalType.Application;
                                        break;
                                    case "Group":
                                        aztype = AzurePrincipalType.Group;
                                        break;
                                    default:
                                        continue;
                                }
                                if (aztype == AzurePrincipalType.Group)
                                {
                                    List<AzurePrincipal> members = await MsGraphScanner.GetAllGroupMemberTransitiv(assignment.subjectId);
                                    foreach (AzurePrincipal member in members)
                                    {
                                        principalsToAssigne.Add(member);
                                    }
                                }
                                else
                                {
                                    principalsToAssigne.Add(new AzurePrincipal(assignment.subjectId, aztype));
                                }
                                // Global recourse
                                if (assignment.scopedResourceId == null)
                                {
                                    // Assign user to role
                                    if (assignment.assignmentState.Equals("Active"))
                                    {
                                        foreach (AzurePrincipal p in principalsToAssigne)
                                        {
                                            role.AddActiveMember(p);
                                        }
                                    }
                                    else
                                    {
                                        foreach (AzurePrincipal p in principalsToAssigne)
                                        {
                                            role.AddEligibleMember(p);
                                        }
                                    }
                                    // If a user can add creds, assign to applications and service principal
                                    if (DirectoryRoleTemplateID.RolesAllowingAddCreds.Contains(role.roleTemplateId))
                                    {
                                        foreach (ServicePrincipal s in Result.AllServicePrincipals.Values)
                                        {
                                            if (s.appOwnerOrganizationId == this.TenantId)
                                            {
                                                s.AddUserAbleToAddCreds(new AzurePrincipal(assignment.subjectId, aztype));
                                            }
                                        }
                                        foreach (Application a in Result.AllApplications.Values)
                                        {
                                            a.AddUserAbleToAddCreds(new AzurePrincipal(assignment.subjectId, aztype));
                                        }
                                    }
                                }
                                else
                                {
                                    if (assignment.scopedResource.type == "Application")
                                    {
                                        foreach (AzurePrincipal p in principalsToAssigne)
                                        {
                                            Result.AllApplications[Guid.Parse(assignment.scopedResource.id)].AddUserAbleToAddCreds(p);
                                        }
                                    }
                                    if (assignment.scopedResource.type == "ServicePrincipal")
                                    {
                                        foreach (AzurePrincipal p in principalsToAssigne)
                                        {
                                            Result.AllServicePrincipals[Guid.Parse(assignment.scopedResource.id)].AddUserAbleToAddCreds(p);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DirectoryRole role in Result.AllDirectoryRoles.Values)
                        {
                            if (DirectoryRoleTemplateID.RolesAllowingAddCreds.Contains(role.roleTemplateId))
                            {
                                List<AzurePrincipal> cloudAdmins = new List<AzurePrincipal>();
                                foreach (AzurePrincipal user in role.GetMembers())
                                {
                                    AzurePrincipal u = new AzurePrincipal(user.id, user.PrincipalType);
                                    cloudAdmins.Add(u);
                                }
                                foreach (Application app in Result.AllApplications.Values)
                                {
                                    foreach (AzurePrincipal a in cloudAdmins)
                                    {
                                        app.AddUserAbleToAddCreds(a);
                                    }
                                }
                                foreach (ServicePrincipal principal in Result.AllServicePrincipals.Values)
                                {
                                    if (principal.appOwnerOrganizationId == this.TenantId)
                                    {
                                        foreach (AzurePrincipal a in cloudAdmins)
                                        {
                                            principal.AddUserAbleToAddCreds(a);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                Result.TenantSettings.AdminCenterSettings = new AdminCenterSettings();

                List<Task> officeTasks = new List<Task>();
                
                officeTasks.Add(MsGraphScanner.GetSettings());
                officeTasks.Add(MsGraphScanner.GetAuthorizationPolicy());
                officeTasks.Add(MsGraphScanner.GetDeviceRegistrationPolicy());
                officeTasks.Add(MsGraphScanner.GetAllCondtionalAccessPolicies());
                officeTasks.Add(MsGraphScanner.GetAuthenticationMethodsPolicy());
               
                officeTasks.Add(MainIamScanner.GetSecurityDefaults());
                officeTasks.Add(MainIamScanner.GetDirectoryProperties());
                officeTasks.Add(MainIamScanner.GetPasswordResetPolicies());
                officeTasks.Add(MainIamScanner.GetPasswordPolicy());
                officeTasks.Add(MainIamScanner.GetADConnectStatus());
                officeTasks.Add(MainIamScanner.GetB2BPolicy());
                officeTasks.Add(MainIamScanner.GetLCMSettings());
                officeTasks.Add(MainIamScanner.GetUserSettings());
                officeTasks.Add(MainIamScanner.GetSsgmProperties());
                officeTasks.Add(MainIamScanner.GetLoginTenantBrandings());
             

                
                officeTasks.Add(ProvisionAPIScanner.GetDirSyncFeatures());
                officeTasks.Add(ProvisionAPIScanner.GetMsolCompanyInformation());
                
                officeTasks.Add(AdminCenterScanner.GetSkypeTeamsSettings());
                officeTasks.Add(AdminCenterScanner.GetOfficeFormsSettings());
                officeTasks.Add(AdminCenterScanner.GetOfficeStoreSettings());
                officeTasks.Add(AdminCenterScanner.GetO365PasswordPolicy());
                officeTasks.Add(AdminCenterScanner.GetSwaySettings());
                officeTasks.Add(AdminCenterScanner.GetCalendarsharing());
                officeTasks.Add(AdminCenterScanner.GetDirsyncManagement());
                officeTasks.Add(AdminCenterScanner.GetOfficeonline());
               
                officeTasks.Add(ComplianceCenterScanner.GetDLPPolicies());
                officeTasks.Add(ComplianceCenterScanner.GetDLPPolicies());
                officeTasks.Add(ComplianceCenterScanner.GetDLPLabels());
               
                

                while (officeTasks.Any())
                {

                    Task result = null;
                    try {
                        result = await Task.WhenAny(officeTasks);
                    }
                    catch (Exception ex)
                    {
                        logger.Warn("[-] An error occurred. Don't panic...");
                        logger.Debug("Scanner.ScanTenant: OfficeTasks failed.");
                        logger.Debug(ex.Message);
                        officeTasks.Remove(result);
                        continue;
                    }
                    switch (result)
                    {
                        case Task<List<EnterpriseApplicationUserSettings>> getEnterpriseApplicationUserSettingsTask:
                            Result.EnterpriseApplicationUserSettings = await getEnterpriseApplicationUserSettingsTask;
                            break;
                        case Task<AuthorizationPolicy> getAuthorizationPolicyTask:
                            Result.TenantSettings.AuthorizationPolicy = await getAuthorizationPolicyTask;
                            break;
                        case Task<DeviceRegistrationPolicy> getDeviceRegistrationPolicyTask:
                            Result.TenantSettings.DeviceRegistrationPolicy = await getDeviceRegistrationPolicyTask;
                            break;
                        case Task<Dictionary<Guid, ConditionalAccessPolicy>> getConditionalAccessPolicyTask:
                            Result.AllCAPolicies = await getConditionalAccessPolicyTask;
                            break;
                        case Task<SecurityDefaults> getSecurityDefaultsTask:
                            Result.TenantSettings.SecurityDefaults = await getSecurityDefaultsTask;
                            break;
                        case Task<DirectoryProperties> getDirectoryPropertiesTask:
                            Result.TenantSettings.DirectoryProperties = await getDirectoryPropertiesTask;
                            break;
                        case Task<PasswordResetPolicies> getPasswordResetPolicies:
                            Result.TenantSettings.PasswordResetPolicies = await getPasswordResetPolicies;
                            break;
                        case Task<AzureADPasswordPolicy> getAzureADPasswordPolicy:
                            Result.TenantSettings.PasswordPolicy = await getAzureADPasswordPolicy;
                            break;
                        case Task<ADConnectStatus> getADConnectStatusTask:
                            Result.TenantSettings.ADConnectStatus = await getADConnectStatusTask;
                            break;
                        case Task<B2BPolicy> getB2BPolicyTask:
                            Result.TenantSettings.B2BPolicy = await getB2BPolicyTask;
                            break;
                        case Task<LCMSettings> getLCMSettingsTask:
                            Result.TenantSettings.LCMSettings = await getLCMSettingsTask;
                            break;
                        case Task<UserSettings> getUserSettingsTask:
                            Result.TenantSettings.UserSettings = await getUserSettingsTask;
                            break;
                        case Task<SsgmProperties> getSsgmPropertiesTask:
                            Result.TenantSettings.SsgmProperties = await getSsgmPropertiesTask;
                            break;
                        case Task<List<LoginTenantBranding>> getLoginTenantBrandingTask:
                            Result.TenantSettings.LoginTenantBrandings = await getLoginTenantBrandingTask;
                            break;
                        case Task<DirSyncFeatures> getDirSyncFeaturesTask:
                            Result.TenantSettings.DirSyncFeatures = await getDirSyncFeaturesTask;
                            break;
                        case Task<SkypeTeams> getSkypeTeamsTask:
                            Result.TenantSettings.AdminCenterSettings.SkypeTeams = await getSkypeTeamsTask;
                            break;
                        case Task<OfficeFormsSettings> getOfficeFormsSettingsTask:
                            Result.TenantSettings.AdminCenterSettings.OfficeFormsSettings = await getOfficeFormsSettingsTask;
                            break;
                        case Task<OfficeStoreSettings> getOfficeStoreSettingsTask:
                            Result.TenantSettings.AdminCenterSettings.OfficeStoreSettings = await getOfficeStoreSettingsTask;
                            break;
                        case Task<O365PasswordPolicy> getO365PasswordPolicyTask:
                            Result.TenantSettings.AdminCenterSettings.O365PasswordPolicy = await getO365PasswordPolicyTask;
                            break;
                        case Task<SwaySettings> getSwaySettingsTask:
                            Result.TenantSettings.AdminCenterSettings.SwaySettings = await getSwaySettingsTask;
                            break;
                        case Task<Calendarsharing> getCalendarsharingTask:
                            Result.TenantSettings.AdminCenterSettings.Calendarsharing = await getCalendarsharingTask;
                            break;
                        case Task<DirsyncManagement> getDirsyncManagementTask:
                            Result.TenantSettings.AdminCenterSettings.DirsyncManagement = await getDirsyncManagementTask;
                            break;
                        case Task<List<DlpCompliancePolicy>> getDlpCompliancePolicyTask:
                            Result.TenantSettings.OfficeDLPPolicies = await getDlpCompliancePolicyTask;
                            break;
                        case Task<List<DlpLabel>> getDlpLabelTask:
                            Result.TenantSettings.DlpLabels = await getDlpLabelTask;
                            break;
                        case Task<AuthenticationMethodsPolicy> getAuthenticationMethodsPolicyTask:
                            Result.TenantSettings.AuthenticationMethodsPolicy = await getAuthenticationMethodsPolicyTask;
                            break;
                        case Task<MsolCompanyInformation> getMSOLCompanyInformationTask:
                            Result.TenantSettings.MSOLCompanyInformation = await getMSOLCompanyInformationTask;
                            break;
                        case Task<Officeonline> getOfficeonline:
                            Result.TenantSettings.Officeonline = await getOfficeonline;
                            break;
                        default:
                            Console.WriteLine("Scanner.ScanTennant: OfficeTask Default. This should not happen");
                            break;
                    }
                    officeTasks.Remove(result);
                }
            }
            if (scopes.Contains(ScopeEnum.Teams))
            {
                TeamsSettings settings = new TeamsSettings();
                 
                Task<TeamsClientConfiguration> getTeamsClientConfigurationTask = TeamsScanner.GetTeamsClientConfiguration();
                Task<TenantFederationSettings> getTenantFederationSettingsTask = TeamsScanner.GetTenantFederationSettings();

                List<Task> teamsTasks = new List<Task> { getTeamsClientConfigurationTask, getTenantFederationSettingsTask };

                while (teamsTasks.Any())
                {
                    Task result = null;
                    try
                    {
                        result = await Task.WhenAny(teamsTasks);
                    }
                    catch (Exception ex)
                    {
                        logger.Warn("[-] An error occurred. Don't panic...");
                        logger.Debug("Scanner.ScanTenant: TeamsTasks failed.");
                        logger.Debug(ex.Message);
                        continue;
                    }

                    if (result == getTeamsClientConfigurationTask)
                    {
                        settings.TeamsClientConfiguration = await getTeamsClientConfigurationTask;
                    }
                    if (result == getTenantFederationSettingsTask)
                    {
                        settings.TenantFederationSettings = await getTenantFederationSettingsTask;
                    }
                    teamsTasks.Remove(result);
                }
                Result.TeamsSettings = settings;
                
            }

            if (scopes.Contains(ScopeEnum.SPO))
            {
                
                if (Result.TenantSettings.SecurityDefaults == null)
                {
                    Result.TenantSettings.SecurityDefaults = await MainIamScanner.GetSecurityDefaults();
                }
                if (isGlobalAdmin | isSharePointAdmin)
                {
                    Result.SharepointInformation = await ProvisionAPIScanner.GetSharepointInformation();
                    if (Result.SharepointInformation != null)
                    {
                        Console.WriteLine("[+] Found SharePoint on: {0}", Result.SharepointInformation.SharepointUrl);
                        Console.WriteLine("[+] Found SharePoint-Admin on: {0}", Result.SharepointInformation.AdminUrl);
                        SharePointScanner sharePointScanner = new SharePointScanner(this, Result.SharepointInformation.AdminUrl);
                        Result.SharepointInformation.SharepointInternalInfos = await sharePointScanner.GetSharePointSettings();
                    }
                }
                
            }

            if (scopes.Contains(ScopeEnum.EXO))
            {
                if (Result.TenantSettings.SecurityDefaults == null)
                {
                    Result.TenantSettings.SecurityDefaults = await MainIamScanner.GetSecurityDefaults();
                }
                if (Result.domains == null)
                {
                    Result.domains = await MsGraphScanner.GetAzDomains();
                }

                Result.ExchangeOnlineSettings = new ExchangeOnlineSettings();

                
                List<Task> exchangeTask = new List<Task>();
                exchangeTask.Add(ExchangeOnlineScanner.GetAdminAuditLogConfig());
                exchangeTask.Add(ExchangeOnlineScanner.GetHostedOutboundSpamFilterPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetMalwareFilterPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetTransportRules());
                exchangeTask.Add(ExchangeOnlineScanner.GetAcceptedDomains());
                exchangeTask.Add(ExchangeOnlineScanner.GetDkimSigningConfig());
                exchangeTask.Add(AdminCenterScanner.GetExchangeModernAuthSettings());
                exchangeTask.Add(ExchangeOnlineScanner.GetMalwareFilterRules());
                exchangeTask.Add(ExchangeOnlineScanner.GeRoleAssignmentPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetRemoteDomains());
                exchangeTask.Add(ExchangeOnlineScanner.GetMailboxes());
                exchangeTask.Add(ExchangeOnlineScanner.GetOrganizationConfig());
                exchangeTask.Add(ExchangeOnlineScanner.GetAuthenticationPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetEXOUsers());
                exchangeTask.Add(ExchangeOnlineScanner.GetOwaMailboxPolicy());

                while (exchangeTask.Any())
                {
                    Task result = null;
                    try
                    {
                        result = await Task.WhenAny(exchangeTask);
                    }
                    catch (Exception ex)
                    {
                        logger.Warn("[-] An error occurred. Don't panic...");
                        logger.Debug("Scanner.ScanTenant: ExchangeTasks failed.");
                        logger.Debug(ex.Message);
                        continue;
                    }

                    switch (result)
                    {
                        case Task<AdminAuditLogConfig> getAdminAuditLogConfigTask:
                            Result.ExchangeOnlineSettings.AdminAuditLogConfig = await getAdminAuditLogConfigTask;
                            break;
                        case Task<List<HostedOutboundSpamFilterPolicy>> getHostedOutboundSpamFilterPolicyTask:
                            Result.ExchangeOnlineSettings.HostedOutboundSpamFilterPolicy = await getHostedOutboundSpamFilterPolicyTask;
                            break;
                        case Task<List<MalwareFilterPolicy>> getMalwareFilterPolicyTask:
                            Result.ExchangeOnlineSettings.MalwareFilterPolicy = await getMalwareFilterPolicyTask;
                            break;
                        case Task<List<TransportRule>> getTransportRuleTask:
                            Result.ExchangeOnlineSettings.TransportRules = await getTransportRuleTask;
                            break;
                        case Task<List<AcceptedDomain>> getAcceptedDomainTask:
                            Result.ExchangeOnlineSettings.AcceptedDomains = await getAcceptedDomainTask;
                            break;
                        case Task<List<DkimSigningConfig>> getDkimSigningConfigTask:
                            Result.ExchangeOnlineSettings.DkimSigningConfigs = await getDkimSigningConfigTask;
                            break;
                        case Task<ExchangeModernAuthSettings> getExchangeModernAuthSettingsTask:
                            Result.ExchangeOnlineSettings.ExchangeModernAutheSettings = await getExchangeModernAuthSettingsTask;
                            break;
                        case Task<List<MalwareFilterRule>> getMalwareFilterRuleTask:
                            Result.ExchangeOnlineSettings.MalwareFilterRule = await getMalwareFilterRuleTask;
                            break;
                        case Task<List<Mailbox>> getMailboxTask:
                            Result.ExchangeOnlineSettings.Mailboxes = await getMailboxTask;
                            break;
                        case Task<List<RemoteDomain>> getRemoteDomainTask:
                            Result.ExchangeOnlineSettings.RemoteDomains = await getRemoteDomainTask;
                            break;
                        case Task<List<RoleAssignmentPolicy>> getRoleAssignmentPolicyTask:
                            Result.ExchangeOnlineSettings.RoleAssignmentPolicies = await getRoleAssignmentPolicyTask;
                            break;
                        case Task<OrganizationConfig> getOrganizationConfigTask:
                            Result.ExchangeOnlineSettings.OrganizationConfig = await getOrganizationConfigTask;
                            break;
                        case Task<List<AuthenticationPolicy>> getAuthenticationPolicyTask:
                            Result.ExchangeOnlineSettings.AuthenticationPolicies = await getAuthenticationPolicyTask;
                            break;
                        case Task<List<EXOUser>> getEXOUserTask:
                            Result.ExchangeOnlineSettings.EXOUsers = await getEXOUserTask;
                            break;
                        case Task<OwaMailboxPolicy> getOwaMailboxPolicyTask:
                            Result.ExchangeOnlineSettings.OwaMailboxPolicy = await getOwaMailboxPolicyTask;
                            break;
                        default:
                            Console.WriteLine("Scanner.ScanTennant: Hit default in exchangeTasks.");
                            break;
                    }

                    exchangeTask.Remove(result);
                }
                
                
                if (Result.ExchangeOnlineSettings.EXOUsers != null)
                {
                    Console.WriteLine("[+] Found {0} Exchange-User.", Result.ExchangeOnlineSettings.EXOUsers.Count);
                }
                Console.WriteLine("[+] Start scanning Azure Resources.");
            }

            if (scopes.Contains(ScopeEnum.Azure))
            {
               
                Result.ManagementGroups = await AzMgmtScanner.GetAllManagementGroups();
                Result.ManagementGroupSettings = await AzMgmtScanner.GetManagementGroupSettings();
                Result.Subscriptions = await AzMgmtScanner.GetAllSubscriptions();
                Result.SubscriptionPolicy = await AzMgmtScanner.GetSubscriptionPolicy();
                foreach (Subscription sub in Result.Subscriptions.Values)
                {
                    sub.Resources.StorageAccounts = await AzMgmtScanner.GetStorageAccounts(sub.subscriptionId);
                    sub.Resources.KeyVaults = await AzMgmtScanner.GetKeyVaults(sub.subscriptionId);
                    if (sub.Resources.KeyVaults != null)
                    {
                        foreach (KeyVault vault in sub.Resources.KeyVaults)
                        {
                            KeyVaultScanner vaultScanner = new KeyVaultScanner(this, vault.properties.vaultUri);
                            if (vaultScanner != null)
                            {
                                vault.Keys = await vaultScanner.GetKeyVaultKeys();
                                vault.Secrets = await vaultScanner.GetKeyVaultSecrets();
                            }
                        }
                    }
                    sub.Resources.ActivityLogAlerts = await AzMgmtScanner.GetActivityLogAlerts(sub.subscriptionId);
                    sub.Resources.NetworkSecurityGroups = await AzMgmtScanner.GetNetworkSecurityGroups(sub.subscriptionId);
                    sub.Resources.SQLServers = await AzMgmtScanner.GetSQLServers(sub.subscriptionId);
                    sub.AutoProvisioningSettings = await AzMgmtScanner.GetProvisioningSettings(sub.subscriptionId);
                    sub.SecurityCenterBuiltIn = await AzMgmtScanner.GetSecurityCenterBuiltIn(sub.subscriptionId);
                    sub.SecurityContact = await AzMgmtScanner.GetSecurityContacts(sub.subscriptionId);
                    sub.Resources.VirtualMachines = await AzMgmtScanner.GetVirtualMachines(sub.subscriptionId);
                    sub.Resources.PostgreSQLs = await AzMgmtScanner.GetPostgreSQLFlexibleServers(sub.subscriptionId);
                    sub.PolicyAssignment = await AzMgmtScanner.GetPolicyAssignment(sub.subscriptionId);
                }
                
            }

            if (scopes.Contains(ScopeEnum.MDM))
            {
                MDMScanner = new MDMScanner(this);
                if (MDMScanner.CheckIntunePowerShellAvailable())
                {
                    MDMSettings mdmSettings = new MDMSettings();
                    mdmSettings.MobileDeviceConfigurations = await MDMScanner.GetMobileDeviceConfigurations();
                    mdmSettings.ConfigurationPolicies = await MDMScanner.GetConfigurationPolicies();
                    mdmSettings.MobileDeviceCompliancePolicies = await MDMScanner.GetMobileDeviceCompliancePolicies();
                    Result.MDMSettings = mdmSettings;
                }
            }

            Console.WriteLine("[+] Finished collecting infos.");
            return Result;  
        }
        
        private bool CheckP1License(List<LicenseDetails> licenses)
        {
            return CheckLicense(licenses, "AAD_PREMIUM");
        }

        private bool CheckP2License(List<LicenseDetails> licenses)
        {
            return CheckLicense(licenses, "AAD_PREMIUM_P2");
        }

        private bool CheckLicense(List<LicenseDetails> licenses, String licenseName)
        {
            foreach (LicenseDetails detail in licenses)
            {
                foreach (Serviceplan plan in detail.servicePlans)
                {
                    if (plan.servicePlanName == licenseName & plan.provisioningStatus == "Success" & plan.appliesTo == "User")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
