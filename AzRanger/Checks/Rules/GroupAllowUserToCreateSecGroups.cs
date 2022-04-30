using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("GroupAllowUserToCreateSecGroups", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [RuleScore("All users can create security groups", "This may result in a big number of groups, which increases the management overhead", 2)]
    internal class GroupAllowUserToCreateSecGroups : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AuthorizationPolicy.defaultUserRolePermissions.allowedToCreateSecurityGroups == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
