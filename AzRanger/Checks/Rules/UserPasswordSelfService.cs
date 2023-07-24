using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("UserPasswordSelfService", ScopeEnum.AAD, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/PasswordResetMenuBlade/Properties")]
    [CISM365("1.1.8", "", Level.L1, "v2.0")]
    [RuleInfo("Password Self-Service is disabled", "No risk at all. But this can help to reduce calls to the Helpdesk.", 1, "https://docs.microsoft.com/en-us/azure/active-directory/authentication/tutorial-enable-sspr", "Allowing to use a secure password reset mechanism reduces the number of calls in your self-service.", "Use the link in the reference below.")]
    class UserPasswordSelfService : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            // 0 => Self service password reset enabled = None
            if (tenant.TenantSettings.PasswordResetPolicies.enablementType == 0)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
