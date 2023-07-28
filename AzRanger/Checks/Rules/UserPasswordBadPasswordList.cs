using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks
{
    [RuleMeta("UserPasswordBadPasswordList", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection")]
    [CISAZ("1.7", "", Level.L1, "v2.0")]
    [CISM365("1.1.10", "", Level.L1, "v2.0")]
    [RuleInfo("The Tenant is no configured for a bad password list", "This increases the risk, users uses easy guesable password.", 4, null, null, @"1. Go to Entra Admin Center </br> 2. Go to Protection </br> 3. Go to Authentication methods</br> 4. Go to Password protection </br> 5. 4. Ensure Enforce custom list is set to Yes.")]
    class UserPasswordBadPasswordList : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.enablementType == 0)
            {
                this.SetReason("Password Reset is not configured");
                return CheckResult.NotApplicable;
            }
            if (tenant.TenantSettings.PasswordPolicy.enforceCustomBannedPasswords == false )
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
