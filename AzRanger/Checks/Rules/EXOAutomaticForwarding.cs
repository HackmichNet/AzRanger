using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOAutomaticForwarding", Scope.EXO, MaturityLevel.Mature)]
    [RuleScore("Auto forwarding is not enabled", "Attack can use auto forwarding exfiltrade data", 5, "https://docs.microsoft.com/en-us/archive/blogs/exovoice/disable-automatic-forwarding-in-office-365-and-exchange-server-to-prevent-information-leakage ")]
    class EXOAutomaticForwarding : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(RemoteDomain domain in tenant.ExchangeOnlineSettings.RemoteDomains)
            {
                if(domain.Name == "Default" & domain.AutoForwardEnabled == false & domain.AllowedOOFType == "External")
                {
                    return CheckResult.Passed;
                }
            }
            return CheckResult.Failed;
        }
    }
}
