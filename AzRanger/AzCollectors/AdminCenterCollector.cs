using AzRanger.Models.AdminCenter;
using System;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class AdminCenterCollector : AbstractCollector
    {

        public const String ExchangeModernAuthSettings = "/admin/api/services/apps/modernAuth";
        public const String O365PasswordPolicy = "/admin/api/Settings/security/passwordpolicy";

        // https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/Store
        // Let user access the officestore
        public const String OfficeStoreAccess = "/admin/api/settings/apps/store";

        //Let user start tiral in behalf of the Org 
        public const String OfficeStartTrials = "/admin/api/storesettings/iwpurchaseallowed";

        // Let user autoclaim licences
        public const String OfficeLicenceAutoClaim = "/admin/api/storesettings/iwpurchasefeatureenabled";

        // Office Forms settings 
        public const String OfficeFormsSettings = "/admin/api/settings/apps/officeforms";

        // Office Sway Settings
        public const String SwaySettings = "/admin/api/settings/apps/Sway";

        // SkypeTeams
        public const String SkypeTeams = "/admin/api/settings/apps/skypeteams";

        // https://admin.microsoft.com/Adminportal/Home#/Settings/Services/:/Settings/L1/Calendar
        public const String Calendarsharing = "/admin/api/settings/apps/calendarsharing";

        // https://admin.microsoft.com/#/dirsyncmanagement
        public const String DirsyncManagement = "/admin/api/DirsyncManagement/manage";

        public const String Officeonline = "/admin/api/settings/apps/officeonline";

        public AdminCenterCollector(IAuthenticator authenticator, String proxy)
        {
            this.Authenticator = authenticator;
            this.BaseAddress = "https://admin.microsoft.com";
            this.Scope = new String[] { "https://admin.microsoft.com/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, proxy);
        }

        public Task<Officeonline> GetOfficeonline()
        {
            return Get<Officeonline>(AdminCenterCollector.Officeonline);
        }

        public Task<SkypeTeams> GetSkypeTeamsSettings()
        {
            return Get<SkypeTeams>(AdminCenterCollector.SkypeTeams);
        }

        public Task<Calendarsharing> GetCalendarsharing()
        {
            return Get<Calendarsharing>(AdminCenterCollector.Calendarsharing);
        }


        public Task<SwaySettings> GetSwaySettings()
        {
            return Get<SwaySettings>(AdminCenterCollector.SwaySettings);
        }

        public Task<DirsyncManagement> GetDirsyncManagement()
        {
            return Get<DirsyncManagement>(AdminCenterCollector.DirsyncManagement);
        }

        public Task<O365PasswordPolicy> GetO365PasswordPolicy()
        {
            return Get<O365PasswordPolicy>(AdminCenterCollector.O365PasswordPolicy);
        }

        public async Task<OfficeStoreSettings> GetOfficeStoreSettings()
        {
            try
            {
                Task<bool> LetUserAccessOfficeStoreTask = Get<bool>(AdminCenterCollector.OfficeStoreAccess);
                Task<bool> LetUserStartTrialTask = Get<bool>(AdminCenterCollector.OfficeStartTrials);
                Task<bool> LetUserAutoClaimTask = Get<bool>(AdminCenterCollector.OfficeLicenceAutoClaim);

                object LetUserAccessOfficeStore = await LetUserAccessOfficeStoreTask;
                object LetUserStartTrial = await LetUserStartTrialTask;
                object LetUserAutoClaim = await LetUserAutoClaimTask;

                return new OfficeStoreSettings((bool)LetUserAccessOfficeStore, (bool)LetUserStartTrial, (bool)LetUserAutoClaim);
            }
            catch (Exception e)
            {
                logger.Warn("AdminCenterScanner.GetOfficeStoreSettings failed.");
                logger.Debug(e.Message);
                return null;
            }
        }

        public Task<OfficeFormsSettings> GetOfficeFormsSettings()
        {
            return Get<OfficeFormsSettings>(AdminCenterCollector.OfficeFormsSettings);
        }

        public Task<ExchangeModernAuthSettings> GetExchangeModernAuthSettings()
        {
            return Get<ExchangeModernAuthSettings>(AdminCenterCollector.ExchangeModernAuthSettings);
        }



    }
}
