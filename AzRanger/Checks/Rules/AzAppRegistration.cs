using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzAppRegistration", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings")]
    [CISM365("2.1", "", Level.L2, "v1.4")]
    [CISAZ("1.11", "", Level.L2, "v1.4")]
    [RuleInfo("Users are allowed to register own applications", "Allowing user to register their own applications allows an attacker to persist access or to register an internal malicious applications.", 7, "https://www.cloud-architekt.net/detection-and-mitigation-consent-grant-attacks-azuread/", null, @"Go to <a href=""https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings"">https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings</a> and switch the button ""Users can register applications"" to ""No"".")]
    class AzAppRegistration : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.DirectoryProperties.usersCanRegisterApps == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
