using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("ADPasswordHashSyncEnabled", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/DirectoriesADConnectBlade")]
    [CISM365("1.1.12", "", Level.L1, "v2.0")]
    [RuleInfo("Password hash sync is not enabled", "If password hash sync is not enabled, there is a risk that the leak of user credentials remains unnoticed.", 3, null, null, @"You have to enable Password hash sync on the server where you have installed you Azure AD Connect tool. During the setup under ""Optional Features"" ""Password hash synchronization can be enabled""")]
    class ADPasswordHashSyncEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.ADConnectStatus.passwordHashSyncEnabled == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
