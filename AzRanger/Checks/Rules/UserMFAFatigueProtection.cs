using AzRanger.Models;
using AzRanger.Models.MSGraph;
using NLog;

namespace AzRanger.Checks.Rules
{
    class UserMFAFatigueProtection : BaseCheck
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AuthenticationMethodsPolicy == null)
            {
                logger.Error("Check.UserMFAFatigueProtection: No AuthenticationMethodsPolicy found. Possibly insufficient privileges");
                return CheckResult.Error;
            }

            foreach (Authenticationmethodconfiguration method in tenant.TenantSettings.AuthenticationMethodsPolicy.authenticationMethodConfigurations)
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
