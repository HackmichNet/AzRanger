using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOAutomaticForwarding", ScopeEnum.EXO, MaturityLevel.Mature)]
    [CISM365("4.3", "", Level.L1, "v2.0")]
    [RuleInfo("Auto forwarding is not disabled", "An attacken can use auto forwarding to exfiltrate data.", 5, "https://docs.microsoft.com/en-us/archive/blogs/exovoice/disable-automatic-forwarding-in-office-365-and-exchange-server-to-prevent-information-leakage ")]
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
