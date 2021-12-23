using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("ADPasswordHashSyncEnabled", Scope.O365, MaturityLevel.Mature, "https://aad.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/AzureADConnect")]
    [RuleScore("Password hash sync is not enabled", "Password hash sync allows to detect leaked credentials", 3)]
    class ADPasswordHashSyncEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ADConnectStatus.passwordHashSyncEnabled == true)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
