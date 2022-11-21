using AzRanger.Models;
using AzRanger.Models.AdminCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class AdminCenterScanner : IScanner
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

        public AdminCenterScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://admin.microsoft.com";
            this.Scope = new String[]{"https://admin.microsoft.com/.default", "offline_access"};
        }

        public SkypeTeams GetSkypeTeamsSettings()
        {
            return (SkypeTeams)Get<SkypeTeams>(AdminCenterScanner.SkypeTeams);
        }

        public Calendarsharing GetCalendarsharing()
        {
            return (Calendarsharing)Get<Calendarsharing>(AdminCenterScanner.Calendarsharing);
        }


        public SwaySettings GetSwaySettings()
        {
            return (SwaySettings)Get<SwaySettings>(AdminCenterScanner.SwaySettings);
        }

        public DirsyncManagement GetDirsyncManagement()
        {
            return (DirsyncManagement)Get<DirsyncManagement>(AdminCenterScanner.DirsyncManagement);
        }

        public O365PasswordPolicy GetO365PasswordPolicy()
        {
            return (O365PasswordPolicy)Get<O365PasswordPolicy>(AdminCenterScanner.O365PasswordPolicy);
        }

        public OfficeStoreSettings GetOfficeStoreSettings()
        {
            try
            {
                bool LetUserAccessOfficeStore = (bool)Get<bool>(AdminCenterScanner.OfficeStoreAccess);
                bool LetUserStartTrial = (bool)Get<bool>(AdminCenterScanner.OfficeStartTrials);
                bool LetUserAutoClaim = (bool)Get<bool>(AdminCenterScanner.OfficeLicenceAutoClaim);
                return new OfficeStoreSettings(LetUserAccessOfficeStore, LetUserStartTrial, LetUserAutoClaim);
            }
            catch (Exception e)
            {
                logger.Warn("AdminCenterScanner.GetOfficeStoreSettings failed.");
                logger.Debug(e.Message);
                return null;
            }
        }

        public OfficeFormsSettings GetOfficeFormsSettings()
        {
            return (OfficeFormsSettings)Get<OfficeFormsSettings>(AdminCenterScanner.OfficeFormsSettings);
        }

        public ExchangeModernAuthSettings GetExchangeModernAuthSettings()
        {
            return (ExchangeModernAuthSettings)Get<ExchangeModernAuthSettings>(AdminCenterScanner.ExchangeModernAuthSettings);
        }



    }
}
