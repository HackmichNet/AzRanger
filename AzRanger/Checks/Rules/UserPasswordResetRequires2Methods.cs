using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserPasswordResetRequires2Methods", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/PasswordResetMenuBlade/~/AuthenticationMethods")]
    [CISAZ( "1.6", "", CISLevel.L1, "v2.0")]
    
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