using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("UserAdminConsent", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/StartboardApplicationsMenuBlade/UserSettings/menuId/")]
    [RuleScore("Admin Consent workflow is not enabled in your organization", "Admin Consent workflow allows to review applications your user wants to use and approve or deny them", 8, "https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/configure-admin-consent-workflow")]
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
                                return CheckResult.Passed;
                            }
                        }
                    }
                }
            }
            return CheckResult.Failed;
        }
    }
}
