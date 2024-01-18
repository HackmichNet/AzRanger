using AzRanger.Models;
using AzRanger.Models.MSGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
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
