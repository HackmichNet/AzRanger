using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    class UserPerUserMFA : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (User user in tenant.Users.Values.ToList())
            {
                //ignore external users
                if (user.userPrincipalName.Contains("#EXT#"))
                {
                    continue;
                }
                if (!user.perUserMfaState.ToLower().Equals("enabled"))
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
