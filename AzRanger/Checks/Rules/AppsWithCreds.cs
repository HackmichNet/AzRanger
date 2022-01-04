using AzRanger.Models;
using AzRanger.Models.MSGraph;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("AppsWithCreds", Scope.O365, MaturityLevel.Mature, "https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps")]
    [RuleScore("These apps contain credentials, thus a user might be able to authenticate as this app", "If an app has higher privileges than the user created the credentials, then this can lead to privilege escalation. Additional this could be abused as backdoor.", 9, "https://posts.specterops.io/azure-privilege-escalation-via-service-principal-abuse-210ae2be2a5")]
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
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
