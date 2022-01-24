using DnsClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public static class DNSScanner
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool hasSPF(String domains)
        {
            var lookup = new LookupClient();
            try
            {
                var result = lookup.Query(domains, QueryType.TXT);
                foreach (var record in result.Answers)
                {
                    String rawDate = record.ToString();
                    logger.Debug("DNSScanner.hasSPF: Checking record: {0}", rawDate);
                    if (rawDate.Contains("spf.protection.outlook.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static bool hasDMARC(String domains)
        {
            var lookup = new LookupClient();
            try
            {
                var result = lookup.Query("_dmarc." + domains, QueryType.TXT);
                foreach (var record in result.Answers)
                {
                    String rawDate = record.ToString();
                    logger.Debug("DNSScanner.Query: Checking record: {0}", rawDate);
                    if (rawDate.Contains("DMARC1"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
