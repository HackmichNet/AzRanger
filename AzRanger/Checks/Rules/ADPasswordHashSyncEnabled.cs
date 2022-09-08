using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("ADPasswordHashSyncEnabled", Scope.O365, MaturityLevel.Mature, "https://aad.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/AzureADConnect")]
    [CISM365("1.1.7", "", Level.L1, "v1.4")]
    [RuleInfo("Password hash sync is not enabled", "If password hash sync is not enabled, there is a risk that the leak of user credentials remains unnoticed.", 3, null, null, @"You have to enable Password hash sync on the server where you have installed you Azure AD Connect tool. During the setup under ""Optional Features"" ""Password hash synchronization can be enabled""")]
    class ADPasswordHashSyncEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ADConnectStatus.passwordHashSyncEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
