
namespace AzRanger.Models.MainIAM
{
    public class ADConnectStatus
    {
        public int verifiedDomainCount { get; set; }
        public int verifiedCustomDomainCount { get; set; }
        public int federatedDomainCount { get; set; }
        public object numberOfHoursFromLastSync { get; set; }
        public bool dirSyncEnabled { get; set; }
        public bool dirSyncConfigured { get; set; }
        public object passThroughAuthenticationEnabled { get; set; }
        public object seamlessSingleSignOnEnabled { get; set; }
        public bool passwordHashSyncEnabled { get; set; }
    }

}
