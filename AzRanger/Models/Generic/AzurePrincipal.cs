using System;

namespace AzRanger.Models.Generic
{
    public class AzurePrincipal
    {
        public Guid id { get; }
        public AzurePrincipalType PrincipalType { get; }

        public AzurePrincipal(Guid id, AzurePrincipalType principalType)
        {
            this.id = id;
            this.PrincipalType = principalType;
        }

        public bool Equals(AzurePrincipal azurePrincipal)
        {
            return this.id == azurePrincipal.id;
        }
    }
}
