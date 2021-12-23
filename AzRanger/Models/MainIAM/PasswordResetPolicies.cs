
namespace AzRanger.Models.MainIAM
{
    public class PasswordResetPolicies
    {
        public string objectId { get; set; }
        public int enablementType { get; set; }
        public int numberOfAuthenticationMethodsRequired { get; set; }
        public bool emailOptionEnabled { get; set; }
        public bool mobilePhoneOptionEnabled { get; set; }
        public bool officePhoneOptionEnabled { get; set; }
        public bool securityQuestionsOptionEnabled { get; set; }
        public bool mobileAppNotificationEnabled { get; set; }
        public bool mobileAppCodeEnabled { get; set; }
        public int numberOfQuestionsToRegister { get; set; }
        public int numberOfQuestionsToReset { get; set; }
        public bool registrationRequiredOnSignIn { get; set; }
        public int registrationReconfirmIntevalInDays { get; set; }
        public bool skipRegistrationAllowed { get; set; }
        public int skipRegistrationMaxAllowedDays { get; set; }
        public bool customizeHelpdeskLink { get; set; }
        public string customHelpdeskEmailOrUrl { get; set; }
        public bool notifyUsersOnPasswordReset { get; set; }
        public bool notifyOnAdminPasswordReset { get; set; }
        public string[] passwordResetEnabledGroupIds { get; set; }
        public string passwordResetEnabledGroupName { get; set; }
        public object[] securityQuestions { get; set; }
        public object[] registrationConditionalAccessPolicies { get; set; }
        public bool emailOptionAllowed { get; set; }
        public bool mobilePhoneOptionAllowed { get; set; }
        public bool officePhoneOptionAllowed { get; set; }
        public bool securityQuestionsOptionAllowed { get; set; }
        public bool mobileAppNotificationOptionAllowed { get; set; }
        public bool mobileAppCodeOptionAllowed { get; set; }
    }

}
