using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserAdminConsent", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/ConsentPoliciesMenuBlade/~/AdminConsentSettings")]
    [CISM365("2.1", "", Level.L1, "v2.0")]
    [RuleInfo("Admin Consent workflow is not enabled in your organization", "No direct risk at all.", 1, "https://learn.microsoft.com/en-us/azure/active-directory/manage-apps/configure-admin-consent-workflow", "Not allowing your users to use other applications with their credential, can lead to a lot of angry calls. However, your admins should check each request carefully.", @"Go to the Portal Url and set ""Users can request admin consent to apps they are unable to consent"" to​ ""Yes"".")]
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
