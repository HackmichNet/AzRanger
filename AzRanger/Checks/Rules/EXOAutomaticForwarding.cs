using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    class EXOAutomaticForwarding : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(RemoteDomain domain in tenant.ExchangeOnlineSettings.RemoteDomains)
            {
                if(domain.Name == "Default" & domain.AutoForwardEnabled == false & domain.AllowedOOFType == "External")
                {
                    return CheckResult.NoFinding;
                }
            }
            return CheckResult.Finding;
        }
    }
}
