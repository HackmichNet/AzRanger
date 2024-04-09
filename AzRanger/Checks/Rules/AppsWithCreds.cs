using AzRanger.Models;
using AzRanger.Models.MSGraph;

namespace AzRanger.Checks.Rules
{

    class AppsWithCreds : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool passed = true;

            foreach (Application app in tenant.Applications.Values)
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
