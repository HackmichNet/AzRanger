namespace AzRanger.Models.MainIAM
{
    public class TenantSkuInfo
    {
        public bool unauthorized { get; set; }
        public bool aadPremiumBasic { get; set; }
        public bool aadPremium { get; set; }
        public bool aadPremiumP2 { get; set; }
        public bool aadBasic { get; set; }
        public bool aadBasicEdu { get; set; }
        public bool aadSmb { get; set; }
        public bool enterprisePackE3 { get; set; }
        public bool enterprisePremiumE5 { get; set; }
    }

}
