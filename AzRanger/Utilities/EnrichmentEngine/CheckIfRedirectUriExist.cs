using AzRanger.Models;
using AzRanger.Models.MSGraph;
using DnsClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Utilities.EnrichmentEngine
{
    internal static class CheckIfRedirectUriExist
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        internal static LookupClient lookup = new LookupClient();
        public async static Task Enrich(Tenant tenant)
        {
            List<Task> dnsTasks = new List<Task>();
            foreach (Application application in tenant.Applications.Values)
            {
                if (application.web != null)
                {
                    foreach (string redirectUri in application.web.redirectUris)
                    {
                        Uri uri;
                        try {
                            uri = new Uri(redirectUri);
                            if (uri.Host.ToLower().Equals("localhost") || uri.Host.ToLower().Equals("visualstudio"))
                            {
                                continue;
                            }
                        }
                        catch (Exception e)
                        {
                            logger.Debug("CheckIfRedirectUriExist.enrich: Creating record {0} failed.", redirectUri);
                            logger.Debug(e.Message);
                            application.web.allRedirectUrisAreRegistered = false;
                            continue;
                        }
                        dnsTasks.Add(GetDNSData(application, uri.Host));
                    }
                }
            }
            await Task.WhenAll(dnsTasks);
            return;
        }

        private async static Task GetDNSData(Application app, string host){
            try
            {
                var result = await lookup.QueryAsync(host, QueryType.A);
                if (result != null)
                {
                    if (!result.HasError)
                    {
                        app.web.allRedirectUrisAreRegistered = false;
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("CheckIfRedirectUriExist.enrich: Checking record {0} failed.", host);
                logger.Debug(e.Message);
                app.web.allRedirectUrisAreRegistered = false;
            }
            return;
        }
    }
}
