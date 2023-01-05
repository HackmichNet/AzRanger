using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzGuestInvite", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/CompanyRelationshipsMenuBlade/Settings")]
    [CISAZ("1.13", "", Level.L2, "v1.4")]
    [RuleInfo("Guest Invitation is not restricted", "This may lead to an unwanted number of guests in the tenant.", 2, null, "Either everyone (including guests) or all members of the tenant can invite guests.", @"Go to ""External Identities | External collaboration settings"" and set the ""Guest invite restrictions"" to ""Only users assigned to specific admin roles can invite guest users"" or ""No one in the organization can invite guest users including admins (most restrictive)""")]
    internal class AzGuestInvite : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.AuthorizationPolicy.allowInvitesFrom == "adminsAndGuestInviters" | tenant.TenantSettings.AuthorizationPolicy.allowInvitesFrom == "none")
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
