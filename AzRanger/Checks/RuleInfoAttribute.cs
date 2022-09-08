using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks
{
    class RuleInfoAttribute : Attribute
    {
        public string ShortDescription { get; }
        public string Risk { get; }
        public string RefernceLink { get; }
        public string Solution { get; }
        public string LongDescription { get; }

        // Value between 0 and 10
        public int RiskScore { get; }

        public RuleInfoAttribute(String shortDecription, string risk, int riskscore, string referencelink = null, string longDescription = null, string solution = null)
        {
            this.ShortDescription = shortDecription;
            this.Risk = risk;
            this.RiskScore = riskscore;
            this.RefernceLink = referencelink;
            this.LongDescription = LongDescription;
            this.Solution = solution;
        }
    }
}
