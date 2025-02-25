using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPODefaultLinkPermissions : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
           if (tenant.SharePointInformation.SharePointInternalInfos.DefaultLinkPermission == 1)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
