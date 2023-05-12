using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOCheckTransportRules", ScopeEnum.EXO, MaturityLevel.Mature, "https://admin.exchange.microsoft.com/#/transportrules")]
    [CISM365("4.3", "", Level.L1, "v1.5-1")]
    [RuleInfo("Transport rules seems to forward mails to external domains", "This can lead to unwanted data loss.", 5, null, "Data are automatically sent outside of the organization.", "Disabled the rules shown below.")]
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
                        // IT is ok, if it is a Subdomains
                        if (maildomain.EndsWith(AZDomain))
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
