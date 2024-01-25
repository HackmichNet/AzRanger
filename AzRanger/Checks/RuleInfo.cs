using AzRanger.Utilities;

namespace AzRanger.Checks
{
    internal class RuleInfo 
    {
        public string ShortDescription { get; private set; }
        public string Risk { get; private set; }
        public string ReferenceLink { get; private set; }
        public string Solution { get; private set; }
        public string LongDescription { get; private set; }

        // Value between 0 and 10
        public int RiskScore { get; private set; }

        public static bool TryGet(string identifier, out RuleInfo ruleInfo)
        {
            if (identifier == null || identifier.Length == 0)
            {
                ruleInfo = null;
                return false;
            }

            var section = RuleInfoData.GetSectionOrNull(identifier);
            if (section == null) {
                ruleInfo = null;
                return false;
            }

            ruleInfo = new RuleInfo();
            ruleInfo.RiskScore = section.GetIntOrThrow("score");
            ruleInfo.ShortDescription = section.GetStringOrThrow("short");
            ruleInfo.Risk = MarkdownRenderer.Render(section.GetStringOrThrow("risk"));
            ruleInfo.ReferenceLink = section.GetStringOrNull("link");
            ruleInfo.LongDescription = MarkdownRenderer.Render(section.GetStringOrNull("long"));
            ruleInfo.Solution = MarkdownRenderer.Render(section.GetStringOrNull("solution"));
            return true;
        }
    }
}
