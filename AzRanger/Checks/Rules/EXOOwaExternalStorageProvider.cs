using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleInfo("EXOOwaExternalStorageProvider", Scope.EXO, MaturityLevel.Mature)]
    [RuleScore("Users can use external storage for Outlook Web", "Allowing users to use external storage can lead to data lost", 1, "https://docs.microsoft.com/en-us/powershell/module/exchange/set-owamailboxpolicy?view=exchange-ps")]
    class EXOOwaExternalStorageProvider : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if (tenant.ExchangeOnlineSettings.OwaMailboxPolicy.AdditionalStorageProvidersAvailable == false)
            {
                return CheckResult.Passed;
            }
            return CheckResult.Failed;
        }
    }
}
