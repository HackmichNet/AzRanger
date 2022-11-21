
namespace AzRanger.Models.AdminCenter
{
    public class ExchangeModernAuthSettings
    {
        public bool ShowBasicAuthSettings { get; set; }
        public bool EnableModernAuth { get; set; }
        public bool SecureDefaults { get; set; }
        public bool DisableModernAuth { get; set; }
        public bool AllowBasicAuthActiveSync { get; set; }
        public bool AllowBasicAuthImap { get; set; }
        public bool AllowBasicAuthPop { get; set; }
        public bool AllowBasicAuthWebServices { get; set; }
        public bool AllowBasicAuthPowershell { get; set; }
        public bool AllowBasicAuthAutodiscover { get; set; }
        public bool AllowBasicAuthMapi { get; set; }
        public bool AllowBasicAuthOfflineAddressBook { get; set; }
        public bool AllowBasicAuthRpc { get; set; }
        public bool AllowBasicAuthSmtp { get; set; }
        public object AllowOutlookClient { get; set; }
    }

}
