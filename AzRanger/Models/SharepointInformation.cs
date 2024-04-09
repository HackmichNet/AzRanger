using AzRanger.Models.SharePoint;
using System;


namespace AzRanger.Models
{
    public class SharePointInformation
    {
        public String AdminUrl;
        public String SharePointUrl;
        public SPOInternalUseOnly SharePointInternalInfos;

        public SharePointInformation(string AdminUrl, String SharePointUrl)
        {
            this.AdminUrl = AdminUrl;
            this.SharePointUrl = SharePointUrl;
        }
    }
}
