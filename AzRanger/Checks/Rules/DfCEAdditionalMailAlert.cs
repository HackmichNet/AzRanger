using AzRanger.Models;
using AzRanger.Models.AzMgmt;

namespace AzRanger.Checks.Rules
{
    class DfCEAdditionalMailAlert : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Subscription sub in tenant.Subscriptions.Values)
            {
                if (sub.SecurityContact[0].properties.notificationsByRole.state.Equals("On") && sub.SecurityContact[0].properties.alertNotifications.state.Equals("On"))
                {
                    if (sub.SecurityContact[0].properties.emails.Length == 0)
                    {
                        AddAffectedEntity(sub);
                        passed = false;
                    }
                }
                else
                {
                    AddAffectedEntity(sub);
                    passed = false;
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
