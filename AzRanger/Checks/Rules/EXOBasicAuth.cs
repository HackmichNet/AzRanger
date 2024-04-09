using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using AzRanger.Utilities;

namespace AzRanger.Checks.Rules
{
    class EXOBasicAuth : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.SecurityDefaults.securityDefaultsEnabled == true)
            {
                this.SetReason("Security Defaults are enables. All legacy authentication protocols should be blocked.");
                return CheckResult.NotApplicable;
            }
            // Case 1: If no policy exist, not good => Check if Conditional Access applies
            if (tenant.ExchangeOnlineSettings.AuthenticationPolicies == null)
            {
                return CheckResult.Finding;
            }

            bool defaultPolicyPassed = false;
            bool userWithNoPolicy = true;
            bool userPolicyPassed = true;

            // Case 2: DefaultAuthenticationPolicy is set, we have to check user too
            if (tenant.ExchangeOnlineSettings.OrganizationConfig.DefaultAuthenticationPolicy != null)
            {
                // If default Policy is set, at least one policy must exist
                foreach (AuthenticationPolicy policy in tenant.ExchangeOnlineSettings.AuthenticationPolicies)
                {
                    // Default policy
                    if (policy.Name == (string)tenant.ExchangeOnlineSettings.OrganizationConfig.DefaultAuthenticationPolicy.ToString())
                    {
                        if (IsPolicySafe(policy))
                        {
                            defaultPolicyPassed = true;
                        }
                        this.RawData = JSONDumper.GetSerialized(policy);
                    }
                }
            }
            foreach (EXOUser user in tenant.ExchangeOnlineSettings.EXOUsers)
            {
                if (user.AuthenticationPolicy != null)
                {
                    userWithNoPolicy = false;
                    foreach (AuthenticationPolicy policy in tenant.ExchangeOnlineSettings.AuthenticationPolicies)
                    {
                        // Default policy
                        if (policy.Name == (string)user.AuthenticationPolicy.ToString())
                        {
                            if (IsPolicySafe(policy))
                            {
                                continue;
                            }
                            else
                            {
                                userPolicyPassed = false;
                                this.AddAffectedEntity(user);
                            }
                        }
                    }
                }
            }

            // Default policy is good and we have no user policy
            if (defaultPolicyPassed && userWithNoPolicy)
            {
                return CheckResult.NoFinding;
            }
            // User assigned policies are secure and all users have a custom policy
            if (userPolicyPassed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;

        }

        private bool IsPolicySafe(AuthenticationPolicy policy)
        {
            if (policy.AllowBasicAuthActiveSync == false &&
                policy.AllowBasicAuthAutodiscover == false &&
                policy.AllowBasicAuthImap == false &&
                policy.AllowBasicAuthMapi == false &&
                policy.AllowBasicAuthOfflineAddressBook == false &&
                policy.AllowBasicAuthOutlookService == false &&
                policy.AllowBasicAuthPop == false &&
                policy.AllowBasicAuthReportingWebServices == false &&
                policy.AllowBasicAuthRest == false &&
                policy.AllowBasicAuthRpc == false &&
                policy.AllowBasicAuthSmtp == false &&
                policy.AllowBasicAuthWebServices == false &&
                policy.AllowBasicAuthPowershell == false)
            {
                return true;
            }
            return false;
        }
    }
}
