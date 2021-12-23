using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks
{
    class RuleScoreAttribute : Attribute
    {
        public string Description { get; }
        public string Rational { get; }
        public string Link { get; }

        // Value between 0 and 10
        public int RiskScore { get; }

        public RuleScoreAttribute(String description, string rational, int riskscore, string link = null)
        {
            this.Description = description;
            this.Rational = rational;
            this.RiskScore = riskscore;
            this.Link = link;
        }
    }
}
