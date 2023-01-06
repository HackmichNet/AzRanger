using AzRanger.Models.Generic;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AzRanger.AzScanner
{
    public abstract class IScanner
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        internal Scanner Scanner;
        internal String BaseAdresse;
        internal String[] Scope;
        internal String ClientID;
        public String Domain { get; }
        public String Tenant { get; set; }
        
        internal List<Tuple<string, string>> additionalHeaders;


        internal virtual object Get<T>(string endPoint, string query = null)
        {

        String accessToken = this.Scanner.Authenticator.GetAccessToken(this.Scope);      
        if (accessToken == null)
        {
            logger.Warn("IScanner.Get: Failed to authenticate for " + this.Scope.ToString());
            return null;
        }
        AuthenticationHeaderValue authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
        return Get<T>(authenticationHeader, endPoint, query);
        }

        internal virtual object Get<T>(AuthenticationHeaderValue authenticationHeader, String endPoint, string query = null)
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
            logger.Debug("IScanner.Get: {0}|{1}", typeof(T).ToString(), usedEndpoint);

            using (var client = Helper.GetDefaultClient(BaseAdresse, false, additionalHeaders, this.Scanner.Proxy))
            using (var message = new HttpRequestMessage(HttpMethod.Get, usedEndpoint))
            {

                message.Headers.Authorization = authenticationHeader;
                var response = client.SendAsync(message).Result;
                String manipulatedResponse = null;
                if (response.IsSuccessStatusCode)
                {
                    ArrayList r = new ArrayList();
                    var result = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        manipulatedResponse = this.ManipulateResponse(result, endPoint);
                        return JsonSerializer.Deserialize<T>(manipulatedResponse);
                    }
                    catch (Exception e)
                    {
                        logger.Debug("IScanner.Get: DeserializationFailed");
                        logger.Debug(e.Message);
                        logger.Debug(manipulatedResponse);
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        logger.Debug("IScanner.Get: {0}|{1} was not successfull", typeof(T).ToString(), usedEndpoint);
                        logger.Debug("IScanner.Get: Status Code {0}", response.StatusCode);
                        logger.Debug(response.Content.ReadAsStringAsync().Result);
                    }
                    catch { }
                }
            }            
            return null;
        }

        internal virtual String ManipulateResponse(String response, String endPoint)
        {
            return response;
        }


        internal virtual List<T> GetAllOf<T>(string endPoint, string query = null, List<T> alreadyCollectedItems = null)
        {
            String accessToken = this.Scanner.Authenticator.GetAccessToken(this.Scope);
            if(accessToken == null)
            {
                logger.Warn("IScanner.GetAllOf: {0}|{1} failed to get token!", typeof(T).ToString(), this.Scope.ToString());
                return null;
            }
            AuthenticationHeaderValue authenticationHeader = new AuthenticationHeaderValue("Bearer", accessToken);
            return GetAllOf<T>(authenticationHeader, endPoint, query, alreadyCollectedItems, additionalHeaders);
        }

        internal virtual List<T> GetAllOf<T>(AuthenticationHeaderValue authenticationHeader, string endPoint, string query = null, List<T> alreadyCollectedItems = null, List<Tuple<string, string>> additionalHeaders = null)
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
            logger.Debug("IScanner.GetAllOf: {0}|{1}", typeof(T).ToString(), usedEndpoint);
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
                    result = ManipulateResponse(result, endPoint);
                    GenResponse genericAnswer = JsonSerializer.Deserialize<GenResponse>(result);
                    logger.Debug("IScanner.GetAllOf: {0} elements in response", genericAnswer.value.Length);

                    /// Go throuththe generic object and parse the value field
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
                        }
                    }

                    // If the generic Answer has a nextLink, we have more items then responded in the first answer
                    if (genericAnswer.odatanextLink != null)
                    {
                        // Create the next endpoint => Endpoint + nextLink Attribute
                        string[] seperator = new string[] { endPoint };  
                        string NewQuery = genericAnswer.odatanextLink.Split(seperator, StringSplitOptions.None)[1];

                        /// If the function was called already, we hand over some values
                        if (alreadyCollectedItems != null)
                        {
                            foreach (var item in alreadyCollectedItems)
                            {
                                r.Add(item);
                            }
                        }

                        /// Call the function again with the already collected items and the nextLink
                        return GetAllOf<T>(authenticationHeader, endPoint, NewQuery, r, additionalHeaders);
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
                    logger.Debug("IScanner.GetAllOf: {0}|{1} was not successfull", typeof(T).ToString(), usedEndpoint);
                    logger.Debug("IScanner.GetAllOf: Status Code {0}", response.StatusCode);
                    try
                    {
                        logger.Debug(response.Content.ReadAsStringAsync().Result);
                    }
                    catch (Exception) { }
                    
                }
            }
            return null;
        }
    }
}
