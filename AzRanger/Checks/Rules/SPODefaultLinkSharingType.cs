using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPODefaultLinkSharingType : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.DefaultSharingLinkType == 1)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
