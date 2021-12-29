using AzRanger.Models.WinGraph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzRanger.AzScanner
{
    public class GraphWinScanner : IScanner
    {
        public const String UsersInternal = "/{0}/users/{1}";
        public const String RoleDefinitions = "/myorganization/roleDefinitions";

        public GraphWinScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://graph.windows.net";
            this.Scope = new string[] { "https://graph.windows.net/.default", "offline_access" };
        }

        public List<RoleDefinition> GetRoleDefinitons()
        {
            return GetAllOf<RoleDefinition>(RoleDefinitions, "?api-version=1.61-internal&$select=objectId,displayName,isBuilt,InisEnabled");
        }

        public bool HasMFA(Guid objectId)
        {
            String endPoint = string.Format(UsersInternal, this.Scanner.TenantId, objectId);
            StrongAuthenticationDetailAndObjectId details = (StrongAuthenticationDetailAndObjectId)Get<StrongAuthenticationDetailAndObjectId>(endPoint, "?api-version=1.61-internal&$select=objectId,strongAuthenticationDetail");
            if (details == null)
            {
                return false;
            }
            if(details.strongAuthenticationDetail.methods != null && details.strongAuthenticationDetail.methods.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal override List<T> GetAllOf<T>(AuthenticationHeaderValue authenticationHeader, string endPoint, string query = null, List<T> alreadyCollectedItems = null, List<Tuple<string, string>> additionalHeaders = null)
        {
            string usedEndpoint = endPoint;
            if (query != null)
            {
                if (query.StartsWith("?"))
                {
                    usedEndpoint = endPoint + query;
                }
                else
                {
                    usedEndpoint = endPoint + "?" + query;
                }
            }
            logger.Debug("GraphWinScanner.GetAllOf: {0}|{1}", typeof(T).ToString(), usedEndpoint);
            using (var client = Helper.GetDefaultClient(BaseAdresse, false, additionalHeaders, this.Scanner.Proxy))
            using (var message = new HttpRequestMessage(HttpMethod.Get, usedEndpoint))
            {
                message.Headers.Authorization = authenticationHeader;
                var response = client.SendAsync(message).Result;
                if (response.IsSuccessStatusCode)
                {
                    /// Create the result list
                    List<T> r = new List<T>();

                    /// Parse the result in GenericObjects
                    var result = response.Content.ReadAsStringAsync().Result;
                    WinGraphGernericResponse genericAnswer = JsonSerializer.Deserialize<WinGraphGernericResponse>(result);
                    logger.Debug("GraphWinScanner.GetAllOf: " + genericAnswer.value.Length + " elements in generic response");

                    /// Go throuththe geneirc object and parse the value field
                    foreach (var entry in genericAnswer.value)
                    {
                        try
                        {
                            var resultParsed = JsonSerializer.Deserialize<T>(entry.ToString());
                            r.Add(resultParsed);
                        }
                        catch (Exception e)
                        {
                            logger.Debug("IScanner.GetAllOf: DeserializationFailed");
                            logger.Debug(e.Message);
                            logger.Debug(entry.ToString());
                            return null;
                        }
                    }

                    /// If the generic Answer has a nextLink, we have more items then responded in the first answer
                    if (genericAnswer.odatanextLink != null)
                    {
                        if (query.Contains("$skiptoken="))
                        {
                            query = query.Split("&$skiptoken")[0];
                        }
                        /// Create the next endpoint => Endpoint + nextLink Attribute
                        string nextLink = genericAnswer.odatanextLink.Split("$skiptoken=")[1];
                        string newQuery = query + "&$skiptoken=" + nextLink;

                        /// If the function was called already, we hand over some values
                        if (alreadyCollectedItems != null)
                        {
                            foreach (var item in alreadyCollectedItems)
                            {
                                r.Add(item);
                            }
                        }

                        /// Call the function again with the already collected items and the nextLink
                        return GetAllOf<T>(authenticationHeader, endPoint, newQuery, r, additionalHeaders);
                    }
                    else /// No next link anymore, just check if we have some items and concat them with the current result
                    {
                        if (alreadyCollectedItems != null)
                        {
                            foreach (var item in alreadyCollectedItems)
                            {
                                r.Add(item);
                            }
                        }
                    }
                    return r;
                }
                else
                {
                    logger.Debug("GraphWinScanner.GetAllOf: {0}|{1} was not successfull", typeof(T).ToString(), usedEndpoint);
                    logger.Debug("GraphWinScanner.GetAllOf: Status Code {0}", response.StatusCode);
                    logger.Debug(response.Content.ReadAsStringAsync().Result);  
                }
            }
            return null;
        }
    }


    public class WinGraphGernericResponse
    {
        [JsonPropertyName("odata.metadata")]
        public string odatametadata { get; set; }
        [JsonPropertyName("odata.nextLink")]
        public string odatanextLink { get; set; }
        public object[] value { get; set; }
    }


    public class StrongAuthenticationDetailAndObjectId
    {
        [JsonPropertyName("odata.type")]
        public string odatatype { get; set; }
        public Guid objectId { get; set; }
        public StrongAuthenticationDetail strongAuthenticationDetail { get; set; }
    }

   
    public class StrongAuthenticationDetail
    {
        public object encryptedPinHash { get; set; }
        public object encryptedPinHashHistory { get; set; }
        public Method[] methods { get; set; }
        public Oathtokenmetadata[] oathTokenMetadata { get; set; }
        public object[] requirements { get; set; }
        public Phoneappdetail[] phoneAppDetails { get; set; }
        public object proofupTime { get; set; }
        public object verificationDetail { get; set; }
    }

    public class Method
    {
        public string methodType { get; set; }
        public bool isDefault { get; set; }
    }

    public class Oathtokenmetadata
    {
        public string id { get; set; }
        public object enabled { get; set; }
        public string tokenType { get; set; }
        public string manufacturer { get; set; }
        public object[] manufacturerProperties { get; set; }
        public string serialNumber { get; set; }
    }
}
