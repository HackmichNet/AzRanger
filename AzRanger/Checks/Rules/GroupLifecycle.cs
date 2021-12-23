using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("GroupLifecycle", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/Lifecycle")]
    [RuleScore("Temporary groups do not expire", "This can result in a hugh amount of groups over the time", 3, "https://docs.microsoft.com/en-us/microsoft-365/solutions/plan-organization-lifecycle-governance?view=o365-worldwide")]
    class GroupLifecycle : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // It is not set
            if(tenant.LCMSettings.policyIdentifier == "00000000-0000-0000-0000-000000000000")
            {
                return CheckResult.Failed;
            }

            // No group is selected
            if(tenant.LCMSettings.groupIdsToMonitorExpirations.Length == 0)
            {
                return CheckResult.Failed;
            }

            // No admin will be notified
            if(tenant.LCMSettings.adminNotificationEmails == null)
            {
                return CheckResult.Failed;
            }

            return CheckResult.Passed;
        }
    }
}
