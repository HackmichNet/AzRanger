using AzRanger.Utilities;
using NLog;
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
        public string ReferenceLink { get; }
        public string Solution { get; }
        public string LongDescription { get; }

        // Value between 0 and 10
        public int RiskScore { get; }

        public RuleInfoAttribute(string identifier)
        {
            if (identifier == null || identifier.Length == 0)
            {
                throw new ArgumentNullException("The identifier for RuleInfoAttribute must not be null or empty");
            }

            var data = RuleInfoData.GetSectionOrThrow(identifier);
            this.ShortDescription = data.GetStringOrThrow("short");
            this.Risk = MarkdownRenderer.Render(data.GetStringOrThrow("risk"));
            this.ReferenceLink = data.GetStringOrNull("link");
            this.LongDescription = MarkdownRenderer.Render(data.GetStringOrNull("long"));
            this.Solution = MarkdownRenderer.Render(data.GetStringOrNull("solution"));
            this.RiskScore = data.GetIntOrThrow("score");
        }
    }
}
