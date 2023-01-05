using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzMgmtGroupCreation", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_Azure_ManagementGroups/ManagementGroupBrowseBlade/MGBrowse_settingsItem")]
    [RuleInfo("Everyone can create Management Groups", "This may lead to an unwanted number of Management Groups in the tenant.", 2, null, @"Go to <a href=""https://portal.azure.com/#view/Microsoft_Azure_ManagementGroups/ManagementGroupBrowseBlade/~/MGBrowse_settingsItem"">https://portal.azure.com/#view/Microsoft_Azure_ManagementGroups/ManagementGroupBrowseBlade/~/MGBrowse_settingsItem</a> and toogle ""Require write permissions for creating new management groups."" to Off.")]
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
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
