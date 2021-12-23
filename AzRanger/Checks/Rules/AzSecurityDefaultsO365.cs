using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AzSecurityDefaultsO365", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Properties")]
    [RuleScore("Security Defaults are enabled.", "Security Defaults are only recommended in small enviroments. It is better to use Conditional Acces Policy", 0)]
    class AzSecurityDefaultsO365 : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SecurityDefaults.securityDefaultsEnabled == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
