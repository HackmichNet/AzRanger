using AzRanger.Models.Generic;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

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

        internal async virtual Task<T> Get<T>(String endPoint, string query = null)
        {
            String accessToken = await this.Scanner.Authenticator.GetAccessToken(this.Scope);
            if (accessToken == null)
            {
                logger.Warn("IScanner.Get: {0}|{1} failed to get token!", typeof(T).ToString(), this.Scope.ToString());
                return default(T);
            }
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
            string url = BaseAdresse + usedEndpoint;
            logger.Debug("IScanner.Get: {0}|{1}", typeof(T).ToString(), url);

            using (var client = Helper.GetDefaultClient(additionalHeaders, this.Scanner.Proxy))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);
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
                        return default(T);
                    }
                }
                else
                {
                    try
                    {
                        logger.Debug("IScanner.Get: {0}|{1} was not successfull", typeof(T).ToString(), url);
                        logger.Debug("IScanner.Get: Status Code {0}", response.StatusCode);
                        logger.Debug(await response.Content.ReadAsStringAsync());
                    }
                    catch { }
                }
            }            
            return default(T);
        }

        internal virtual String ManipulateResponse(String response, String endPoint)
        {
            return response;
        }
        internal async virtual Task<List<T>> GetAllOf<T>(string endPoint, string query = null, List<Tuple<string, string>> additionalHeaders = null)
        {
            String accessToken = await this.Scanner.Authenticator.GetAccessToken(this.Scope);
            if (accessToken == null)
            {
                logger.Warn("IScanner.GetAllOf: {0}|{1} failed to get token!", typeof(T).ToString(), this.Scope.ToString());
                return null;
            }
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
            string url = BaseAdresse + usedEndpoint;
            List<T> resultList = new List<T>();
            while (url != null)
            {
                logger.Debug("IScanner.GetAllOf: {0}|{1}", typeof(T).ToString(), url);
                using (var client = Helper.GetDefaultClient(additionalHeaders, this.Scanner.Proxy))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        /// Parse the result in GenericObjects
                        var result = await response.Content.ReadAsStringAsync();
                        result = ManipulateResponse(result, endPoint);
                        GenResponse genericAnswer = JsonSerializer.Deserialize<GenResponse>(result);
                        logger.Debug("IScanner.GetAllOf: {0} elements in response", genericAnswer.value.Length);

                        /// Go throuththe generic object and parse the value field
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
                            }
                        }

                        // If the generic Answer has a nextLink, we have more items then responded in the first answer
                        if (genericAnswer.odatanextLink != null)
                        {
                            url = genericAnswer.odatanextLink;
                        }
                        else
                        {
                            url = null;
                        }
                    }
                    else
                    {
                        try
                        {
                            logger.Debug("IScanner.GetAllOf: {0}|{1} was not successfull", typeof(T).ToString(), usedEndpoint);
                            logger.Debug("IScanner.GetAllOf: Status Code {0}", response.StatusCode);
                            logger.Debug(await response.Content.ReadAsStringAsync());
                            return null;
                        }
                        catch (Exception) {
                            return null;
                        }
                    }
                }
            }
            return resultList;
        }
    }
}
