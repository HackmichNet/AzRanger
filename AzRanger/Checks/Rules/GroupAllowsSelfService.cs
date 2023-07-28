using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupAllowsSelfService", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/GroupsManagementMenuBlade/~/General/menuId/General")]
    [CISAZ("1.20", "", Level.L2, "v2.0")]
    [RuleInfo("Users can manage the group membership of their own security groups", "This may result in unwanted groups configurations or group memberships.", 2, null, null, @"Go to the Portal URL and set ""Owners can manage group membership requests in My Groups"" is set to ""No"".")]
    internal class GroupAllowsSelfService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SsgmProperties.selfServiceGroupManagementEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
