﻿using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class EXOWhitelistBypass : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(TransportRule rule in tenant.ExchangeOnlineSettings.TransportRules)
            {
                if (rule.State != "Enabled")
                {
                    continue;
                }
                if (rule.SetSCL != null && rule.SetSCL.Equals("-1") && rule.SenderDomainIs != null)
                {
                    passed = false;
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
