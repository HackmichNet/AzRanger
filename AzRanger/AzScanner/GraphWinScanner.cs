using AzRanger.Models.WinGraph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public class GraphWinScanner : IScannerModule
    {
        public const String UsersInternal = "/{0}/users/{1}";
        public const String RoleDefinitions = "/myorganization/roleDefinitions";

        public GraphWinScanner(Scanner scanner)
        {
            this.Scanner = scanner;
            this.BaseAdresse = "https://graph.windows.net";
            this.Scope = new string[] { "https://graph.windows.net/.default", "offline_access" };
            this.client = Helper.GetDefaultClient(additionalHeaders, this.Scanner.Proxy);
        }

        public Task<List<RoleDefinition>> GetRoleDefinitons()
        {
            return GetAllOf<RoleDefinition>(RoleDefinitions, "?api-version=1.61-internal&$select=objectId,displayName,isBuilt,InisEnabled");
        }
        public Task<StrongAuthenticationDetail> GetStrongAuthenticationDetail(Guid objectId)
        {
            String endPoint = string.Format(UsersInternal, this.Scanner.TenantId, objectId);
            return Get<StrongAuthenticationDetail>(endPoint, "?api-version=1.61-internal&$select=strongAuthenticationDetail,objectId");
        }

        internal async override Task<List<T>> GetAllOf<T>(string endPoint, string query = null, List<Tuple<string, string>> additionalHeaders = null)
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
            /// Create the result list
            List<T> resultList = new List<T>();
            string url = BaseAdresse + usedEndpoint;
            while (url != null)
            {

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    /// Parse the result in GenericObjects
                    var result = await response.Content.ReadAsStringAsync();
                    WinGraphGernericResponse genericAnswer = JsonSerializer.Deserialize<WinGraphGernericResponse>(result);
                    logger.Debug("GraphWinScanner.GetAllOf: {0} elements in response", genericAnswer.value.Length);

                    /// Go throuththe geneirc object and parse the value field
                    foreach (var entry in genericAnswer.value)
                    {
                        try
                        {
                            var resultParsed = JsonSerializer.Deserialize<T>(entry.ToString());
                            resultList.Add(resultParsed);
                        }
                        catch (Exception e)
                        {
                            logger.Debug("IScanner.GetAllOf: DeserializationFailed");
                            logger.Debug(e.Message);
                            logger.Debug(entry.ToString());
                            return null;
                        }
                    }

                    // If the generic Answer has a nextLink, we have more items then responded in the first answer
                    if (genericAnswer.odatanextLink != null)
                    {
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
                        logger.Debug("GraphWinScanner.GetAllOf: {0}|{1} was not successfull", typeof(T).ToString(), usedEndpoint);
                        logger.Debug("GraphWinScanner.GetAllOf: Status Code {0}", response.StatusCode);
                        logger.Debug(response.Content.ReadAsStringAsync().Result);
                        return null;
                    }
                    catch {
                        return null;
                    }
                }

            }
            return resultList;
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
}
