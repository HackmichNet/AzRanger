using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzRestrictUserAccessGroupFeature", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/GroupsManagementMenuBlade/General")]
    [RuleScore("Guest are not maximal restricted", "When guests are not restricted a Gues can enumerate the whole tenant", 2)]
    internal class AzRestrictUserAccessGroupFeature : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SsgmProperties.groupsInAccessPanelEnabled == true)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
