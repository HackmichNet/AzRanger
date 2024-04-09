using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPOModernAuthDisabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.OfficeClientADALDisabled == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
