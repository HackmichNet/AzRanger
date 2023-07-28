using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupAllowUserToCreateSecGroups", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [CISAZ("1.19", "", Level.L2, "v2.0")]
    [RuleInfo("All users can create security groups using the Azure Portal, API or PowerShell", "This can result in a high number of groups over the time.", 2, null, null, @"Go to the Portal URL and set ""Users can create security groups in Azure portals, API or PowerShell"" to ""No"".")]
    internal class GroupAllowUserToCreateSecGroups : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.AuthorizationPolicy.defaultUserRolePermissions.allowedToCreateSecurityGroups == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
