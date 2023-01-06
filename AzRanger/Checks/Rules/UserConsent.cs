using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserConsent", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/")]
    [CISM365("2.6", "", Level.L2, "v1.5")]
    [CISAZ("1.9", "", Level.L2, "v1.4")]
    [RuleInfo("User can consent to apps accessing data on their behalf", "Malicious apps are often used in phishing scenarios. Thus, allowing users to consent to applications increases the risk for a successful attack.", 10, "https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/configure-user-consent?tabs=azure-portal", null, @"Go to the Portal URL and set ""Users can request admin consent to apps they are unable to consent to""​ 'No' ")]
    class UserConsent : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData != null && tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
