using AzRanger.Models.Generic;
using AzRanger.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace AzRanger
{
    static class Helper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static String GetTenantIdToDomain(string domain, string proxy)
        {
            String result = null;
            result = GetOpenIDConfiguration(domain, proxy);
            if (result == null)
            {
                return null;
            }
            var resultParsed = JsonSerializer.Deserialize<OpenIDConfiguration>(result);
            if (resultParsed.authorization_endpoint != null)
            {
                result = resultParsed.authorization_endpoint.Split('/')[3];
            }
            else
            {
                return null;
            }
            return result;
        }

        public static String ObjectToJson(object obj)
        {
            var options = new JsonSerializerOptions
            {
                MaxDepth = 16,
                IncludeFields = true,
                WriteIndented = true
            };
            return JsonSerializer.Serialize(obj, options);
        }

        internal static string GetOpenIDConfiguration(string domain, string proxy)
        {
            string uri = "/" + domain + "/.well-known/openid-configuration";
            return GetFrom("https://login.microsoftonline.com", uri, proxy);
        }

        internal static string GetFrom(string baseAdress, string uri, String proxy)
        {
            using (var message = new HttpRequestMessage(HttpMethod.Get, baseAdress + uri))
            using (var client = Helper.GetDefaultClient(null, proxy))
            {
                var response = client.SendAsync(message).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        internal static async void PressKeyToContinue(string message = "Press any key to continue...")
        {
            Console.WriteLine(message);
            while (!Console.KeyAvailable)
            {
                await Task.Delay(250);
            }
            Console.ReadKey(true);
        }

        internal static HttpClient GetDefaultClient(List<Tuple<String, String>> additionalHeaders = null, String proxy = null)
        {

            HttpClientHandler handler = new HttpClientHandler();
            if (proxy != null)
            {
                var usedproxy = new WebProxy
                {
                    Address = new Uri($"http://{proxy}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false
                };
                handler.Proxy = usedproxy;
            }

            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            handler.AllowAutoRedirect = false;
            handler.MaxConnectionsPerServer = 25;
            var client = new HttpClient(new RetryHandler(handler));
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; Win64; x64; Trident/7.0; .NET4.0C; .NET4.0E)");
            client.DefaultRequestHeaders.Add("X-Ms-Client-Request-Id", Guid.NewGuid().ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (additionalHeaders != null)
            {
                foreach (Tuple<string, string> header in additionalHeaders)
                {
                    client.DefaultRequestHeaders.Add(header.Item1, header.Item2);
                }
            }
            return client;
        }
    }
}
