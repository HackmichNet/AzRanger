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
    public abstract class IScannerModule
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        internal Scanner Scanner;
        internal String BaseAdresse;
        internal String[] Scope;
        internal List<Tuple<string, string>> additionalHeaders = null;
        //https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        internal HttpClient client;

        internal async virtual Task<T> Get<T>(String endPoint, string query = null)
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
            string url = BaseAdresse + usedEndpoint;
            logger.Debug("IScanner.Get: {0}|{1}", typeof(T).ToString(), url);
            var response = await client.GetAsync(url);
            String manipulatedResponse = null;
            if (response.IsSuccessStatusCode)
            {
                ArrayList r = new ArrayList();
                var result = await response.Content.ReadAsStringAsync();
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
                    return default;
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
                        
            return default(T);
        }

        internal virtual String ManipulateResponse(String response, String endPoint)
        {
            return response;
        }
        internal async virtual Task<List<T>> GetAllOf<T>(string endPoint, string query = null, List<Tuple<string, string>> additionalHeaders = null)
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
            string url = BaseAdresse + usedEndpoint;
            List<T> resultList = new List<T>();
            while (url != null)
            {
                logger.Debug("IScanner.GetAllOf: {0}|{1}", typeof(T).ToString(), url);
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
            return resultList;
        }
    }
}
