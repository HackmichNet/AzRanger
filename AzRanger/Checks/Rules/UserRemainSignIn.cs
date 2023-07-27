using AzRanger.Models;
using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRemainSignIn", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_UsersAndTenants/UserManagementMenuBlade/~/UserSettings/menuId/UserSettings")]
    [CISM365("1.1.19", "", Level.L2, "v2.0")]
    [RuleInfo("User sees the 'Remains Sign-In' Button", "Allowing a user to use this option, it might happen that they use is on a public computer. So the risk that the user session is compromised increases.", 4, null, null, @"Go to the Portal Url and ensure that 'Show keep user signed in' is set to ""No"".")]
    class UserRemainSignIn : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(LoginTenantBranding branding in tenant.TenantSettings.LoginTenantBrandings)
            {
                if (branding.isConfigured && branding.hideKeepMeSignedIn != null && (bool)branding.hideKeepMeSignedIn == false)
                {
                    return CheckResult.Finding;
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
