using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("EXOOwaExternalStorageProvider", ScopeEnum.EXO, MaturityLevel.Mature)]
    [CISM365("6.5", "", Level.L2, "v2.0")]
    [RuleInfo("Users can use external storage for Outlook Web", "This increases the risk of unwanted data loss.", 1, "https://docs.microsoft.com/en-us/powershell/module/exchange/set-owamailboxpolicy?view=exchange-ps", null, @"Connect to Exchange Online using 'Connect-ExchangeOnline' CMDLET and run the following code: 'Set-OwaMailboxPolicy -Identity OwaMailboxPolicy-Default -AdditionalStorageProvidersAvailable $false'.")]
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
