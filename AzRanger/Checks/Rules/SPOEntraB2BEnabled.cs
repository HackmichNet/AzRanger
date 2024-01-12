﻿using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("SPOEntraB2BEnabled", ScopeEnum.SPO, MaturityLevel.Mature)]
    [CISM365("2.12", "", Level.L1, "v2.0")]
    
    class SPOEntraB2BEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
            {
                this.SetReason("Security Defaults are enabled. This disabled legacy authentication protocols.");
                return CheckResult.NotApplicable;
            }
            if (tenant.SharePointInformation.SharePointInternalInfos.LegacyAuthProtocolsEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
