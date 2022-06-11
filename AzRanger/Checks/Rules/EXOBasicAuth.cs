using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOBasicAuth", Scope.EXO, MaturityLevel.Mature, "https://admin.microsoft.com/#/Settings/Services/:/Settings/L1/ModernAuthentication")]
    [RuleScore("Either globaly or some users can use basic auth for ExchangeOnline", "Basic auth does not support secure authentication mechanism like MFA", 5, "https://docs.microsoft.com/en-us/exchange/clients-and-mobile-in-exchange-online/disable-basic-authentication-in-exchange-online")]
    class EXOBasicAuth : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.SecurityDefaults.securityDefaultsEnabled == true)
            {
                return CheckResult.NotApplicable;
            }
            // Case 1: If no policy exist, not good => Check if Conditional Access applies
            if(tenant.ExchangeOnlineSettings.AuthenticationPolicies == null)
            {
                return CheckResult.Failed;
            }

            bool defaultPolicyPassed = false;
            bool userWithNoPolicy = false;
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
                    }
                }
            }
            foreach(EXOUser user in tenant.ExchangeOnlineSettings.EXOUsers)
            {
                if(user.AuthenticationPolicy != null)
                {
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
                else
                {
                    userWithNoPolicy = true;
                }
            }
            
            // Default policy is good and we have no user policy
            if(defaultPolicyPassed && userWithNoPolicy)
            {
                return CheckResult.Passed;
            }
            // User assigned policies are secure and all users have a custom poliicy
            if(userPolicyPassed && !userWithNoPolicy)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
            
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
