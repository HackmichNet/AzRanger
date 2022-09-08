using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AzSecurityDefaultsO365", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Properties")]
    [CISM365("1.1.11", "", Level.L1, "v1.4")]
    [RuleInfo("Security Defaults are enabled.", "If no other security features like Conditional Access is available, this can expose a massive risk to the tenant.", 0, null, "Security Defaults are only recommended in small enviroments. It is better to use Conditional Acces Policy.", "Enable Security Defaults if you have only a basic subscription.")]
    class AzSecurityDefaultsO365 : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SecurityDefaults.securityDefaultsEnabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
