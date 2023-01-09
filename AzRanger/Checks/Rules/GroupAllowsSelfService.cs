﻿using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupAllowsSelfService", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [CISAZ("1.20", "", Level.L2, "v1.5")]
    [RuleInfo("User can manage the group membership of their security groups", "This may result in unwanted groups configurations or group memberships.", 2, null, null, @"Go to the Portal URL and set ""Owners can manage group membership requests in the Access Panel"" is set to ""No"".")]
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
