using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class AuthenticationMethodsPolicy
    {
        [JsonPropertyName("@odata.context")]
        public string odatacontext { get; set; }
        public string id { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
        public string policyVersion { get; set; }
        public Registrationenforcement registrationEnforcement { get; set; }
        public Reportsuspiciousactivitysettings reportSuspiciousActivitySettings { get; set; }
        public string authenticationMethodConfigurationsodatacontext { get; set; }
        public Authenticationmethodconfiguration[] authenticationMethodConfigurations { get; set; }
    }

    public class Registrationenforcement
    {
        public Authenticationmethodsregistrationcampaign authenticationMethodsRegistrationCampaign { get; set; }
    }

    public class Authenticationmethodsregistrationcampaign
    {
        public int snoozeDurationInDays { get; set; }
        public string state { get; set; }
        public object[] excludeTargets { get; set; }
        public Includetarget[] includeTargets { get; set; }
    }
    public class Reportsuspiciousactivitysettings
    {
        public string state { get; set; }
        public int voiceReportingCode { get; set; }
        public Includetarget includeTarget { get; set; }
    }

    public class Authenticationmethodconfiguration
    {
        [JsonPropertyName("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public string state { get; set; }
        public bool isSelfServiceRegistrationAllowed { get; set; }
        public bool isAttestationEnforced { get; set; }
        public Keyrestrictions keyRestrictions { get; set; }
        [JsonPropertyName("@includeTargets@odata.context")]
        public string includeTargetsodatacontext { get; set; }
        public Includetarget[] includeTargets { get; set; }
        public Featuresettings featureSettings { get; set; }
        public int defaultLifetimeInMinutes { get; set; }
        public int defaultLength { get; set; }
        public int minimumLifetimeInMinutes { get; set; }
        public int maximumLifetimeInMinutes { get; set; }
        public bool isUsableOnce { get; set; }
        public string allowExternalIdToUseEmailOtp { get; set; }
        public Certificateuserbinding[] certificateUserBindings { get; set; }
        public Authenticationmodeconfiguration authenticationModeConfiguration { get; set; }
    }

    public class Keyrestrictions
    {
        public bool isEnforced { get; set; }
        public string enforcementType { get; set; }
        public object[] aaGuids { get; set; }
    }

    public class Featuresettings
    {
        public Companionappallowedstate companionAppAllowedState { get; set; }
        public Numbermatchingrequiredstate numberMatchingRequiredState { get; set; }
        public Displayappinformationrequiredstate displayAppInformationRequiredState { get; set; }
        public Displaylocationinformationrequiredstate displayLocationInformationRequiredState { get; set; }
    }

    public class Companionappallowedstate
    {
        public string state { get; set; }
        public Includetarget includeTarget { get; set; }
        public Excludetarget excludeTarget { get; set; }
    }

    public class Excludetarget
    {
        public string targetType { get; set; }
        public string id { get; set; }
    }

    public class Numbermatchingrequiredstate
    {
        public string state { get; set; }
        public Includetarget includeTarget { get; set; }
        public Excludetarget excludeTarget { get; set; }
    }

    public class Displayappinformationrequiredstate
    {
        public string state { get; set; }
        public Includetarget includeTarget { get; set; }
        public Excludetarget excludeTarget { get; set; }
    }

    public class Displaylocationinformationrequiredstate
    {
        public string state { get; set; }
        public Includetarget includeTarget { get; set; }
        public Excludetarget excludeTarget { get; set; }
    }


    public class Authenticationmodeconfiguration
    {
        public string x509CertificateAuthenticationDefaultMode { get; set; }
        public object[] rules { get; set; }
    }

    public class Includetarget
    {
        public string targetType { get; set; }
        public string id { get; set; }
        public bool isRegistrationRequired { get; set; }
        public string authenticationMode { get; set; }
        public string displayAppInformationRequiredState { get; set; }
        public string numberMatchingRequiredState { get; set; }
        public bool isUsableForSignIn { get; set; }
        public string targetedAuthenticationMethod { get; set; }
    }

    public class Certificateuserbinding
    {
        public string x509CertificateField { get; set; }
        public string userProperty { get; set; }
        public int priority { get; set; }
    }

}
