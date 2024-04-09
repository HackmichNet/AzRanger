using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    internal class AzASCDefaultPolicy : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;
            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.SecurityCenterBuiltIn.properties.enforcementMode != "Default")
                {
                    passed = false;
                    this.AddAffectedEntity(sub);
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
