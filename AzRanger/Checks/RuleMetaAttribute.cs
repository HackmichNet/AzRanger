using System;

namespace AzRanger.Checks
{
    internal class RuleMetaAttribute : Attribute
    {
        public String ShortName { get; }
        public ScopeEnum Scope { get; }
        // Indicates How good a rule is
        public MaturityLevel MaturityLevel { get; }
        public string PortalUrl { get; }
        public ServiceEnum Service { get; }

        public RuleMetaAttribute(String shortName, ScopeEnum scope, MaturityLevel maturityLevel = MaturityLevel.Mature, string portalUrl = null, ServiceEnum service = ServiceEnum.None)
        {
            this.ShortName = shortName;
            this.Scope = scope;
            this.MaturityLevel = maturityLevel;
            this.PortalUrl = portalUrl;
            this.Service = service;
        }
    }
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
        None = 0,
        StorageAccount = 1,
        KeyVault = 2,
        Monitoring = 3,
        NetworksSecurityGroup = 4,
        SQLServer = 5
    }

    public enum MaturityLevel
    {
        Tentative = 0,
        Mature = 1
    }

}