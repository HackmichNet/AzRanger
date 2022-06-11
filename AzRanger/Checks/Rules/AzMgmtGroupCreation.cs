using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzMgmtGroupCreation", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_Azure_ManagementGroups/ManagementGroupBrowseBlade/MGBrowse_settingsItem")]
    [RuleScore("Every user in the tenant can create Management Groups", "This may lead to an unwanted number of Management Groups in the tenant", 2, "", "It is recommended to toogle the button to off")]
    internal class AzMgmtGroupCreation : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.ManagementGroupSettings == null)
            {
                return CheckResult.NotApplicable;
            }
            if(tenant.ManagementGroupSettings.properties.requireAuthorizationForGroupCreation == true )
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
