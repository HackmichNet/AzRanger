using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserConsent", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/")]
    [RuleScore("Users can consent to apps accessing data on their behalf", "App consent is often used in phishing scenarios and other attacks", 10, "https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/configure-user-consent?tabs=azure-portal")]
    class UserConsent : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.UserSettings.usersCanAllowAppsToAccessData == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
