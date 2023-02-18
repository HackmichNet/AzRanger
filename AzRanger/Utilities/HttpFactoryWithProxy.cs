using Microsoft.Identity.Client;
using System;
using System.Net;
using System.Net.Http;

namespace AzRanger.Utilities
{
    internal class HttpFactoryWithProxy : IMsalHttpClientFactory
    {
        private static HttpClient _httpClient;

        public HttpFactoryWithProxy(String proxystring)
        {
            // Consider using Lazy<T> 
            if (_httpClient == null)
            {
                var proxy = new WebProxy
                {
                    Address = new Uri($"http://{proxystring}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = true,
                };

                // Now create a client handler which uses that proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    }
                };

                _httpClient = new HttpClient(handler: httpClientHandler);
            }
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }
    }
}
