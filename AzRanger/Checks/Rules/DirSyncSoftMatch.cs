using AzRanger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzRanger.Checks.Rules
{
    [RuleMeta("DirSyncSoftMatch", Scope.O365, MaturityLevel.Mature, "https://www.semperis.com/blog/smtp-matching-abuse-in-azure-ad/")]
    [RuleInfo("Your Tenant uses soft(SMTP)-match to synchronize on-premise user and cloud user.", "It is possible to use SMTP matching to synchronize on-prem users to Azure AD users that are eligible for administrative roles. Attackers who have gained on-prem access can use this approach to compromise Azure AD.", 5, "https://docs.microsoft.com/en-us/powershell/module/msonline/set-msoldirsyncfeature?view=azureadps-1.0#example-2-block-soft-matching-for-the-tenant", null, @"Disable SMTP matching with the PowerShell Commandlet written in the reference.")]
    class DirSyncSoftMatch : BaseCheck
    {
        public override CheckResult Audit(Tenant tenant)
        {
            if(tenant.ADConnectStatus.passwordHashSyncEnabled == false){
                this.SetReason("Password hashsync is not enabled.");
                return CheckResult.NotApplicable;
            }
            if(tenant.DirSyncFeatures.BlockSoftMatch == true)
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
