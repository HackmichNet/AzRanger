using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks
{
    [RuleMeta("UserPasswordBadPasswordList", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/AuthenticationMethodsMenuBlade/~/PasswordProtection")]
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
