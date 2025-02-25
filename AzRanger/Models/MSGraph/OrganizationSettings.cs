using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MSGraph
{
    public class OrganizationSettings
    {
        public string id { get; set; }
        public object deletedDateTime { get; set; }
        public string[] businessPhones { get; set; }
        public object city { get; set; }
        public object country { get; set; }
        public string countryLetterCode { get; set; }
        public DateTime createdDateTime { get; set; }
        public object defaultUsageLocation { get; set; }
        public string displayName { get; set; }
        public object isMultipleDataLocationsForServicesEnabled { get; set; }
        public object[] marketingNotificationEmails { get; set; }
        public DateTime onPremisesLastSyncDateTime { get; set; }
        public bool onPremisesSyncEnabled { get; set; }
        public object partnerTenantType { get; set; }
        public object postalCode { get; set; }
        public string preferredLanguage { get; set; }
        public object[] securityComplianceNotificationMails { get; set; }
        public object[] securityComplianceNotificationPhones { get; set; }
        public object state { get; set; }
        public object street { get; set; }
        public string[] technicalNotificationMails { get; set; }
        public string tenantType { get; set; }
        public OrganizationSettingsDirectorysizequota directorySizeQuota { get; set; }
        public object privacyProfile { get; set; }
        public OrganizationSettingsAssignedplan[] assignedPlans { get; set; }
        public OrganizationSettingsOnpremisessyncstatu[] onPremisesSyncStatus { get; set; }
        public OrganizationSettingsProvisionedplan[] provisionedPlans { get; set; }
        public OrganizationSettingsVerifieddomain[] verifiedDomains { get; set; }
    }

    public class OrganizationSettingsDirectorysizequota
    {
        public int used { get; set; }
        public int total { get; set; }
    }

    public class OrganizationSettingsAssignedplan
    {
        public DateTime assignedDateTime { get; set; }
        public string capabilityStatus { get; set; }
        public string service { get; set; }
        public string servicePlanId { get; set; }
    }

    public class OrganizationSettingsOnpremisessyncstatu
    {
        public string attributeSetName { get; set; }
        public string state { get; set; }
        public int version { get; set; }
    }

    public class OrganizationSettingsProvisionedplan
    {
        public string capabilityStatus { get; set; }
        public string provisioningStatus { get; set; }
        public string service { get; set; }
    }

    public class OrganizationSettingsVerifieddomain
    {
        public string capabilities { get; set; }
        public bool isDefault { get; set; }
        public bool isInitial { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

}
