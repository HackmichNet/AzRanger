using AzRanger.Models;
using AzRanger.Models.MSGraph;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("AppsWithCreds", ScopeEnum.AAD, MaturityLevel.Mature, "https://entra.microsoft.com/#view/Microsoft_AAD_RegisteredApps/ApplicationsListBlade/quickStartType~/null/sourceType/Microsoft_AAD_IAM")]
    [RuleInfo("Apps with credentials", "If an app has higher privileges than the user created the credentials, then this can lead to privilege escalation. Additional this could be abused as backdoor.", 3, "https://posts.specterops.io/azure-privilege-escalation-via-service-principal-abuse-210ae2be2a5", "If an apps contain credentials, a user who knows this credenitals is able to authenticate as this app", "Check and remove the credentials from the app if they are not needed by any application")]
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
