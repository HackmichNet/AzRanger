using AzRanger.Checks.AzRanger.Checks;
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

    internal class RuleMeta
    {
        public string ShortName { get; private set; }
        public ScopeEnum Scope { get; private set; }
        // Indicates How good a rule is
        public MaturityLevel MaturityLevel { get; private set; }
        public string PortalUrl { get; private set; }
        public ServiceEnum Service { get; private set; }

        public static bool TryGet(string identifier, out RuleMeta meta)
        {
            if (identifier == null || identifier.Length == 0)
            {
                meta = null;
                return false;
            } 

            var section = RuleMetaData.GetSectionOrNull(identifier);
            if (section == null)
            { 
                meta = null;
                return false;
            }

            meta = new RuleMeta();
            meta.ShortName = identifier;
            meta.Service = ServiceEnum.None;
            meta.PortalUrl = section.GetStringOrNull("portal");

            var serviceString = section.GetStringOrNull("service");
            if (serviceString != null)
            {
                if (Enum.TryParse(serviceString, out ServiceEnum service))
                {
                    meta.Service = service;
                }
            }

            if (!Enum.TryParse(section.GetStringOrThrow("scope"), out ScopeEnum scope))
            {
                meta = null;
                return false;
            }
            if (!Enum.TryParse(section.GetIntOrThrow("maturity").ToString(), out MaturityLevel maturity))
            {
                meta = null;
                return false;
            }

            meta.Scope = scope;
            meta.MaturityLevel = maturity;

            return true;
        }
    }
}
