using DnsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.AzScanner
{
    public static class DNSScanner
    {
        public static bool hasSPF(String domains)
        {
            var lookup = new LookupClient();
            try
            {
                var result = lookup.Query(domains, QueryType.TXT);
                foreach (var record in result.Answers)
                {
                    if (record.ToString().Contains("include:spf.protection.outlook.com"))
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
                    if (record.ToString().Contains("v=DMARC1"))
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
