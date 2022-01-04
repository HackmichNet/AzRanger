using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Conditions conditions { get; set; }
        public Grantcontrols grantControls { get; set; }
        public Sessioncontrols sessionControls { get; set; }
    }

    public class Conditions
    {
        public string[] userRiskLevels { get; set; }
        public string[] signInRiskLevels { get; set; }
        public string[] clientAppTypes { get; set; }
        public object deviceStates { get; set; }
        public object clientApplications { get; set; }
        public Applications applications { get; set; }
        public Users users { get; set; }
        public Platforms platforms { get; set; }
        public Locations locations { get; set; }
        public Devices devices { get; set; }
    }

    public class Applications
    {
        public string[] includeApplications { get; set; }
        public object[] excludeApplications { get; set; }
        public object[] includeUserActions { get; set; }
        public object[] includeAuthenticationContextClassReferences { get; set; }
    }

    public class Users
    {
        public string[] includeUsers { get; set; }
        public string[] excludeUsers { get; set; }
        public object[] includeGroups { get; set; }
        public object[] excludeGroups { get; set; }
        public object[] includeRoles { get; set; }
        public object[] excludeRoles { get; set; }
    }

    public class Platforms
    {
        public string[] includePlatforms { get; set; }
        public string[] excludePlatforms { get; set; }
    }

    public class Locations
    {
        public string[] includeLocations { get; set; }
        public string[] excludeLocations { get; set; }
    }

    public class Devices
    {
        public object[] includeDeviceStates { get; set; }
        public object[] excludeDeviceStates { get; set; }
        public string[] includeDevices { get; set; }
        public string[] excludeDevices { get; set; }
        public object deviceFilter { get; set; }
    }

    public class Grantcontrols
    {
        public string _operator { get; set; }
        public string[] builtInControls { get; set; }
        public object[] customAuthenticationFactors { get; set; }
        public object[] termsOfUse { get; set; }
    }

    public class Sessioncontrols
    {
        public bool disableResilienceDefaults { get; set; }
        public object applicationEnforcedRestrictions { get; set; }
        public object signInFrequency { get; set; }
        public object continuousAccessEvaluation { get; set; }
        public Cloudappsecurity cloudAppSecurity { get; set; }
        public Persistentbrowser persistentBrowser { get; set; }
    }

    public class Cloudappsecurity
    {
        public string cloudAppSecurityType { get; set; }
        public bool isEnabled { get; set; }
    }

    public class Persistentbrowser
    {
        public string mode { get; set; }
        public bool isEnabled { get; set; }
    }

}
