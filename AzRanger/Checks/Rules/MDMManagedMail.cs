using AzRanger.Models;
using AzRanger.Models.MSGraph.MDM;

namespace AzRanger.Checks.Rules
{
    // TODO
    class MDMManagedMail : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool iosPass = false;



            foreach (IosCompliancePolicy policy in tenant.MDMSettings.MobileDeviceCompliancePolicies.GetIosCompliancePolicies())
            {
                if (policy.managedEmailProfileRequired)
                {
                    iosPass = true;
                }
            }

            if (iosPass)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
