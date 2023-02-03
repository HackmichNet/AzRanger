﻿using AzRanger.Checks;
using AzRanger.Models;
using AzRanger.Models.AzMgmt;
using AzRanger.Models.Azrbac;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

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


        public Scanner(String username, String password, String proxy, String tenant = null)
        {
            this.Username = username;
            this.Password = password;
            this.Proxy = proxy;
            this.Domain = username.Split('@')[1];
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
            this.Username = this.Authenticator.GetUsername();
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
        }

        public Tenant ScanTenant(List<ScopeEnum> scopes)
        {
            if(this.TenantId == null)
            {
                logger.Warn("Scanner.ScanTenant: Cannot retrieve TenantId. Aborting!");
                return null;
            }
                
            Tenant Result = new Tenant();
            Result.TenantId = this.TenantId;   
            Result.Username = this.Username;
            String currentUserId = this.Authenticator.GetUserId();
            bool isGlobalAdmin = false;
            bool isGlobalReader = false;
            bool isSharePointAdmin = false;
            bool scanAzureOnly = false;

            if(scopes.Count == 1 & scopes.Contains(ScopeEnum.Azure))
            {
                scanAzureOnly = true;
            }

            if (!scanAzureOnly)
            {
                Result.TenantSettings = new M365Settings();
                Result.TenantSettings.TenantSkuInfo = MainIamScanner.GetTenantSkuInfo();
                Result.AllDirectoryRoles = MsGraphScanner.GetAllDirectoryRoles();

                if (currentUserId != null)
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

                Result.domains = MsGraphScanner.GetAzDomains();
                Console.WriteLine("[+] Scanning the tenant: {0}.", this.TenantId);

                Result.AllUsers = MsGraphScanner.GetAllUsers();
                Console.WriteLine("[+] Found {0} users.", Result.AllUsers.Count);

                Console.WriteLine("[+] Found {0} roles.", Result.AllDirectoryRoles.Count);

                Result.AllGuests = MsGraphScanner.GetAllGuests();
                Console.WriteLine("[+] Found {0} guests.", Result.AllGuests.Count);

                Result.AllApplications = MsGraphScanner.GetAllApplications();
                Console.WriteLine("[+] Found {0} applications.", Result.AllApplications.Count);

                Result.AllServicePrincipals = MsGraphScanner.GetAllServicePrincipals();
                Console.WriteLine("[+] Found {0} serviceprincipals.", Result.AllServicePrincipals.Count);

                Result.AllGroups = MsGraphScanner.GetAllGroups();
                Console.WriteLine("[+] Found {0} groups.", Result.AllGroups.Count);

                Result.RoleDefinitions = GraphWinScanner.GetRoleDefinitons();


                if (HasP2License)
                {
                    foreach (DirectoryRole role in Result.AllDirectoryRoles.Values)
                    {
                        List<PIMRoleAssignments> roleAssignments = AzrbacScanner.GetRoleAssignemts(Guid.Parse(this.TenantId), Guid.Parse(role.roleTemplateId));
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
                                List<AzurePrincipal> members = MsGraphScanner.GetAllGroupMemberTransitiv(assignment.subjectId);
                                foreach (AzurePrincipal member in members)
                                {
                                    principalsToAssigne.Add(member);
                                }
                            }
                            else
                            {
                                principalsToAssigne.Add(new AzurePrincipal(assignment.subjectId, aztype));
                            }
                            // Global resourese
                            if (assignment.scopedResourceId == null)
                            {
                                // Assigne user to role
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
                                        role.AddElligbleMember(p);
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

                Result.EnterpriseApplicationUserSettings = MsGraphScanner.GetSettings();
                Result.TenantSettings.AuthorizationPolicy = MsGraphScanner.GetAuthorizationPolicy();
                Result.TenantSettings.DeviceRegistrationPolicy = MsGraphScanner.GetDeviceRegistrationPolicy();

                Result.AllCAPolicies = MsGraphScanner.GetAllCondtionalAccessPolicies();
                Result.TenantSettings.SecurityDefaults = MainIamScanner.GetSecurityDefaults();
                Result.TenantSettings.DirectoryProperties = MainIamScanner.GetDirectoryProperties();
                Result.TenantSettings.PasswordResetPolicies = MainIamScanner.GetPasswordResetPolicies();
                Result.TenantSettings.PasswordPolicy = MainIamScanner.GetPasswordPolicy();
                Result.TenantSettings.ADConnectStatus = MainIamScanner.GetADConnectStatus();
                Result.TenantSettings.B2BPolicy = MainIamScanner.GetB2BPolicy();
                Result.TenantSettings.LCMSettings = MainIamScanner.GetLCMSettings();
                Result.TenantSettings.UserSettings = MainIamScanner.GetUserSettings();
                Result.TenantSettings.SsgmProperties = MainIamScanner.GetSsgmProperties();
                Result.TenantSettings.LoginTenantBrandings = MainIamScanner.GetLoginTenantBrandings();
                Result.TenantSettings.DirSyncFeatures = ProvisionAPIScanner.GetDirSyncFeatures();
                Result.TenantSettings.AdminCenterSettings = new AdminCenterSettings();
                Result.TenantSettings.AdminCenterSettings.SkypeTeams = AdminCenterScanner.GetSkypeTeamsSettings();
                Result.TenantSettings.AdminCenterSettings.OfficeFormsSettings = AdminCenterScanner.GetOfficeFormsSettings();
                Result.TenantSettings.AdminCenterSettings.OfficeStoreSettings = AdminCenterScanner.GetOfficeStoreSettings();
                Result.TenantSettings.AdminCenterSettings.O365PasswordPolicy = AdminCenterScanner.GetO365PasswordPolicy();
                Result.TenantSettings.AdminCenterSettings.SwaySettings = AdminCenterScanner.GetSwaySettings();
                Result.TenantSettings.AdminCenterSettings.Calendarsharing = AdminCenterScanner.GetCalendarsharing();
                Result.TenantSettings.AdminCenterSettings.DirsyncManagement = AdminCenterScanner.GetDirsyncManagement();
                Result.TenantSettings.OfficeDLPPolicies = ComplianceCenterScanner.GetDLPPolicies();
                Result.TenantSettings.DlpLabels = ComplianceCenterScanner.GetDLPLabels();
                Result.TenantSettings.AuthenticationMethodsPolicy = MsGraphScanner.GetAuthenticationMethodsPolicy();
                Result.TenantSettings.MSOLCompanyInformation = ProvisionAPIScanner.GetMsolCompanyInformation();
            }
            if (scopes.Contains(ScopeEnum.Teams))
            {
                TeamsSettings settings = new TeamsSettings();
                settings.TeamsClientConfiguration = TeamsScanner.GetTeamsClientConfiguration();
                settings.TenantFederationSettings = TeamsScanner.GetTenantFederationSettings();
                Result.TeamsSettings = settings;
            }

            if (scopes.Contains(ScopeEnum.SPO))
            {
                if(Result.TenantSettings.SecurityDefaults == null)
                {
                    Result.TenantSettings.SecurityDefaults = MainIamScanner.GetSecurityDefaults();
                }
                if (isGlobalAdmin | isSharePointAdmin)
                {
                    Result.SharepointInformation = ProvisionAPIScanner.GetSharepointInformation();
                    if (Result.SharepointInformation != null)
                    {
                        Console.WriteLine("[+] Found SharePoint on: {0}", Result.SharepointInformation.SharepointUrl);
                        Console.WriteLine("[+] Found SharePoint-Admin on: {0}", Result.SharepointInformation.AdminUrl);
                        SharePointScanner sharePointScanner = new SharePointScanner(this, Result.SharepointInformation.AdminUrl);
                        Result.SharepointInformation.SharepointInternalInfos = sharePointScanner.GetSharepointSettings();
                    }
                }
            }

            if (scopes.Contains(ScopeEnum.EXO))
            {
                if (Result.TenantSettings.SecurityDefaults == null)
                {
                    Result.TenantSettings.SecurityDefaults = MainIamScanner.GetSecurityDefaults();
                }
                if(Result.domains == null)
                {
                    Result.domains = MsGraphScanner.GetAzDomains();
                }
                Result.ExchangeOnlineSettings = new ExchangeOnlineSettings();
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
                if (Result.ExchangeOnlineSettings.EXOUsers != null)
                {
                    Console.WriteLine("[+] Found {0} Exchange-User.", Result.ExchangeOnlineSettings.EXOUsers.Count);
                }
                Result.ExchangeOnlineSettings.OwaMailboxPolicy = ExchangeOnlineScanner.GetOwaMailboxPolicy();

                Console.WriteLine("[+] Start scanning Azure Resources.");
            }

            if (scopes.Contains(ScopeEnum.Azure))
            {
                Result.ManagementGroups = AzMgmtScanner.GetAllManagementGroups();
                Result.ManagementGroupSettings = AzMgmtScanner.GetManagementGroupSettings();
                Result.Subscriptions = AzMgmtScanner.GetAllSubscriptions();
                Result.SubscriptionPolicy = AzMgmtScanner.GetSubscriptionPolicy();
                foreach (Subscription sub in Result.Subscriptions.Values)
                {
                    sub.Resources.StorageAccounts = AzMgmtScanner.GetStorageAccounts(sub.subscriptionId);
                    sub.Resources.KeyVaults = AzMgmtScanner.GetKeyVaults(sub.subscriptionId);
                    foreach (KeyVault vault in sub.Resources.KeyVaults)
                    {
                        KeyVaultScanner vaultScanner = new KeyVaultScanner(this, vault.properties.vaultUri);
                        vault.Keys = vaultScanner.GetKeyVaultKeys();
                        vault.Secrets = vaultScanner.GetKeyVaultSecrets();
                    }
                    sub.Resources.ActivityLogAlerts = AzMgmtScanner.GetActivityLogAlerts(sub.subscriptionId);
                    sub.Resources.NetworkSecurityGroups = AzMgmtScanner.GetNetworkSecurityGroups(sub.subscriptionId);
                    sub.Resources.SQLServers = AzMgmtScanner.GetSQLServers(sub.subscriptionId);
                    sub.AutoProvisioningSettings = AzMgmtScanner.GetProvisioningSettings(sub.subscriptionId);
                    sub.SecurityCenterBuiltIn = AzMgmtScanner.GetSecurityCenterBuiltIn(sub.subscriptionId);
                    sub.SecurityContact = AzMgmtScanner.GetSecurityContacts(sub.subscriptionId);
                    sub.Resources.VirtualMachines = AzMgmtScanner.GetVirtualMachines(sub.subscriptionId);
                }
            }

            if (scopes.Contains(ScopeEnum.MDM))
            {
                MDMScanner = new MDMScanner(this);
                if (MDMScanner.CheckIntunePowerShellAvailable())
                {
                    MDMSettings mdmSettings = new MDMSettings();
                    mdmSettings.MobileDeviceConfigurations = MDMScanner.GetMobileDeviceConfigurations();
                    mdmSettings.ConfigurationPolicies = MDMScanner.GetConfigurationPolicies();
                    mdmSettings.MobileDeviceCompliancePolicies = MDMScanner.GetMobileDeviceCompliancePolicies();
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
