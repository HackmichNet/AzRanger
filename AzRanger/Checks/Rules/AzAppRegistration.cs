using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzAppRegistration", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/UsersManagementMenuBlade/UserSettings")]
    [RuleScore("Users are allowed to register own applications", "Allowing user to register their own applications allows an attacker to persist access or to register an internal malicious applications", 7, "https://www.cloud-architekt.net/detection-and-mitigation-consent-grant-attacks-azuread/")]
    class AzAppRegistration : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.DirectoryProperties.usersCanRegisterApps == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
