using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{

    [RuleMeta("UserMFAFatigueProtection", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/AdminAuthMethods")]
    [CISM365("1.1.5", "", Level.L1, "v2.0")]
    
    class UserMFAFatigueProtection : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            foreach(Authenticationmethodconfiguration method in tenant.TenantSettings.AuthenticationMethodsPolicy.authenticationMethodConfigurations)
            {
                if (method.id.Equals("MicrosoftAuthenticator"))
                {
                    if (!method.featureSettings.numberMatchingRequiredState.state.Equals("enabled"))
                    {
                        return CheckResult.Finding;
                    }
                    if (!method.featureSettings.displayLocationInformationRequiredState.Equals("enabled"))
                    {
                        return CheckResult.Finding;
                    }
                    if (!method.featureSettings.displayAppInformationRequiredState.Equals("enabled"))
                    {
                        return CheckResult.Finding;
                    }
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
