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
    [CISM365("1.1.4", "", Level.L1, "v2.0")]
    [CISAZ("1.1.3", "", Level.L2, "v2.0")]
    [RuleInfo("Not all users using MFA", "Users with MFA are way easier victims to phishing attacks.", 7, "https://learn.microsoft.com/en-us/azure/active-directory/conditional-access/howto-conditional-access-policy-all-users-mfa", null, "Think about to implement MFA for all your users.")]
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
