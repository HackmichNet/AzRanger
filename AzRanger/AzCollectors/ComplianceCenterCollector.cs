using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    class ComplianceCenterCollector : AbstractCollector
    {
        public const String DLPPolicies = "/Psws/service.svc/DlpCompliancePolicy";
        public const String DLPLabels = "/Psws/service.svc/Label";
        public const String PowerShellLiveId = "/Powershell-LiveId";
        public const String InitBaseAddress = "https://ps.compliance.protection.outlook.com";
        public ComplianceCenterCollector(IAuthenticator authenticator, String tenantId, String proxy)
        {
            this.Authenticator = authenticator;
            this.TenantId = tenantId;
            this.Scope = new String[] { "https://ps.compliance.protection.outlook.com/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(this.additionalHeaders, proxy);
        }

        public Task<List<DlpCompliancePolicy>> GetDLPPolicies()
        {
            if(BaseAddress == null) return null;
            return GetAllOf<DlpCompliancePolicy>(DLPPolicies, null, null);
        }

        public Task<List<DlpLabel>> GetDLPLabels()
        {
            if (BaseAddress == null) return null;
            return GetAllOf<DlpLabel>(DLPLabels, null, null);
        }

        public async Task<String> GetBaseAddress()
        {
            String accessToken = await this.Authenticator.GetAccessToken(this.Scope);
            if (accessToken == null)
            {
                return null;
            }
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string url = InitBaseAddress + PowerShellLiveId;
            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.Redirect)
            {
                logger.Debug("ComplianceCenterScanner.GetBaseAddress: Get base Url: {0}", response.Headers.Location.ToString());
                if (response.Headers.Location.ToString().Contains(PowerShellLiveId))
                {
                    String[] seperator = new string[] { PowerShellLiveId };
                    return response.Headers.Location.ToString().Split(seperator, StringSplitOptions.None)[0];
                }
                logger.Warn("ComplianceCenterScanner.GetBaseAddress: Failed to get Redirect URL!");
                return null;
            }
            else
            {
                logger.Warn("ComplianceCenterScanner.GetBaseAddress: Failed getting base url");
                logger.Debug("ComplianceCenterScanner.GetBaseAddress: Status code: ", (int)response.StatusCode);
            }  
            return null;
        }
    }
}
