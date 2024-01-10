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
    [RuleInfo("EXOOwaExternalStorageProvider")]
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
