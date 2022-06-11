using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzGuestInvite", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/CompanyRelationshipsMenuBlade/Settings")]
    [RuleScore("Either everyone (including guests) or all members of the tenant can invite guests", "This may lead to an unwanted number of guests in the tenant", 2)]
    internal class AzGuestInvite : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AuthorizationPolicy.allowInvitesFrom == "adminsAndGuestInviters" | tenant.AuthorizationPolicy.allowInvitesFrom == "none")
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
