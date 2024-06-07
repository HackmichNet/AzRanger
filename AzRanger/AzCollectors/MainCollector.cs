using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Models.AdminCenter;
using AzRanger.Models.AzMgmt;
using AzRanger.Models.ComplianceCenter;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.Generic;
using AzRanger.Models.MainIAM;
using AzRanger.Models.MSGraph;
using AzRanger.Models.Provision;
using AzRanger.Models.Teams;
using AzRanger.Utilities;
using AzRanger.Utilities.EnrichmentEngine;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class MainCollector
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        internal string Username { get; }
        internal String Proxy { get; }

        internal IAuthenticator AADPowerShellAuthenticator;
        internal IAuthenticator PowerAutomateAuthenticator;
        internal IAuthenticator MSGraphCommandlineAuthenticator;
        internal String Domain;
        internal String TenantId;
        internal bool HasP1License = false;
        internal bool HasP2License = false;


        internal AdminCenterCollector AdminCenterCollector;
        internal MSGraphCollector MSGraphCollector;
        internal ProvisionAPICollector ProvisionAPICollector;
        internal ExchangeOnlineCollector ExchangeOnlineScanner;
        internal MainIamCollector MainIamCollector;
        internal ComplianceCenterCollector ComplianceCenterScanner;
        internal GraphWinCollector GraphWinCollector;
        internal TeamsCollector TeamsCollector;
        internal AzMgmtCollector AzMgmtCollector;

        public MainCollector(IAuthenticator aadPowerShellUserAuthenticator, IAuthenticator powerAutomateUserAuthenticator, IAuthenticator msGraphCommandlineAuthenticator ,String proxy, String tenant = null)
        {
            this.Proxy = proxy;
            this.TenantId = tenant;
            this.AADPowerShellAuthenticator = aadPowerShellUserAuthenticator;
            this.PowerAutomateAuthenticator = powerAutomateUserAuthenticator;
            this.MSGraphCommandlineAuthenticator = msGraphCommandlineAuthenticator;
            if (this.TenantId == null)
            {
                this.TenantId = this.AADPowerShellAuthenticator.GetTenantId().Result;
            }
            this.Username = this.AADPowerShellAuthenticator.GetUsername().Result;
        }

        public async Task<Tenant> ScanTenant(List<ScopeEnum> scopes)
        {
            if (this.TenantId == null)
            {
                logger.Warn("Scanner.ScanTenant: Cannot retrieve TenantId. Aborting!");
                return null;
            }

            Tenant Result = new Tenant();
            Result.TenantId = this.TenantId;
            Result.Username = this.Username;
            String currentUserId = await this.AADPowerShellAuthenticator.GetUserId();
            bool isGlobalAdmin = false;
            bool isGlobalReader = false;
            bool isSharePointAdmin = false;
            bool scanAzureOnly = false;

            AdminCenterCollector = new AdminCenterCollector(AADPowerShellAuthenticator, Proxy);
            MSGraphCollector = new MSGraphCollector(AADPowerShellAuthenticator, TenantId, Proxy);
            ProvisionAPICollector = new ProvisionAPICollector(AADPowerShellAuthenticator, TenantId, Proxy);
            ExchangeOnlineScanner = new ExchangeOnlineCollector(AADPowerShellAuthenticator, TenantId, Proxy);
            MainIamCollector = new MainIamCollector(AADPowerShellAuthenticator, TenantId, Proxy);
            ComplianceCenterScanner = new ComplianceCenterCollector(AADPowerShellAuthenticator, TenantId, Proxy);
            GraphWinCollector = new GraphWinCollector(AADPowerShellAuthenticator, TenantId, Proxy);
            TeamsCollector = new TeamsCollector(AADPowerShellAuthenticator, TenantId, Proxy);
            AzMgmtCollector = new AzMgmtCollector(AADPowerShellAuthenticator, TenantId, Proxy);

            if (scopes.Count == 1 & scopes.Contains(ScopeEnum.Azure))
            {
                scanAzureOnly = true;
            }

            if (!scanAzureOnly)
            {
                Result.TenantSettings = new M365Settings();
                if (this.AADPowerShellAuthenticator.GetType() == typeof(UserAuthenticator))
                {
                    Result.DirectoryRoles = await MSGraphCollector.GetAllDirectoryRoles();
                    if ((Result.DirectoryRoles != null && currentUserId != null))
                    {
                        Console.WriteLine("[+] You are using {0} roles in your tenant", Result.DirectoryRoles.Count);
                        foreach (DirectoryRole role in Result.DirectoryRoles.Values)
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
                    else
                    {
                        logger.Warn("Scanner.ScanTenant: Cannot get User Id. Should not happen!");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("[+] Running AzRanger with a service principal.... Good luck!");
                }
            }

            if (scopes.Contains(ScopeEnum.AAD))
            {

                Result.TenantSettings.TenantSkuInfo = await MainIamCollector.GetTenantSkuInfo();
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

                if (Result.DirectoryRoles == null)
                {
                    Task<Dictionary<Guid, DirectoryRole>> getAllDirectoryRoles = MSGraphCollector.GetAllDirectoryRoles(); ;
                    Result.DirectoryRoles = await getAllDirectoryRoles;
                    if (Result.DirectoryRoles != null)
                    {
                        Console.WriteLine("[+] You have {0} roles in your tenant", Result.DirectoryRoles.Count);
                    }
                }
                Task<List<Domain>> getDomainTask = MSGraphCollector.GetAzDomains();
                Result.Domains = await getDomainTask;
                if (Result.Domains != null)
                {
                    Console.WriteLine("[+] You have {0} domains in your tenant", Result.Domains.Count);
                }
                Task<Dictionary<Guid, User>> getUserTask = MSGraphCollector.GetAllUsers(HasP1License, GraphWinCollector);
                Result.Users = await getUserTask;
                if (Result.Users != null)
                {
                    Console.WriteLine("[+] You have {0} users in your tenant", Result.Users.Count);
                }
                Task<Dictionary<Guid, User>> getGuestTask = MSGraphCollector.GetAllGuests(HasP1License);
                Result.Guests = await getGuestTask;
                if (Result.Guests != null)
                {
                    Console.WriteLine("[+] You have {0} guests in your tenant", Result.Guests.Count);
                }
                Task<Dictionary<Guid, Application>> getAllApplications = MSGraphCollector.GetAllApplications();
                Result.Applications = await getAllApplications;
                if (Result.Applications != null)
                {
                    Console.WriteLine("[+] You have {0} applications in your tenant", Result.Applications.Count);
                }
                Task<Dictionary<Guid, ServicePrincipal>> getAllServicePrincipals = MSGraphCollector.GetAllServicePrincipals();
                Result.ServicePrincipals = await getAllServicePrincipals;
                if (Result.ServicePrincipals != null)
                {
                    Console.WriteLine("[+] You have {0} service principals in your tenant", Result.ServicePrincipals.Count);
                }
                Task<Dictionary<Guid, Group>> getGroupTask = MSGraphCollector.GetAllGroups();
                Result.Groups = await getGroupTask;
                if (Result.Groups != null)
                {
                    Console.WriteLine("[+] You have {0} groups in your tenant", Result.Groups.Count);
                }

                // Calculate role membership and if a user can add creds to an application
                if (Result.DirectoryRoles != null)
                {
                    if (HasP2License)
                    {
                        MSGraphCollector PIMCollector = new MSGraphCollector(PowerAutomateAuthenticator, TenantId, Proxy);

                        foreach (DirectoryRole role in Result.DirectoryRoles.Values)
                        {
                            role.pimRoleAssignments = await PIMCollector.GetDirectoryRoleAssignments(TenantId, role.roleTemplateId);
                            role.pimRoleAssignmentsEligible = await PIMCollector.GetDirectoryRoleAssignmentsEligible(TenantId, role.roleTemplateId);
                        }
                    }
                    // If not Premium P2 ist much easier
                    // TODO: Add to enrichment engine
                    else
                    {
                        foreach (DirectoryRole role in Result.DirectoryRoles.Values)
                        {
                            if (DirectoryRoleTemplateID.RolesAllowingAddCreds.Contains(role.roleTemplateId))
                            {
                                List<AzurePrincipal> cloudAdmins = new List<AzurePrincipal>();
                                foreach (AzurePrincipal user in role.GetMembers())
                                {
                                    AzurePrincipal u = new AzurePrincipal(user.id, user.PrincipalType);
                                    cloudAdmins.Add(u);
                                }
                                foreach (Application app in Result.Applications.Values)
                                {
                                    foreach (AzurePrincipal a in cloudAdmins)
                                    {
                                        app.AddUserAbleToAddCreds(a);
                                    }
                                }
                                foreach (ServicePrincipal principal in Result.ServicePrincipals.Values)
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

                officeTasks.Add(MSGraphCollector.GetSettings());
                officeTasks.Add(MSGraphCollector.GetAuthorizationPolicy());
                officeTasks.Add(MSGraphCollector.GetDeviceRegistrationPolicy());
                officeTasks.Add(MSGraphCollector.GetAllCondtionalAccessPolicies());
                officeTasks.Add(MSGraphCollector.GetAuthenticationMethodsPolicy());

                officeTasks.Add(MainIamCollector.GetSecurityDefaults());
                officeTasks.Add(MainIamCollector.GetDirectoryProperties());
                officeTasks.Add(MainIamCollector.GetPasswordResetPolicies());
                officeTasks.Add(MainIamCollector.GetPasswordPolicy());
                officeTasks.Add(MainIamCollector.GetADConnectStatus());
                officeTasks.Add(MainIamCollector.GetB2BPolicy());
                officeTasks.Add(MainIamCollector.GetLCMSettings());
                officeTasks.Add(MainIamCollector.GetUserSettings());
                officeTasks.Add(MainIamCollector.GetSsgmProperties());
                officeTasks.Add(MainIamCollector.GetLoginTenantBrandings());
                officeTasks.Add(MainIamCollector.GetOnPremisesPasswordResetPolicy());

                officeTasks.Add(ProvisionAPICollector.GetDirSyncFeatures());
                officeTasks.Add(ProvisionAPICollector.GetMsolCompanyInformation());

                officeTasks.Add(AdminCenterCollector.GetSkypeTeamsSettings());
                officeTasks.Add(AdminCenterCollector.GetOfficeFormsSettings());
                officeTasks.Add(AdminCenterCollector.GetOfficeStoreSettings());
                officeTasks.Add(AdminCenterCollector.GetO365PasswordPolicy());
                officeTasks.Add(AdminCenterCollector.GetSwaySettings());
                officeTasks.Add(AdminCenterCollector.GetCalendarsharing());
                officeTasks.Add(AdminCenterCollector.GetDirsyncManagement());
                officeTasks.Add(AdminCenterCollector.GetOfficeonline());

                officeTasks.Add(ComplianceCenterScanner.GetDLPPolicies());
                officeTasks.Add(ComplianceCenterScanner.GetDLPLabels());

                while (officeTasks.Any())
                {

                    Task result = null;
                    try
                    {
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
                            Result.CAPolicies = await getConditionalAccessPolicyTask;
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
                        case Task<OnPremisesPasswordResetPolicy> getOnPremisesPasswordResetPolicyTask:
                            Result.TenantSettings.OnPremisesPasswordResetPolicy = await getOnPremisesPasswordResetPolicyTask;
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

                Task<TeamsClientConfiguration> getTeamsClientConfigurationTask = TeamsCollector.GetTeamsClientConfiguration();
                Task<TenantFederationSettings> getTenantFederationSettingsTask = TeamsCollector.GetTenantFederationSettings();

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
                    Result.TenantSettings.SecurityDefaults = await MainIamCollector.GetSecurityDefaults();
                }
                if (isGlobalAdmin | isSharePointAdmin)
                {
                    Result.SharePointInformation = await ProvisionAPICollector.GetSharepointInformation();
                    if (Result.SharePointInformation != null)
                    {
                        Console.WriteLine("[+] Found SharePoint on: {0}", Result.SharePointInformation.SharePointUrl);
                        Console.WriteLine("[+] Found SharePoint-Admin on: {0}", Result.SharePointInformation.AdminUrl);
                        SharePointCollector sharePointScanner = new SharePointCollector(MSGraphCommandlineAuthenticator, Result.SharePointInformation.AdminUrl, TenantId, Proxy);
                        Result.SharePointInformation.SharePointInternalInfos = await sharePointScanner.GetSharePointSettings();
                    }
                }

            }

            if (scopes.Contains(ScopeEnum.EXO))
            {
                if (Result.TenantSettings.SecurityDefaults == null)
                {
                    Result.TenantSettings.SecurityDefaults = await MainIamCollector.GetSecurityDefaults();
                }
                if (Result.Domains == null)
                {
                    Result.Domains = await MSGraphCollector.GetAzDomains();
                }

                Result.ExchangeOnlineSettings = new ExchangeOnlineSettings();

                List<Task> exchangeTask = new List<Task>();
                exchangeTask.Add(ExchangeOnlineScanner.GetAdminAuditLogConfig());
                exchangeTask.Add(ExchangeOnlineScanner.GetHostedOutboundSpamFilterPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetMalwareFilterPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetTransportRules());
                exchangeTask.Add(ExchangeOnlineScanner.GetAcceptedDomains());
                exchangeTask.Add(ExchangeOnlineScanner.GetDkimSigningConfig());
                exchangeTask.Add(AdminCenterCollector.GetExchangeModernAuthSettings());
                exchangeTask.Add(ExchangeOnlineScanner.GetMalwareFilterRules());
                exchangeTask.Add(ExchangeOnlineScanner.GeRoleAssignmentPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetRemoteDomains());
                exchangeTask.Add(ExchangeOnlineScanner.GetMailboxes());
                exchangeTask.Add(ExchangeOnlineScanner.GetOrganizationConfig());
                exchangeTask.Add(ExchangeOnlineScanner.GetAuthenticationPolicies());
                exchangeTask.Add(ExchangeOnlineScanner.GetEXOUsers());
                exchangeTask.Add(ExchangeOnlineScanner.GetOwaMailboxPolicy());
                exchangeTask.Add(ExchangeOnlineScanner.GetMailboxAuditBypassAssociations());
                exchangeTask.Add(ExchangeOnlineScanner.GetExternalInOutlooks());

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
                        case Task<List<MailboxAuditBypassAssociation>> getMailboxAuditBypassAssociationTask:
                            Result.ExchangeOnlineSettings.MailboxAuditBypassAssociations = await getMailboxAuditBypassAssociationTask;
                            break;
                        case Task<List<ExternalInOutlook>> GetExternalInOutlooksTask:
                            Result.ExchangeOnlineSettings.ExternalInOutlooks = await GetExternalInOutlooksTask;
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
                Result.ManagementGroups = await AzMgmtCollector.GetAllManagementGroups();
                Result.ManagementGroupSettings = await AzMgmtCollector.GetManagementGroupSettings();
                Result.Subscriptions = await AzMgmtCollector.GetAllSubscriptions();
                Result.SubscriptionPolicy = await AzMgmtCollector.GetSubscriptionPolicy();
                foreach (Subscription sub in Result.Subscriptions.Values)
                {
                    sub.Resources.StorageAccounts = await AzMgmtCollector.GetStorageAccounts(sub.subscriptionId);
                    sub.Resources.KeyVaults = await AzMgmtCollector.GetKeyVaults(sub.subscriptionId);
                    if (sub.Resources.KeyVaults != null)
                    {
                        foreach (KeyVault vault in sub.Resources.KeyVaults)
                        {
                            KeyVaultCollector vaultScanner = new KeyVaultCollector(AADPowerShellAuthenticator, vault.properties.vaultUri, TenantId, Proxy);
                            if (vaultScanner != null)
                            {
                                vault.Keys = await vaultScanner.GetKeyVaultKeys();
                                vault.Secrets = await vaultScanner.GetKeyVaultSecrets();
                            }
                        }
                    }
                    sub.Resources.ActivityLogAlerts = await AzMgmtCollector.GetActivityLogAlerts(sub.subscriptionId);
                    sub.Resources.NetworkSecurityGroups = await AzMgmtCollector.GetNetworkSecurityGroups(sub.subscriptionId);
                    sub.Resources.SQLServers = await AzMgmtCollector.GetSQLServers(sub.subscriptionId);
                    sub.AutoProvisioningSettings = await AzMgmtCollector.GetProvisioningSettings(sub.subscriptionId);
                    sub.SecurityCenterBuiltIn = await AzMgmtCollector.GetSecurityCenterBuiltIn(sub.subscriptionId);
                    sub.SecurityContact = await AzMgmtCollector.GetSecurityContacts(sub.subscriptionId);
                    sub.Resources.VirtualMachines = await AzMgmtCollector.GetVirtualMachines(sub.subscriptionId);
                    sub.Resources.PostgreSQLs = await AzMgmtCollector.GetPostgreSQLFlexibleServers(sub.subscriptionId);
                    sub.PolicyAssignment = await AzMgmtCollector.GetPolicyAssignment(sub.subscriptionId);
                }
            }

            // Ignore infos not available
            /**if (scopes.Contains(ScopeEnum.MDM))
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
            }**/

            if (Result.Users != null && Result.Users.Count > 0 && Result.CAPolicies != null && Result.CAPolicies.Count > 0)
            {
                EnrichUserWithCAPolicies.Enrich(Result);
            }
            if (HasP2License)
            {
                await AssignUserToRole.Enrich(Result, MSGraphCollector);
                await AssignEligibleUserToRole.Enrich(Result, MSGraphCollector);
                AssignUserCanAddCreds.Enrich(Result);
            }
            

            Console.WriteLine("[+] Finished collecting information.");
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
