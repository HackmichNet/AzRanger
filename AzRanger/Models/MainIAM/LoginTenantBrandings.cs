using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.MainIAM
{    public class LoginTenantBranding
    {
        public string locale { get; set; }
        public string signInUserIdLabel { get; set; }
        public string signInPageText { get; set; }
        public object signInPageTextHeading { get; set; }
        public string signInBackColor { get; set; }
        public object bannerLogoUrl { get; set; }
        public object squareLogoDarkUrl { get; set; }
        public object tileLogoUrl { get; set; }
        public object illustrationUrl { get; set; }
        public object hideKeepMeSignedIn { get; set; }
        public object postSignoutUrl { get; set; }
        public object postSignoutUrlText { get; set; }
        public string localeDisplayName { get; set; }
        public bool isIllustrationImageUpdated { get; set; }
        public bool isBannerLogoUpdated { get; set; }
        public bool isTileLogoUpdated { get; set; }
        public bool isSquareDarkLogoUpdated { get; set; }
        public bool isConfigured { get; set; }
    }
}
