using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.Generic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class ExchangeOnlineScanner : IScanner
    {
        private const String MalwareFilterPolicy = "Get-MalwareFilterPolicy";
        private const String MalwareFilterRule = "Get-MalwareFilterRule";
        private const String HostedOutboundSpamFilterPolicy = "Get-HostedOutboundSpamFilterPolicy";
        private const String TransportRule = "Get-TransportRule";
        private const String AcceptedDomain = "Get-AcceptedDomain";
        private const String DkimSigningConfig = "Get-DkimSigningConfig";
        private const String AdminAuditLogConfig = "Get-AdminAuditLogConfig";
        private const String RemoteDomain = "Get-RemoteDomain";
        private const String Mailbox = "Get-Mailbox";
        private const String RoleAssignmentPolicy = "Get-RoleAssignmentPolicy";
        private const String OrganizationConfig = "Get-OrganizationConfig";
        private const String Users = "Get-User";
        private const String AuthenticationPolicy = "Get-AuthenticationPolicy";
        private const String OwaMailboxPolicy = "Get-OwaMailboxPolicy";
        private String EndPoint;

        public ExchangeOnlineScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://outlook.office365.com";
            this.EndPoint = "/adminapi/beta/" + scanner.TenantId + "/InvokeCommand";
            this.Scope = new String[] { "openid", "offline_access", "profile", "https://outlook.office365.com/.default" };
        }

        public object GetExoUsers()
        {
            return GetAllOf<String>(Users);
        }

        public async Task<OrganizationConfig> GetOrganizationConfig()
        {
            List<OrganizationConfig> result = await GetAllOf<OrganizationConfig>(OrganizationConfig);
            if(result == null)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetOrganizationConfig() is null."));
                return null;
            }
            if (result.Count > 1)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetOrganizationConfig() has {0} results.", result.Count));
                return null;
            }
            return result[0];
        }

        public async Task<OwaMailboxPolicy> GetOwaMailboxPolicy()
        {
            List<OwaMailboxPolicy> result = await GetAllOf<OwaMailboxPolicy>(OwaMailboxPolicy);
            if (result == null)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetOwaMailboxPolicy() is null."));
                return null;
            }
            if (result.Count > 1)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetOwaMailboxPolicy() has {0} results.", result.Count));
                return null;
            }
            return result[0];
        }

        public Task<List<EXOUser>> GetEXOUsers()
        {
            return GetAllOf<EXOUser>(Users);
        }

        public Task<List<AuthenticationPolicy>> GetAuthenticationPolicies()
        {
            return GetAllOf<AuthenticationPolicy>(AuthenticationPolicy);
        }

        public Task<List<RemoteDomain>> GetRemoteDomains()
        {
            return GetAllOf<RemoteDomain>(RemoteDomain);
        }

        public Task<List<RoleAssignmentPolicy>> GeRoleAssignmentPolicies()
        {
            return GetAllOf<RoleAssignmentPolicy>(RoleAssignmentPolicy);
        }

        public Task<List<Mailbox>> GetMailboxes()
        {
            List<Tuple<String, String>> parameters = new List<Tuple<String, String>>();
            parameters.Add(new Tuple<String,String>("ResultSize","unlimited"));
            return GetAllOf<Mailbox>(Mailbox, parameters);
        }

        public Task<List<MalwareFilterRule>> GetMalwareFilterRules()
        {
            return GetAllOf<MalwareFilterRule>(MalwareFilterRule);
        }

        public async Task<List<AcceptedDomain>> GetAcceptedDomains()
        {
            List<AcceptedDomain> allDomains = await GetAllOf<AcceptedDomain>(AcceptedDomain);
            if (allDomains == null)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetAcceptedDomains() is null."));
                return null;
            }
            foreach (AcceptedDomain domain in allDomains)
            {
                domain.HasSPF = DNSScanner.hasSPF(domain.DomainName);
                domain.HasDMARC = DNSScanner.hasDMARC(domain.DomainName);
            }
            return allDomains;
        }

        public async Task<AdminAuditLogConfig> GetAdminAuditLogConfig()
        {
            // We should only have one entry
            List<AdminAuditLogConfig> result = await GetAllOf<AdminAuditLogConfig>(AdminAuditLogConfig);
            if (result == null)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetAdminAuditLogConfig() is null."));
                return null;
            }
            if (result.Count > 1)
            {
                logger.Debug(String.Format("ExchangeOnlineScanner.GetAdminAuditLogConfig() has {0} results.", result.Count));
                return null;
            }
            return result[0];
        }

        public Task<List<DkimSigningConfig>> GetDkimSigningConfig()
        {
            return GetAllOf<DkimSigningConfig>(DkimSigningConfig);
        }

        public Task<List<HostedOutboundSpamFilterPolicy>> GetHostedOutboundSpamFilterPolicies()
        {
            return GetAllOf<HostedOutboundSpamFilterPolicy>(HostedOutboundSpamFilterPolicy);
        }

        public Task<List<MalwareFilterPolicy>> GetMalwareFilterPolicies()
        {
            return GetAllOf<MalwareFilterPolicy>(ExchangeOnlineScanner.MalwareFilterPolicy);
        }

        public Task<List<TransportRule>> GetTransportRules()
        {
            return GetAllOf<TransportRule>(ExchangeOnlineScanner.TransportRule);
        }

        internal async Task<List<T>> GetAllOf<T>(string command, List<Tuple<string, string>> parameters = null)
        {
            logger.Debug("ExchangeOnlineScanner.GetAllOf: {0}|{1}", typeof(T).ToString(), command );

            String accessToken = await this.Scanner.Authenticator.GetAccessToken(this.Scope);
            if(accessToken == null)
            {
                logger.Warn("ExchangeOnlineScanner.GetAllOf: Failed fetching access token!");
                return null;
            }
            String url = this.BaseAdresse + this.EndPoint;
            List<T> resultList = new List<T>();
            while (url != null)
            {
                using (var client = Helper.GetDefaultClient(null, this.Scanner.Proxy))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    HttpContent content = this.CreateMessage(command, parameters);
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {

                        /// Parse the result in GenericObjects
                        var result = await response.Content.ReadAsStringAsync();
                        GenResponse genericAnswer = null;
                        try
                        {
                            genericAnswer = JsonSerializer.Deserialize<GenResponse>(result);
                        }
                        catch (Exception e)
                        {
                            logger.Debug("ExchangeOnlineScanner.GetAllOf.GenReponse: Failed to parse response.");
                            logger.Debug(e.Message);
                            logger.Debug(result.ToString());
                            return null;
                        }
                        if (genericAnswer == null)
                        {
                            return null;
                        }
                        logger.Debug("ExchangeOnlineScanner.GetAllOf: Response has {0} entries.", genericAnswer.value.Length);
                        /// Go through the generic object and parse the value field
                        foreach (var entry in genericAnswer.value)
                        {
                            try
                            {
                                var resultParsed = JsonSerializer.Deserialize<T>(entry.ToString());
                                resultList.Add(resultParsed);
                            }
                            catch (Exception e)
                            {
                                logger.Debug("ExchangeOnlineScanner.GetAllOf: Failed to parse response.");
                                logger.Debug(e.Message);
                                logger.Debug(entry.ToString());
                                return null;
                            }
                        }

                        // If the generic Answer has a nextLink, we have more items then responded in the first answer
                        if (genericAnswer.odatanextLink != null)
                        {
                            logger.Debug("ExchangeOnlineScanner.GetAllOf: Odatanextlink is: {0}", genericAnswer.odatanextLink);
                            url = genericAnswer.odatanextLink;
                        }
                        else /// No next link anymore, just check if we have some items and concat them with the current result
                        {
                            url = null;
                        }
                    }
                    else
                    {
                        try
                        {
                            logger.Debug("ExchangeOnlineScanner.GetAllOf: {0} was not successfull", typeof(T).ToString());
                            logger.Debug("ExchangeOnlineScanner.GetAllOf: Status Code {0}", response.StatusCode);
                            logger.Debug(await response.Content.ReadAsStringAsync());
                        }
                        catch { }
                    }
                }
            }
            return resultList;
        }

        private HttpContent CreateMessage(string command, List<Tuple<string, string>> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return new StringContent(String.Format(@"{{""CmdletInput"":{{""CmdletName"":""{0}"",""Parameters"":{{ }} }} }}", command), Encoding.UTF8, "application/json");
            }
            else
            {
                String commandString = String.Format(@"{{""CmdletInput"":{{""CmdletName"":""{0}"",""Parameters"":{{", command);
                foreach(Tuple<String, String> tuple in parameters)
                {
                    commandString = commandString + String.Format(@" ""{0}"":""{1}"",", tuple.Item1, tuple.Item2);
                }
                // Remove last comma
                commandString = commandString.Substring(0, commandString.Length - 1);
                commandString = commandString + "} } }";
                return new StringContent(commandString, Encoding.UTF8, "application/json");
            }
        }
    }
}
