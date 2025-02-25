using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPOVerificationCode : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.EmailAttestationEnabled && tenant.SharePointInformation.SharePointInternalInfos.EmailAttestationReAuthDays <= 15)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
