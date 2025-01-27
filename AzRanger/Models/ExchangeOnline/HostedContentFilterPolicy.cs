using AzRanger.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.ExchangeOnline
{
    public class HostedContentFilterPolicy : IReporting
    {
        public string AdminDisplayName { get; set; }
        public string AddXHeaderValue { get; set; }
        public string ModifySubjectValue { get; set; }
        public string RedirectToRecipientsodatatype { get; set; }
        public object[] RedirectToRecipients { get; set; }
        public string TestModeBccToRecipientsodatatype { get; set; }
        public object[] TestModeBccToRecipients { get; set; }
        public string FalsePositiveAdditionalRecipientsodatatype { get; set; }
        public object[] FalsePositiveAdditionalRecipients { get; set; }
        public string QuarantineRetentionPerioddatatype { get; set; }
        public int QuarantineRetentionPeriod { get; set; }
        public string TestModeAction { get; set; }
        public string IncreaseScoreWithImageLinks { get; set; }
        public string IncreaseScoreWithNumericIps { get; set; }
        public string IncreaseScoreWithRedirectToOtherPort { get; set; }
        public string IncreaseScoreWithBizOrInfoUrls { get; set; }
        public string MarkAsSpamEmptyMessages { get; set; }
        public string MarkAsSpamJavaScriptInHtml { get; set; }
        public string MarkAsSpamFramesInHtml { get; set; }
        public string MarkAsSpamObjectTagsInHtml { get; set; }
        public string MarkAsSpamEmbedTagsInHtml { get; set; }
        public string MarkAsSpamFormTagsInHtml { get; set; }
        public string MarkAsSpamWebBugsInHtml { get; set; }
        public string MarkAsSpamSensitiveWordList { get; set; }
        public string MarkAsSpamSpfRecordHardFail { get; set; }
        public string MarkAsSpamFromAddressAuthFail { get; set; }
        public string MarkAsSpamBulkMail { get; set; }
        public string MarkAsSpamNdrBackscatter { get; set; }
        public bool IsDefault { get; set; }
        public string LanguageBlockListodatatype { get; set; }
        public object[] LanguageBlockList { get; set; }
        public string RegionBlockListodatatype { get; set; }
        public object[] RegionBlockList { get; set; }
        public string HighConfidenceSpamAction { get; set; }
        public string SpamAction { get; set; }
        public bool DownloadLink { get; set; }
        public bool EnableRegionBlockList { get; set; }
        public bool EnableLanguageBlockList { get; set; }
        public string BulkThresholddatatype { get; set; }
        public int BulkThreshold { get; set; }
        public string AllowedSendersodatatype { get; set; }
        public string[] AllowedSenders { get; set; }
        public string AllowedSenderDomainsodatatype { get; set; }
        public string[] AllowedSenderDomains { get; set; }
        public string BlockedSendersodatatype { get; set; }
        public object[] BlockedSenders { get; set; }
        public string BlockedSenderDomainsodatatype { get; set; }
        public object[] BlockedSenderDomains { get; set; }
        public bool ZapEnabled { get; set; }
        public bool InlineSafetyTipsEnabled { get; set; }
        public string BulkSpamAction { get; set; }
        public string PhishSpamAction { get; set; }
        public bool SpamZapEnabled { get; set; }
        public bool PhishZapEnabled { get; set; }
        public string IntraOrgFilterState { get; set; }
        public string HighConfidencePhishAction { get; set; }
        public string RecommendedPolicyType { get; set; }
        public string SpamQuarantineTag { get; set; }
        public string HighConfidenceSpamQuarantineTag { get; set; }
        public string PhishQuarantineTag { get; set; }
        public string HighConfidencePhishQuarantineTag { get; set; }
        public string BulkQuarantineTag { get; set; }
        public string Identity { get; set; }
        public string Id { get; set; }
        public bool IsValid { get; set; }
        public string ExchangeVersion { get; set; }
        public string Name { get; set; }
        public string DistinguishedName { get; set; }
        public string ObjectCategory { get; set; }
        public string ObjectClassodatatype { get; set; }
        public string[] ObjectClass { get; set; }
        public string WhenChangeddatatype { get; set; }
        public DateTime WhenChanged { get; set; }
        public string WhenCreateddatatype { get; set; }
        public DateTime WhenCreated { get; set; }
        public string WhenChangedUTCdatatype { get; set; }
        public DateTime WhenChangedUTC { get; set; }
        public string WhenCreatedUTCdatatype { get; set; }
        public DateTime WhenCreatedUTC { get; set; }
        public string ExchangeObjectIddatatype { get; set; }
        public string ExchangeObjectIdodatatype { get; set; }
        public string ExchangeObjectId { get; set; }
        public string OrganizationalUnitRoot { get; set; }
        public string OrganizationId { get; set; }
        public string Guiddatatype { get; set; }
        public string Guidodatatype { get; set; }
        public string Guid { get; set; }
        public string OriginatingServer { get; set; }

        public AffectedItem GetAffectedItem()
        {
            return new AffectedItem(this.Identity, this.Guid);
        }

        public string PrintConsole()
        {
            return String.Format("{0} - {1}", this.Identity, this.Guid);
        }

        public string PrintCSV()
        {
            return String.Format("{0};{1}", this.Identity, this.Guid);
        }
    }


}
