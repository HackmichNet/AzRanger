using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOCheckSPF", ScopeEnum.EXO, MaturityLevel.Mature)]
    [CISM365("4.8", "", Level.L1, "v1.5")]
    [RuleInfo("Missing SPF for Domains used in Exchange Online", "The Sender Policy Framework can prevent that someone impersonates your domain.", 3)]
    class EXOCheckSPF : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(AcceptedDomain domain in tenant.ExchangeOnlineSettings.AcceptedDomains)
            {
                if (!domain.HasSPF)
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
