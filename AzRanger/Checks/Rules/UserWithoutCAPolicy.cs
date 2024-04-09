using AzRanger.Models;
using AzRanger.Models.MSGraph;

namespace AzRanger.Checks.Rules
{
    class UserWithoutCAPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.CAPolicies == null || tenant.CAPolicies.Count == 0)
            {
                SetReason("No Conditional Access Policies defined.");
                return CheckResult.NotApplicable;
            }
            bool finding = false;
            foreach (User user in tenant.Users.Values)
            {
                if(user.assignedCAPolicies == null)
                {
                    finding = true;
                    AddAffectedEntity(user);
                    continue;
                }
                if(user.assignedCAPolicies.Count == 0)
                {
                    finding = true;
                    AddAffectedEntity(user);
                    continue;
                }
            }
            if (finding)
            {
                return CheckResult.Finding;
            }
            else
            {
                return CheckResult.NoFinding;
            }
        }
    }
}
