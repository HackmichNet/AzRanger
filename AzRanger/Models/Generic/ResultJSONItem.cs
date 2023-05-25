using AzRanger.Checks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Models.Generic
{
    internal class ResultJSONList
    {
        public List<ResultJSONItem> Finding;
        public List<ResultJSONItem> NoFinding;
        public List<ResultJSONItem> Error;
        public List<ResultJSONItem> NotApplicable;
    }
    internal class ResultJSONItem
    {
        public String Section { get; set; }
        public String Level { get; set; }
        public String Version { get; set; }
        public string ShortDescription { get; set; }
        public string Risk { get; set; }
        public string RefernceLink { get; set; }
        public string Solution { get; set; }
        public string LongDescription { get; set; }
        public int RiskScore { get; set; }
        public String ShortName { get; set; }
        public String Scope { get; set; }
        public String MaturityLevel { get; set; }
        public String PortalUrl { get; set; }
        public String Service { get; set; }
        public String CISDocument { get; set; }
        public List<String> AffedtedItems { get; set; }
    }
}
