using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzB2BPolicy", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/AllowlistPolicyBlade")]
    [CISM365("1.1.13", "", Level.L2, "v1.5")]
    [RuleInfo("Guest users are not restricted to spcific domains", "Guest users can be invited from all tenants. This could lead to an unwanted data loss.", 5, null, null, @"Go to the Portal URL and set check ""Allow invitations only to the specified domains (most restrictive)"".")]
    class AzB2BPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // If not set it can be null
            if(tenant.TenantSettings.B2BPolicy == null)
            {
                return CheckResult.Finding;
            }
            if (tenant.TenantSettings.B2BPolicy.isAllowlist)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
