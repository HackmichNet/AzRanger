using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks
{
    [RuleMeta("UserPasswordBadPasswordList", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection")]
    [CISAZ("1.7", "", Level.L1, "v1.5")]
    [RuleInfo("The Tenant is no configured for a bad password list", "This increases the risk, users uses easy guesable password.", 4, null, null, @"1. Go to Azure Active Directory </br> 2. Go to Security </br> 3. Go to Authentication methods</br> 4. Go to Password protection </br> 5. 4. Ensure Enforce custom list is set to Yes.")]
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
