using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Provision
{
    // NOTE TO ME: NEVER CHANGE THIS CLASS NAME :D

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://provisioning.microsoftonline.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://provisioning.microsoftonline.com/", IsNullable = false)]
    public partial class GetCompanyInformationResponse
    {

        private GetCompanyInformationResponseGetCompanyInformationResult getCompanyInformationResultField;

        /// <remarks/>
        public GetCompanyInformationResponseGetCompanyInformationResult GetCompanyInformationResult
        {
            get
            {
                return this.getCompanyInformationResultField;
            }
            set
            {
                this.getCompanyInformationResultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://provisioning.microsoftonline.com/")]
    public partial class GetCompanyInformationResponseGetCompanyInformationResult
    {

        private ReturnValue returnValueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebServic" +
            "e")]
        public ReturnValue ReturnValue
        {
            get
            {
                return this.returnValueField;
            }
            set
            {
                this.returnValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebServic" +
        "e")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebServic" +
        "e", IsNullable = false)]
    public partial class ReturnValue
    {

        private bool allowAdHocSubscriptionsField;

        private bool allowEmailVerifiedUsersField;

        private string[] authorizedServiceInstancesField;

        private object authorizedServicesField;

        private object cityField;

        private object companyDeletionStartTimeField;

        private string[] companyTagsField;

        private string companyTypeField;

        private object compassEnabledField;

        private object countryField;

        private string countryLetterCodeField;

        private object dapEnabledField;

        private object defaultUsageLocationField;

        private object dirSyncAnchorAttributeField;

        private object dirSyncApplicationTypeField;

        private object dirSyncClientMachineNameField;

        private object dirSyncClientVersionField;

        private object dirSyncServiceAccountField;

        private bool directorySynchronizationEnabledField;

        private string directorySynchronizationStatusField;

        private string displayNameField;

        private string initialDomainField;

        private object lastDirSyncTimeField;

        private object lastPasswordSyncTimeField;

        private object marketingNotificationEmailsField;

        private object multipleDataLocationsForServicesEnabledField;

        private string objectIdField;

        private bool passwordSynchronizationEnabledField;

        private PortalSettings portalSettingsField;

        private object postalCodeField;

        private string preferredLanguageField;

        private object releaseTrackField;

        private string replicationScopeField;

        private bool rmsViralSignUpEnabledField;

        private object securityComplianceNotificationEmailsField;

        private object securityComplianceNotificationPhonesField;

        private bool selfServePasswordResetEnabledField;

        private ServiceInformationServiceInformation[] serviceInformationField;

        private ServiceInstanceInformationServiceInstanceInformation[] serviceInstanceInformationField;

        private object stateField;

        private object streetField;

        private bool subscriptionProvisioningLimitedField;

        private TechnicalNotificationEmails technicalNotificationEmailsField;

        private ulong telephoneNumberField;

        private object uIExtensibilityUrisField;

        private bool usersPermissionToCreateGroupsEnabledField;

        private bool usersPermissionToCreateLOBAppsEnabledField;

        private bool usersPermissionToReadOtherUsersEnabledField;

        private bool usersPermissionToUserConsentToAppEnabledField;

        private System.DateTime whenCreatedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool AllowAdHocSubscriptions
        {
            get
            {
                return this.allowAdHocSubscriptionsField;
            }
            set
            {
                this.allowAdHocSubscriptionsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool AllowEmailVerifiedUsers
        {
            get
            {
                return this.allowEmailVerifiedUsersField;
            }
            set
            {
                this.allowEmailVerifiedUsersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable = false)]
        public string[] AuthorizedServiceInstances
        {
            get
            {
                return this.authorizedServiceInstancesField;
            }
            set
            {
                this.authorizedServiceInstancesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public object AuthorizedServices
        {
            get
            {
                return this.authorizedServicesField;
            }
            set
            {
                this.authorizedServicesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object CompanyDeletionStartTime
        {
            get
            {
                return this.companyDeletionStartTimeField;
            }
            set
            {
                this.companyDeletionStartTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable = false)]
        public string[] CompanyTags
        {
            get
            {
                return this.companyTagsField;
            }
            set
            {
                this.companyTagsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string CompanyType
        {
            get
            {
                return this.companyTypeField;
            }
            set
            {
                this.companyTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object CompassEnabled
        {
            get
            {
                return this.compassEnabledField;
            }
            set
            {
                this.compassEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string CountryLetterCode
        {
            get
            {
                return this.countryLetterCodeField;
            }
            set
            {
                this.countryLetterCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DapEnabled
        {
            get
            {
                return this.dapEnabledField;
            }
            set
            {
                this.dapEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DefaultUsageLocation
        {
            get
            {
                return this.defaultUsageLocationField;
            }
            set
            {
                this.defaultUsageLocationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DirSyncAnchorAttribute
        {
            get
            {
                return this.dirSyncAnchorAttributeField;
            }
            set
            {
                this.dirSyncAnchorAttributeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DirSyncApplicationType
        {
            get
            {
                return this.dirSyncApplicationTypeField;
            }
            set
            {
                this.dirSyncApplicationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DirSyncClientMachineName
        {
            get
            {
                return this.dirSyncClientMachineNameField;
            }
            set
            {
                this.dirSyncClientMachineNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DirSyncClientVersion
        {
            get
            {
                return this.dirSyncClientVersionField;
            }
            set
            {
                this.dirSyncClientVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object DirSyncServiceAccount
        {
            get
            {
                return this.dirSyncServiceAccountField;
            }
            set
            {
                this.dirSyncServiceAccountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool DirectorySynchronizationEnabled
        {
            get
            {
                return this.directorySynchronizationEnabledField;
            }
            set
            {
                this.directorySynchronizationEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string DirectorySynchronizationStatus
        {
            get
            {
                return this.directorySynchronizationStatusField;
            }
            set
            {
                this.directorySynchronizationStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string InitialDomain
        {
            get
            {
                return this.initialDomainField;
            }
            set
            {
                this.initialDomainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object LastDirSyncTime
        {
            get
            {
                return this.lastDirSyncTimeField;
            }
            set
            {
                this.lastDirSyncTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object LastPasswordSyncTime
        {
            get
            {
                return this.lastPasswordSyncTimeField;
            }
            set
            {
                this.lastPasswordSyncTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public object MarketingNotificationEmails
        {
            get
            {
                return this.marketingNotificationEmailsField;
            }
            set
            {
                this.marketingNotificationEmailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object MultipleDataLocationsForServicesEnabled
        {
            get
            {
                return this.multipleDataLocationsForServicesEnabledField;
            }
            set
            {
                this.multipleDataLocationsForServicesEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string ObjectId
        {
            get
            {
                return this.objectIdField;
            }
            set
            {
                this.objectIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool PasswordSynchronizationEnabled
        {
            get
            {
                return this.passwordSynchronizationEnabledField;
            }
            set
            {
                this.passwordSynchronizationEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public PortalSettings PortalSettings
        {
            get
            {
                return this.portalSettingsField;
            }
            set
            {
                this.portalSettingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string PreferredLanguage
        {
            get
            {
                return this.preferredLanguageField;
            }
            set
            {
                this.preferredLanguageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object ReleaseTrack
        {
            get
            {
                return this.releaseTrackField;
            }
            set
            {
                this.releaseTrackField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public string ReplicationScope
        {
            get
            {
                return this.replicationScopeField;
            }
            set
            {
                this.replicationScopeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool RmsViralSignUpEnabled
        {
            get
            {
                return this.rmsViralSignUpEnabledField;
            }
            set
            {
                this.rmsViralSignUpEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public object SecurityComplianceNotificationEmails
        {
            get
            {
                return this.securityComplianceNotificationEmailsField;
            }
            set
            {
                this.securityComplianceNotificationEmailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public object SecurityComplianceNotificationPhones
        {
            get
            {
                return this.securityComplianceNotificationPhonesField;
            }
            set
            {
                this.securityComplianceNotificationPhonesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool SelfServePasswordResetEnabled
        {
            get
            {
                return this.selfServePasswordResetEnabledField;
            }
            set
            {
                this.selfServePasswordResetEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        [System.Xml.Serialization.XmlArrayItemAttribute("ServiceInformation", IsNullable = false)]
        public ServiceInformationServiceInformation[] ServiceInformation
        {
            get
            {
                return this.serviceInformationField;
            }
            set
            {
                this.serviceInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        [System.Xml.Serialization.XmlArrayItemAttribute("ServiceInstanceInformation", IsNullable = false)]
        public ServiceInstanceInformationServiceInstanceInformation[] ServiceInstanceInformation
        {
            get
            {
                return this.serviceInstanceInformationField;
            }
            set
            {
                this.serviceInstanceInformationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object Street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool SubscriptionProvisioningLimited
        {
            get
            {
                return this.subscriptionProvisioningLimitedField;
            }
            set
            {
                this.subscriptionProvisioningLimitedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public TechnicalNotificationEmails TechnicalNotificationEmails
        {
            get
            {
                return this.technicalNotificationEmailsField;
            }
            set
            {
                this.technicalNotificationEmailsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public ulong TelephoneNumber
        {
            get
            {
                return this.telephoneNumberField;
            }
            set
            {
                this.telephoneNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = true)]
        public object UIExtensibilityUris
        {
            get
            {
                return this.uIExtensibilityUrisField;
            }
            set
            {
                this.uIExtensibilityUrisField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool UsersPermissionToCreateGroupsEnabled
        {
            get
            {
                return this.usersPermissionToCreateGroupsEnabledField;
            }
            set
            {
                this.usersPermissionToCreateGroupsEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool UsersPermissionToCreateLOBAppsEnabled
        {
            get
            {
                return this.usersPermissionToCreateLOBAppsEnabledField;
            }
            set
            {
                this.usersPermissionToCreateLOBAppsEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool UsersPermissionToReadOtherUsersEnabled
        {
            get
            {
                return this.usersPermissionToReadOtherUsersEnabledField;
            }
            set
            {
                this.usersPermissionToReadOtherUsersEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public bool UsersPermissionToUserConsentToAppEnabled
        {
            get
            {
                return this.usersPermissionToUserConsentToAppEnabledField;
            }
            set
            {
                this.usersPermissionToUserConsentToAppEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
        public System.DateTime WhenCreated
        {
            get
            {
                return this.whenCreatedField;
            }
            set
            {
                this.whenCreatedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class PortalSettings
    {

        private ApplicationDataRoot applicationDataRootField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public ApplicationDataRoot ApplicationDataRoot
        {
            get
            {
                return this.applicationDataRootField;
            }
            set
            {
                this.applicationDataRootField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ApplicationDataRoot
    {

        private ApplicationDataRootApplicationData applicationDataField;

        /// <remarks/>
        public ApplicationDataRootApplicationData ApplicationData
        {
            get
            {
                return this.applicationDataField;
            }
            set
            {
                this.applicationDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ApplicationDataRootApplicationData
    {

        private ApplicationDataRootApplicationDataFD fdField;

        private string keyField;

        /// <remarks/>
        public ApplicationDataRootApplicationDataFD fd
        {
            get
            {
                return this.fdField;
            }
            set
            {
                this.fdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ApplicationDataRootApplicationDataFD
    {

        private string tenantIdField;

        private string pidField;

        private bool auField;

        private sbyte cpField;

        private byte fiField;

        private byte ftField;

        /// <remarks/>
        public string TenantId
        {
            get
            {
                return this.tenantIdField;
            }
            set
            {
                this.tenantIdField = value;
            }
        }

        /// <remarks/>
        public string pid
        {
            get
            {
                return this.pidField;
            }
            set
            {
                this.pidField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool au
        {
            get
            {
                return this.auField;
            }
            set
            {
                this.auField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sbyte cp
        {
            get
            {
                return this.cpField;
            }
            set
            {
                this.cpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte fi
        {
            get
            {
                return this.fiField;
            }
            set
            {
                this.fiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ft
        {
            get
            {
                return this.ftField;
            }
            set
            {
                this.ftField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    public partial class ServiceInformationServiceInformation
    {

        private ServiceInformationServiceInformationServiceElements serviceElementsField;

        private string serviceInstanceField;

        /// <remarks/>
        public ServiceInformationServiceInformationServiceElements ServiceElements
        {
            get
            {
                return this.serviceElementsField;
            }
            set
            {
                this.serviceElementsField = value;
            }
        }

        /// <remarks/>
        public string ServiceInstance
        {
            get
            {
                return this.serviceInstanceField;
            }
            set
            {
                this.serviceInstanceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    public partial class ServiceInformationServiceInformationServiceElements
    {

        private XElement xElementField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.datacontract.org/2004/07/System.Xml.Linq")]
        public XElement XElement
        {
            get
            {
                return this.xElementField;
            }
            set
            {
                this.xElementField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/System.Xml.Linq")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/System.Xml.Linq", IsNullable = false)]
    public partial class XElement
    {

        private Topology topologyField;

        private RmsCompanyServiceInfo rmsCompanyServiceInfoField;

        private WindowsIntuneServiceInfo windowsIntuneServiceInfoField;

        private ServiceExtension serviceExtensionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public Topology Topology
        {
            get
            {
                return this.topologyField;
            }
            set
            {
                this.topologyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public RmsCompanyServiceInfo RmsCompanyServiceInfo
        {
            get
            {
                return this.rmsCompanyServiceInfoField;
            }
            set
            {
                this.rmsCompanyServiceInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
        public WindowsIntuneServiceInfo WindowsIntuneServiceInfo
        {
            get
            {
                return this.windowsIntuneServiceInfoField;
            }
            set
            {
                this.windowsIntuneServiceInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema" +
            ".xsd")]
        public ServiceExtension ServiceExtension
        {
            get
            {
                return this.serviceExtensionField;
            }
            set
            {
                this.serviceExtensionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Topology
    {

        private byte ringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Ring
        {
            get
            {
                return this.ringField;
            }
            set
            {
                this.ringField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class RmsCompanyServiceInfo
    {

        private RmsCompanyServiceInfoServiceLocation[] serviceLocationsField;

        private string serviceDataVersionField;

        private byte configVersionField;

        private bool aADRMEnabledField;

        private bool docTrackingEnabledField;

        private bool adHocEnabledField;

        private bool basicPlanEnabledField;

        private string subscriptionStatusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ServiceLocation", IsNullable = false)]
        public RmsCompanyServiceInfoServiceLocation[] ServiceLocations
        {
            get
            {
                return this.serviceLocationsField;
            }
            set
            {
                this.serviceLocationsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ServiceDataVersion
        {
            get
            {
                return this.serviceDataVersionField;
            }
            set
            {
                this.serviceDataVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ConfigVersion
        {
            get
            {
                return this.configVersionField;
            }
            set
            {
                this.configVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool AADRMEnabled
        {
            get
            {
                return this.aADRMEnabledField;
            }
            set
            {
                this.aADRMEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool DocTrackingEnabled
        {
            get
            {
                return this.docTrackingEnabledField;
            }
            set
            {
                this.docTrackingEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool AdHocEnabled
        {
            get
            {
                return this.adHocEnabledField;
            }
            set
            {
                this.adHocEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool BasicPlanEnabled
        {
            get
            {
                return this.basicPlanEnabledField;
            }
            set
            {
                this.basicPlanEnabledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SubscriptionStatus
        {
            get
            {
                return this.subscriptionStatusField;
            }
            set
            {
                this.subscriptionStatusField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class RmsCompanyServiceInfoServiceLocation
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class WindowsIntuneServiceInfo
    {

        private WindowsIntuneServiceInfoServiceParameters serviceParametersField;

        /// <remarks/>
        public WindowsIntuneServiceInfoServiceParameters ServiceParameters
        {
            get
            {
                return this.serviceParametersField;
            }
            set
            {
                this.serviceParametersField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class WindowsIntuneServiceInfoServiceParameters
    {

        private WindowsIntuneServiceInfoServiceParametersServiceParameter serviceParameterField;

        /// <remarks/>
        public WindowsIntuneServiceInfoServiceParametersServiceParameter ServiceParameter
        {
            get
            {
                return this.serviceParameterField;
            }
            set
            {
                this.serviceParameterField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class WindowsIntuneServiceInfoServiceParametersServiceParameter
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema" +
        ".xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema" +
        ".xsd", IsNullable = false)]
    public partial class ServiceExtension
    {

        private ServiceExtensionServiceParameters serviceParametersField;

        private ServiceExtensionDNSRecord[] dNSRecordsField;

        /// <remarks/>
        public ServiceExtensionServiceParameters ServiceParameters
        {
            get
            {
                return this.serviceParametersField;
            }
            set
            {
                this.serviceParametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DNSRecord", IsNullable = false)]
        public ServiceExtensionDNSRecord[] DNSRecords
        {
            get
            {
                return this.dNSRecordsField;
            }
            set
            {
                this.dNSRecordsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema" +
        ".xsd")]
    public partial class ServiceExtensionServiceParameters
    {

        private ServiceExtensionServiceParametersServiceParameter[] serviceParameterField;

        private string cachePolicyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ServiceParameter")]
        public ServiceExtensionServiceParametersServiceParameter[] ServiceParameter
        {
            get
            {
                return this.serviceParameterField;
            }
            set
            {
                this.serviceParameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CachePolicy
        {
            get
            {
                return this.cachePolicyField;
            }
            set
            {
                this.cachePolicyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema" +
        ".xsd")]
    public partial class ServiceExtensionServiceParametersServiceParameter
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/online/serviceextensions/2009/08/ExtensibilitySchema" +
        ".xsd")]
    public partial class ServiceExtensionDNSRecord
    {

        private string recordTypeField;

        private string targetField;

        private string sharePointOriginalDomainField;

        /// <remarks/>
        public string RecordType
        {
            get
            {
                return this.recordTypeField;
            }
            set
            {
                this.recordTypeField = value;
            }
        }

        /// <remarks/>
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SharePointOriginalDomain
        {
            get
            {
                return this.sharePointOriginalDomainField;
            }
            set
            {
                this.sharePointOriginalDomainField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    public partial class ServiceInstanceInformationServiceInstanceInformation
    {

        private ServiceInstanceInformationServiceInstanceInformationGeographicLocation geographicLocationField;

        private string serviceInstanceField;

        private ServiceInstanceInformationServiceInstanceInformationServiceEndpoint[] serviceInstanceEndpointsField;

        /// <remarks/>
        public ServiceInstanceInformationServiceInstanceInformationGeographicLocation GeographicLocation
        {
            get
            {
                return this.geographicLocationField;
            }
            set
            {
                this.geographicLocationField = value;
            }
        }

        /// <remarks/>
        public string ServiceInstance
        {
            get
            {
                return this.serviceInstanceField;
            }
            set
            {
                this.serviceInstanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("ServiceEndpoint", IsNullable = false)]
        public ServiceInstanceInformationServiceInstanceInformationServiceEndpoint[] ServiceInstanceEndpoints
        {
            get
            {
                return this.serviceInstanceEndpointsField;
            }
            set
            {
                this.serviceInstanceEndpointsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    public partial class ServiceInstanceInformationServiceInstanceInformationGeographicLocation
    {

        private string countryField;

        private string regionField;

        private object stateField;

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public string Region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public object State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    public partial class ServiceInstanceInformationServiceInstanceInformationServiceEndpoint
    {

        private string addressField;

        private string nameField;

        /// <remarks/>
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class TechnicalNotificationEmails
    {

        private string stringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string @string
        {
            get
            {
                return this.stringField;
            }
            set
            {
                this.stringField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class AuthorizedServiceInstances
    {

        private string[] stringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] @string
        {
            get
            {
                return this.stringField;
            }
            set
            {
                this.stringField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class CompanyTags
    {

        private string[] stringField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("string", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
        public string[] @string
        {
            get
            {
                return this.stringField;
            }
            set
            {
                this.stringField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class ServiceInformation
    {

        private ServiceInformationServiceInformation[] serviceInformation1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ServiceInformation")]
        public ServiceInformationServiceInformation[] ServiceInformation1
        {
            get
            {
                return this.serviceInformation1Field;
            }
            set
            {
                this.serviceInformation1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration", IsNullable = false)]
    public partial class ServiceInstanceInformation
    {

        private ServiceInstanceInformationServiceInstanceInformation[] serviceInstanceInformation1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ServiceInstanceInformation")]
        public ServiceInstanceInformationServiceInstanceInformation[] ServiceInstanceInformation1
        {
            get
            {
                return this.serviceInstanceInformation1Field;
            }
            set
            {
                this.serviceInstanceInformation1Field = value;
            }
        }
    }



}
