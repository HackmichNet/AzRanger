using AzRanger.Models;

namespace AzRanger.Checks.Rules
{

    [RuleMeta("ADPasswordHashSyncEnabled", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_IAM/DirectoriesADConnectBlade")]
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
