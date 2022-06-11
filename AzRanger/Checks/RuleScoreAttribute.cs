using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks
{
    class RuleScoreAttribute : Attribute
    {
        public string Finding { get; }
        public string Impact { get; }
        public string Link { get; }
        public string Solution { get; }

        // Value between 0 and 10
        public int RiskScore { get; }

        public RuleScoreAttribute(String finding, string impact, int riskscore, string link = null, string solution = null)
        {
            this.Finding = finding;
            this.Impact = impact;
            this.RiskScore = riskscore;
            this.Link = link;
            this.Solution = solution;
        }
    }
}
