﻿using AzRanger.Models;
using AzRanger.Models.Generic;
using AzRanger.Models.MSGraph;
using AzRanger.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AppsAsGlobalAdmin", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps")]
    [RuleScore("These service principals are member of the Global Admin Role or Privileged Authentication Administrator", "This can lead to a potential privilege escalation in your tenant", 9)]

    class AppsAsGlobalAdmin : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (DirectoryRole role in tenant.AllDirectoryRoles.Values)
            {
                if (DirectoryRoleTemplateID.GlobalAdmins.Any(role.roleTemplateId.Contains))
                {
                    foreach(AzurePrincipal id in role.GetMembers())
                    {
                        if(id.PrincipalType == AzurePrincipalType.ServicePrincipal)
                        {
                            passed = false;
                            this.AddAffectedEntity(tenant.AllServicePrincipals[id.id]);
                        }
                    }
                }
            }

            if (passed)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
