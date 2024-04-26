using AzRanger.Models;

namespace AzRanger.Checks.Rules
{
    class EXOExternalSenderIdentified : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ExchangeOnlineSettings.ExternalInOutlooks != null)
            {
                if (tenant.ExchangeOnlineSettings.ExternalInOutlooks[0].Enabled)
                {
                    return CheckResult.NoFinding;
                }
                else
                {
                    return CheckResult.Finding;
                }
            }
            return CheckResult.NoFinding;
        }
    }
}
