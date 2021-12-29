using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzB2BPolicy", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/AllowlistPolicyBlade")]
    [RuleScore("Collaboration restrictions is not set to \"Allow invitations only to the specified domains (most restrictive)\"", "It is possible to invite users from arbitrary domains", 5)]
    class AzB2BPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // If not set it can be null
            if(tenant.B2BPolicy == null)
            {
                return CheckResult.Failed;
            }
            if (tenant.B2BPolicy.isAllowlist)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
