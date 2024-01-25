using AzRanger.Models;

namespace AzRanger.Checks.Rules
{    
    class GroupLifecycle : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // Policy is not set
            if (tenant.TenantSettings.LCMSettings == null)
            {
                return CheckResult.Finding;
            }

            // Policy is not set
            if(tenant.TenantSettings.LCMSettings.policyIdentifier == "00000000-0000-0000-0000-000000000000")
            {
                return CheckResult.Finding;
            }

            // Enable expiration for these Microsoft 365 groups => None = 2
            // Enable expiration for these Microsoft 365 groups => Selected = 1
            // Enable expiration for these Microsoft 365 groups => All = 0
            if (tenant.TenantSettings.LCMSettings.managedGroupTypes == 2)
            {
                return CheckResult.Finding;
            }

            // No admin will be notified
            if(tenant.TenantSettings.LCMSettings.adminNotificationEmails == null || tenant.TenantSettings.LCMSettings.adminNotificationEmails == "")
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
