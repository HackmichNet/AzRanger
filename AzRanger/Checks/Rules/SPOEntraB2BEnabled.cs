using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class SPOEntraB2BEnabled : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.SharePointInformation.SharePointInternalInfos.EnableAzureADB2BIntegration == true)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
