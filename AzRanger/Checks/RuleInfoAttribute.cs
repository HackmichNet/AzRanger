using System;

namespace AzRanger.Checks
{
    internal class RuleInfoAttribute : Attribute
    {
        public String ShortName { get; }
        public Scope Scope { get; }
        // Indicates How good a rule is
        public MaturityLevel MaturityLevel { get; }
        public string PortalUrl { get; }
        public Service Service { get; }

        public RuleInfoAttribute(String shortName, Scope scope, MaturityLevel maturityLevel = MaturityLevel.Mature, string portalUrl = null, Service service = Service.None)
        {
            this.ShortName = shortName;
            this.Scope = scope;
            this.MaturityLevel = maturityLevel;
            this.PortalUrl = portalUrl;
            this.Service = service;
        }
    }

    public enum Scope
    {
        O365 = 1,
        Azure = 2,
        MDM = 3, 
        EXO = 4, 
        SPO = 5
    }

    public enum Service
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