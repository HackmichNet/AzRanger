using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserLinkedInConnection", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings")]
    [CISM365("1.1.14", "", Level.L2, "v1.4")]
    [RuleInfo("User can connect their accounts to LinkedIn", "Allowing users to sync their account with LinkedIn could disclose useful information for an attacker.", 7, "https://docs.microsoft.com/en-us/azure/active-directory/enterprise-users/linkedin-integration", null, @"Go to the Portal Url and set the value under ""LinkedIn account connections"" to No.")]
    class UserLinkedInConnection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.DirectoryProperties.enableLinkedInAppFamily == 1)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
