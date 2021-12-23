using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXODKIM", Scope.EXO, MaturityLevel.Mature)]
    [RuleScore("Not all of your Exchange Online Domains seems to have DKIM enabled", "DomainKeys Identified Mail (DKIM) helps a receiver to verify the origin of a mail", 5)]
    class EXODKIM : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ExchangeOnlineSettings.DkimSigningConfigs == null)
            {
                return CheckResult.Passed;
            }
            // I assume there is only the main domain and exchange online is not used
            if (tenant.ExchangeOnlineSettings.AcceptedDomains.Count == 1)
            {
                return CheckResult.Passed;
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
                return CheckResult.Passed;
            }
            else
            {
                return CheckResult.Failed;
            }
        }
    }
}
