using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
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
