using AzRanger.Models;
using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserRemainSignIn", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/~/LoginTenantBranding")]
    [CISM365("1.1.16", "", Level.L2, "v1.5")]
    [RuleInfo("User sees the 'Remains Sign-In' Button", "Allowing a user to use this option, it might happen that they use is on a public computer. So the risk that the user session is compromised increases.", 4, null, null, @"Go to the Portal Url and ensue that the company branding and ensure 'Show option to remain signed in' is not checked.")]
    class UserRemainSignIn : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(LoginTenantBranding branding in tenant.TenantSettings.LoginTenantBrandings)
            {
                if (branding.isConfigured && branding.hideKeepMeSignedIn == false)
                {
                    return CheckResult.Finding;
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
