using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzRestrictUserAccessGroupFeature", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/GroupsManagementMenuBlade/~/General/menuId/General")]
    [CISAZ("1.18", "", Level.L2, "v2.0")]
    [RuleInfo("User can do self-service group management", "In the current configuration users can perform self-service for their groups. This can result in an unwanted configuration.", 2, null, null, @"Go to the link in the reference and ensure that ""Restrict user ability to access groups features in My Groups."" is set to ""Yes"". ")]
    internal class AzRestrictUserAccessGroupFeature : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.SsgmProperties.groupsInAccessPanelEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
