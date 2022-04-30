using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzGuestRestrictions", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/CompanyRelationshipsMenuBlade/Settings")]
    [RuleScore("Guest are not maximal restricted", "When guests are not restricted a Gues can enumerate the whole tenant", 2, "https://danielchronlund.com/2021/11/18/scary-azure-ad-tenant-enumeration-using-regular-b2b-guest-accounts/")]
    internal class AzGuestRestrictions : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // "Restricted Guest User" => Template ID: 2af84b1e-32c8-42b7-82bc-daa82404023b
            // "Guest User" => Template ID: 10dae51f-b6af-4016-8d66-8c2a99b929b3
            // "User" => Template ID: a0b1b346-4d3e-4e8b-98f8-753987be4970
            if (tenant.AuthorizationPolicy.guestUserRoleId == "2af84b1e-32c8-42b7-82bc-daa82404023b")
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
