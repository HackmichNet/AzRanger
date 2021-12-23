
namespace AzRanger.Models.AdminCenter
{
    public class OfficeFormsSettings
    {
        public bool ExternalCollaborationEnabled { get; set; }
        public bool ExternalSendFormEnabled { get; set; }
        public bool ExternalShareCollaborationEnabled { get; set; }
        public bool ExternalShareTemplateEnabled { get; set; }
        public bool ExternalShareResultEnabled { get; set; }
        public bool RecordIdentityByDefaultEnabled { get; set; }
        public bool BingImageSearchEnabled { get; set; }
        public bool InOrgFormsPhishingScanEnabled { get; set; }
        public object InOrgSurveyIncentiveEnabled { get; set; }
    }

}
