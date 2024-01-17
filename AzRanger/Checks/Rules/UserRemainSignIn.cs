using AzRanger.Models;
using AzRanger.Models.MainIAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
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
