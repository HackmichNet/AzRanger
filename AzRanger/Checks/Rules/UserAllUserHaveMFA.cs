using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{

    [RuleMeta("UserAllUserHaveMFA", Scope.O365)]
    [CISM365("1.1.2", "", Level.L2, "v1.4")]
    [CISAZ("1.2", "", Level.L2, "v1.4")]
    [RuleInfo("Not all users using MFA", "Users with MFA are way easier victims to phishing attacks.", 7, "https://docs.microsoft.com/en-us/azure/active-directory/conditional-access/howto-conditional-access-policy-all-users-mfa")]
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
