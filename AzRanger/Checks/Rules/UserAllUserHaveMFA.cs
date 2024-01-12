using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{

    [RuleMeta("UserAllUserHaveMFA", ScopeEnum.AAD)]
    [CISAZ("1.1.3", "", CISLevel.L2, "v2.0")]
    class UserAllUserHaveMFA : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach(User user in tenant.AllUsers.Values.ToList())
            {
                //ignore external users
                if (user.userPrincipalName.Contains("#EXT#"))
                {
                    continue;
                }
                if (!user.isMFAEnabled)
                {
                    passed = false;
                    this.AddAffectedEntity(user);
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
