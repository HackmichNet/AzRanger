using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzRestrictUserAccessGroupFeature", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [CISAZ("1.15", "", Level.L2, "v1.4")]
    [RuleInfo("User can do self-service group management", "In the current configuration users can perform self-service for their groups. This can result in an unwanted configuration.", 2)]
    internal class AzRestrictUserAccessGroupFeature : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SsgmProperties.groupsInAccessPanelEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
