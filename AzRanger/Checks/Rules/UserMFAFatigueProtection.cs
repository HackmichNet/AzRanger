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
    [RuleInfo("Users protection against MFA fatigue could be improved", "Users tend to approve MFA request, in case they receive to many or it is not obvious that the requests are fake.", 7, "https://techcommunity.microsoft.com/t5/microsoft-entra-azure-ad-blog/defend-your-users-from-mfa-fatigue-attacks/ba-p/2365677", null, @"Go to the reference link. Then click on ""Microsoft Authenticator"" an go to ""Configure"". Here ensure that ""Show application name in push and passwordless notifications"" and ""Show geographic location in push and passwordless notifications"" are enabled.")]
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
