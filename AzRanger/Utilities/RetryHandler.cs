using NLog;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AzRanger.Utilities
{

    // https://stackoverflow.com/questions/19260060/retrying-httpclient-unsuccessful-requests
    public class RetryHandler : DelegatingHandler
    {
        // Strongly consider limiting the number of retries - "retry forever" is
        // probably not the most user friendly way you could respond to "the
        // network cable got pulled out."
        private const int MaxRetries = 3;
        private bool SlowDown = false;
        Random rnd = new Random();
        internal static Logger logger = LogManager.GetCurrentClassLogger();

        public RetryHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        { }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            if (SlowDown)
            {
                int backOff = rnd.Next(3, 11);
                await Task.Delay(backOff * 100, cancellationToken);
            }
            for (int i = 0; i < MaxRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                if ((int)response.StatusCode == 400 | (int)response.StatusCode == 401 | (int)response.StatusCode == 403)
                {
                    return response;
                }
                if ((int)response.StatusCode == 429)
                {
                    logger.Info("[-] I'm too fast, need to slow down a little bit. Sorry.");
                    SlowDown = true;
                    await Task.Delay(500, cancellationToken);
                    continue;
                }
            }
            return response;
        }
    }
}
