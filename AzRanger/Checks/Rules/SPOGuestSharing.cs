using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPOGuestSharing : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.PreventExternalUsersFromResharing == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
