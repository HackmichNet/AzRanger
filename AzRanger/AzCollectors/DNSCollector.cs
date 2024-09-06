using DnsClient;
using NLog;
using System;

namespace AzRanger.AzScanner
{
    public static class DNSCollector
    {
        internal static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool HasSPF(String domains)
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

        public static bool HasDMARC(String domains)
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
