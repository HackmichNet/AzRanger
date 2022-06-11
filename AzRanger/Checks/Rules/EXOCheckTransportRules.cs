using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOCheckTransportRules", Scope.EXO, MaturityLevel.Mature, "https://admin.exchange.microsoft.com/#/transportrules")]
    [RuleScore("Some transport rules seems to forward mails to external domains", "Data are automatically sent outside of the organization", 5)]
    class EXOCheckTransportRules : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // Check Tenant.ExchangeOnlineSettings.TransportRules.RedirectMessageTo[]
            List<String> domainsToCheck = new List<string>();
            foreach(Domain domain in tenant.domains)
            {
                domainsToCheck.Add(domain.id);
            }
            bool passed = true;
            foreach(TransportRule rule in tenant.ExchangeOnlineSettings.TransportRules)
            {
                bool allDomainsInAzDomain = true;
                if (rule.RedirectMessageTo == null)
                {
                    continue;
                }
                foreach (object adressToRedirectTo in rule.RedirectMessageTo)
                {
                    string addressToRedirectToStr = (String)adressToRedirectTo.ToString();
                    bool domainIsInAzDomains = false;
                    string maildomain = addressToRedirectToStr.Split('@')[1];
                    foreach (String AZDomain in domainsToCheck)
                    {
                        if(maildomain == AZDomain)
                        {
                            domainIsInAzDomains = true;
                        }
                    }
                    if(domainIsInAzDomains == false)
                    {
                        allDomainsInAzDomain = false;
                        passed = false;
                    }
                }
                if(allDomainsInAzDomain == false)
                {
                    this.AddAffectedEntity(rule);
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
