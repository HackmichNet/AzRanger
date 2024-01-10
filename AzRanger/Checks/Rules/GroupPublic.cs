using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupPublic", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/GroupsManagementMenuBlade/~/AllGroups/menuId/AllGroups")]
    [RuleInfo("GroupPublic")]
    [CISM365("1.1.16", "", Level.L2, "v2.0")]

    class GroupPublic : BaseCheck
    {
       
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (Group group in tenant.AllGroups.Values.ToList())
            {
                if((group.visibility != null && (string)group.visibility.ToString() == "Public") & (group.securityEnabled))
                {
                    passed = false;
                    this.AddAffectedEntity(group);
                }
            }
            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
