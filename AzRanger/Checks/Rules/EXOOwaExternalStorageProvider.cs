using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class EXOOwaExternalStorageProvider : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ExchangeOnlineSettings.OwaMailboxPolicy.AdditionalStorageProvidersAvailable == false)
            {
                return CheckResult.NoFinding;
            }
            return CheckResult.Finding;
        }
    }
}
