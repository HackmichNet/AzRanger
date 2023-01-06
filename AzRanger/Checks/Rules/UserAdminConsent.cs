using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAdminConsent", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/")]
    [CISM365("2.7", "", Level.L2, "v1.5")]
    [RuleInfo("Admin Consent workflow is not enabled in your organization", "No direct risk at all.", 1, "https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/configure-admin-consent-workflow", "Not allowing your users to use other applications with their credential, can lead to a lot of angry call. However, your admins should check each request carefully.", @"Go to the Portal Url and set ""Users can request admin consent to apps they are unable to consent"" to​ ""Yes"".")]
    class UserAdminConsent : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(EnterpriseApplicationUserSettings setting in tenant.EnterpriseApplicationUserSettings)
            {
                if(setting.displayName == "Consent Policy Settings")
                {
                    foreach(Value value in setting.values)
                    {
                        if(value.name == "EnableAdminConsentRequests")
                        {
                            if(value.value == "true")
                            {
                                return CheckResult.NoFinding;
                            }
                        }
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
