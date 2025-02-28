using AzRanger.Models;
using AzRanger.Models.MainIAM;
using System;

namespace AzRanger.Checks.Rules
{
    class UserRemainSignIn : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach (LoginTenantBranding branding in tenant.TenantSettings.LoginTenantBrandings)
            {
                if (branding.isConfigured && branding.hideKeepMeSignedIn != null && Convert.ToBoolean(branding.hideKeepMeSignedIn) == false)
                {
                    return CheckResult.Finding;
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
