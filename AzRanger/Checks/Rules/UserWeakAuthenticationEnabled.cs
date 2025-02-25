using AzRanger.Models;
using AzRanger.Models.MSGraph;
using NLog;

namespace AzRanger.Checks.Rules
{
    class UserWeakAuthenticationEnabled : BaseCheck
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.AuthenticationMethodsPolicy == null)
            {
                logger.Error("Check.UserWeakAuthenticationDisabled: No AuthenticationMethodsPolicy found. Possibly insufficient privileges");
                return CheckResult.Error;
            }

            foreach (Authenticationmethodconfiguration method in tenant.TenantSettings.AuthenticationMethodsPolicy.authenticationMethodConfigurations)
            {
                if (method.id.ToLower().Equals("sms"))
                {
                    if (method.state.ToLower().Equals("enabled"))
                    {
                        SetReason($"SMS authentication is enabled");
                        return CheckResult.Finding;
                    }
                }
                if(method.id.ToLower().Equals("voice"))
                {
                    if (method.state.ToLower().Equals("enabled"))
                    {
                        SetReason($"Phone call authentication is enabled");
                        return CheckResult.Finding;
                    }
                }
                if (method.id.ToLower().Equals("email"))
                {
                    if (method.state.ToLower().Equals("enabled"))
                    {
                        SetReason($"Email authentication is enabled");
                        return CheckResult.Finding;
                    }
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
