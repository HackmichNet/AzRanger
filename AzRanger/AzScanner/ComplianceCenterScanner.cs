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
    class ComplianceCenterScanner : IScanner
    {
        public const String DLPPolicies = "/Psws/service.svc/DlpCompliancePolicy";
        public const String DLPLabels = "/Psws/service.svc/Label";
        public const String PowerShellLiveId = "/Powershell-LiveId";
        public const String InitBaseAdress = "https://ps.compliance.protection.outlook.com";
        public ComplianceCenterScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = GetBaseAddress().Result;

            this.ClientID = "1b730954-1685-4b74-9bfd-dac224a7b894";
            this.Scope = new String[] { "https://ps.compliance.protection.outlook.com/.default", "offline_access", "openid", "profile"};
        }

        public Task<List<DlpCompliancePolicy>> GetDLPPolicies()
        {
            if(BaseAdresse == null) return null;
            return GetAllOf<DlpCompliancePolicy>(DLPPolicies, null, null);
        }

        public Task<List<DlpLabel>> GetDLPLabels()
        {
            if (BaseAdresse == null) return null;
            return GetAllOf<DlpLabel>(DLPLabels, null, null);
        }

        public async Task<String> GetBaseAddress()
        {
            String accessToken = await this.Scanner.Authenticator.GetAccessToken(this.Scope);
            if (accessToken == null)
            {
                logger.Warn("ComplianceCenterScanner.GetBaseAddress: {0} failed to get token!", this.Scope.ToString());
                return null;
            }
            AuthenticationHeaderValue authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
            string url = InitBaseAdress + PowerShellLiveId;
            using (var client = Helper.GetDefaultClient(null, this.Scanner.Proxy))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
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
                    logger.Debug("ComplianceCenterScanner.GetBaseAddress: Statuscode: ", (int)response.StatusCode);
                }
            }
            return null;
        }
    }
}
