﻿using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupAllowUserToCreateSecGroups", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [CISAZ("1.16", "", Level.L2, "v1.4")]
    [RuleInfo("All users can create security groups", "This can result in a high number of groups over the time.", 2, null, null, @"Go to the Portal URL and set ""Users can create security groups in Azure portals, API or PowerShell"" to ""No"".")]
    internal class GroupAllowUserToCreateSecGroups : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.AuthorizationPolicy.defaultUserRolePermissions.allowedToCreateSecurityGroups == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}