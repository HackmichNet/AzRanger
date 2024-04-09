using AzRanger.Models;
using System.Linq;

namespace AzRanger.Checks.Rules
{
    class GroupPublic : BaseCheck
    {

        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (Group group in tenant.Groups.Values.ToList())
            {
                if ((group.visibility != null && (string)group.visibility.ToString().ToLower() == "public") & (group.securityEnabled))
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
