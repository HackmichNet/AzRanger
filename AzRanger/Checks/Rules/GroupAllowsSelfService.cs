using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("GroupAllowsSelfService", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [RuleScore("User can manage the group membership of their security groups", "This may result in unwanted groups configurations or group memberships", 2)]
    internal class GroupAllowsSelfService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SsgmProperties.selfServiceGroupManagementEnabled == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
