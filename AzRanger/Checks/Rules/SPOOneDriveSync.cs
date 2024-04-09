using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPOOneDriveSync : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.IsUnmanagedSyncClientForTenantRestricted == true &&
               tenant.SharePointInformation.SharePointInternalInfos.BlockMacSync == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
