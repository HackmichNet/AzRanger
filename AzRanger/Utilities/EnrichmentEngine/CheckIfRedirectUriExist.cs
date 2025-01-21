using AzRanger.Models;
using AzRanger.Models.MSGraph;
using DnsClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Utilities.EnrichmentEngine
{
    internal static class CheckIfRedirectUriExist
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Enrich(Tenant tenant)
        {
            foreach (Application application in tenant.Applications.Values)
            {
                if (application.web != null)
                {
                    foreach (string redirectUri in application.web.redirectUris)
                    {
                        Uri uri;
                        try {
                            uri = new Uri(redirectUri);
                            if (uri.Host.Equals("localhost"))
                            {
                                continue;
                            }
                        }
                        catch
                        {
                            logger.Debug("CheckIfRedirectUriExist.enrich: Creating record {0} failed.", redirectUri);
                            application.web.allRedirectUrisAreRegistered = false;
                            continue;
                        }

                        var lookup = new LookupClient();
                        try
                        {
                            var result = lookup.Query(uri.Host, QueryType.A);
                            if (result != null)
                            {
                                if (result.HasError)
                                {
                                    application.web.allRedirectUrisAreRegistered = false;
                                }
                            }
                        }
                        catch
                        {
                            logger.Debug("CheckIfRedirectUriExist.enrich: Checking record {0} failed.", redirectUri);
                            application.web.allRedirectUrisAreRegistered = false;
                        }
                    }
                }
            }
        }
    }
}
