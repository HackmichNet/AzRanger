using AzRanger.Models.SharePoint;
using System;


namespace AzRanger.Models
{
    public class SharepointInformation
    {
        public String AdminUrl;
        public String SharepointUrl;
        public SPOInternalUseOnly SharepointInternalInfos; 

        public SharepointInformation(string AdminUrl, String SharepointUrl)
        {
            this.AdminUrl = AdminUrl;
            this.SharepointUrl = SharepointUrl;
        }
    }
}
