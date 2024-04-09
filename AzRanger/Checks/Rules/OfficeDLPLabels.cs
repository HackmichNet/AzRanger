using AzRanger.Models;
using AzRanger.Models.ComplianceCenter;

namespace AzRanger.Checks.Rules
{
    class OfficeDLPLabels : BaseCheck
    {
        // TODO: Maybe we can check if they makes sense
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.TenantSettings.DlpLabels != null)
            {
                foreach (DlpLabel label in tenant.TenantSettings.DlpLabels)
                {
                    if (label.Disabled != false & label.Mode.Equals("Enforce"))
                    {
                        return CheckResult.NoFinding;
                    }
                }
            }
            return CheckResult.Finding;
        }
    }
}
