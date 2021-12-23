using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOCheckSPF", Scope.EXO, MaturityLevel.Mature)]
    [RuleScore("Not every domain that can be used with ExchangeOnline does have an SPF set", "The Sender Policy Framework prevents that someone impersonates your domain", 3)]
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
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
