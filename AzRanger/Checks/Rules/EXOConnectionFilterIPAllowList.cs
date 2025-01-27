using AzRanger.Models;
using AzRanger.Models.ExchangeOnline;

namespace AzRanger.Checks.Rules
{
    class EXOConnectionFilterIPAllowList : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            bool finding = false;
            foreach(var item in tenant.ExchangeOnlineSettings.HostedConnectionFilterPolicy)
            {
                if(item.Identity.Equals("Default")) {
                    if (item.IPAllowList != null && item.IPAllowList.Length > 0)
                    {
                        finding = true;
                    }
                } 
            }

            if (finding)
            {
                return CheckResult.Finding;
            }
            return CheckResult.NoFinding;
        }
    }
}
