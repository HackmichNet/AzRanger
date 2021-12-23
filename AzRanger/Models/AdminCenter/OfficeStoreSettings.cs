
namespace AzRanger.Models.AdminCenter
{
    public class OfficeStoreSettings
    {
        public bool LetUserAccessTheOfficeStore;
        public bool LetUserStartTrialsInBehalfOfTheOrg;
        public bool LetUserAutoClaimLicencis;

        public OfficeStoreSettings (bool LetUserAccessTheOfficeStore, bool LetUserStartTrialsInBehalfOfTheOrg, bool LetUserAutoClaimLicencis)
        {
            this.LetUserAccessTheOfficeStore = LetUserAccessTheOfficeStore;
            this.LetUserStartTrialsInBehalfOfTheOrg = LetUserStartTrialsInBehalfOfTheOrg;
            this.LetUserAutoClaimLicencis = LetUserAutoClaimLicencis;
        }
    }
}
