using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXODMARC", ScopeEnum.EXO, MaturityLevel.Mature)]
    [CISM365("4.9", "", Level.L1, "v1.5")]
    [RuleInfo("Not all of Exchange Online Domains have DMARC enabled", "This increases the risk, that an attacker can impersonate your domain.", 5, null, null, "Enable DMARC for all your domains.")]
    class EXODMARC : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(AcceptedDomain domain in tenant.ExchangeOnlineSettings.AcceptedDomains)
            {
                if (domain.HasDMARC == false)
                {
                    passed = false;
                    this.AddAffectedEntity(domain);
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
