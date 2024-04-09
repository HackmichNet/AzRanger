namespace AzRanger.Models
{
    public class ConditionalAccessPolicy
    {
        public string id { get; set; }
        public string displayName { get; set; }
        // DateTime
        public object createdDateTime { get; set; }
        public object modifiedDateTime { get; set; }
        public string state { get; set; }
        public ConditionalAccessPolicyConditions conditions { get; set; }
        public ConditionalAccessPolicyGrantcontrols grantControls { get; set; }
        public ConditionalAccessPolicySessioncontrols sessionControls { get; set; }
    }

    public class ConditionalAccessPolicyConditions
    {
        public string[] userRiskLevels { get; set; }
        public string[] signInRiskLevels { get; set; }
        public string[] clientAppTypes { get; set; }
        public object deviceStates { get; set; }
        public object clientApplications { get; set; }
        public ConditionalAccessPolicyApplications applications { get; set; }
        public ConditionalAccessPolicyUsers users { get; set; }
        public ConditionalAccessPolicyPlatforms platforms { get; set; }
        public ConditionalAccessPolicyLocations locations { get; set; }
        public ConditionalAccessPolicyDevices devices { get; set; }
    }
    public class ConditionalAccessPolicyUsers
    {
        public object[] includeUsers { get; set; }
        public object[] excludeUsers { get; set; }
        public object[] includeGroups { get; set; }
        public object[] excludeGroups { get; set; }
        public object[] includeRoles { get; set; }
        public object[] excludeRoles { get; set; }
    }

    public class ConditionalAccessPolicyPlatforms
    {
        public string[] includePlatforms { get; set; }
        public string[] excludePlatforms { get; set; }
    }

    public class ConditionalAccessPolicyLocations
    {
        public string[] includeLocations { get; set; }
        public string[] excludeLocations { get; set; }
    }

    public class ConditionalAccessPolicyDevices
    {
        public object[] includeDeviceStates { get; set; }
        public object[] excludeDeviceStates { get; set; }
        public string[] includeDevices { get; set; }
        public string[] excludeDevices { get; set; }
        public object deviceFilter { get; set; }
    }

    public class ConditionalAccessPolicyGrantcontrols
    {
        public string _operator { get; set; }
        public string[] builtInControls { get; set; }
        public object[] customAuthenticationFactors { get; set; }
        public object[] termsOfUse { get; set; }
    }

    public class ConditionalAccessPolicyCloudappsecurity
    {
        public string cloudAppSecurityType { get; set; }
        public bool isEnabled { get; set; }
    }

    public class ConditionalAccessPolicyPersistentbrowser
    {
        public string mode { get; set; }
        public bool isEnabled { get; set; }
    }
    public class ConditionalAccessPolicySessioncontrols
    {
        public object disableResilienceDefaults { get; set; }
        public object cloudAppSecurity { get; set; }
        public object signInFrequency { get; set; }
        public object persistentBrowser { get; set; }
        public object continuousAccessEvaluation { get; set; }
        public object secureSignInSession { get; set; }
        public object networkAccessSecurity { get; set; }
        public ConditionalAccessPolicyApplicationenforcedrestrictions applicationEnforcedRestrictions { get; set; }
    }

    public class ConditionalAccessPolicyApplicationenforcedrestrictions
    {
        public bool isEnabled { get; set; }
    }
    public class ConditionalAccessPolicyApplications
    {
        public string[] includeApplications { get; set; }
        public object[] excludeApplications { get; set; }
        public object[] includeUserActions { get; set; }
        public object[] includeAuthenticationContextClassReferences { get; set; }
        public object applicationFilter { get; set; }
        public object networkAccess { get; set; }
    }


}
