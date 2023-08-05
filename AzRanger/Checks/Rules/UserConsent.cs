using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserConsent", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/ConsentPoliciesMenuBlade/~/UserSettings")]
    [CISM365("2.7", "", Level.L2, "v2.0")]
    [CISAZ("1.11", "", Level.L2, "v2.0")]
    [RuleInfo("User can consent to apps accessing data on their behalf", "Malicious apps are often used in phishing scenarios. Thus, allowing users to consent to applications increases the risk for a successful attack.", 10, "https://docs.microsoft.com/en-us/azure/active-directory/manage-apps/configure-user-consent?tabs=azure-portal", null, @"Go to the Portal URL and set ""Users can request admin consent to apps they are unable to consent"" to​ ""No"".")]
    class UserConsent : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // Seems to be null now nowadays...
            if(tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData != null && tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData == false)
            {
                return CheckResult.NoFinding;
            }
            if(tenant.TenantSettings.UserSettings.usersCanAllowAppsToAccessData == null)
            {
                if(tenant.TenantSettings.AuthorizationPolicy.permissionGrantPolicyIdsAssignedToDefaultUserRole != null)
                {
                    List<String> data = tenant.TenantSettings.AuthorizationPolicy.permissionGrantPolicyIdsAssignedToDefaultUserRole.Select(s => (string)s.ToString()).ToList();
                    // String[] data = (String[])tenant.TenantSettings.AuthorizationPolicy.permissionGrantPolicyIdsAssignedToDefaultUserRole;
                    foreach (String entry in data)
                    {
                        if (entry.Equals("ManagePermissionGrantsForSelf.microsoft-user-default-legacy"))
                        {
                            return CheckResult.Finding;
                        }
                    }
                    return CheckResult.NoFinding;
                }
            }
            return CheckResult.Error;
        }
    }
}
