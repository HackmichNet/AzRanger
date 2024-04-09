namespace AzRanger.Models.AdminCenter
{
    public class DirsyncManagement
    {
        public bool IsDirSyncEnabled { get; set; }
        public bool IsFederated { get; set; }
        public bool IsDirSyncNeedUpgrade { get; set; }
        public bool IsRightToLeft { get; set; }
        public bool IsPasswordSyncEnabled { get; set; }
        public bool IsSyncNormal { get; set; }
        public bool IsPasswordSyncNormal { get; set; }
        public bool IsDirSyncRedWarning { get; set; }
        public bool IsPasswordSyncRedWarning { get; set; }
        public bool IsServiceAccountPasswordExpired { get; set; }
        public bool IsServiceAccountPasswordExpiring { get; set; }
        public bool IsDirSyncObjectErrors { get; set; }
        public object DirSyncErrorsText { get; set; }
        public string CompanyName { get; set; }
        public string DirSyncClientVersion { get; set; }
        public string DirSyncClientMachineName { get; set; }
        public int DirSyncLastSyncTimeDiff { get; set; }
        public string DirSyncLastSyncTime { get; set; }
        public string DirSyncLastSyncTimeForWidget { get; set; }
        public int PasswordSyncLastSyncTimeDiff { get; set; }
        public string PasswordSyncLastSyncTime { get; set; }
        public string PasswordSyncLastSyncTimeForWidget { get; set; }
        public string DirSyncServiceAccount { get; set; }
        public object IconClass { get; set; }
        public object IconAlt { get; set; }
        public int PasswordExpiringDays { get; set; }
        public int FederatedDomainCount { get; set; }
        public int VerifiedDomainCount { get; set; }
        public int UnverifiedDomainCount { get; set; }
        public Cdnurls CDNUrls { get; set; }
        public string TroubleShootUrl { get; set; }
        public string DirSyncObjectErrorsUrl { get; set; }
        public string LearnMoreUrl { get; set; }
        public bool EnableDirSyncManagementNewPortal { get; set; }
        public bool EnableDirSyncObjectErrorsNewPortal { get; set; }
        public string DownloadAADConnectUrl { get; set; }
        public string LearnMoreAADConnectUrl { get; set; }
        public string DownloadIDFixUrl { get; set; }
        public string LearnMoreIDFixUrl { get; set; }
        public string LearnMoreDomainUrl { get; set; }
        public string DomainsPageUrl { get; set; }
    }

    public class Cdnurls
    {
        public string banner_up_mobile { get; set; }
        public string banner_up { get; set; }
        public string banner_down_mobile { get; set; }
        public string banner_down { get; set; }
    }

}
