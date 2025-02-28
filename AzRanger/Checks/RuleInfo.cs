using AzRanger.Utilities;
using System;

namespace AzRanger.Checks
{
    public enum ScopeEnum
    {
        AAD,
        Azure,
        MDM,
        EXO,
        SPO,
        Teams
    }

    public enum ServiceEnum
    {
        None,
        StorageAccount,
        KeyVault,
        Monitoring,
        NetworksSecurityGroup,
        SQLServer,
        VirtualMachine,
        PSQLServer
    }

    public enum MaturityLevel
    {
        Tentative = 0,
        Mature = 1
    }
    internal class RuleInfo
    {
        public string ShortDescription { get; private set; }
        public string Risk { get; private set; }
        public string ReferenceLink { get; private set; }
        public string Solution { get; private set; }
        public string LongDescription { get; private set; }
        public string ShortName { get; private set; }
        public ScopeEnum Scope { get; private set; }
        // Indicates How good a rule is
        public MaturityLevel MaturityLevel { get; private set; }
        public string PortalUrl { get; private set; }
        public ServiceEnum Service { get; private set; }
        // Value between 0 and 10
        public int RiskScore { get; private set; }
        public String CISM365Section { get; private set; }
        public String CISM365Level { get; private set; }
        public String CISM365version { get; private set; }
        public String CISMAZSection { get; private set; }
        public String CISMAZLevel { get; private set; }
        public String CISMAZversion { get; private set; }

        public static bool TryGet(string identifier, out RuleInfo ruleInfo)
        {
            if (identifier == null || identifier.Length == 0)
            {
                ruleInfo = null;
                return false;
            }

            var section = RuleInfoData.GetSectionOrNull(identifier);
            if (section == null)
            {
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
            ruleInfo.ShortName = identifier;
            ruleInfo.Service = ServiceEnum.None;
            ruleInfo.PortalUrl = section.GetStringOrNull("portal");
            ruleInfo.CISM365Section = section.GetStringOrNull("cism365section");
            ruleInfo.CISM365Level = section.GetStringOrNull("cism365level");
            ruleInfo.CISM365version = section.GetStringOrNull("cism365version");

            var serviceString = section.GetStringOrNull("service");
            if (serviceString != null)
            {
                if (Enum.TryParse(serviceString, out ServiceEnum service))
                {
                    ruleInfo.Service = service;
                }
            }

            if (!Enum.TryParse(section.GetStringOrThrow("scope"), out ScopeEnum scope))
            {
                ruleInfo = null;
                return false;
            }
            if (!Enum.TryParse(section.GetIntOrThrow("maturity").ToString(), out MaturityLevel maturity))
            {
                ruleInfo = null;
                return false;
            }

            ruleInfo.Scope = scope;
            ruleInfo.MaturityLevel = maturity;
            return true;
        }
    }
}
