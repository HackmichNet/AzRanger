using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserPasswordResetRequires2Methods", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/AuthenticationMethods")]
    [CISAZ( "1.6", "", Level.L1, "v1.5")]
    [RuleInfo("Password Self-Service does not require two methods for reset", "This increases the risk, that a threat actor can perform a password reset for a user.", 1, null, null, @"1. Go to Azure Active Directory </br> 2. Go to Users </br> 3. Go to Password reset </br> 4. Go to Authentication methods </br> 5. Set the Number of methods required to reset to 2")]
    class UserPasswordResetRequires2Methods : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.PasswordResetPolicies.enablementType == 0)
            {
                this.SetReason("Password Reset is not configured");
                return CheckResult.NotApplicable;
            }
            if (tenant.TenantSettings.PasswordResetPolicies.numberOfAuthenticationMethodsRequired < 2)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}