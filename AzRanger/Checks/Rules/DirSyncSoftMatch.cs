using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("DirSyncSoftMatch", ScopeEnum.AAD, MaturityLevel.Mature, "https://www.semperis.com/blog/smtp-matching-abuse-in-azure-ad/")]
    [RuleInfo("DirSyncSoftMatch")]
    class DirSyncSoftMatch : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.TenantSettings.ADConnectStatus.passwordHashSyncEnabled == false){
                this.SetReason("Password hashsync is not enabled.");
                return CheckResult.NotApplicable;
            }
            if(tenant.TenantSettings.DirSyncFeatures.BlockSoftMatch == true)
            {
                return CheckResult.NoFinding;
            }
            else
            {
                return CheckResult.Finding;
            }
            
        }
    }
}
