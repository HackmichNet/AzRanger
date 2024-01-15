using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzGuestRestrictions", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AllowlistPolicyBlade")]    
    internal class AzGuestRestrictions : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // "Restricted Guest User" => Template ID: 2af84b1e-32c8-42b7-82bc-daa82404023b
            // "Guest User" => Template ID: 10dae51f-b6af-4016-8d66-8c2a99b929b3
            // "User" => Template ID: a0b1b346-4d3e-4e8b-98f8-753987be4970
            if (tenant.TenantSettings.AuthorizationPolicy.guestUserRoleId.Equals("2af84b1e-32c8-42b7-82bc-daa82404023b"))
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
