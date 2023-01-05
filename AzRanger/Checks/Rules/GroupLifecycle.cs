using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("GroupLifecycle", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/Lifecycle")]
    [RuleInfo("Temporary groups do not expire", "This can result in a high number of groups over the time.", 3, "https://docs.microsoft.com/en-us/microsoft-365/solutions/plan-organization-lifecycle-governance?view=o365-worldwide", null, @"Go to the Portal URL and set ""Enable expiration for these Microsoft 365 groups"" to ""Yes"".")]
    class GroupLifecycle : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
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
