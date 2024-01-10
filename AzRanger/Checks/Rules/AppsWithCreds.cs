using AzRanger.Models;
using AzRanger.Models.MSGraph;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AppsWithCreds", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_RegisteredApps/ApplicationsListBlade/quickStartType~/null/sourceType/Microsoft_AAD_IAM")]
    [RuleInfo("AppsWithCreds")]
    class AppsWithCreds : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach(Application app in tenant.AllApplications.Values)
            {
                if (app.credentialsCreated)
                {
                    passed = false;
                    this.AddAffectedEntity(app);
                }
            }

            if (passed)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
