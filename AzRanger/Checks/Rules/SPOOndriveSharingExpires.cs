using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPOOndriveSharingExpires : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.ExternalUserExpirationRequired && tenant.SharePointInformation.SharePointInternalInfos.ExternalUserExpireInDays <= 30)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
