using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXODKIM", ScopeEnum.EXO, MaturityLevel.Mature)]
    [CISM365("4.7", "", Level.L1, "v1.5")]
    [RuleInfo("Not all of Exchange Online Domains have DKIM enabled", "This increases the risk, that an attacker can impersonate your domain.", 5, null, null, "Configure DKIM for all your domains.")]
    class EXODKIM : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ExchangeOnlineSettings.DkimSigningConfigs == null)
            {
                return CheckResult.NoFinding;
            }
            // I assume there is only the main domain and exchange online is not used
            if (tenant.ExchangeOnlineSettings.AcceptedDomains.Count == 1)
            {
                return CheckResult.NoFinding;
            }
            bool passed = true;
            foreach(DkimSigningConfig config in tenant.ExchangeOnlineSettings.DkimSigningConfigs)
            {
                if (config.Domain.EndsWith(".onmicrosoft.com"))
                {
                    continue;
                }
                else
                {
                    if(config.Enabled == false)
                    {
                        passed = false;
                        this.AddAffectedEntity(config);
                    }
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            else
            {
                return CheckResult.Finding;
            }
        }
    }
}
